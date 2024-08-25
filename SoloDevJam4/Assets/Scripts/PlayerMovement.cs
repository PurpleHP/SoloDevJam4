using System.Collections;
using CustomCamera;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 5f;
    public float rotationDuration = 1.5f;

    [Header("Light - Camera")]
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private Light sceneLight;
    
    private Vector3 _gravity;
    private bool _onNormalGravity = true;
    private bool _canChangeGravity;
    
    [Header("Ground Check")]
    [SerializeField] private GameObject playerFeet;
    [SerializeField] float collisionRadius = 0.8f;
    [SerializeField] LayerMask collisionLayer;

    [Header("Animation")] 
    [SerializeField] private Animator animator;
    //Shooting---
    private bool _isShooting;
    [SerializeField] private GameObject gunPoint;
    [SerializeField] private float gunCooldownAndBulletDestroy = 0.5f; 
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    private static readonly int DidShoot = Animator.StringToHash("didShoot");

    void Start()
    {
        FollowCamera2D.SetTarget(gameObject.transform);
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _gravity = Physics.gravity;
        _canChangeGravity = true;
        _isShooting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canChangeGravity)
        {
            ChangeGravity();
        }
        Collider[] colliders= Physics.OverlapSphere(playerFeet.transform.position, collisionRadius, collisionLayer);
        
        if(colliders.Length != 0)
            _canChangeGravity = true;

       
        if (Input.GetKeyDown(KeyCode.Q) && !_isShooting)
        {
            Debug.Log("Shot");
            _isShooting = true;
            animator.SetTrigger(DidShoot);
            StartCoroutine(Shoot());
        }
        
    }
    private void FixedUpdate()
    {
        Vector3 rbVelocity = _rb.linearVelocity;
        _rb.linearVelocity = new Vector3(speed, rbVelocity.y, rbVelocity.z);
        _rb.AddForce(_gravity, ForceMode.Acceleration);
        
    }

    //Changes gravity, rotates light and camera for smoothness (and rotates player -> the empty gameobject)
    void ChangeGravity()
    {
        _gravity *= -1;
        _canChangeGravity = false;
        RotateAll();
    }

    void RotateAll()
    {
        Vector3 tempScale = gameObject.transform.localScale;
        transform.localScale = new Vector3(tempScale.x, tempScale.y * -1, tempScale.z);
        StartCoroutine(RotateCameraAndLight());
    }
    
    //For rotating in [duration] seconds
    private IEnumerator RotateCameraAndLight()
    {
        Vector3 cameraAngles = sceneCamera.transform.rotation.eulerAngles;
        Vector3 lightAngles = sceneLight.transform.rotation.eulerAngles;
        FollowCamera2D.cameraCenterY = _onNormalGravity ? 0.6f : 0.4f;
        _onNormalGravity = !_onNormalGravity;

        Debug.Log(FollowCamera2D.cameraCenterY);
        float startCameraRot = cameraAngles.x;
        Quaternion startCameraRotation = Quaternion.Euler(startCameraRot, cameraAngles.y, cameraAngles.z);
        Quaternion targetCameraRotation = Quaternion.Euler(startCameraRot*-1, cameraAngles.y, cameraAngles.z);

        float startLightRot = lightAngles.x;
        Quaternion startLightRotation = Quaternion.Euler(startLightRot, lightAngles.y, lightAngles.z);
        Quaternion targetLightRotation = Quaternion.Euler(startLightRot*-1, lightAngles.y, lightAngles.z);

        
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;

            Quaternion currentCameraRotation = Quaternion.Lerp(startCameraRotation, targetCameraRotation, elapsedTime / rotationDuration);
            sceneCamera.transform.rotation = currentCameraRotation;
            
            Quaternion currentLightRotation = Quaternion.Lerp(startLightRotation, targetLightRotation, elapsedTime/ rotationDuration);
            sceneLight.transform.rotation = currentLightRotation;

            yield return null; 
        }

        sceneCamera.transform.rotation = targetCameraRotation;
        sceneLight.transform.rotation = targetLightRotation;
    }

    IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.transform.position, gunPoint.transform.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(gameObject.transform.right * bulletSpeed, ForceMode.Impulse);
            
        }
        yield return new WaitForSeconds(gunCooldownAndBulletDestroy);
        _isShooting = false;
        Destroy(bullet, gunCooldownAndBulletDestroy);
    }

}
