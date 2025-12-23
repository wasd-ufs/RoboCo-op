using System;
using UnityEngine;

public class GameManagerr : MonoBehaviour
{
    public static GameManagerr instance { private set; get;}

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; 
        }
        instance = this;
        DontDestroyOnLoad(instance);

        //Metodo para procurar e anexar as cameras e os player
        //Fazer uma especie de observer para quando o player fazer a acao de ir para o passado/futuro
        //Executar o metodo para fazer a animacao e trocar de cenario
    }

    void Update()
    {
        
    }
}
