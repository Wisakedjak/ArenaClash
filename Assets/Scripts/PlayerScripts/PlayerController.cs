using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputAction mouseClickAction;
        private ClickToMove _clickToMove;
        private PlayerAnimationController _playerAnimationController;
        private Animator _animator;
        private PlayerMagicSystem _playerMagicSystem;
        private Camera _mainCamera;
        
        private int _groundLayer;

        private void Awake()
        {
            _clickToMove= GetComponent<ClickToMove>();
            _playerAnimationController = GetComponent<PlayerAnimationController>();
            _playerMagicSystem = GetComponent<PlayerMagicSystem>();
            _mainCamera = Camera.main;
            _groundLayer = LayerMask.NameToLayer("Ground");
        }

        private void OnEnable()
        {
            mouseClickAction.Enable();
            mouseClickAction.performed += Move;
            _clickToMove.OnMove += OnMove;
            _playerMagicSystem.OnBoltCast += SpellCast;

        }

       private void OnDisable()
       {
           mouseClickAction.performed += Move;
           mouseClickAction.Disable();
           _clickToMove.OnMove -= _playerAnimationController.OnMove;
       }

       private void SpellCast(bool obj)
       {
           _clickToMove.StopMove();
           _playerAnimationController.OnBoltCast(obj);
           
       }

       private void OnMove(bool obj)
       {
           if (_playerMagicSystem.IsCastingMagic) return;
           _playerAnimationController.OnMove(obj);
       }

       private void Move(InputAction.CallbackContext context)
       {
           if (_playerMagicSystem.IsCastingMagic) return;
           Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray: ray, hitInfo: out RaycastHit hit) && hit.collider != null && hit.collider.gameObject.layer.CompareTo(_groundLayer)==0)
            {
               _clickToMove.Move(hit);
            }
       }
    }
}