using System;
using System.Collections.Generic;
using Core.Scripts.Game;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class gravesmasherScript : Enemy
{
    private GameObject player;
    Rigidbody2D gsRigidbody;
    [SerializeField] private List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        base.Start();
        gsRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        base.Update();
        RotateSprite();
        Movement();
    }

    void Movement()
    {
        Vector2 playerPosition = player.transform.position;
        if (Vector2.Distance(transform.position, playerPosition) > 2)
        {
            Vector2 enemyVector = playerPosition - gsRigidbody.position;
            Move(enemyVector);
        } else
        {
            Move(new Vector2(0,0));
        }
    }

    void RotateSprite()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = playerPosition - transform.position;
        float playerAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        playerAngle = Mathf.Repeat(playerAngle, 360);
        int spriteDirection = 0;
        switch (playerAngle)
        {
            case <= 22.5f:
                spriteDirection = 0;
                break;
            case <= 67.5f:
                spriteDirection = 1;
                break;
            case <= 112.5f:
                spriteDirection = 2;
                break;
            case <= 157.5f:
                spriteDirection = 3;
                break;
            case <= 202.5f:
                spriteDirection = 4;
                break;
            case <= 247.5f:
                spriteDirection = 5;
                break;
            case <= 292.5f:
                spriteDirection = 6;
                break;
            case <= 337.5f:
                spriteDirection = 7;
                break;
            default:
                spriteDirection = 0;
                break;
        }
        spriteRenderer.sprite = sprites[spriteDirection];
    }
}
