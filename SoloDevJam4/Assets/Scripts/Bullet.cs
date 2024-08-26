using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject enemyDeath;
    [SerializeField] private GameObject ufoDeath;
    
    [SerializeField] AudioSource enemySound;

    [SerializeField] AudioSource ufoSound;
    public float bulletSpeed;
    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject;
        if (enemy.CompareTag("Enemy"))
        {
            if (enemy.name.Contains("UFO"))
            {
                Instantiate(ufoDeath, enemy.transform.position, Quaternion.identity);
                ufoSound.Play();
            }
            else
            {
                Instantiate(enemyDeath, enemy.transform.position, Quaternion.identity);
                enemySound.Play();
            }

            if (enemy.transform.parent != null)
            {
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

    private void Update()
    {
        transform.position += transform.right * (bulletSpeed * Time.deltaTime);
    }
}
