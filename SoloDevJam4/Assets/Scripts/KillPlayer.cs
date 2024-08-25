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
            if (pm.hp <= 0)
            {
                Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                cameraFade.StartFade();
                StartCoroutine(WaitForFade());
            }
            else
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
    
    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(1.5f);
        gameOverScreen.SetActive(true);
    }
}
