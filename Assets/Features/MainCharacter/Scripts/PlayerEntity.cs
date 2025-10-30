using System.Collections;
using UnityEngine;
using Core.Scripts.Game;

public class PlayerEntity : Entity
{
    [Tooltip("These hitboxes may be temporarily disabled when taking damage (temporary invincibility)")]
    [SerializeField] private Hurtbox[] _hurtboxes;

    [Tooltip("Invincibility time when taking damage")] 
    [SerializeField] private float hitInvincibilityTime = 1.5f;
    
    /// <summary>
    /// This value is true while the player is invincible due to temporary hit invincibility
    /// </summary>
    /// <returns></returns>
    private bool _hitInvincible;
    
    public void OnEnable()
    {
        OnDamaged += OnPlayerDamaged;
        OnDeath += OnPlayerDefeated;
    }
    
    public void OnDisable()
    {
        OnDamaged -= OnPlayerDamaged;
        OnDeath -= OnPlayerDefeated;
    }

    public void OnPlayerDamaged()
    {
        if (!_hitInvincible && hitInvincibilityTime > 0f)
        {
            StartCoroutine(TemporaryInvincibility());
        }
    }

    public void OnPlayerDefeated()
    {
        Debug.Log("Player died :(");
        Destroy(gameObject);
    }
    
    public IEnumerator TemporaryInvincibility()
    {
        // Make all hurtboxes invincible
        _hitInvincible = true;
        foreach (var hurtbox in _hurtboxes)
        {
            hurtbox.StartInvincibility();
        }

        // Wait until not invincible anymore
        yield return new WaitForSeconds(hitInvincibilityTime);
            
        // Make all hurtboxes not invincible aymore.
        _hitInvincible = false;
        foreach (var hurtbox in _hurtboxes)
        {
            hurtbox.EndInvincibility();
            // If invincibility down causes taking damage which causes invincibility up, stop the loop
            if (_hitInvincible) break;
        }
    }
}
