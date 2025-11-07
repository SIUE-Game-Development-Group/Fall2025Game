using Core.Scripts.Input;
using UnityEngine;


public class TemplateGun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    GameObject bullet;
    private Vector2 direction;

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
        
        // Get direction to target
        var mousePos = InputManager.Instance.MousePosition;
        mousePos.z = 10f;
        var mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
        mousePosWorld.z = 0;
        direction = mousePosWorld - this.gameObject.transform.position;

        // Calculate the angle in degrees
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        direction.Normalize();
        
    }

    void Attack()
    {
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
    }
}