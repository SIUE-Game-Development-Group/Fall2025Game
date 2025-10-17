using System.Collections.Generic;
using Core.Scripts.Game;
using UnityEngine;

public class PassiveDamageTracker : MonoBehaviour
{
    private DissociationShield shield; // Reference to the shield passive
    private List<Enemy> trackedEnemies = new();

    void Awake()
    {
        shield = GetComponent<DissociationShield>();
        if (shield == null)
            Debug.LogError("PassiveDamageTracker: DissociationShield component not found on the same GameObject.");
    }

    void Start()
    {
        Enemy[] allEnemies = FindObjectsOfType<Enemy>();

    foreach (var enemy in allEnemies)
    {
        trackedEnemies.Add(enemy);
        enemy.OnDamaged += () => shield.OnEnemyDamaged(enemy);
    }
    }

   
}