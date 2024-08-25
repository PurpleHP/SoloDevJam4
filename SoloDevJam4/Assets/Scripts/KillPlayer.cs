using System;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private CameraFade cameraFade;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            cameraFade.StartFade();
        }
    }
}
