using System.Collections.Generic;
using Core.Scripts.Game;
using UnityEngine;
//Summary: Use in junction with Shield passive to track and add to hit counter globally only when enemies are damage by weapon.
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
