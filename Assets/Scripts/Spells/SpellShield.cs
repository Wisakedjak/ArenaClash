using Managers;
using UnityEngine;

namespace Spells
{
    public class SpellShield : SelfSpellBaseClass 
    {
        public override void CastSelfSpell(GameObject caster)
        {
            /*var shield = ObjectPoolManager.SpawnObject(spellPrefab, caster.transform);
            shield.transform.SetParent(caster.transform);*/
            
            Debug.Log("Spell Shield Casted");
        }

    }
}