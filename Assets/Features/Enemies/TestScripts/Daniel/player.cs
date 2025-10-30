using Features.MainCharacter.Scripts;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody2D player_rb;
    private float horizontalInput;
    private float verticalInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        Vector2 direction = new Vector2(horizontalInput, verticalInput);
        player_rb.linearVelocity = direction.normalized * speed;
    }
}
