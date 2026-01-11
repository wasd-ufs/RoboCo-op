using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool isHoldingSomething = false;
    private PlayerMovement playerMovement;
    
    [Header("Audio morte player")]
    [SerializeField] private AudioClip _dieClip;
    [SerializeField] private float _volume, _pitch;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// Gerencia as acoes que irao acontecer na morte do player 
    /// </summary>
    public void Die()
    {
        enabled = false;          // para o player
        playerMovement.StopMovement();
        
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        AudioManager.instance.PlayAudio(_dieClip,_volume,_pitch);
        IniciaAnimacaoTransicaoCena.Instancia.IniciarTransicao("Start",indexScene,1.5f);
    }
}
