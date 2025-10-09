using UnityEngine;

public class faith : MonoBehaviour
{

    public float speed;
    public GameObject player;
    public GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        Vector3 playerPosition = player.transform.position; //player position

        Vector3 enemyPosition = enemy.transform.position; //enemy position

        // Find playerPosition in relation to enemyPosition
        Vector3 direction = (playerPosition - enemyPosition).normalized;

        //Move in that direction
        enemy.transform.position += direction * Time.deltaTime * speed;
    }
}
