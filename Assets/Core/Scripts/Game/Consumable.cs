using UnityEngine;

public abstract class Consumable : Item
{
    public int _quantity;
    public abstract void Use();
    public abstract bool CanUse();
}
