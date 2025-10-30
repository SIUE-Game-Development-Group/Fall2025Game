//using System.Numerics;
using Core.Scripts.Game;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EuphoricAngel : Enemy
{
    //[Tooltip("This value controls the movement speed of the angel")]
    //public float speed;

    [Tooltip("This value controls how wide the spiral is (e.g. a weight of 0 is a straight line and a weight of 100 is a perfect circle)")]
    public float spiralWeight;

    [Tooltip("This value determines the angle (in degrees) of the FOV the angel needs to be inside to move")]
    public float playerFOV;

    [Tooltip("This value determines how fast the projectiles move")]
    public float bulletSpeed;

    [Tooltip("This value determines how many attacks happen per second")]
    public float attackSpeed;

    float i = 0;

    public GameObject player;
    public GameObject bulletPrefab;

    private TempPlayerScript playerAnimation;

    private Rigidbody2D rb;

    [SerializeField] private List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        base.Update();
        if (IsBeingLookedAt(playerAnimation.Direction()))
        {
            Movement();
        }
        else if (i >= 1 / attackSpeed)
        {
            Attack();
            i = 0;
            Move(Vector2.zero);
        }
        else
        {
            Move(Vector2.zero);
        }
        i += Time.deltaTime;
        RotateSprite();
       
    }
    void Movement()
    {
        Vector2 playerPosition = player.transform.position;

        Vector2 directionToPlayer = playerPosition - new Vector2(transform.position.x, transform.position.y);

        directionToPlayer.Normalize();

        if (Mathf.Pow(directionToPlayer.x, 2) + Mathf.Pow(directionToPlayer.y, 2) >= 0.1) { // makes the angel move in a spiral when it is far enough away
            /*
            spiralWeight controls how wide the spiral is (e.g. a spiralWeight of 0 is a straight line and a spiralWeight of 100 is a circle)
            a value greater than 100 makes the angel spiral away, and a negative value technically makes it spiral in the other direction,
            however, it doesnt scale nearly as fast since it is meant to spiral clockwise. The intended value for spiralWeight is 80.
            */
            Vector3 spiral = new Vector2(((100 - spiralWeight) * directionToPlayer.x + spiralWeight * -directionToPlayer.y), ((100 - spiralWeight) * directionToPlayer.y + spiralWeight * directionToPlayer.x));
            // set the direction

            spiral.Normalize();
            Move(spiral);
            // move the angel

        } else
        {
            Move(directionToPlayer);
            // makes the angel just move straight to the player when they are close enough
        }

    }
    void RotateSprite()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = playerPosition - transform.position;
        float playerAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
        playerAngle = Mathf.Repeat(playerAngle, 360);
        int spriteDirection = 0;
        switch (playerAngle)
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
    void Attack()
    {
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Vector3 bulletDirection = player.transform.position - transform.position;
        bulletDirection.Normalize();

        bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletDirection * bulletSpeed;
    }
    bool IsBeingLookedAt(Vector2 facing)
    {
        facing.Normalize();

        Vector2 toAngel = transform.position - player.transform.position;
        toAngel.Normalize();

        // calculates the left vector of the FOV
        Vector2 leftFOV = new Vector2(((facing.x * Mathf.Cos((playerFOV / 2) * (Mathf.PI / 180))) - (facing.y * Mathf.Sin((playerFOV / 2) * (Mathf.PI / 180)))),
            ((facing.x * Mathf.Sin((playerFOV / 2) * (Mathf.PI / 180))) + (facing.y * Mathf.Cos((playerFOV / 2) * (Mathf.PI / 180)))));

        // calculates the right vector of the FOV
        Vector2 rightFOV = new Vector2(((facing.x * Mathf.Cos(-(playerFOV / 2) * (Mathf.PI / 180))) - (facing.y * Mathf.Sin(-(playerFOV / 2) * (Mathf.PI / 180)))),
            ((facing.x * Mathf.Sin(-(playerFOV / 2) * (Mathf.PI / 180))) + (facing.y * Mathf.Cos(-(playerFOV / 2) * (Mathf.PI / 180))))); 
        leftFOV.Normalize();
        rightFOV.Normalize();
        

        // finds the angles between the FOV vectors and the direction to the angel
        float angleFOV = Vector2.Angle(leftFOV, rightFOV);
        float angleToLeftFOV = Vector2.Angle(leftFOV, toAngel);
        float angleToRightFOV = Vector2.Angle(rightFOV, toAngel);

        if ((angleToLeftFOV <= angleFOV) && (angleToRightFOV <= angleFOV)) // determines if the angel is inside the FOV
        {
            return true;
        } else
        {
            return false;
        }
        
    }
    
}
