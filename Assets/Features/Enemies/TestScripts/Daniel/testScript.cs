using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public float speed;
    private float timePassed;
    private readonly float fireRate = 3;
    private readonly float fireSpeed = 1000;
    public GameObject player;
    public GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //timePassed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        timePassed += Time.deltaTime;
        if (timePassed > fireRate)
        {
            Attack();
            timePassed = 0;
        }

    }

    void Attack()
    {
        GameObject bullet;
        Vector3 direction = player.transform.position - transform.position;
        float playerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0,0,playerAngle - 90));
        bullet.GetComponent<Rigidbody2D>().linearVelocity = (fireSpeed * Time.deltaTime * direction.normalized);

    }

    void Movement()
    {
        
        Vector3 playerPosition = player.transform.position;
        Vector3 enemyVector = (playerPosition - transform.position);
        transform.position = transform.position + (speed * Time.deltaTime * enemyVector.normalized);
        
    }
}
