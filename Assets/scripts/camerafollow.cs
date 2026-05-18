using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float hauteur = 4f;
    public float distance = 6f;
    public float vitesseSuivi = 10f;

    void LateUpdate()
    {
        if (player == null) return;

        // La caméra se place TOUJOURS derrière le joueur
        // selon la direction où il regarde
        Vector3 positionCible = player.position
            - player.forward * distance
            + Vector3.up * hauteur;

        // Suit le joueur
        transform.position = Vector3.Lerp(
            transform.position,
            positionCible,
            vitesseSuivi * Time.deltaTime
        );

        // Regarde toujours le joueur
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}