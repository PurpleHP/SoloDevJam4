using System.Collections;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform player;
    public float throwSpeed = 500.0f;
    private Animator anim;
    public float countdownTime = 4.0f;
    private Transform spawnPoint;
    private static readonly int Rock = Animator.StringToHash("throwRock");

    void Start()
    {
        anim = GetComponent<Animator>();
        Transform[] childTransforms = GetComponentsInChildren<Transform>();

        foreach (Transform child in childTransforms)
        {
            if (child.CompareTag("Spawn")) // Find the child with spawn tag
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
            yield return new WaitForSeconds(0.65f);
            
            GameObject thrownRock = Instantiate(rockPrefab, spawnPoint.position, Quaternion.identity);
            thrownRock.GetComponent<Rock>().spawnTime = 4f;
            thrownRock.transform.parent = transform;

            StartCoroutine(MoveRockToPlayer(thrownRock)); // Start moving the rock

            yield return new WaitForSeconds(countdownTime - 0.65f);
        }
    }

    IEnumerator MoveRockToPlayer(GameObject rock)
    {
        float elapsedTime = 0f;

        Vector3 playerpos = player.position;

        while (rock != null && elapsedTime < 4f)
        {
            var position = rock.transform.position;
            Vector3 directionToPlayer = (playerpos - position).normalized;
            position += directionToPlayer * (throwSpeed * Time.deltaTime);
            rock.transform.position = position;

            elapsedTime += Time.deltaTime;  
            yield return null;  
        }

        if (rock != null)
        {
            Destroy(rock);
        }
    }

}