using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject road;
    private Vector3 _spawnPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Spawn"))
                {
                    _spawnPosition = child.gameObject.transform.position;
                    break;

                }
            }
            float randomValue = Random.Range(-1f, 1f);
            Vector3 spawnPointCoordinates = new Vector3(_spawnPosition.x, randomValue,
                _spawnPosition.z);
            Instantiate(road, spawnPointCoordinates, Quaternion.identity);
        }
    }
}
