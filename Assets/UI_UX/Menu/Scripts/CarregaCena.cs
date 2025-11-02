using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregaCena : MonoBehaviour
{
    /*
     * Responsavel por carregar uma cena especifica
     *
     * @param int numeroCena
     * @return void
     */
    public static void CarregarCena(int numeroCena)
    {
        SceneManager.LoadScene(numeroCena);
    }
}
