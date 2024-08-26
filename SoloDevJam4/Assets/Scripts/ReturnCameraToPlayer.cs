using System;
using System.Collections;
using UnityEngine;

public class ReturnCameraToPlayer : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private GameObject player;
    private Vector3 playerPos;
    private bool found = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            found = true;
            Debug.Log("Starting");
            player = other.gameObject;
            playerPos = player.gameObject.transform.position;
            StartCoroutine(WaitAndReturnCamera());
        }
    }

    IEnumerator WaitAndReturnCamera()
    {
        yield return new WaitForSeconds(2f);

        Vector3 startPosition = camera.transform.position;
        Vector3 targetPosition = new Vector3(playerPos.x+4, startPosition.y, startPosition.z);

        float elapsedTime = 0f;

        while (elapsedTime < smoothTime)
        {
            camera.transform.position = Vector3.SmoothDamp(startPosition, targetPosition, ref velocity, smoothTime, float.MaxValue, Time.deltaTime);
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
        }
    }
}
