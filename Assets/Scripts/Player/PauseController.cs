using System;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class PauseController : MonoBehaviour
{   
    [Header("References")]
    [SerializeField] private MonoBehaviour inputSource;
    private IPlayerInput input;
    
    [SerializeField] private Animator _animatorTelaConfiguracoes;
    private bool isPaused = false;
    public GameObject pauseMenu;

    private void Awake()
    {
        input = inputSource as IPlayerInput;
        if (input == null)
        {
            Debug.LogError("PlayerInteractor: inputSource não implementa IPlayerInput");
            enabled = false;
        }
    }

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (input.Pause.JustPressed)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else PauseGame();
        }
    }
    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        IniciaAnimacaoTransicaoCena.IniciarTransicao("Start", 0);
    }
    public void Resume()
    {
        ResumeGame();
    }
}

