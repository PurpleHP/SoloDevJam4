using UnityEngine;

public class HeadEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    void Update()
    {
        var pos = gameObject.transform.position;
        gameObject.transform.position = new Vector3(pos.x - speed * Time.deltaTime, pos.y, pos.z);
        
    }
}
