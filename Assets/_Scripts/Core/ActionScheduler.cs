using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGToolKit.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction _currentAction = null;

        public void StartAction(IAction action)
        {
            if (_currentAction == action) return;
            if(_currentAction != null)
            {
                _currentAction.CancelAction();
            }
            _currentAction = action;
        }
    }
}
