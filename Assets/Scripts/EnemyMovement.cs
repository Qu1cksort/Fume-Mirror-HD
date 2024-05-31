using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;

    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        if (move.magnitude >= 0.1f)
        {
            // Convertir dirección a ángulo y rotar el personaje
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

            // Mover el personaje
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

            // Activar la animación de caminar
            animator.SetBool("isWalking", true);
        }
        else
        {
            // Desactivar la animación de caminar
            animator.SetBool("isWalking", false);
        }
    }
}
