using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public GameObject quitterButton;

    void Start()
    {
        string result = PlayerPrefs.GetString("result");

        if (result == "WIN")
        {
            resultText.text = "Bravo ! Vous avez gagné !";
        }
        else
        {
            resultText.text = "Dommage... Essayez encore !";
        }

        quitterButton.SetActive(true);
    }

    public void Rejouer()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene"); 
    }

    public void Quitter()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}