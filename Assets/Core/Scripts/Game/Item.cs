using UnityEngine;

/// <summary>
/// Anything you can store in your inventory
/// </summary>
public abstract class Item : MonoBehaviour
{
    Sprite _icon;

    string _name;
    string _description;

    public enum Rarity {
        Common,
        Uncommon,
        Rare,
        Legendary
    }
        
    [SerializeField] public string id;
    [SerializeField] public string displayName;
    [SerializeField] public string description;
    [SerializeField] public Rarity rarity;
    [SerializeField] public Sprite icon;
}
