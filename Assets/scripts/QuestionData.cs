using UnityEngine;

[System.Serializable]
public class Question
{
    public string enonce;

    // Pour QCM
    public string[] choix;
    public int bonneReponseIndex;

    // Pour texte libre
    public bool estTexteLibre = false;
    public string bonneReponseTexte;
}
