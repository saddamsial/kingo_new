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
    public int RocketsFired = 0;
    public GameObject SmokeVFX;
    public  bool Reloading;
    private AudioSource AS;

 void OnEnable() 
{
  Player= transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
  weaponstatus=Player.GetComponent<WeaponStatus>();   
   if ( weaponstatus.CurrentClip < 1 && weaponstatus.NoAmmo != true)
        {
            StartCoroutine(Reload());
        }
}

private void OnDisable()
{
    //reset VFX
     SmokeVFX.gameObject.SetActive(false);
     Reloading = false;
}
void Start() 
{
    
    fireRate= RocketWeapon.FireRate;
    weapontype=RocketWeapon.Weapontype;
    weaponstatus.CurrentClip = RocketWeapon.MaxClip;
    AS = Player.GetComponent<AudioSource>();
   
}


    void Update()

    {  
       
          //sync vars    
         canfire = Player.GetComponent<PlayerActionsVar>().canfire;
         Player.GetComponent<PlayerActionsVar>().Fired = Fired;
         Player.GetComponent<PlayerActionsVar>().IsReloading=Reloading;

        if (Input.GetButton("Fire1") &&canfire && Time.time > nextFireTime && weaponstatus.CurrentClip >0 && !Reloading)
        {
            nextFireTime = Time.time + 1f / fireRate;
            FireRocket();
            StartCoroutine(ResetFired());
        }

 //NO AMMO SET UP

    if (weaponstatus.MaxedAmmo)
    {
        weaponstatus.TotalAmmo = RocketWeapon.MaxAmmo;
    }
        if(weaponstatus.CurrentClip < 1 && weaponstatus.TotalAmmo == 0)  
        {
            weaponstatus.NoAmmo = true;
            RocketsFired = RocketWeapon.MaxClip;      
        }
        
        else 
    {
        {
            weaponstatus.NoAmmo = false;      
        }
    }

      //Reset BulletsFired Custom conditions(calculate the difference manually)
      if (weaponstatus.TotalAmmo == 0 )
      {
        RocketsFired = RocketWeapon.MaxClip - weaponstatus.CurrentClip;
      }


        //Ammo Clamp
        if (weaponstatus.TotalAmmo <= 0)
        {
            weaponstatus.TotalAmmo = 0;
        }

        //Clip Clamp
        if (weaponstatus.CurrentClip <= 0)
        {
            weaponstatus.CurrentClip = 0;
        }

        if (weaponstatus.CurrentClip >= RocketWeapon.MaxClip)

          {weaponstatus.CurrentClip = RocketWeapon.MaxClip;}


        

        ///check reload conditions///

        //auto reload

        if (weaponstatus.CurrentClip == 0&!Reloading && ! weaponstatus.NoAmmo && weaponstatus.TotalAmmo > 0)
        {
          StartCoroutine( Reload());
        }
        //Manual reload
        if (Input.GetKey(KeyCode.R) && !Reloading && !weaponstatus.NoAmmo && weaponstatus.CurrentClip < RocketWeapon.MaxClip && weaponstatus.TotalAmmo > 0) 
        {
            StartCoroutine(Reload());
            
        } 
    }
    void FireRocket()
    {
        Fired = true;
         //track shots fired
         RocketsFired ++;
         //subtract bullets
        weaponstatus.CurrentClip = weaponstatus.CurrentClip - 1;
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


    
   
   IEnumerator Reload()

    {//sf
   
     Reloading = true;
     AS.PlayOneShot(RocketWeapon.ReloadSFX, 1f);
     weaponstatus.MaxedAmmo = false; 
    {
        yield return new WaitForSeconds(RocketWeapon.ReloadTime);

 if     (weaponstatus.TotalAmmo < RocketWeapon.MaxClip)
     {
        
         weaponstatus.CurrentClip = weaponstatus.CurrentClip + weaponstatus.TotalAmmo;
         weaponstatus.TotalAmmo = weaponstatus.TotalAmmo - RocketsFired;
         RocketsFired = 0;
         Reloading = false;
     }

else
        {
         weaponstatus.CurrentClip = weaponstatus.CurrentClip + RocketsFired;
         weaponstatus.TotalAmmo = weaponstatus.TotalAmmo - RocketsFired;
         RocketsFired = 0;
         Reloading = false;
        }
    
       
        }


    }//ef  


}
