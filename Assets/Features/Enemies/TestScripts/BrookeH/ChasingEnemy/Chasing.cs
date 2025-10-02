using UnityEngine;

public class Brooke : MonoBehaviour
{
    [SerializeField] private float speed;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 distToPlayer = playerPos - transform.position;
        transform.position += distToPlayer.normalized * Time.deltaTime * speed;
    }
}
