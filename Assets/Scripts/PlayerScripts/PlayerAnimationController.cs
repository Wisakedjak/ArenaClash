using System;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnMove(bool obj)
        {
            _animator.SetBool("isIdle", !obj);
            _animator.SetBool("isWalking", obj);
        }
        
        public void OnBoltCast(bool obj)
        {
            _animator.SetBool("isIdle", !obj);
            _animator.SetBool("isBoltCast", obj);
            _animator.SetBool("isWalking", false);
        }
    }
}