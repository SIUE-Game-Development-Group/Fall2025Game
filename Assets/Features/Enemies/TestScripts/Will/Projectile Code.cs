using UnityEngine;

public class ProjectileCode : MonoBehaviour
{
    float projectileDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        projectileDuration += Time.deltaTime;
        if (projectileDuration > 2)
        {
            Destroy(gameObject);
        }
            
    }
}
