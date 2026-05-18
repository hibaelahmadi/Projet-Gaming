using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    [Header("Type de question")]
    public bool estTexteLibre = false;

    [Header("QCM")]
    public string enonce;
    public string choix0;
    public string choix1;
    public string choix2;
    public string choix3;
    public int bonneReponseIndex;

    [Header("Texte Libre")]
    public string bonneReponseTexte;

    [Header("Mur a ouvrir")]
    public GameObject mur;
    
    public GameObject groupeBoxes;

    private bool dejaDeclanche = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dejaDeclanche)
        {
            dejaDeclanche = true;

            Question q = new Question();
            q.enonce = enonce;
            q.estTexteLibre = estTexteLibre;

            if (!estTexteLibre)
            {
                q.choix = new string[]
                    { choix0, choix1, choix2, choix3 };
                q.bonneReponseIndex = bonneReponseIndex;
            }
            else
            {
                q.bonneReponseTexte = bonneReponseTexte;
            }

            EnigmeManager.instance.mur = mur;
            EnigmeManager.instance.groupeBoxes = groupeBoxes;
            EnigmeManager.instance.AfficherQuestion(q);
        }
    }
}
