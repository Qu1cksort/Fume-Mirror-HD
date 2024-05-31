using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public float rotationSpeed = 5f;

    void Update()
    {
        // Obtener la posici�n del mouse en la pantalla y convertirla a coordenadas de mundo
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Define el plano cercano para el punto Z
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calcular la direcci�n desde el prefab hacia la posici�n del mouse
        Vector3 direction = worldMousePosition - transform.position;

        // Calcular la rotaci�n necesaria para mirar hacia la posici�n del mouse
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Aplicar la rotaci�n suavemente
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
