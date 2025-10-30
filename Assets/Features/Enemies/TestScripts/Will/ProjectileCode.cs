using UnityEngine;

public class ProjectileCode : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    float projectileDuration;
    public Vector3 projectileDirection;
    public GameObject enemy;
    // public EnemyAIWill enemyAIWill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileDirection = rb.linearVelocity.normalized;
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        speed -= Time.deltaTime * 10f;
        projectileDuration += Time.deltaTime;
        if (projectileDuration > 5.0f)
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
}