using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class SimpleMove : MonoBehaviour
{
    [SerializeField] private int speed = 0;
    private bool moveFlip = false;
    void Update()
    {
        if (transform.position.y < -3.0 || transform.position.y > 3.0)
        {
            moveFlip = !moveFlip;
        }

        if (moveFlip)
        {
            float yPos = transform.position.y;
            yPos += speed * -1 * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, yPos);
        }
        else
        {
            float yPos = transform.position.y;
            yPos += speed * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, yPos);
        }
    }
}
