using System;
using System.Collections.Generic;
using Enums;
using Spells;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        private ClickToMove _clickToMove;
        private PlayerAnimationController _playerAnimationController;
        private Animator _animator;
        private PlayerMagicSystem _playerMagicSystem;
        private PlayerInputController _playerInputController;
        private Camera _mainCamera;
        
        private int _groundLayer;

        private void Awake()
        {
            _clickToMove= GetComponent<ClickToMove>();
            _playerAnimationController = GetComponent<PlayerAnimationController>();
            _playerMagicSystem = GetComponent<PlayerMagicSystem>();
            _playerInputController = GetComponent<PlayerInputController>();
            _mainCamera = Camera.main;
            _groundLayer = LayerMask.NameToLayer("Ground");
        }

        private void OnEnable()
        {
            _playerInputController.InputAction += (inputType) =>
            {
                switch (inputType)
                {
                    case InputTypeEnum.Move:
                        Move();
                        break;
                    case InputTypeEnum.SpellCastQ:
                        SpellCast(inputType);
                        break;
                    case InputTypeEnum.SpellCastW:
                        SpellCast(inputType);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
                }
            };
            _clickToMove.OnMove += OnMove;
            _playerMagicSystem.OnSpellCast += OnSpellCast;

        }

       private void OnDisable()
       {
           _clickToMove.OnMove -= OnMove;
           _playerMagicSystem.OnSpellCast -= OnSpellCast;
       }

       private void SpellCast(InputTypeEnum inputType)
       {
           switch (inputType)
           {
               case InputTypeEnum.SpellCastQ:
                   _playerMagicSystem.SpellCastQ();
                   break;
               case InputTypeEnum.SpellCastW:
                   _playerMagicSystem.SpellCastW();
                   break;
               default:
                   throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
           }
           
       }
       
       private void OnSpellCast(bool obj)
       {
           if (obj)
           {
               _clickToMove.StopMove();
               _playerAnimationController.OnBoltCast(true);
           }
           
       }

       private void OnMove(bool obj)
       {
           if (_playerMagicSystem.IsCastingMagic) return;
           _playerAnimationController.OnMove(obj);
       }

       private void Move()
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