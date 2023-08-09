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
    private float nextFireTime = 0f;
    public bool canfire;
    public int weapontype;
    public bool Fired;
    public int RocketsFired = 0;
    public int currentclip;
    public int totalammo;
    public bool noammo;
    public  bool Reloading;
    private AudioSource AS;
    public GameObject SmokeVFX;
    public Transform crosshair;
    private Transform Player;
 void OnEnable() 
{
  Player= transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
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
    totalammo=RocketWeapon.MaxAmmo;
    currentclip=RocketWeapon.MaxClip;
    fireRate= RocketWeapon.FireRate;
    weapontype=RocketWeapon.Weapontype;
    currentclip= RocketWeapon.MaxClip;
    AS = Player.GetComponent<AudioSource>();
   
}


    void Update()

    {  
       
          //sync vars    
         canfire = Player.GetComponent<PlayerActionsVar>().canfire;
         Player.GetComponent<PlayerActionsVar>().Fired = Fired;
         Player.GetComponent<PlayerActionsVar>().IsReloading=Reloading;
         Player.GetComponent<WeaponStatus>().CurrentClip=currentclip;
         Player.GetComponent<WeaponStatus>().TotalAmmo=totalammo;
         Player.GetComponent<WeaponStatus>().NoAmmo=noammo;
        if (ControlFreak2.CF2Input.GetButton("Fire1") &&canfire && Time.time > nextFireTime && weaponstatus.CurrentClip >0 && !Reloading)
        {
            nextFireTime = Time.time + 1f / fireRate;
            FireRocket();
            StartCoroutine(ResetFired());
        }

 //NO AMMO SET UP

    if (weaponstatus.MaxedAmmo)
    {
        totalammo = RocketWeapon.MaxAmmo;
    }
        if(currentclip < 1 && totalammo == 0)  
        {
            noammo = true;
            RocketsFired = RocketWeapon.MaxClip;      
        }
        
        else 
    {
        {
           noammo = false;      
        }
    }

      //Reset BulletsFired Custom conditions(calculate the difference manually)
      if (totalammo== 0 )
      {
        RocketsFired = RocketWeapon.MaxClip - currentclip;
      }


        //Ammo Clamp
        if (totalammo <= 0)
        {
            totalammo= 0;
        }

        //Clip Clamp
        if (currentclip <= 0)
        {
            currentclip= 0;
        }

        if (currentclip >= RocketWeapon.MaxClip)

          {currentclip = RocketWeapon.MaxClip;}


        

        ///check reload conditions///

        //auto reload

        if (currentclip == 0&!Reloading && ! noammo && totalammo> 0)
        {
          StartCoroutine( Reload());
        }
        //Manual reload
        if (ControlFreak2.CF2Input.GetKey(KeyCode.R) && !Reloading && !noammo && currentclip < RocketWeapon.MaxClip && totalammo> 0) 
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
         currentclip--;
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

 if     (totalammo < RocketWeapon.MaxClip)
     {
        
         currentclip += totalammo;
         totalammo -= RocketsFired;
         RocketsFired = 0;
         Reloading = false;
     }

else
        {
         currentclip += RocketsFired;
         totalammo-= RocketsFired;
         RocketsFired = 0;
         Reloading = false;
        }
    
       
        }


    }//ef  


}
