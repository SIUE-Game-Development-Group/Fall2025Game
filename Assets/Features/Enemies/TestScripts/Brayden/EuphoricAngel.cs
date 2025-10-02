using UnityEngine;

public class EuphoricAngel : MonoBehaviour
{
    [Tooltip("This value controls the movement speed of the angel")]
    public float speed;

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

    public TempPlayerScript tempPlayerScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBeingLookedAt(tempPlayerScript.Direction()))
        {
            Movement();
        }
        else if (i >= 1 / attackSpeed)
        {
            Attack();
            i = 0;
        }
        i += Time.deltaTime;
       
    }
    void Movement()
    {
        Vector3 playerPosition = player.transform.position;

        Vector3 directionToPlayer = playerPosition - transform.position;
        

        if (Mathf.Pow(directionToPlayer.x, 2) + Mathf.Pow(directionToPlayer.y, 2) >= 0.1) { // makes the angel move in a spiral when it is far enough away
            /*
            spiralWeight controls how wide the spiral is (e.g. a spiralWeight of 0 is a straight line and a spiralWeight of 100 is a circle)
            a value greater than 100 makes the angel spiral away, and a negative value technically makes it spiral in the other direction,
            however, it doesnt scale nearly as fast since it is meant to spiral clockwise. The intended value for spiralWeight is 80.
            */
            Vector3 spiral = new Vector3(((100 - spiralWeight) * directionToPlayer.x + spiralWeight * -directionToPlayer.y), ((100 - spiralWeight) * directionToPlayer.y + spiralWeight * directionToPlayer.x), 0);
            // set the direction

            spiral.Normalize();
            transform.position = transform.position + (spiral * speed * Time.deltaTime); // move the angel

        } else
        {
            directionToPlayer.Normalize();
            transform.position = transform.position + (directionToPlayer * speed * Time.deltaTime); // makes the angel just move straight to the player when they are close enough
        }

    }
    void Attack()
    {
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }
    bool IsBeingLookedAt(Vector3 facing)
    {
        facing.Normalize();

        Vector3 toAngel = transform.position - player.transform.position;
        toAngel.Normalize();

        // calculates the left vector of the FOV
        Vector3 leftFOV = new Vector3(((facing.x * Mathf.Cos((playerFOV / 2) * (Mathf.PI / 180))) - (facing.y * Mathf.Sin((playerFOV / 2) * (Mathf.PI / 180)))),
            ((facing.x * Mathf.Sin((playerFOV / 2) * (Mathf.PI / 180))) + (facing.y * Mathf.Cos((playerFOV / 2) * (Mathf.PI / 180)))),0);

        // calculates the right vector of the FOV
        Vector3 rightFOV = new Vector3(((facing.x * Mathf.Cos(-(playerFOV / 2) * (Mathf.PI / 180))) - (facing.y * Mathf.Sin(-(playerFOV / 2) * (Mathf.PI / 180)))),
            ((facing.x * Mathf.Sin(-(playerFOV / 2) * (Mathf.PI / 180))) + (facing.y * Mathf.Cos(-(playerFOV / 2) * (Mathf.PI / 180)))), 0); 
        leftFOV.Normalize();
        rightFOV.Normalize();
        

        // finds the angles between the FOV vectors and the direction to the angel
        float angleFOV = Vector3.Angle(leftFOV, rightFOV);
        float angleToLeftFOV = Vector3.Angle(leftFOV, toAngel);
        float angleToRightFOV = Vector3.Angle(rightFOV, toAngel);

        if ((angleToLeftFOV <= angleFOV) && (angleToRightFOV <= angleFOV)) // determines if the angel is inside the FOV
        {
            return true;
        } else
        {
            return false;
        }
        
    }
    
}
