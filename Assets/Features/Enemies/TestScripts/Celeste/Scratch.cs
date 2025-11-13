using UnityEngine;

public class Scratch : MonoBehaviour
{
    int framecount = 0;

    public int FrameLifeSpan;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        framecount++;
        if (framecount > FrameLifeSpan) { 
        Destroy(gameObject);
        }
        
    }
}
