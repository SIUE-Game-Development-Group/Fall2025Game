using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletTime = 10f;
    void Awake()
    {
        Invoke("bulletDeath", bulletTime);
    }

    void bulletDeath()
    {
        Destroy(this.gameObject);
    }
}
