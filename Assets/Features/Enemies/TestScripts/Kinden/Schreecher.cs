using UnityEngine;
using Core.Scripts.Game;

public class Schreecher : Enemy
{
    public float speed;
    public float bulletSpeed = 50f;
    public GameObject player;
    public GameObject bulletPrefab;
    private Rigidbody2D rb;
    public float stopDistance;
    public GameObject Explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        movement();
    }

    void Attack()
    {
        GameObject bullet;
        bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
     
        Vector3 direction = player.transform.position - transform.position ;
        direction.Normalize();

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    } 

    void movement()
    { 
        Vector3 playerPosition = player.transform.position;
        Vector2 direction = playerPosition - transform.position;
        float distance = direction.magnitude;
        if (distance <= stopDistance)
        {
            Move(Vector2.zero);
            GameObject explosion;
            explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Move(direction);
        }

           // rb.linearVelocity = direction.normalized * speed;












    }
        
}
