using System;
using System.Collections;
using CustomCamera;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 5f;
    public float rotationDuration = 1.5f;
    private float score;
    public float hp;
    private float startingScaleCount;
    private float _imgDecreseCount;
    public Material mat;

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

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Image _greenImage;
    [SerializeField] private Image redImage;
    
    [Header("Animation")] 
    [SerializeField] private Animator animator;
    //Shooting---
    private bool _isShooting;
    [SerializeField] private GameObject gunPoint;
    [SerializeField] private float gunCooldownAndBulletDestroy = 0.5f; 
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject bulletPrefab;
    private static readonly int DidShoot = Animator.StringToHash("didShoot");
    private static readonly int IsOnAir = Animator.StringToHash("isOnAir");
    private static readonly int Hit = Animator.StringToHash("gotHit");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    
    [Header("Effects")]
    [SerializeField] private GameObject explosion;



    private void Awake()
    {
        mat.color = Color.white;
        if(hp ==0)
            hp = 3;
        PlayerPrefs.SetInt("Score",0);
        score = 0;
        scoreText.text = "0";
        hpText.text = "3";
        startingScaleCount = hp;
        _imgDecreseCount = _greenImage.rectTransform.sizeDelta.x / startingScaleCount;
    }

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
        speed += 0.001f;
        score += Time.deltaTime + (speed * 0.0001f);
        int intScore = Mathf.FloorToInt(score);
        
        PlayerPrefs.SetInt("Score",intScore);

        scoreText.text = intScore + " m";
        
        if (Input.GetKeyDown(KeyCode.Space) && _canChangeGravity)
        {
            ChangeGravity();
        }
        Collider[] colliders= Physics.OverlapSphere(playerFeet.transform.position, collisionRadius, collisionLayer);

        if (colliders.Length != 0)
        {
            _canChangeGravity = true;
            animator.SetBool(IsOnAir,false);
        }
        

       
        if (Input.GetKeyDown(KeyCode.Q) && !_isShooting)
        {
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
        animator.SetBool(IsOnAir,true);
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
        Transform gunpoint = gunPoint.transform;
        var position = gunpoint.position;
        GameObject bullet = Instantiate(bulletPrefab, position, gunpoint.rotation);
        var bulletSmoke =  Instantiate(explosion, position, Quaternion.identity);
        bulletSmoke.transform.parent = gunpoint;

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.AddForce(gameObject.transform.right * bulletSpeed, ForceMode.Impulse);
            
        }
        yield return new WaitForSeconds(gunCooldownAndBulletDestroy);
        _isShooting = false;
        Destroy(bullet, gunCooldownAndBulletDestroy);
    }

    public void GotHit()
    {
        animator.SetTrigger(Hit);
        if (hp > 0)
            hp--;

        hpText.text = hp.ToString();
        ScaleBarDown();
        if (hp <= 0)
        {
            speed = 0;
            animator.SetBool(IsDead,true);
            _rb.linearVelocity = Vector3.zero;
            _rb.useGravity = true;

        }
        StartCoroutine(TakeDamage());
        
    }
    
    public void ScaleBarDown()
    {
        RectTransform rt = _greenImage.rectTransform;

        //Decreses widht by imgDecreseCount
        var imgWidth = rt.sizeDelta;
        imgWidth = new Vector2(imgWidth.x - _imgDecreseCount, imgWidth.y);
        rt.sizeDelta = imgWidth;

        //Moves to right by half of imgDecreseCount
        var imgPos = rt.anchoredPosition;
        imgPos = new Vector2(imgPos.x + (_imgDecreseCount / 2), imgPos.y);
        rt.anchoredPosition = imgPos;
    }

    public IEnumerator TakeDamage()
    {
        float duration = 0.3f;
        float targetAlpha = 0.3f;
        float startAlpha = 0f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            SetImageAlpha(alpha);
            yield return null; 
        }

        SetImageAlpha(targetAlpha);

        yield return new WaitForSeconds(0.2f);

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(targetAlpha, startAlpha, t / duration);
            SetImageAlpha(alpha);
            yield return null;
        }

        SetImageAlpha(startAlpha);
    }

    private void SetImageAlpha(float alpha)
    {
        if (redImage != null)
        {
            Color color = redImage.color;
            color.a = alpha; 
            redImage.color = color; 
        }
    }

}
