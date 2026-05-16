using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float vitesse = 5f;
    public float vitesseRotation = 150f;
    public bool peutBouger = true;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!peutBouger) return;

        bool bouge = false;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * vitesse * Time.deltaTime);
            bouge = true;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * vitesse * Time.deltaTime);
            bouge = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up * -vitesseRotation * Time.deltaTime);
            bouge = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * vitesseRotation * Time.deltaTime);
            bouge = true;
        }

        if (animator != null)
            animator.SetBool("isWalking", bouge);
    }
}