using System.Collections;
using UnityEngine;

public class KillInstant : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private CameraFade cameraFade;
    [SerializeField] private GameObject gameOverScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
            pm.hp = 1;
            pm.GotHit();

            pm.speed = 0;
            pm.GetComponent<Rigidbody>().isKinematic = true;

            StartCoroutine(WaitForFade(other.gameObject));
        }
    }
    IEnumerator WaitForFade(GameObject player)
    {
        yield return new WaitForSeconds(1.4f);
        cameraFade.StartFade();
        yield return new WaitForSeconds(0.3f);
        Instantiate(explosion, player.transform.position, Quaternion.identity);
        Destroy(player);
        yield return new WaitForSeconds(1.2f);
        gameOverScreen.SetActive(true);
    }
}