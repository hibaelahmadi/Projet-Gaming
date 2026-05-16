using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void JouerJeu()
    {
        // Charge le Level_01
        SceneManager.LoadScene("MainScene");
    }

    public void QuitterJeu()
    {
     #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
     #else
        Application.Quit();
     #endif
    }
}
