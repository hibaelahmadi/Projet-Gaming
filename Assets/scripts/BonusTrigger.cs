using UnityEngine;

public class BonusTrigger : MonoBehaviour
{
    public string messageBonus;
    public GameObject murAOuvrir;
    public GameObject groupeBoxes;

    public AudioClip sonVictoire;

    [Range(0f, 1f)]
    public float volume = 1f;

    private bool dejaActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dejaActive)
        {
            dejaActive = true;

            if (sonVictoire != null)
            {
                GameObject sonObj = new GameObject("SonBonus");
                AudioSource src = sonObj.AddComponent<AudioSource>();
                src.clip = sonVictoire;
                src.spatialBlend = 0f;
                src.volume = volume;
                src.Play();
                Destroy(sonObj, sonVictoire.length);
            }

            EnigmeManager.instance.AfficherMessageBonus(messageBonus, murAOuvrir);

            if (groupeBoxes != null)
            {
                groupeBoxes.SetActive(false);
            }
        }
    }
}