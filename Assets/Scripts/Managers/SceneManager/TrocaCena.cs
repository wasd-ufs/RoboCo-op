using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Desk : MonoBehaviour
{
    [Header("Proxima Cena")]
	[SerializeField] private int _nextScene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Human"))
        {
	        IniciaAnimacaoTransicaoCena.Instancia.IniciarTransicao("Start", _nextScene);
        }
    }
}