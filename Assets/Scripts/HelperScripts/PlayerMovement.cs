using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionReference m_mouseClickAction;
    [SerializeField] private InputActionReference mousePositionAction;
    [SerializeField] private InputActionReference m_attackAction; // Acció per atacar (clic dret, espai, etc.)

    [Header("Configuració")]
    [SerializeField] private LayerMask m_groundLayer; // Assegura't de posar-hi la capa "Ground"
    [SerializeField] private NavMeshAgent m_navMeshAgent;
    [SerializeField] private Animator m_animator;

    private void OnEnable()
    {
        // Escoltem quan fem clic per moure'ns
        m_mouseClickAction.action.performed += OnMouseClick;
        // Escoltem quan premem el botó d'atac
        m_attackAction.action.performed += OnAttack;
    }

    private void OnDisable()
    {
        // Deixem d'escoltar quan l'objecte es desactiva
        m_mouseClickAction.action.performed -= OnMouseClick;
        m_attackAction.action.performed -= OnAttack;
    }

    // --- LÒGICA DE MOVIMENT ---
    private void OnMouseClick(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Llancem el raig només contra la capa del terra (Ground)
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, m_groundLayer))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
            {
                m_navMeshAgent.isStopped = false;
                m_navMeshAgent.SetDestination(navHit.position);
            }
        }
    }

    // --- LÒGICA D'ATAC ---
    private void OnAttack(InputAction.CallbackContext context)
    {
        // 1. Aturem el personatge perquè no ataqui mentre rellisca pel terra
        StopMovement();

        // 2. Avisem l'Animator que comenci l'animació d'atac
        m_animator.SetTrigger("Attack");
    }

    private void Update()
    {
        // Dibuixem el raig per veure'l a l'editor (opcional)
        Vector2 mousePosition = mousePositionAction.action.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);

        // Actualitzem la velocitat a l'Animator perquè faci l'animació de caminar/córrer
        m_animator.SetFloat("SpeedMagnitude", m_navMeshAgent.velocity.magnitude);
    }

    // Funció per aturar el NavMeshAgent de cop
    public void StopMovement()
    {
        m_navMeshAgent.isStopped = true;
        m_navMeshAgent.ResetPath();
        m_navMeshAgent.velocity = Vector3.zero;
    }
}