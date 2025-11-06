using Core.Scripts.Input;
using UnityEngine;


public class TemplateGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    

    void shoot()
    {
        Attack();
        Invoke("shoot", 3f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        GameObject bullet;
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Get direction to target
        var mousePos = InputManager.Instance.MousePosition;
        mousePos.z = 10f;
        var mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
        mousePosWorld.z = 0;
        Vector2 direction = mousePosWorld - transform.position;

        // Calculate the angle in degrees
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        direction.Normalize();

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }
}