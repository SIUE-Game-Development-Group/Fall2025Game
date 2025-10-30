using Unity.Mathematics;
using UnityEngine;

public class shield : MonoBehaviour
{
    public GameObject player;
    private float rotSpeed = 90f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
    
    void FollowPlayer()
    {
        float step = rotSpeed * Time.deltaTime;
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = playerPosition - transform.position;
        float playerAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, playerAngle), step);

    }
}
