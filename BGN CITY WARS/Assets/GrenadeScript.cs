using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
Rigidbody rb;
public float SplodeTime;
 private void Start() 
{
    rb = this.GetComponent<Rigidbody>();
}


public void SetOff()

{
 rb.isKinematic=false;

 StartCoroutine(Splode());

}

 IEnumerator Splode()
    {
        
        yield return new WaitForSeconds(SplodeTime);
        transform.Find("SET OFF").gameObject.SetActive(true);
        transform.GetComponentInChildren<MeshRenderer>().enabled=(false);
        transform.GetComponentInChildren<SphereCollider>().enabled=(false);
        Destroy(this.gameObject,3f);
    }


}
