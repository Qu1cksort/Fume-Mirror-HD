using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;
    private bool isCaught = false;
    private bool hasTurnedToEnemy = false;
    private Vector3 enemyPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!isCaught)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -30f, 30f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            if (!hasTurnedToEnemy)
            {
                // Girar la cámara hacia el enemigo
                Vector3 directionToEnemy = (enemyPosition - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * mouseSensitivity);

                if (Quaternion.Angle(transform.rotation, lookRotation) < 10f)
                {
                    hasTurnedToEnemy = true;
                }
            }
        }
    }

    public void OnPlayerCaught(Transform enemyTransform)
    {
        // Si ya se ha capturado al jugador, no hagas nada
        if (isCaught)
        {
            return;
        }

        // Desactivar el control de la cámara
        this.isCaught = true;
        this.enemyPosition = enemyTransform.position + Vector3.up;
    }
}
