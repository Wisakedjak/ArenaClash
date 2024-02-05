using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Managers;
using Spells;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace PlayerScripts
{
    public class PlayerMagicSystem : MonoBehaviour
    {
        [SerializeField] private Transform castPoint;

        [SerializeField] private InputAction spellCastQ;
        [SerializeField] private bool isCastingMagic;
        
        public List<SpellBaseClass> spells=new List<SpellBaseClass>();

        public event Action<bool> OnSpellCast;

        public bool IsCastingMagic
        {
            get => isCastingMagic;
            set => isCastingMagic = value;
        }
        
        public void SpellCastQ()
        {
            if (IsCastingMagic) return;
            IsCastingMagic = true;
            if (spells[0] is SelfSpellBaseClass selfSpellBaseClass)
            {
                selfSpellBaseClass.CastSelfSpell(gameObject);
                OnSpellCast?.Invoke(selfSpellBaseClass.isStopMoving);
                StartCoroutine(FinishCasting(selfSpellBaseClass.spellCastTime));
            }
            if (spells[0] is TargetSpellBaseClass targetSpell)
            {
                targetSpell.CastTargetSpell(gameObject, gameObject);
                OnSpellCast?.Invoke(targetSpell.isStopMoving);
                StartCoroutine(FinishCasting(targetSpell.spellCastTime));
            }
        }
        
        public void SpellCastW()
        {
            if (IsCastingMagic) return;
            IsCastingMagic = true;
            if (spells[1] is SelfSpellBaseClass selfSpellBaseClass)
            {
                selfSpellBaseClass.CastSelfSpell(gameObject);
                OnSpellCast?.Invoke(selfSpellBaseClass.isStopMoving);
                StartCoroutine(FinishCasting(selfSpellBaseClass.spellCastTime));
            }
            if (spells[1] is TargetSpellBaseClass targetSpell)
            {
                targetSpell.CastTargetSpell(gameObject, gameObject);
                OnSpellCast?.Invoke(targetSpell.isStopMoving);
                StartCoroutine(FinishCasting(targetSpell.spellCastTime));
            }
           
        }
        
        
       IEnumerator FinishCasting(float time)
       {
           yield return new WaitForSeconds(time);
           IsCastingMagic = false;
       }

        
    }
    
}