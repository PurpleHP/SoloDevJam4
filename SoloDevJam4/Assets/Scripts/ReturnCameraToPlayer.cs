using System;
using System.Collections;
using CustomCamera;
using UnityEngine;

public class ReturnCameraToPlayer : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    public float smoothTime = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private GameObject player;
    private Vector3 playerPos;
    private bool found = false;
    private float playerSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            found = true;
            player = other.gameObject;
            playerSpeed = player.gameObject.GetComponent<PlayerMovement>().speed;
            playerPos = player.gameObject.transform.position;
            StartCoroutine(WaitAndReturnCamera());
        }
    }

    IEnumerator WaitAndReturnCamera()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        Vector3 startPosition = camera.transform.position;
        Vector3 targetPosition = new Vector3(playerPos.x, startPosition.y, startPosition.z);

        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            Vector3 adjustedTargetPosition = targetPosition + Vector3.right * ((playerSpeed+1) * Time.deltaTime);

            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, adjustedTargetPosition, ref velocity, smoothTime);

            elapsedTime += Time.deltaTime; 
            yield return null;
        }
    
        camera.transform.position = targetPosition;
    }

    private void Update()
    {
        if (found)
        {
            playerPos = player.gameObject.transform.position;
            playerSpeed += 0.001f;
        }
    }
}
