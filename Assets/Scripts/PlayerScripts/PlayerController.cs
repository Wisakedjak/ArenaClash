using System;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        private ClickToMove _clickToMove;
        private PlayerAnimationController _playerAnimationController;
        private Animator _animator;

        private void Awake()
        {
            _clickToMove= GetComponent<ClickToMove>();
            _playerAnimationController = GetComponent<PlayerAnimationController>();
        }

        private void OnEnable()
        {
            _clickToMove.OnMove += _playerAnimationController.OnMove;
        }

       
    }
}