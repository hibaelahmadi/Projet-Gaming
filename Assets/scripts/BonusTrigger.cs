using UnityEngine;

public class BonusTrigger : MonoBehaviour
{
    public string messageBonus;
    public GameObject murAOuvrir;
    public GameObject groupeBoxes;

    private bool dejaActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dejaActive)
        {
            dejaActive = true;

            EnigmeManager.instance.AfficherMessageBonus(messageBonus, murAOuvrir);

            if (groupeBoxes != null)
            {
                groupeBoxes.SetActive(false);
            }
        }
    }
}