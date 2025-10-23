using System.Collections;
using Core.Scripts.Game;
using UnityEngine;

public class CrystalLeech : Item
{
    GameObject player;

    public float DrainHPTo = 1f;
    private float invincibilityTimer = 10f;
    private Hurtbox hurtbox;
    
    // For test purpose activation
    private float timer = 0;
    private bool abilityUsed = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        hurtbox = player.GetComponent<Hurtbox>();
    }

    // For testing purposes use ability automatically after 5 seconds
    void Update()
    {
        // Wait 5 seconds to use ability once
        if (abilityUsed == false)
        {
            timer += Time.deltaTime;

            if (timer >= 5)
            {
                UseAbility();
                abilityUsed = true;
            }
        }
    }

    // Drains life to minimum and calls ability function to activate power
    public void UseAbility()
    {
        // ~ ~ ~ ~ Drain life to minimum amount ~ ~ ~ ~ 
        Entity playerEntity = player.GetComponent<Entity>();

        // Warn in debug logs that the hp to "drain" to was greater than player's health (a.k.a didn't lose health)
        if (DrainHPTo >= playerEntity._health) Debug.LogWarning($"Crystal Leech ability didn't drain hp but instead gave {DrainHPTo - playerEntity._health} hp!");
        
        playerEntity._health = DrainHPTo;

        Debug.Log($"Using Crystal Leech Totem! Drained player health to {playerEntity._health}");

        // ~ ~ ~ ~ Call upon ability for player ~ ~ ~ ~ 
        StartCoroutine(Ability());
    }
    // Invincibility for "invinciblityTimer" duration
    private IEnumerator Ability()
    {
        Debug.Log("Invincibility Activated!");
        
        hurtbox.StartInvincibility();

        yield return new WaitForSeconds(invincibilityTimer);

        hurtbox.EndInvincibility();
        Debug.Log("Invincibility Deactivated!");

        Destroy(this.gameObject);
    }


}
