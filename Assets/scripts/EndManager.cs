using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;
    public GameObject quitterButton;

    public AudioClip sonVictoire;
    public AudioClip sonDefaite;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        string result = PlayerPrefs.GetString("result");

        if (result == "WIN")
        {
            resultText.text = "Bravo ! Vous avez gagné !";
            audioSource.PlayOneShot(sonVictoire);
        }
        else
        {
            resultText.text = "Dommage... Essayez encore !";
            audioSource.PlayOneShot(sonDefaite);
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