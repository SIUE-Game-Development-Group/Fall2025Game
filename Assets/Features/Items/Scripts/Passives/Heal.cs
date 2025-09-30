using Core.Scripts.Game;
using UnityEngine;

public class HealPassive : MonoBehaviour
{
    // How long before healing player
    public float HealTime;

    private float timer = 0f;
    public float HealAmount;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= HealTime)
        {
            timer = 0f;
            HealPlayer();
        }

    }

    void HealPlayer()
    {
        // Grab player info to get health
        GameObject player = GameObject.FindWithTag("Player");

        // Get Entity script from player to grab health from this "entity"
        Entity playerEntity = player.GetComponent<Entity>();

        // If health added greater than max health, just set health to max (don't give extra health)
        if ((playerEntity._health + HealAmount) > playerEntity._maxHealth)
        {
            // Debug.Log($"Healed {playerEntity._maxHealth - playerEntity._health}");
            playerEntity._health = playerEntity._maxHealth;
        }
        else
        {
            playerEntity._health += HealAmount;
            // Debug.Log($"Healed {HealAmount} hp!");
        }

    }

}
