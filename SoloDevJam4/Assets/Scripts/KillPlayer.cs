using System;
using System.Collections;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerDeathEffect;
    public CameraFade cameraFade;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Material mat;
    [SerializeField] private Transform bloodSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
            pm.GotHit();
            if (pm.hp <= 0 && other.gameObject != null)
            {
                pm.speed = 0;
                pm.GetComponent<Rigidbody>().isKinematic = true;
                pm.run.Stop();
                pm.playerDeath.Play();
                GameObject o;
                (o = other.gameObject).GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(WaitForFade(o));
            }
            else if(pm.hp > 0)
            {
                if (mat != null)
                {
                    Color color = mat.color;
                    pm.playerHurt.Play();
                    StartCoroutine(ChangeColor(color));
                }
            }
            
            
        }
    }

    private IEnumerator ChangeColor(Color originalColor)
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f); 
        mat.color = originalColor;
    }
    
    IEnumerator WaitForFade(GameObject player)
    {
        yield return new WaitForSeconds(0.4f);
        Instantiate(playerDeathEffect, bloodSpawn.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);

        cameraFade.StartFade();
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(1.2f);
        gameOverScreen.SetActive(true);
    }
}
