using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    float i = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (i >= 5)
        {
            Destroy(gameObject);
        }
        i += Time.deltaTime;
    }
}
