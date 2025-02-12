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
        [SerializeField] private float _weaponRange;

        private Mover _mover;
        private Transform _currentTarget;
        private ActionScheduler _actionScheduler;

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (_currentTarget)
            {
                if (!IsInRange())
                {
                    _mover.MoveTo(_currentTarget.position);
                    return;
                }

                _mover.CancelAction();
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
    }
}
