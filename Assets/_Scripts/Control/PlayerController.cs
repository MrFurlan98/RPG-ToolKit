using UnityEngine;
using RPGToolKit.Movement;
using RPGToolKit.Combat;

namespace RPGToolKit.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Mover _playerMover;
        [SerializeField] private Fighter _fighter;

        private void Start()
        {
            if (!_playerMover)
                _playerMover.GetComponent<Mover>();
            if (!_fighter)
                _fighter.GetComponent<Fighter>();
        }

        void FixedUpdate()
        {
            UpdateCombat();
            UpdateMovement();
        }

        private void UpdateCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!target) continue;

                if (Input.GetMouseButtonDown(0))
                    _fighter.Attack(target);
            }
        }

        private void UpdateMovement()
        {
            if (Input.GetMouseButton(0))
                MoveToCursor();
        }

        private void MoveToCursor()
        {
            RaycastHit hit;

            if (Physics.Raycast(GetMouseRay(), out hit) && _playerMover)
                _playerMover.MoveTo(hit.point);
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
