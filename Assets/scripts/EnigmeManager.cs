using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnigmeManager : MonoBehaviour
{
    [Header("Panneau QCM")]
    public GameObject panneauQCM;
    public TextMeshProUGUI texteQuestionQCM;
    public Button[] boutonsChoix;
    public TextMeshProUGUI texteFeedbackQCM;

    [Header("Panneau Texte Libre")]
    public GameObject panneauTexte;
    public TextMeshProUGUI texteQuestionTexte;
    public TMP_InputField champReponse;
    public TextMeshProUGUI texteFeedbackTexte;

    [Header("Tentatives Globales")]
    public TextMeshProUGUI texteTentatives;
    public GameObject panneauGameOver;

    [Header("Audio")]
    public AudioClip sonBonneReponse;
    private AudioSource audioSource;


    [HideInInspector] public GameObject mur;

    [HideInInspector] public GameObject groupeBoxes;

    private Question questionActuelle;
    private int tentativesRestantes = 3;

    public static EnigmeManager instance;

    void Awake() { instance = this; }

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        tentativesRestantes = 3;
        panneauQCM.SetActive(false);
        panneauTexte.SetActive(false);
        if (panneauGameOver != null)
            panneauGameOver.SetActive(false);
        MettreAJourTentatives();
    }

    public void AfficherQuestion(Question q)
    {
        questionActuelle = q;

        if (q.estTexteLibre)
        {
            panneauQCM.SetActive(false);
            panneauTexte.SetActive(true);
            texteQuestionTexte.text = q.enonce;
            texteFeedbackTexte.text = "";
            champReponse.text = "";
        }
        else
        {
            panneauTexte.SetActive(false);
            panneauQCM.SetActive(true);
            texteQuestionQCM.text = q.enonce;
            texteFeedbackQCM.text = "";

            for (int i = 0; i < boutonsChoix.Length; i++)
            {
                int index = i;
                boutonsChoix[i].GetComponentInChildren<TextMeshProUGUI>().text = q.choix[i];
                boutonsChoix[i].onClick.RemoveAllListeners();
                boutonsChoix[i].onClick.AddListener(() => VerifierQCM(index));
                boutonsChoix[i].interactable = true;
            }
        }

        Time.timeScale = 0f;
    }

    void VerifierQCM(int indexChoisi)
    {
        if (indexChoisi == questionActuelle.bonneReponseIndex)
        {
            JouerSonBonneReponse();
            texteFeedbackQCM.text = "Bonne reponse ! Continue !";
            StartCoroutine(FermerPanneau(true));
        }
        else
        {
            tentativesRestantes--;
            MettreAJourTentatives();
            if (tentativesRestantes <= 0)
            {
                texteFeedbackQCM.text = "Game Over !";
                StartCoroutine(LancerGameOver());
            }
            else
            {
                texteFeedbackQCM.text = "Faux ! Il te reste " + tentativesRestantes + " tentative(s) !";
            }
        }
    }

    public void VerifierTexteLibre()
    {
        string reponseJoueur = champReponse.text.Trim().ToLower();
        string bonneReponse = questionActuelle.bonneReponseTexte.Trim().ToLower();

        if (reponseJoueur == bonneReponse)
        {
            JouerSonBonneReponse();
            texteFeedbackTexte.text = "Bonne reponse ! Continue !";
            StartCoroutine(FermerPanneau(true)); ;
        }
        else
        {
            tentativesRestantes--;
            MettreAJourTentatives();
            if (tentativesRestantes <= 0)
            {
                texteFeedbackTexte.text = "Game Over !";
                StartCoroutine(LancerGameOver());
            }
            else
            {
                texteFeedbackTexte.text = "Faux ! Il te reste " + tentativesRestantes + " tentative(s) !";
                champReponse.text = "";
            }
        }
    }

    void JouerSonBonneReponse()
    {
        if (audioSource != null && sonBonneReponse != null)
            audioSource.PlayOneShot(sonBonneReponse);
    }

    IEnumerator FermerPanneau(bool bonneReponse)
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1f;
        panneauQCM.SetActive(false);
        panneauTexte.SetActive(false);
        if (bonneReponse && mur != null)
        {
            mur.SetActive(false);

            if (groupeBoxes != null)
            {
                groupeBoxes.SetActive(false);
            }

            mur = null;
        }
    }

    IEnumerator LancerGameOver()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        panneauQCM.SetActive(false);
        panneauTexte.SetActive(false);
        if (panneauGameOver != null)
            PlayerPrefs.SetString("result", "LOSE");
            SceneManager.LoadScene("EndScene");
    }

    public void Recommencer()
    {
        tentativesRestantes = 3;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }




    public void AfficherVictoire()
    {
        panneauQCM.SetActive(false);
        panneauTexte.SetActive(false);
        Time.timeScale = 1f;
        PlayerPrefs.SetString("result", "WIN");
        SceneManager.LoadScene("EndScene");
    }

    public void Quitter()
    {
        Time.timeScale = 1f;
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void AfficherMessageBonus(string message, GameObject murAOuvrir)
    {
        panneauQCM.SetActive(true);
        panneauTexte.SetActive(false);

        texteQuestionQCM.text = message;
        texteFeedbackQCM.text = "";

        // cacher les boutons
        for (int i = 0; i < boutonsChoix.Length; i++)
        {
            boutonsChoix[i].gameObject.SetActive(false);
        }

        Time.timeScale = 0f;

        StartCoroutine(FermerMessageBonus(murAOuvrir));
    }

    IEnumerator FermerMessageBonus(GameObject murAOuvrir)
    {
        yield return new WaitForSecondsRealtime(2f);

        panneauQCM.SetActive(false);

        // remettre les boutons
        for (int i = 0; i < boutonsChoix.Length; i++)
        {
            boutonsChoix[i].gameObject.SetActive(true);
        }

        if (murAOuvrir != null)
        {
            murAOuvrir.SetActive(false);
        }
    
        Time.timeScale = 1f;
    }





    void MettreAJourTentatives()
    {
        if (texteTentatives != null)
            texteTentatives.text = "Vies : " + tentativesRestantes + "/3";
    }
}
