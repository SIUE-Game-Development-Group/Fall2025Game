using UnityEngine;
using Core.Scripts.Game;

public class Whatever : Enemy
{
     
    public float bulletspeed;
    private Vector2 direction;
    private GameObject player;
    public float attackRange;
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    private float totalTime = 0;
    public float nextFireTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        //Vector3 direction3 =player.transform.position - transform.position;
        //direction = new Vector2(direction3.x,direction3.y);
        direction = player.transform.position - transform.position;

        

        totalTime += Time.deltaTime;
        if (direction.magnitude < attackRange)
        {
            if(totalTime > fireRate){
                Attack();
                totalTime = 0;
            }
            Move(Vector2.zero);
            
        }else{
            Move(direction);
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

    
}
