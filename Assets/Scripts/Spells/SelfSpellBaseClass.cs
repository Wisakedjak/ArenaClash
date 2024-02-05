using UnityEngine;

namespace Spells
{
    public abstract class SelfSpellBaseClass : SpellBaseClass
    {
        public GameObject spellPrefab;
        public float spellDuration;
        public float amount;
        
        public abstract void CastSelfSpell(GameObject caster);
        

    }
}