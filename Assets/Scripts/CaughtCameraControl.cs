using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtCameraControl : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private Transform enemyTransform;

    void Update()
    {
        if (enemyTransform != null)
        {
            Vector3 directionToEnemy = (enemyTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void OnPlayerCaught(Transform enemyTransform)
    {
        // Desactivar el control de la cámara del jugador
        GetComponent<FirstPersonCamera>().enabled = false;

        // Activar este script y guardar la referencia al enemigo
        this.enabled = true;
        this.enemyTransform = enemyTransform;
        Debug.Log("Player caught by enemy!");
    }
}
