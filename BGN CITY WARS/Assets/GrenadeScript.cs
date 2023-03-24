using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
Rigidbody rb;
public float SplodeTime;
private Transform CollidersParent;
 private void Start() 

{
    rb = this.GetComponent<Rigidbody>();
    CollidersParent =  this.transform.Find("SET OFF");
    
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
        Destroy(this.gameObject,3f);
        if(transform.GetComponentInChildren<SphereCollider>().enabled)
        {
        
        StartCoroutine(DisableColliders());

        }
    }







 IEnumerator DisableColliders()

{
    yield return new WaitForSeconds(0.1f);

    foreach (SphereCollider sphereCollider in CollidersParent.GetComponentsInChildren<SphereCollider>())
    {
        sphereCollider.enabled = false;
    }


}








 





}
