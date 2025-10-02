using UnityEngine;

public class Schreecher : MonoBehaviour
{
    public float speed;
    public float bulletSpeed = 50f;
    public GameObject player;
    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        Vector3 direction = playerPosition - transform.position;
        transform.position += direction.normalized * Time.deltaTime * speed;
        













    }
        
}
