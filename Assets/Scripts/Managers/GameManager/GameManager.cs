using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsavel por controlar eventos do jogo
/// </summary>
public class GameManagerr : MonoBehaviour
{
    public static GameManagerr instance { private set; get;}
    [SerializeField] private GameObject _cameraMundoHumano;
    [SerializeField] private GameObject _cameraMundoRobo;
    [SerializeField] private GameObject _playerHumano;
    [SerializeField] private GameObject _playerRobo;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; 
        }
        instance = this;
        DontDestroyOnLoad(instance);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Callback para quando uma nova cena eh carregada
    /// </summary>
    /// <param name="scene">Cena atual</param>
    /// <param name="mode">Modo da cena atual</param>
    /// <return>void</return>
    /// <author>Wallisson de jesus</author>
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
		if(scene.buildIndex >= 2){
			AnexarDependenciasTrocaDeMundo();
		}
    }

    private void Update()
    {
        /*
         * Todo: Vai ser removido, e adicionado em um local apropriado, lembrar de verificar dependendo do player,
         * vai ser chamado uma funcao diferente
         */
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            IniciaAnimacaoTransicaoCena.Instancia.IniciarTransicaoEntreMundos("Start",TipoMundo.MundoHumano);
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IniciaAnimacaoTransicaoCena.Instancia.IniciarTransicaoEntreMundos("Start",TipoMundo.MundoRobo);
        }
    }
    
    /// <summary>
    /// Responsavel por anexar os objetos necessarios para o funcionamento da troca entre mundos
    /// </summary>
    /// <returns>void</returns>
    /// <author>Wallisson de jesus</author>
    private void AnexarDependenciasTrocaDeMundo()
    {
        _cameraMundoHumano = GameObject.FindGameObjectWithTag("CameraMundoHumano");
        _cameraMundoRobo = GameObject.FindGameObjectWithTag("CameraMundoDroid");
        _playerHumano = GameObject.FindGameObjectWithTag("Human");
        _playerRobo = GameObject.FindGameObjectWithTag("Droid");
    }

    /// <summary>
    /// Responsavel por realizar a troca para a mundo especifico
    /// </summary>
    /// <param name="mundoId"></param>
    /// <returns>void</returns>
    /// <author>Wallisson de jesus</author>
    public void TrocarMundo(TipoMundo mundoId)
    {
        if (mundoId == TipoMundo.MundoHumano)
        {
            _playerHumano.SetActive(true);
            _cameraMundoHumano.SetActive(true);
            _playerRobo.SetActive(false);
            _cameraMundoRobo.SetActive(false);
            return;
        }
        
        _playerRobo.SetActive(true);
        _cameraMundoRobo.SetActive(true);
        _playerHumano.SetActive(false);
        _cameraMundoHumano.SetActive(false);
    }
}
