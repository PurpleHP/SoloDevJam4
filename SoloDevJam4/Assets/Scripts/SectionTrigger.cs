using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject road;
    private Vector3 _spawnPosition;
    
    [Header("Enemy Spawn")]
    public GameObject prefab; // Reference to the prefab you want to instantiate
    //public Transform parentObject; // Reference to the parent GameObject
    public int numberOfPrefabs = 4; // Number of prefabs to instantiate
    public float minDistanceBetweenPrefabs = 5f; // Minimum allowed distance between prefabs
    private List<Vector2> usedPositions = new List<Vector2>(); // List to store used positions

    
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
            float randomValue = Random.Range(-2f, 2f);
            Vector3 spawnPointCoordinates = new Vector3(_spawnPosition.x, randomValue,
                _spawnPosition.z);
            Instantiate(road, spawnPointCoordinates, Quaternion.identity);

            // Define the local coordinate ranges relative to the parent object
            Vector2 xRange = new Vector2(spawnPointCoordinates.x - 21f, spawnPointCoordinates.x + 49f);
            Vector2 yRange = new Vector2(spawnPointCoordinates.y - 2f, spawnPointCoordinates.y + 7f);

            for (int i = 0; i < numberOfPrefabs; i++)
            {
                Vector2 randomLocalPosition;
                bool positionIsValid;

                do
                {
                    randomLocalPosition = GenerateRandomPosition(xRange, yRange);
                    positionIsValid = IsPositionValid(randomLocalPosition);
                }
                while (!positionIsValid);

                // Store the valid position
                usedPositions.Add(randomLocalPosition);

                // Instantiate the prefab as a child of the parent object
                GameObject instance = Instantiate(prefab, gameObject.transform);
                instance.transform.localPosition = new Vector3(randomLocalPosition.x, randomLocalPosition.y, 0);
            }
        }
    }
    
    
    //Enemy Spawn
    private Vector2 GenerateRandomPosition(Vector2 xRange, Vector2 yRange)
    {
        float x = Random.Range(xRange.x, xRange.y);
        float y = Random.Range(yRange.x, yRange.y);
        return new Vector2(x, y);
    }

    private bool IsPositionValid(Vector2 position)
    {
        foreach (Vector2 usedPosition in usedPositions)
        {
            if (Vector2.Distance(position, usedPosition) < minDistanceBetweenPrefabs)
            {
                return false;
            }
        }
        return true;
    }
}
