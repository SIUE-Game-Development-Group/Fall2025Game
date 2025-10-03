using UnityEngine;

public class enemyPresident : Core.Scripts.Game.Enemy
{

    private GameObject player;
    private Vector3 direction3;
    private Vector2 direction;


    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    player = GameObject.FindWithTag("Player");
    //    Movement();

    //}

    private void Awake()
    {
        Move(Vector2.up);
    }

    // Update is called once per frame
    //void Update()
    //{

    //    if (player == null) return;

    //    direction3 = player.transform.position - transform.position;
    //    direction = new Vector2(direction.x, direction.y);


    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Attack();
    //    }

    //    Movement();



    //}

    void Movement()
    {
        if (direction.magnitude < .05f)
        {
            Move(Vector2.zero);
            return;
        }

        Move(direction);
    }

    void Attack()
    {

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction3.normalized * bulletSpeed;

    }
}
