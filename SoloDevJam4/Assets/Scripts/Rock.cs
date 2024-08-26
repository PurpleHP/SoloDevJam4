using System;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float spawnTime;

    private void Start()
    {
        Destroy(gameObject, spawnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Enemy"))
            Destroy(gameObject);
    }
    
    
}
