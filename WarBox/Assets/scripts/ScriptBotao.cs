using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptBotao : MonoBehaviour
{
    public void OnStartGameButtonClick()
    {
        // Trocar para a cena do jogo
        SceneManager.LoadScene("Jogo");
    }
}