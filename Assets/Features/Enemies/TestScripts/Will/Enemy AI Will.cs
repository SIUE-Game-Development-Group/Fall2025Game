using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    
    public float speed;
    float projectileTickRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        movement();

        projectileTickRate += Time.deltaTime;

        if (projectileTickRate > 0.5)
        {
            Attack();
            projectileTickRate = 0;
        }
    }
    void movement()
    {
        Vector3 playerPosition = player.transform.position;

        Vector3 direction = playerPosition - transform.position;
        direction.Normalize();

        transform.position = transform.position + direction * Time.deltaTime * speed;
    }
    void Attack()
    {
        GameObject projectile;
        projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;

    }

}
