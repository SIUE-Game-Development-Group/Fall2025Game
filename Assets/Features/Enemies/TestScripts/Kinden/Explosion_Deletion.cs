using UnityEngine;

public class Explosion_Deletion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float i =0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (i >= .5)
        {
            Destroy(gameObject);
                
        }
        i += Time.deltaTime;
    }
}
