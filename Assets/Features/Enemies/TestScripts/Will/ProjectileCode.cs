using UnityEngine;

public class ProjectileCode : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private float setSpeed;
    float projectileDuration;
    public Vector3 projectileDirection;
    public GameObject enemy;
    private int id;
    // public EnemyAIWill enemyAIWill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileDirection = rb.linearVelocity.normalized;
        
        speed = 10f - (float)id;
        setSpeed = -10f + (float)id;
        

    }

    // Update is called once per frame
    void Update()
    {
        speed -= Time.deltaTime * 8f;
        projectileDuration += Time.deltaTime;
        
        if (speed < setSpeed)
        {
            Destroy(gameObject);
        }
        
        
        movement();

    }
    void movement()
    {
         Vector3 direction = projectileDirection;
         rb.linearVelocity = (direction * speed);
    }
    public void SetId(int id)
    {
               this.id = id;
    }
}