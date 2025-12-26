using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 
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
    public static void IniciarTransicao(string nomeAnimacao, int numeroCena)
    {
        Instancia.StartCoroutine(Instancia.Transition(nomeAnimacao, numeroCena));
    }


    /// <summary>
    /// Responsavel por iniciar uma animacao de transicao de mundos
    /// </summary>
    /// <param name="nomeAnimacao"></param>
    /// <param name="mundoId"></param>
    public static void IniciarTransicaoEntreMundos(string nomeAnimacao,TipoMundo mundoId)
    {
        Instancia.StartCoroutine(Instancia.TransitionMundos(nomeAnimacao, mundoId));
    }
    
    /// <summary>
    /// Corrotina para executar a animacao e atrasar o carregamento da fase
    /// </summary>
    /// <param name="nomeAnimacao"></param>
    /// <param name="numeroCena"></param>
    /// <returns></returns>
    private IEnumerator Transition(string nomeAnimacao, int numeroCena)
    {
        if (_animacaoTransicao == null && _animacaoTransicaoCenaNoTempo == null)
        {
            Debug.LogError("Animator de transição não atribuído!");
            yield break;
        }
        
        int triggerHash = Animator.StringToHash(nomeAnimacao);
        _animacaoTransicao.SetTrigger(triggerHash);    
        yield return new WaitForSeconds(_tempoTransicao);
        CarregaCena.CarregarCena(numeroCena);
    }
    
    /**
  * Corrotina para executar a animacao e atrasar o carregamento da fase
  */
    private IEnumerator TransitionMundos(string nomeAnimacao, TipoMundo mundoId)
    {
        if (_animacaoTransicaoCenaNoTempo == null)
        {
            Debug.LogError("Animator de transição não atribuído!");
            yield break;
        }
        
        int triggerHash = Animator.StringToHash(nomeAnimacao);
        _animacaoTransicaoCenaNoTempo.SetTrigger(triggerHash);
        GameManagerr.instance.TrocarMundo(mundoId);
        yield return new WaitForSeconds(_tempoTransicaoMundos);
    }
}