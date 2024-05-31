using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static Unity.Burst.Intrinsics.X86;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 0f;
    public AudioClip[] footstepSounds;
    public Animator animator;

    private CharacterController controller;
    private AudioSource audioSource;
    private int i;
    public FirstPersonCamera firstPersonCamera; // Referencia a la cámara del jugador

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float movementVertical = Input.GetAxis("Vertical");
        float movementHorizontal = Input.GetAxis("Horizontal");
        int previousIndex = i;

        if (movementVertical >= 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 6f;
                i = 1;
            }
            else
            {
                speed = 2f;
                i = 0;
            }
        }
        else if (movementVertical <= -0.1f || Mathf.Abs(movementHorizontal) >= 0.1f)
        {
            speed = 2f;
            i = 0;
        }
        else
        {
            speed = 0f;
            audioSource.Stop();
        }

        Vector3 moveDirection = transform.TransformDirection(new Vector3(movementHorizontal, 0, movementVertical).normalized);
        float move = speed * Time.deltaTime;
        controller.Move(moveDirection * move);

        animator.SetFloat("Speed", speed);

        if (move != 0 && (!audioSource.isPlaying || previousIndex != i))
        {
            PlayFootstepSound(i);
        }
    }

    void PlayFootstepSound(int index)
    {
        audioSource.clip = footstepSounds[index];
        audioSource.Play();
    }

    public void OnCaughtByEnemy(Transform enemyTransform)
    {
        // Desactivar el control del jugador
        this.enabled = false;

        // Desactivar el control de la cámara y girar hacia el enemigo
        if (firstPersonCamera != null)
        {
            firstPersonCamera.OnPlayerCaught(enemyTransform);
        }
    }
}
