using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionReference m_mouseClickAction, mousePositionAction;

    [SerializeField]
    private LayerMask m_groundLayer;

    [SerializeField]
    private NavMeshAgent m_navMeshAgent;
    [SerializeField]
    private Animator m_animator;
    private void OnEnable()
    {
        m_mouseClickAction.action.performed += OnMouseClick;
    }

    private void OnDisable()
    {
        m_mouseClickAction.action.performed -= OnMouseClick;
    }

 
    private void OnMouseClick(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Ignorem la capa "DetectionLayer" (on està el Player)
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, ~LayerMask.GetMask("DetectionLayer")))
        {
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 1.0f, NavMesh.AllAreas))
            {
                m_navMeshAgent.isStopped = false;
                m_navMeshAgent.SetDestination(navHit.position);
            }
        }
    }




    private void Update()
    {
        Vector2 mousePosition = mousePositionAction.action.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        m_animator.SetFloat("SpeedMagnitude", m_navMeshAgent.velocity.magnitude);
    }

    public void StopMovement()
    {
        m_navMeshAgent.isStopped = true;
        m_navMeshAgent.ResetPath();
        m_navMeshAgent.velocity = Vector3.zero;
    }
}
