using Core.Scripts.Game;
using Features.MainCharacter.Scripts;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class Celeste : Enemy

    
{
    public float bulletspeed;
    public float attackDistance;
    public float fireRate;
    private GameObject player;
    public GameObject bulletPreFab;
    private Vector3 direction;

    private float counter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        counter = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public virtual void Update() {
        base.Update();

       direction = player.transform.position - transform.position;

        

        if (direction.magnitude < attackDistance)
        {
            if(counter > fireRate)
            {
                Attack();
                Move(Vector2.zero);
                counter = 0;
            }
            
        }
        else {
            Movement();
        }

        counter += Time.deltaTime;

       }

    void Attack() {

        GameObject bullet = Instantiate(bulletPreFab, transform.position, Quaternion.identity);
    
       
        

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * bulletspeed;

    }
    void Movement() {
        
        Move(direction);

    }
}

