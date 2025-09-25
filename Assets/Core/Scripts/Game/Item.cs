<<<<<<< Updated upstream
=======
﻿using System.Diagnostics.CodeAnalysis;
>>>>>>> Stashed changes
using UnityEngine;

/// <summary>
/// Anything you can store in your inventory
/// </summary>
public abstract class Item : MonoBehaviour
{
<<<<<<< Updated upstream
    Sprite _icon;

    string _name;
    string _description;

    enum _rarity {
        Common,
        Uncommon,
        Rare,
        Legendary
=======
    public class Item : MonoBehaviour
    {
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

        public Item(string displayName, string description, Rarity rarity, Sprite icon = null)
        {
            this.displayName = displayName;
            this.description = description;
            this.rarity = rarity;
            this.icon = icon;
        }

        public Item()
        {
            
        }
        
>>>>>>> Stashed changes
    }
}
