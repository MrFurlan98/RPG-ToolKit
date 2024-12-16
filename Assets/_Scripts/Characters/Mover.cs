using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private NavMeshAgent _agent;

    private Ray _lastRay;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
            MoveToCursor();

        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        _lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(_lastRay, out hit))
            MoveToPosition(hit.point);
    }

    private void MoveToPosition(Vector3 position)
    {
        if (_agent)
            _agent.SetDestination(position);
    }

    private void UpdateAnimator()
    {
        if (_agent)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(_agent.velocity);

            _animator.SetFloat("forwardSpeed", localVelocity.z);
        }
    }
}
