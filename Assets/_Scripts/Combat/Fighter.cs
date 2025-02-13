using System.Collections;
using System.Collections.Generic;
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

        private Mover _mover;
        private Transform _currentTarget;
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

            if (_currentTarget)
            {
                if (!IsInRange())
                {
                    _mover.MoveTo(_currentTarget.position);
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
            if(_timeSinceLastAttack >= _attackDelay)
            {
                _animator.SetTrigger("attack");
                _timeSinceLastAttack = 0;
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _currentTarget.position) <= _weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _currentTarget = combatTarget.transform;
        }

        public void CancelAction()
        {
            CancelAttack();
        }

        public void CancelAttack()
        {
            _currentTarget = null;
        }

        // Animation Event
        void Hit()
        {
            _timeSinceLastAttack = 0;
        }
    }
}
