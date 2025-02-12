using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGToolKit.Movement;

namespace RPGToolKit.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private float _weaponRange;

        private Mover _mover;
        private Transform _currentTarget;

        private void Start()
        {
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (_currentTarget)
            {
                if(Vector3.Distance(transform.position,_currentTarget.position) > _weaponRange)
                {
                    _mover.MoveTo(_currentTarget.position); 
                    return;
                }

                _mover.Stop();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            _currentTarget = combatTarget.transform;
        }
    }
}
