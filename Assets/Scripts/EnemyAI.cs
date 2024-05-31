using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float hearingThreshold = 50f;
    public Transform player;
    public Transform[] patrolPoints; // Puntos de patrullaje
    //public float waitTime = 3f; // Tiempo de espera en cada punto de patrullaje
    public LayerMask soundLayer; // Capa de los sonidos

    private NavMeshAgent agent;
    private bool isScared = false;
    private int currentPatrolIndex;
    //private bool waiting;
    //private float waitTimer;
    private Animator animator; // Animator del enemigo
    private Transform scaredDestination; // Punto de patrulla al que huir
    private Transform lastPatrolPointPassed; // Último punto de patrulla que logró pasar

    public float scaredPauseTime = 3f; // Tiempo que el enemigo se detiene cuando se asusta
    private bool isPaused = false; // Si el enemigo está pausado
    private float pauseTimer = 0f; // Temporizador para la pausa

    public WhistleController whistleController; // Referencia al WhistleController

    private float cooldownTimer = 0f; // Temporizador para el enfriamiento
    private float cooldownDuration = 15f; // Duración del enfriamiento en segundos

    public GameObject objectToDisable1;
    public GameObject objectToDisable2;

    public DeathMenu deathMenu;

    private int startIndex;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentPatrolIndex = 0;
        MoveToNextPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(new Vector3(player.position.x, 0, player.position.z), new Vector3(transform.position.x, 0, transform.position.z));

        if (isScared)
        {
            // Huir del jugador
            if (scaredDestination == null)
            {
                startIndex = System.Array.IndexOf(patrolPoints, lastPatrolPointPassed);
                int scaredIndex = (startIndex - 3 + patrolPoints.Length) % patrolPoints.Length;
                scaredDestination = patrolPoints[scaredIndex];
            }

            if (isPaused)
            {
                pauseTimer += Time.deltaTime;
                if (pauseTimer >= scaredPauseTime)
                {
                    isPaused = false;
                    agent.isStopped = false;
                    animator.SetFloat("Roar", 0f);
                }

            }
            else if (new Vector3(agent.destination.x, 0, agent.destination.z) != new Vector3(scaredDestination.position.x, 0, scaredDestination.position.z))
            {
                currentPatrolIndex = (startIndex - 1 + patrolPoints.Length) % patrolPoints.Length;
                agent.destination = patrolPoints[currentPatrolIndex].position;
            }
            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                if (new Vector3(agent.destination.x, 0, agent.destination.z) == new Vector3(scaredDestination.position.x, 0, scaredDestination.position.z))
                {
                    isScared = false;
                    scaredDestination = null;
                    animator.SetFloat("Flee", 5f);
                }
                lastPatrolPointPassed = patrolPoints[currentPatrolIndex];
                startIndex = System.Array.IndexOf(patrolPoints, lastPatrolPointPassed);
            }
        }
        else if (distanceToPlayer <= hearingThreshold)
        {
            // Perseguir al jugador
            agent.SetDestination(player.position);
            animator.SetFloat("Found", 5f); // Activar animaciones de persecución
        }
        else
        {
            // Pasear por la ciudad
            animator.SetFloat("Found", 0f); // Desactivar animaciones de persecución
            animator.SetFloat("Flee", 0f);

            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            {
                MoveToNextPatrolPoint();
            }

            if (!isScared)
            {
                DetectSounds();
            }
        }
        // Actualizar el temporizador de enfriamiento
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Comprobar si el enemigo ha alcanzado al jugador
        if (!isScared && distanceToPlayer <= 4f)
        {
            // Notificar al jugador que ha sido alcanzado
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.OnCaughtByEnemy(transform);
            }

            // Activar la animación de ataque
            animator.SetTrigger("Attack");

            // Deshabilitar los dos GameObjects
            if (objectToDisable1 != null)
            {
                objectToDisable1.SetActive(false);
            }
            if (objectToDisable2 != null)
            {
                objectToDisable2.SetActive(false);
            }
            // Iniciar la Coroutine para mostrar el menú de muerte después de 3 segundos
            StartCoroutine(ShowDeathMenuAfterDelay(3f));
        }
    }

    void OnGUI()
    {
        float distanceToPlayer = Vector3.Distance(new Vector3(player.position.x, 0, player.position.z), new Vector3(transform.position.x, 0, transform.position.z));
        if (Input.GetMouseButtonDown(1) && cooldownTimer <= 0)
        {
            whistleController.animator.SetTrigger("Adelante");
            // Iniciar el temporizador de enfriamiento
            cooldownTimer = cooldownDuration;

            // Solo asustar al enemigo si está dentro del rango
            if (distanceToPlayer >= 5f && distanceToPlayer <= 10f)
            {
                isScared = true;
                isPaused = true;
                pauseTimer = 0f;
                agent.isStopped = true;
                animator.SetFloat("Roar", 5f);
                // Aumentar la velocidad del enemigo
                agent.speed += 1;

            }
        }

    }

    void OnAnimationSpeedDown()
    {
        agent.isStopped = true;
    }

    void OnAnimationSpeedUp()
    {
        agent.isStopped = false;
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        lastPatrolPointPassed = patrolPoints[currentPatrolIndex];
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    void DetectSounds()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, hearingThreshold, soundLayer);

        foreach (Collider hit in hits)
        {
            AudioSource audioSource = hit.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.isPlaying)
            {
                agent.destination = hit.transform.position;
                break;
            }
        }
    }

    Transform GetFurthestPatrolPointBehind()
    {
        Transform furthestPoint = null;
        float maxDistance = float.MinValue;

        foreach (Transform point in patrolPoints)
        {
            Vector3 toPoint = point.position - transform.position;
            if (Vector3.Dot(toPoint, transform.forward) < 0) // El punto está detrás
            {
                float distance = toPoint.sqrMagnitude;
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    furthestPoint = point;
                }
            }
        }

        return furthestPoint;
    }

    void MoveToPreviousPatrolPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        currentPatrolIndex--;
        if (currentPatrolIndex < 0)
        {
            currentPatrolIndex = patrolPoints.Length - 1;
        }

        agent.destination = patrolPoints[currentPatrolIndex].position;
    }

    Transform GetFurthestPatrolPoint()
    {
        Transform furthestPoint = null;
        float maxDistance = float.MinValue;

        foreach (Transform point in patrolPoints)
        {
            float distance = Vector3.Distance(point.position, transform.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                furthestPoint = point;
            }
        }

        return furthestPoint;
    }

    private IEnumerator ShowDeathMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        deathMenu.ShowDeathMenu();
    }

}
