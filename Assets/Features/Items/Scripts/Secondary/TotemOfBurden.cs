using UnityEngine;
using System.Collections;
using Core.Scripts.Game;
using System.Collections.Generic;
public class TotemOfBurden : Accessory
{
    [SerializeField] private float linkDuration = 5f;
    // [SerializeField] private GameObject linkProjectilePrefab;

    private Enemy enemyA;
    private Enemy enemyB;
    private bool linkActive = false;
    private float remainingTime;

    public override void Equip() { }
    public override void Unequip() { EndLink(); }
   


    public void CreateLink(Enemy a, Enemy b)
    {
        if (linkActive) return;

        if (a == null || b == null) return;

        enemyA = a;
        enemyB = b;
        linkActive = true;
        remainingTime = linkDuration;

        
        enemyA.OnDeath += () => HandleDeath(enemyA, enemyB);
        enemyB.OnDeath += () => HandleDeath(enemyB, enemyA);

        Debug.Log($"Enemy Link created between {enemyA.name} and {enemyB.name}");
        StartCoroutine(LinkLifetime());
    }

    public void OnEnemyDamaged(Enemy enemy)
    {
        if (!linkActive) return;

        if (enemy == enemyA && enemyB != null)
            enemyB.TakeDamage(enemyA.LastDamageTaken);

        if (enemy == enemyB && enemyA != null)
            enemyA.TakeDamage(enemyB.LastDamageTaken);
    }

    private IEnumerator LinkLifetime()
    {
        while (remainingTime > 0f)
        {
            yield return null;
            remainingTime -= Time.deltaTime;
        }
        EndLink();
    }

    private void HandleDeath(Enemy dead, Enemy survivor)
    {
        if (!linkActive) return;

        Debug.Log($"{dead.name} died, spawning new link projectile from {survivor.name}");

        //TO DO: need to make this create a projectile when enemy dies to link to new enemy while the duration is still working

        EndLink();
    }



    private void EndLink()
    {
        if (!linkActive) return;



        Debug.Log("Enemy Link expired.");
        enemyA = null;
        enemyB = null;
        linkActive = false;
    }
}