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
    
    private void Awake()
    {
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return; 
        }
        Instancia = this;
    }
    
    /**
     * Responsavel por iniciar uma animacao de transicao de cena
     */
    public static void IniciarTransicao(string nomeAnimacao, int numeroCena,bool transicaoCenaComun = true)
    {
        if (Instancia == null)
        {
            Debug.LogError("IniciaAnimacaoTransicaoCena não está na cena. Não foi possível iniciar a transição.");
            return;
        }
        
        Instancia.StartCoroutine(Instancia.Transition(nomeAnimacao, numeroCena,transicaoCenaComun));
    }
    
    /**
     * Corrotina para executar a animacao e atrasar o carregamento da fase
     */
    private IEnumerator Transition(string nomeAnimacao, int numeroCena,bool isTransicaoComun)
    {
        if (_animacaoTransicao == null && _animacaoTransicaoCenaNoTempo == null)
        {
            Debug.LogError("Animator de transição não atribuído!");
            yield break;
        }
        
        int triggerHash = Animator.StringToHash(nomeAnimacao);
        
        if (isTransicaoComun)
        {
            _animacaoTransicao.SetTrigger(triggerHash);    
        }
        _animacaoTransicaoCenaNoTempo.SetTrigger(nomeAnimacao);
        yield return new WaitForSeconds(_tempoTransicao);
        CarregaCena.CarregarCena(numeroCena);
    }
}