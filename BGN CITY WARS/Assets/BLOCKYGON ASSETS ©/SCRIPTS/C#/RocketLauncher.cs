using UnityEngine;
using System.Collections;
public class RocketLauncher : MonoBehaviour
{
    public WeaponDATA2 RocketWeapon;
    private WeaponStatus weaponstatus;
    public GameObject rocketPrefab;
    public Transform launchPoint;
    public float launchForce = 500f;
    public float fireRate;
    public Transform crosshair;
    public Transform Player;
    private float nextFireTime = 0f;
    public bool canfire;
    public int weapontype;
    public bool Fired;
    public GameObject SmokeVFX;
    public  bool Reloading;

 void OnEnable() 
{
  Player= transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
  weaponstatus=Player.GetComponent<WeaponStatus>();   
}

void Start() 
{
    
    fireRate= RocketWeapon.FireRate;
    weapontype=RocketWeapon.Weapontype;
   
}


    void Update()

    {  
       
          //sync vars    
         canfire = Player.GetComponent<PlayerActionsVar>().canfire;
         Player.GetComponent<PlayerActionsVar>().Fired = Fired;


        if (Input.GetButton("Fire1") &&canfire && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            FireRocket();
            StartCoroutine(ResetFired());
        }



        
    }
    void FireRocket()
    {
        Fired = true;
        SmokeVFX.gameObject.SetActive(true);
        GameObject rocketInstance = Instantiate(rocketPrefab, launchPoint.position, launchPoint.rotation);
        Rigidbody rocketRigidbody = rocketInstance.GetComponent<Rigidbody>();
        Vector3 launchDirection = (crosshair.position - launchPoint.position).normalized;
        rocketRigidbody.AddForce(launchDirection * launchForce);
    }
    
   IEnumerator ResetFired()
   {
      yield return new WaitForSeconds(0.25f);
      Fired=false;
   }

private void OnDisable()
{
    //reset VFX
     SmokeVFX.gameObject.SetActive(false);
}
    
   
   


}
