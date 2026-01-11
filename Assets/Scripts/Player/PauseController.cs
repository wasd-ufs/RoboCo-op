using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class PauseController : MonoBehaviour
{
    [SerializeField] private Animator _animatorTelaConfiguracoes;
    private bool isPaused = false;
    public GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        IniciaAnimacaoTransicaoCena.Instancia.IniciarTransicao("Start", 0);
    }
    public void Resume()
    {
        ResumeGame();
    }
}

