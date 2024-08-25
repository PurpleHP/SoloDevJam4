using System.Collections;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform player;
    public float throwSpeed = 5.0f;
    private Animator anim;
    public float countdownTime = 3.0f;
    private Transform spawnPoint;
    private static readonly int Rock = Animator.StringToHash("throwRock");

    void Start()
    {
        anim = GetComponent<Animator>();
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        foreach (Transform child in childTransforms)
        {
            if (child.CompareTag("Spawn")) // Check if the child has the "Spawn" tag
            {
                spawnPoint = child;
                break; 
            }
        }
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (true)  // Loop to keep throwing rocks every countdown time
        {
            anim.SetTrigger(Rock);
            yield return new WaitForSeconds(0.3f);
            
            GameObject thrownRock = Instantiate(rockPrefab, spawnPoint.position, Quaternion.identity);
            StartCoroutine(MoveRockToPlayer(thrownRock)); // Start moving the rock

            yield return new WaitForSeconds(countdownTime - 0.3f);
        }
    }

    IEnumerator MoveRockToPlayer(GameObject rock)
    {
        while (rock != null && Vector3.Distance(rock.transform.position, player.position) > 0.1f)
        {
            var position = rock.transform.position;
            Vector3 directionToPlayer = (player.position - position).normalized;
            position += directionToPlayer * (throwSpeed * Time.deltaTime);
            rock.transform.position = position;

            yield return null; // Wait for the next frame
        }

        if (rock != null)
        {
            Destroy(rock);
        }
    }
}