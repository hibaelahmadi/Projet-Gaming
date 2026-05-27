using UnityEngine;

public class GameOverSound : MonoBehaviour
{
    public AudioClip sonVictoire;
    public AudioClip sonDefaite;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // ✅ Synchronisé avec EndManager qui utilise "result" / "WIN"
        string result = PlayerPrefs.GetString("result");

        if (result == "WIN")
        {
            audioSource.clip = sonVictoire;
        }
        else
        {
            audioSource.clip = sonDefaite;
        }

        audioSource.Play();
    }
}