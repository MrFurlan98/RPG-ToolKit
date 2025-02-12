using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGToolKit.Combat
{
    public class Fighter : MonoBehaviour
    {
        public void Attack(CombatTarget target)
        {
            Debug.Log($"I am Attacking {target.name}");
        }
    }
}
