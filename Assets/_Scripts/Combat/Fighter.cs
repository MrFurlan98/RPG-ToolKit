using UnityEngine;
using RPGToolKit.Movement;
using RPGToolKit.Core;

namespace RPGToolKit.Combat
{
    [RequireComponent(typeof(ActionScheduler))]
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _weaponRange = 3f;
        [SerializeField] private float _attackDelay = 1f;
        [SerializeField] private float _weaponDamage = 5f;

        private Mover _mover;
        private Health _currentTargetHealth;
        private ActionScheduler _actionScheduler;
        float _timeSinceLastAttack = 0;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;

            if (_currentTargetHealth && !_currentTargetHealth.IsDead)
            {
                if (!IsInRange())
                {
                    _mover.MoveTo(_currentTargetHealth.transform.position);
                    return;
                }
                else
                {
                    _mover.CancelAction();
                    AttackBehavior();
                }
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(_currentTargetHealth.transform);

            if(_timeSinceLastAttack >= _attackDelay)
            {
                //This is will trigger the Hit() event.
                TriggerAttack();
                _timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger("cancelAttack");
            _animator.SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            _timeSinceLastAttack = 0;

            if (_currentTargetHealth)
            {
                _currentTargetHealth.TakeDamage(_weaponDamage);
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _currentTargetHealth.transform.position) <= _weaponRange;
        }

        public bool CanAttack(CombatTarget target)
        {
            return target != null && !target.GetComponent<Health>().IsDead;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _currentTargetHealth = combatTarget.GetComponent<Health>();
        }

        public void CancelAction()
        {
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("cancelAttack");
            _currentTargetHealth = null;
        }
    }
}
