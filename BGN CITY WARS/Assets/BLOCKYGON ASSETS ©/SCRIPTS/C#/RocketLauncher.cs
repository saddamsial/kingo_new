using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public WeaponDATA2 RocketWeapon;
    public GameObject rocketPrefab;
    public Transform launchPoint;
    public float launchForce = 500f;
    float fireRate;
    public Transform crosshair;
    public Transform Player;
    private float nextFireTime = 0f;
    public bool canfire;
    public int weapontype;
    public bool Fired;




void OnEnable() 
{
 Player.GetComponent<PlayerActionsVar>().Weapontype= weapontype;
}


void Start() 
{
    Player= transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
    fireRate= RocketWeapon.FireRate;
    weapontype=RocketWeapon.Weapontype;
   
}


    void Update()

    {  
        
          if (Time.time > nextFireTime + 0.2f)
        {     
           
            Fired = false;     
        }
          //sync vars    
         canfire = Player.GetComponent<PlayerActionsVar>().canfire;
         Player.GetComponent<PlayerActionsVar>().Fired = Fired;


        if (Input.GetButton("Fire1") &&canfire && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            FireRocket();
        }



        
    }
    void FireRocket()
    {
        Fired = true;
        GameObject rocketInstance = Instantiate(rocketPrefab, launchPoint.position, launchPoint.rotation);
        Rigidbody rocketRigidbody = rocketInstance.GetComponent<Rigidbody>();
        Vector3 launchDirection = (crosshair.position - launchPoint.position).normalized;
        rocketRigidbody.AddForce(launchDirection * launchForce);
    }
}
