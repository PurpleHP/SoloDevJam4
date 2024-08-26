using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject road;
    private Vector3 _spawnPosition;

    [Header("Enemy Spawn")]  
    [SerializeField] private GameObject[] enemyTypes;
    private GameObject[] enemies; 
    public int numberOfEnemies = 5;
    public float minDistanceBetweenEnemies = 5f; 
    private List<Vector2> usedPositions = new List<Vector2>(); // List to store used positions
    
    private void OnTriggerEnter(Collider other)
    {
        enemies = new GameObject[numberOfEnemies];

        for (int i = 0; i < numberOfEnemies; i++)
        {
            int randomIndex = Random.Range(0, enemyTypes.Length); // Randomly choose from available enemy types
            enemies[i] = enemyTypes[randomIndex];
        }
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
            float randomValue = Random.Range(-3f, 3f);
            Vector3 spawnPointCoordinates = new Vector3(_spawnPosition.x, randomValue,
                _spawnPosition.z);
            Instantiate(road, spawnPointCoordinates, Quaternion.identity);

            Vector2 xRange = new Vector2(spawnPointCoordinates.x - 21f, spawnPointCoordinates.x + 49f);
            Vector2 yRange = new Vector2(spawnPointCoordinates.y - 1f, spawnPointCoordinates.y + 6f);

            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector2 randomWorldPosition;
                bool positionIsValid;

                do
                {
                    randomWorldPosition = GenerateRandomPosition(xRange, yRange);
                    positionIsValid = IsPositionValid(randomWorldPosition);
                }
                while (!positionIsValid);

                usedPositions.Add(randomWorldPosition);

                GameObject instance = Instantiate(enemies[i]);
                instance.transform.position = new Vector3(randomWorldPosition.x, randomWorldPosition.y, 0);
            }
        }
    }

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
            if (Vector2.Distance(position, usedPosition) < minDistanceBetweenEnemies)
            {
                return false;
            }
        }
        return true;
    }
}
