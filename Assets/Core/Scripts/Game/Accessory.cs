using UnityEngine;

namespace Core.Scripts.Game
{
    public abstract class Accessory : Item
    {
        public abstract void Equip();
        public abstract void Unequip();
    }
}
