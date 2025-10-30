using UnityEngine;

public class E : Enemy
{
     public float speed;
    public float bulletspeed;
    public GameObject player;
    public GameObject enemy;
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    public float totalTime;
    public float nextFireTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        base.Update();
        movement();

        totalTime += Time.deltaTime;
        if (totalTime > fireRate)
        {
            Attack();
            totalTime = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        GameObject bullet;
        bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletspeed;
               
    }

    void movement()
    {
        

        Vector3 playerPosition = player.transform.position;


        //playerPosition.x;

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        transform.position = transform.position + direction * Time.deltaTime * speed;
        
    }
}
