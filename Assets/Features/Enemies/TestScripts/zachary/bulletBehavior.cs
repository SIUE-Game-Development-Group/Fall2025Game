using UnityEngine;

public class bulletBehavior : MonoBehaviour
{

    private Camera mainCamera;

    private float cameraWidth;
    private float cameraHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        cameraHeight = mainCamera.orthographicSize;
        cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > mainCamera.transform.position.x + cameraWidth || transform.position.x < mainCamera.transform.position.x - cameraWidth || transform.position.y > mainCamera.transform.position.y + cameraHeight || transform.position.y < mainCamera.transform.position.y - cameraHeight)
        {
            
            Destroy(gameObject);
        }


    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
