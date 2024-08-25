using System;
using System.Collections;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private CameraFade cameraFade;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Material mat;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
            pm.GotHit();
            if (pm.hp == 0)
            {
                pm.speed = 0;
                pm.GetComponent<Rigidbody>().isKinematic = true;
                
                StartCoroutine(WaitForFade(other.gameObject));
            }
            else if(pm.hp < 0)
            {
                if (mat != null)
                {
                    Color color = mat.color;
                    StartCoroutine(ChangeColor(color));
                }
            }
        }
    }

    private IEnumerator ChangeColor(Color originalColor)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.5f); 
        mat.color = originalColor;
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
