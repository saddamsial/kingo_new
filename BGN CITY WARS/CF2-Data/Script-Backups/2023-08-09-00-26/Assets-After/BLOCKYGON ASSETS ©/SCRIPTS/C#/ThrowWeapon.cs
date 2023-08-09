using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    public WeaponDATA2 Weapontype;
    public GameObject rocketPrefab;
    public Transform launchPoint;
    public float launchForce = 500f;
    public float fireRate = 1f;
    public Transform crosshair;
    public Transform Player;
    private float nextFireTime = 0f;
    public bool canfire;
    public int weapontype;
    public bool Fired;


void Start() 
{
    Player= transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;

    fireRate= Weapontype.FireRate;
    weapontype=Weapontype.Weapontype;
   
}







    void Update()
    {
         canfire = Player.GetComponent<PlayerActionsVar>().canfire;
        if (ControlFreak2.CF2Input.GetButton("Fire1") &&canfire && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            FireRocket();
        }



          if (Time.time > 0 + 0.2f)
        {     
           
            Fired = false;     
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
