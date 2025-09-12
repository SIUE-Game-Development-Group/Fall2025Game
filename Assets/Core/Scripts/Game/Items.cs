using UnityEngine;

public abstract class Items : MonoBehaviour
{
    int _quantity;
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
