using UnityEngine;
using UnityEngine.AI;
using RPGToolKit.Core;

namespace RPGToolKit.Movement
{
    [RequireComponent(typeof(ActionScheduler))]
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private Animator _animator;
        private ActionScheduler _actionScheduler;
        private NavMeshAgent _agent;

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            if (_agent)
            {
                Vector3 localVelocity = transform.InverseTransformDirection(_agent.velocity);

                _animator.SetFloat("forwardSpeed", localVelocity.z);
            }
        }

        public void StartMovementAction(Vector3 position)
        {
            _actionScheduler.StartAction(this);
            MoveTo(position);
        }

        public void MoveTo(Vector3 position)
        {
            if (_agent)
            {
                _agent.SetDestination(position);
                _agent.isStopped = false;
            }
        }
        public void CancelAction()
        {
            _agent.isStopped = true;
        }
    }
}
