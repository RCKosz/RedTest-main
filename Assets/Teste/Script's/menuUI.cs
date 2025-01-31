using UnityEngine;
using UnityEngine.SceneManagement;


public class menuUI : MonoBehaviour
{
    public void Resetar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //quando ativado, reinicia a cena
    }

    public void Sair()
    {
        Application.Quit(); //desliga o jogo
    }
}
