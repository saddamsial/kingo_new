using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon;

public class Shotgun : MonoBehaviour
{
    public WeaponDATA WeaponType;
    private int pelletCount;
    private float spreadAngle;
    private float range = 100f;
    public LayerMask layerMask;
    public Transform weaponShoot;
    public Transform cameraTransform;
    private int ammoCount;
    private int maxAmmo = 20;
    private float fireRate = 0.5f;
    private float reloadTime = 1.5f;
    private AudioSource audioSource;
    private AudioClip shootSound;
    private AudioClip reloadSound;
     public bool Fired;
    public bool Canfire;
    public bool isReloading = false;
    private float nextFireTime = 0f;
    public Collider collided;
      //pun variables
    private PhotonView PV;
    public PhotonView TPV;
    public bool bodyshotHit;
    


private void OnEnable() 
{
    spreadAngle=WeaponType.BulletSpread;
     pelletCount=WeaponType.Pellets;
    reloadTime=WeaponType.ReloadTime;
    fireRate=WeaponType.FireRate;
    maxAmmo=WeaponType.MaxAmmo;
    spreadAngle=WeaponType.BulletSpread;
    pelletCount=WeaponType.Pellets;
    ammoCount=WeaponType.CurrentClip;
    range=WeaponType.WeaponRange;
}
 private void Start() 
{
    reloadSound=WeaponType.ReloadSFX;
    shootSound=WeaponType.FireSFX;
    audioSource=this.GetComponent<AudioSource>();




}
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !isReloading)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammoCount < maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        audioSource.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadTime);
        ammoCount = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        if (ammoCount <= 0)
        {
            return;
        }

        audioSource.PlayOneShot(shootSound);

        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 direction = GetSpreadDirection(spreadAngle);
            RaycastHit hit;

            if (Physics.Raycast(weaponShoot.position, direction, out hit, range, layerMask))
            {
                // Do damage to hit object if applicable
                collided=hit.collider;
                TPV = collided.transform.parent.GetComponentInParent<PhotonView>();
                BodyShot();

            }

            Debug.DrawRay(weaponShoot.position, direction * range, Color.red, 1f);
        }

        ammoCount--;
    }

    Vector3 GetSpreadDirection(float spread)
    {
        Vector3 direction = cameraTransform.forward;
        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);
        direction.z += Random.Range(-spread, spread);
        return direction.normalized;
    }

void BodyShot()


    { // SF


        if (collided != null & collided.name == "HIT BOX-BODY")


        {

            if (TPV != null)
              //self shoot detect
            if (TPV.IsMine)
            return;
            else // other online player detect
            {
                audioSource.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                PV.RPC("Bodydamage", RpcTarget.Others);

                //  TPV = collided.GetComponent<PhotonView>();

                Debug.Log("Real Player Detected-Body");

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());
            }



            else if (collided.name == "HIT BOX-BODY" & TPV == null)
            {
                      ///AI detct
            if(collided.CompareTag("AI"))
             
            {
            TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();

                audioSource.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                Debug.Log("AI Target Detected-Body");

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());
               takedamage.Takedamage(WeaponType.BodyDamage);

            }

            else
             {

             audioSource.PlayOneShot(WeaponType.BodyshotSFX, 1f);

                Debug.Log("Iron Target Detected-Body");

              TakeDamage takedamage = collided.transform.parent.GetComponent<TakeDamage>();
                if (takedamage != null)
               {takedamage.Takedamage(WeaponType.BodyDamage);}

                //Hit Reticle Enable
                StartCoroutine(Hitreticle());


            }
              
            }

        }

        else return;

  






  IEnumerator Hitreticle()
    {
      bodyshotHit = true;
        yield return new WaitForSeconds(0.25f);
      bodyshotHit = false;
    }








}
}
