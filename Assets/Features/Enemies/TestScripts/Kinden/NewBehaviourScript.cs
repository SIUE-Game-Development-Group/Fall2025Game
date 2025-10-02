using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public float moveSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {

        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys
        float verticalInput = Input.GetAxis("Vertical");   // W/S or Up/Down arrow keys

        // Create a movement vector based on input
        Vector3 movement = new Vector3(horizontalInput, verticalInput,0f ).normalized;

        // Move the character
        transform.Translate(movement * moveSpeed * Time.deltaTime);









    }
}
