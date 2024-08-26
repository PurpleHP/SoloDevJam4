using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject enemyDeath;
    [SerializeField] private GameObject ufoDeath;

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject;
        if (enemy.CompareTag("Enemy"))
        {
            if (enemy.name.Contains("UFO"))
            {
                Instantiate(ufoDeath, enemy.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(enemyDeath, enemy.transform.position, Quaternion.identity);
            }

            if (enemy.transform.parent != null)
            {
                Debug.Log("Parent found");
                Transform parent = enemy.transform.parent;

                foreach (Transform sibling in parent)
                {
                    Destroy(sibling.gameObject);
                }
                Destroy(enemy.transform.parent);
            }
            Destroy(enemy);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
