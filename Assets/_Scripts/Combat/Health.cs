using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGToolKit.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float _maxHealthPoints = 100f;

        Animator _animator;
        bool _isDead = false;
        float _currentHealthPoints;

        public bool IsDead => _isDead;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _currentHealthPoints = _maxHealthPoints;
        }

        public void TakeDamage(float damage)
        {
            _currentHealthPoints = Mathf.Max(_currentHealthPoints - damage, 0);

            if (_currentHealthPoints == 0)
                Die();
        }

        private void Die()
        {
            if (_isDead) return;

            _isDead = true;
            _animator.SetTrigger("die");
        }
    }
}

