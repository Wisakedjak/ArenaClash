using System;
using Enums;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private InputAction mouseClickAction;
        [SerializeField] private InputAction spellCastQ;
        [SerializeField] private InputAction spellCastW;


        public Action<InputTypeEnum> InputAction;

        private void OnEnable()
        {
            mouseClickAction.Enable();
            mouseClickAction.performed += Move;
            spellCastQ.Enable();
            spellCastQ.performed += SpellCastQ;
            spellCastW.Enable();
            spellCastW.performed += SpellCastW;
        }
        
        private void OnDisable()
        {
            mouseClickAction.performed -= Move;
            mouseClickAction.Disable();
            spellCastQ.performed -= SpellCastQ;
            spellCastQ.Disable();
            spellCastW.performed -= SpellCastW;
            spellCastW.Disable();
        }

        private void SpellCastW(InputAction.CallbackContext obj)
        {
            InputAction?.Invoke(InputTypeEnum.SpellCastW);
        }

        private void SpellCastQ(InputAction.CallbackContext obj)
        {
            InputAction?.Invoke(InputTypeEnum.SpellCastQ);
        }

        private void Move(InputAction.CallbackContext context)
        {
            InputAction?.Invoke(InputTypeEnum.Move);
        }
    }
}