using UnityEngine;
using System.Collections;
using Core.Scripts.Game;

public class BulletScript : MonoBehaviour
{

    [SerializeField] private GameObject hitbox;
    [SerializeField] private bool isRandom = false;  
    float i = 0;
    [SerializeField] private float BulletTime = 5;
    [SerializeField] private float minTime = 2;
    [SerializeField] private float maxTime = 3;
    

void Start()
{
        if (isRandom == true)
        {
            BulletTime = Random.Range(minTime, maxTime);
        }
        else
        {
            
        }
    
}

    // Update is called once per frame
    void Update()
    {
       
        if (i >= BulletTime)
        {
            Destroy(gameObject);
        }
        i += Time.deltaTime;
    }
}
