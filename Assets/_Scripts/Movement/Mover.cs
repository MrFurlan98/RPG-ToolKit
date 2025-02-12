using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPGToolKit.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private NavMeshAgent _agent;

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        void FixedUpdate()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 position)
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
}
