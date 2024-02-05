using UnityEngine;

namespace Spells
{
    public class Bolt : TargetSpellBaseClass
    {
        public override void CastTargetSpell(GameObject caster, GameObject target)
        {
            Debug.Log("Bolt Casted!");
        }
    }
}