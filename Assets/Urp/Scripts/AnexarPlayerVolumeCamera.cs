using UnityEngine;
using UnityEngine.Rendering.Universal;

/**
 * Classe responsavel por ativar o post processing local quando a camera passar pelo trigger   
 */
public class AnexarPlayerVolumeCamera : MonoBehaviour
{
    [SerializeField] private Transform _player1;
    private string _tagPlayer1 = "Human";
    [SerializeField] private Transform _player2;
    [SerializeField] private UniversalAdditionalCameraData _camera;
    void Start()
    {
        AnexarObjetoPlayer();
        _camera = Camera.main.GetUniversalAdditionalCameraData();
        //AnexarPlayerToVolumeTrigger();
    }

    /**
     * Responsavel por adicionar o player como objeto para ser detectado no volume
     *
     * @return void
     */
    /*
    private void AnexarPlayerToVolumeTrigger()
    {
        _camera.volumeTrigger = _player;
    }
    */

    //TODO: Conversar com a equipe para ver com vai ser a mecanica, se cada um vai ter sseu mundo separado
    private void AnexarObjetoPlayer()
    {
        PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();

        foreach (PlayerController player in allPlayers)
        {
            if (player.CompareTag(_tagPlayer1))
            {
               _player1 = player.transform; 
            }
            else
            { 
                _player2 = player.transform; 
            }            
        }
    }
  
}
