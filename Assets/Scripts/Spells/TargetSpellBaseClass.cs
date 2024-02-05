using UnityEngine;

namespace Spells
{
    public abstract class TargetSpellBaseClass : SpellBaseClass
    {
        public GameObject spellPrefab;
        public float spellDuration;
        public float amount;
        
        public abstract void CastTargetSpell(GameObject caster, GameObject target);
    }
}