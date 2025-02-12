using UnityEngine;
using RPGToolKit.Movement;

namespace RPGToolKit.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Mover _playerMover;

        private Ray _lastRay;

        private void Start()
        {
            if (!_playerMover)
                _playerMover.GetComponent<Mover>();
        }

        void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
                MoveToCursor();
        }

        private void MoveToCursor()
        {
            _lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_lastRay, out hit) && _playerMover)
                _playerMover.MoveTo(hit.point);
        }
    }
}
