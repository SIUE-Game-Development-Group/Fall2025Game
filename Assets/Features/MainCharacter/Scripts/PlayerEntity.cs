using System.Collections;
using UnityEngine;
using Core.Scripts.Game;
using Core.Scripts.Input;

public class PlayerEntity : Entity
{
    public void OnEnable()
    {
        OnDeath += OnPlayerDefeated;
    }
    
    public void OnDisable()
    {
        OnDeath -= OnPlayerDefeated;
    }

    public void OnPlayerDefeated()
    {
        Debug.Log("Player died :(");
        Destroy(gameObject);
    }
    
 
}
