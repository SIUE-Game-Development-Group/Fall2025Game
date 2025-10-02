using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool manualShoot = true;

    void Awake()
    {
        if (!manualShoot) {
            Invoke("shoot", 3f);
        }
    }

    void shoot()
    {
        Attack();
        Invoke("shoot", 3f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && manualShoot)
        {
            Attack();
        }
    }

    void Attack()
    {
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }
}
