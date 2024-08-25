using System;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
    
      Destroy(other.transform.parent != null ? other.transform.parent.gameObject : other.gameObject);
   }
   
   
}
