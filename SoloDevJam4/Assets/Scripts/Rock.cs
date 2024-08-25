using UnityEngine;

public class Rock : MonoBehaviour
{
  

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
