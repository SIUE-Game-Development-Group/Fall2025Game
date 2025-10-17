using UnityEngine;
using System.Collections;
using Core.Scripts.Game;
public class DissociationShield : Accessory
{

    [SerializeField] private int hitsRequired = 15;
    [SerializeField] private float shieldDuration = 3f;
    [SerializeField] private float cooldownAfterShield = 6f;

    private Hurtbox hurtbox;
    private PlayerEntity player;
    private int currentHitCount = 0;
    private bool shieldActive = false;
    private bool canBuildHits = true;


    void Start()
    {
        // Find the Player GameObject and get components
        GameObject playerGO = GameObject.Find("Player");  // or FindWithTag("Player")

        if (playerGO == null)
        {
            Debug.LogError("Player GameObject not found!");
            return;
        }

        player = playerGO.GetComponent<PlayerEntity>();
        hurtbox = playerGO.GetComponent<Hurtbox>();

        if (player == null)
            Debug.LogError("PlayerEntity component not found!");

        if (hurtbox == null)
            Debug.LogError("Hurtbox component not found!");
    }

    public void OnEnemyDamaged(Entity enemy)
    {
        if (!canBuildHits || shieldActive) return;

        currentHitCount++;

        if (currentHitCount >= hitsRequired)
        {
            StartCoroutine(ActivateShield());
        }
    }
    public override void Equip()
    {
      
    }
    public override void Unequip()
    {
        
    }

    private IEnumerator ActivateShield()
    {
        shieldActive = true;
        canBuildHits = false;
        currentHitCount = 0;

        Debug.Log("Shield Activated!");
        
        hurtbox.StartInvincibility();
        

        yield return new WaitForSeconds(shieldDuration);

        hurtbox.EndInvincibility();
        shieldActive = false;
            Debug.Log("Shield Deactivated!");



        yield return new WaitForSeconds(cooldownAfterShield);
        canBuildHits = true;
    }
}