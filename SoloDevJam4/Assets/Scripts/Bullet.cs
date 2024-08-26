using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject enemyDeath;
    [SerializeField] private GameObject ufoDeath;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.name.Contains("UFO"))
            {
                Instantiate(ufoDeath, other.gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(enemyDeath, other.gameObject.transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
