using UnityEngine;
using Core.Scripts.Game;

public class bulletE : MonoBehaviour
{
    private float totalTime;
    public float attackTime;

    
    // Update is called once per frame
    void Update()
    {
        totalTime += Time.deltaTime;
        if (totalTime > attackTime ) {
            Destroy(gameObject);
        }
    }
}
