using UnityEngine;
using System.Collections;
using Core.Scripts.Game;

public class BulletScript : MonoBehaviour
{

    [SerializeField] private GameObject hitbox;
      
    float i = 0;
    [SerializeField] private float BulletTime = 5;

void Start()
{
    
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
