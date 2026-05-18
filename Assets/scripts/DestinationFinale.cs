using UnityEngine;

public class DestinationFinale : MonoBehaviour
{
    public GameObject panneauVictoire;
    private bool victoire = false;

    void Update()
    {
        if (victoire) return;
        GameObject joueur = GameObject.FindWithTag("Player");
        if (joueur == null) return;
        float dist = Vector3.Distance(transform.position, joueur.transform.position);
        if (dist < 8f)
        {
            victoire = true;
            EnigmeManager.instance.AfficherVictoire();
        }
    }
}
