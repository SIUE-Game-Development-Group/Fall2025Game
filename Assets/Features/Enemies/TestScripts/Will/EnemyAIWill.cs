using UnityEngine;
using Core.Scripts.Game;
using System.Collections.Generic;

public class EnemyAIWill : Enemy
{
    public GameObject player;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    
    public float speed;
    float tickRate;
    float projectileTickRate;
    private Rigidbody2D rb;
    public Vector3 projectileDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private List<Sprite> sprites;

    private SpriteRenderer spriteRenderer;

    public virtual void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        tickRate = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        base.Update();
        tickRate += Time.deltaTime;
        projectileTickRate += Time.deltaTime;

        RotateSprite();


        if (tickRate < 1.5f)
        {
            Movement();
        }
        else if (tickRate < 2f)
        {
            Move(Vector2.zero);
            
        }
        else if (tickRate < 4.5f)
        {
            if (projectileTickRate > 0.5f)
            {
                Attack();
                projectileTickRate = 0f;
            }
        }
        else
        {
            tickRate = 0f;
        }



    }
    void Movement()
    {
        Vector2 playerPosition = player.transform.position;  
        Vector2 direction = playerPosition - new Vector2(transform.position.x, transform.position.y); // transform.position
        
        // rb.linearVelocity = (direction * speed);
        Move(direction);
    }
    void Attack()
    {
        GameObject projectile;
        projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectileDirection = player.transform.position - transform.position;
        projectileDirection.Normalize();

        projectile.GetComponent<Rigidbody2D>().linearVelocity = projectileDirection * projectileSpeed;

    }
    Vector3 ProjectileDirection()
    {
        return projectileDirection;
    }

    void RotateSprite()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = playerPosition - transform.position;
        float playerAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        playerAngle = Mathf.Repeat(playerAngle, 360);
        int spriteDirection = 0;

        switch(playerAngle)
        {
            case <= 22.5f:
                spriteDirection = 0;
                break;
            case <= 67.5f:
                spriteDirection = 1;
                break;
            case <= 112.5f:
                spriteDirection = 2;
                break;
            case <= 157.5f:
                spriteDirection = 3;
                break;
            case <= 202.5f:
                spriteDirection = 4;
                break;
            case <= 247.5f:
                spriteDirection = 5;
                break;
            case <= 292.5f:
                spriteDirection = 6;
                break;
            case <= 337.5f:
                spriteDirection = 7;
                break;
            default:
                spriteDirection = 0;
                break;
        }
        spriteRenderer.sprite = sprites[spriteDirection];
    }

}
