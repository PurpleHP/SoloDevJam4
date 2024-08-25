using System;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
}
