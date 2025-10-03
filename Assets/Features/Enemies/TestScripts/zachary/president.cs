using UnityEngine;

public class president : MonoBehaviour
{
    
    private GameObject player;
    private Vector3 direction;

    private Rigidbody2D rb;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed, enemySpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) return;

        direction = player.transform.position - transform.position;
        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        Movement();
    }

    void Movement()
    {
        if (direction.magnitude < .05f)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        
        rb.linearVelocity = direction.normalized * enemySpeed;
    }

    void Attack()
    {

        GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);  

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * bulletSpeed;

    }


}
