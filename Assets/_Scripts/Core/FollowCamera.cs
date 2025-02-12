using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGToolKit.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        void LateUpdate()
        {
            if (_target)
                transform.position = _target.position;
        }
    }
}
