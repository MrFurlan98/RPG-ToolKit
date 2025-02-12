using UnityEngine;
using RPGToolKit.Movement;
using RPGToolKit.Combat;

namespace RPGToolKit.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _playerMover;
        private Fighter _fighter;

        private void Start()
        {
            _playerMover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
        }

        void FixedUpdate()
        {
            if (UpdateCombat()) return;
            if (UpdateMovement()) return;
        }

        private bool UpdateCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!target) continue;

                if (Input.GetMouseButtonDown(0))
                    _fighter.Attack(target);

                return true;
            }

            return false;
        }

        private bool UpdateMovement()
        {
            RaycastHit hit;

            if (Physics.Raycast(GetMouseRay(), out hit) && _playerMover)
            {
                if (Input.GetMouseButton(0))
                    _playerMover.MoveTo(hit.point);
                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
