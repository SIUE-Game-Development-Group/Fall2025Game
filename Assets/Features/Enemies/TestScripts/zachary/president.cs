using UnityEngine;

public class president : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }


    void Attack()
    {

        GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);  

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;



    }


}
