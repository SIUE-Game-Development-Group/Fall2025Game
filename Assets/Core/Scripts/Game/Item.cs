using UnityEngine;

/// <summary>
/// Anything you can store in your inventory
/// </summary>
public abstract class Item : MonoBehaviour
{
    Sprite _icon;

    string _name;
    string _description;

    enum _rarity {
        Common,
        Uncommon,
        Rare,
        Legendary
    }
}
