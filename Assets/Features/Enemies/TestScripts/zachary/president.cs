using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class president : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;

    [SerializeField] private List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        spriteRenderer.sprite = sprites[0];

    }


    void Attack()
    {

        GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);  

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction;



    }


}
