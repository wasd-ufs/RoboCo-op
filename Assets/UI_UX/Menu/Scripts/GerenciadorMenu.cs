using System.Collections;
using UnityEngine;

public class GerenciadorMenu : MonoBehaviour
{
    [SerializeField] private Animator _animatorTelaConfiguracoes;
    
    /*
     * Responsavel por iniciar o jogo
     */
    public void PlayGame(int numeroCena)
    {
        IniciaAnimacaoTransicaoCena.Instancia.IniciarTransicao("Start", numeroCena);
    }

    public void TelaConfiguracoes()
    {
        _animatorTelaConfiguracoes.Play("AbrirTelaConfiguracao",-1);
    }

    public void SairTelaConfiguracoes()
    {
        _animatorTelaConfiguracoes.Play("FecharTelaConfiguracao",-1);
    }
    
    /*
     * Responsavel por sair do jogo
     */
    public void QuitGame()
    {
        Debug.Log("Fechar o programa!");
		Application.Quit();   
        
    }
}
