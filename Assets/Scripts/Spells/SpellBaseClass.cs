using UnityEngine;

namespace Spells
{
    public abstract class SpellBaseClass : MonoBehaviour
    {
        public Sprite spellIcon;
        public string spellName;
        public string spellDescription;
        public float spellCooldown;
        public float spellCastTime;
        public bool isStopMoving;
    }
}