using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

/// <summary>
/// Controla a troca de cenas e a suas transicoes
/// </summary>
public class IniciaAnimacaoTransicaoCena : MonoBehaviour
{
    public static IniciaAnimacaoTransicaoCena Instancia { get; private set; }
    [SerializeField] private Animator _animacaoTransicao;
    [SerializeField] private Animator _animacaoTransicaoCenaNoTempo;
    [SerializeField] private float _tempoTransicao;
    [SerializeField] private float _tempoTransicaoMundos;
    
    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return; 
        }
        Instancia = this;
    }
    
    /// <summary>
    /// Responsavel por iniciar uma animacao de transicao de cena
    /// </summary>
    /// <param name="nomeAnimacao"></param>
    /// <param name="numeroCena"></param>
    /// <return>void</return>
    /// <author>Wallisson de jesus</author>
    public void IniciarTransicao(string nomeAnimacao, int numeroCena,float tempoTransicaoCustom = 0)
    {
        Instancia.StartCoroutine(Instancia.Transition(nomeAnimacao, numeroCena,tempoTransicaoCustom));
    }


    /// <summary>
    /// Responsavel por iniciar uma animacao de transicao de mundos
    /// </summary>
    /// <param name="nomeAnimacao"></param>
    /// <param name="mundoId"></param>
    /// <return>void</return>
    /// <author>Wallisson de jesus</author>
    public void IniciarTransicaoEntreMundos(string nomeAnimacao,TipoMundo mundoId)
    {
        Instancia.StartCoroutine(Instancia.TransitionMundos(nomeAnimacao, mundoId));
    }
    
    /// <summary>
    /// Corrotina para executar a animacao e atrasar o carregamento da fase
    /// </summary>
    /// <param name="nomeAnimacao"></param>
    /// <param name="numeroCena"></param>
    /// <returns>IEnumerator</returns>
    /// <author>Wallisson de jesus</author>
    private IEnumerator Transition(string nomeAnimacao, int numeroCena,float tempoTransicaoCustom = 0)
    {
        if (_animacaoTransicao == null)
        {
            Debug.LogError("Animator de transição não atribuído!");
            yield break;
        }
        
        int triggerHash = Animator.StringToHash(nomeAnimacao);
        _animacaoTransicao.SetTrigger(triggerHash);    
        yield return new WaitForSeconds(tempoTransicaoCustom != 0 ? tempoTransicaoCustom : _tempoTransicao);
        CarregaCena.CarregarCena(numeroCena);
    }
    
    
    /// <summary>
    /// Corrotina para executar a animacao e atrasar o carregamento do mundo
    /// </summary>
    /// <param name="nomeAnimacao"></param>
    /// <param name="mundoId"></param>
    /// <returns>IEnumerator</returns>
    /// <author>Wallisson de jesus</author>
    private IEnumerator TransitionMundos(string nomeAnimacao, TipoMundo mundoId)
    {
        if (_animacaoTransicaoCenaNoTempo == null)
        {
            Debug.LogError("Animator de transição não atribuído!");
            yield break;
        }
        
        int triggerHash = Animator.StringToHash(nomeAnimacao);
        _animacaoTransicaoCenaNoTempo.SetTrigger(triggerHash);
        GameManager.instance.TrocarMundo(mundoId);
        yield return new WaitForSeconds(_tempoTransicaoMundos);
    }
}