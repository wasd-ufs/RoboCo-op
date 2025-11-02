using System;
using UnityEngine;

/**
 * Classe responsavel por exibir a imagem do tutorial
 */
public class ShowTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _showTutorial;
    
    /**
     * Responsavel por verificar se um dos player entrou no trigger
     *
     * @param Collider2D other
     * @return void
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
      VerificarTag(other.tag);
    }

    
    /**
     * Responsavel por verificar  se um dos player saiu do trigger
     *
     * @param Collider2D other
     * @return void
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        _showTutorial.SetActive(false);
    }

    /**
     * Responsavel por verificar qual player entrou para exibir o tutorial de cada um
     *
     * @param string tag
     * @return void
     */
    private void VerificarTag(string tag)
    {
        if (tag == "Droid")
        {
            _showTutorial.SetActive(true);
            return;
        }
        
        if (tag == "Human")
        {
            _showTutorial.SetActive(true);
        }
    }
}
