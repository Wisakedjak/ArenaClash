using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    [RequireComponent(typeof(CharacterController))]
    public class ClickToMove : MonoBehaviour
    {
        [SerializeField] private InputAction mouseClickAction;

        private Camera _mainCamera;
        private Coroutine _moveCoroutine;
        private Vector3 _targetPosition;
        
        public float speed = 5f;
        
        private CharacterController _characterController;

        private int _groundLayer;
        
        public event Action<bool> OnMove;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _characterController = GetComponent<CharacterController>();
            _groundLayer = LayerMask.NameToLayer("Ground");
        }

        private void OnEnable()
        {
            mouseClickAction.Enable();
            mouseClickAction.performed += Move;
        }

        private void OnDisable()
        {
            mouseClickAction.performed += Move;
            mouseClickAction.Disable();
        }

        private void Move(InputAction.CallbackContext context)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider != null && hit.collider.gameObject.layer.CompareTo(_groundLayer)==0)
            {
                if (_moveCoroutine != null) StopMove();
                _moveCoroutine = StartCoroutine(PlayerMove(hit.point));
                _targetPosition = hit.point;
                OnMove?.Invoke(true);
            }
        }

        private void StopMove()
        {
            StopCoroutine(_moveCoroutine);
            OnMove?.Invoke(false);
        }

        private IEnumerator PlayerMove(Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 moveDirection = targetPosition - transform.position;
                Vector3 move = moveDirection.normalized * (Time.deltaTime * speed);
                _characterController.Move(move);
                transform.rotation = Quaternion.LookRotation(moveDirection.normalized);
                yield return null;
            }
            OnMove?.Invoke(false);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_targetPosition, 0.5f);
        }
    }
}