using UnityEngine;

namespace Core.Scripts.Game
{
    public class Item : MonoBehaviour
    {
        enum Rarity {
            Common,
            Uncommon,
            Rare,
            Legendary
        }
        [SerializeField] private Rarity rarity;
        [SerializeField] private string displayName;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
    }
}