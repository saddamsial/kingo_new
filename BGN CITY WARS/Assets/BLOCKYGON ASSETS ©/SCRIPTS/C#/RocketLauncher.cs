using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public WeaponDATA Weapontype;
    public GameObject rocketPrefab;
    public Transform launchPoint;
    public float launchForce = 500f;
    public float fireRate = 1f;
    public Transform crosshair;
    public Transform Player;
    private float nextFireTime = 0f;
    public bool canfire;
    public int weapontype;


void Start() 
{
    Player= transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent;
   
}







    void Update()
    {
         canfire = Player.GetComponent<PlayerActionsVar>().canfire;
        if (Input.GetButton("Fire1") &&canfire && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;
            FireRocket();
        }
    }

    void FireRocket()
    {
        GameObject rocketInstance = Instantiate(rocketPrefab, launchPoint.position, launchPoint.rotation);
        Rigidbody rocketRigidbody = rocketInstance.GetComponent<Rigidbody>();
        Vector3 launchDirection = (crosshair.position - launchPoint.position).normalized;
        rocketRigidbody.AddForce(launchDirection * launchForce);
    }
}
