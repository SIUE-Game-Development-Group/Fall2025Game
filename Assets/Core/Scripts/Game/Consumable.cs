using UnityEngine;

namespace Core.Scripts.Game
{
    public abstract class Consumable : Item
    {
        [SerializeField] private int _quantity;
        
        public abstract void Use();
        public abstract bool CanUse();
    }
}
