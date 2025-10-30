using UnityEngine;
using UnityEngine.UIElements;

public class TempPlayerScript : MonoBehaviour
{
    float i = 0;

    public float speed;
    Vector2 direction;
    Vector2 facing = Vector2.right;
    float timeOrthogonal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, moveVertical, 0);
        if (facing != direction)
        {
            if ((moveHorizontal == 0 ^ moveVertical == 0) && (facing.x != 0 && facing.y != 0))
            {
                // We are moving orthoganally and were facing diagonally
                if (timeOrthogonal >= 0.3f)
                {
                    facing = direction;
                    
                } else
                {
                    timeOrthogonal += Time.deltaTime;
                }
            } else if ((moveHorizontal == 0 ^ moveVertical == 0) && !(facing.x != 0 && facing.y != 0))
            {
                // We are moving orthagonally and were facing orthagonally
                facing = direction;
                timeOrthogonal += Time.deltaTime;
            } else if ((moveHorizontal != 0 && moveVertical != 0))
            {
                // We are moving diagonally
                facing = direction;
                timeOrthogonal = 0;
            } else
            {
                // We aren't moving
                timeOrthogonal += Time.deltaTime;
            }
        }
        direction.Normalize();
        facing.Normalize();


        Movement(direction);
    }
    void Movement(Vector3 direction)
    {

        transform.position = transform.position + (direction *  speed * Time.deltaTime);

    }
    public Vector3 Direction()
    {
        return facing;
    }

}
