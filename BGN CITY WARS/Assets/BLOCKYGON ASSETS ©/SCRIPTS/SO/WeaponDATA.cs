using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New BGN Weapon", menuName = "New BGN Weapon/BGN RAYCAST")]
public class WeaponDATA : ScriptableObject
{
    public string WeaponName;
    public int Weapontype;

   public float FireRate;
   public float WeaponRange;
    public float BodyDamage;
    public float HeadDamage;
    public float ReloadTime;
    public int CurrentClip;
    public int MaxClip;
    public int Ammo;
    public int MaxAmmo;
    public bool MaxedAmmo;
    public bool Scope;
    public bool IsMelee;
    public bool isShotgun;
    public bool Pump;
    public int ShellPerReload;
    public float DefaultBulletSpread;
     public float AimBulletSpread;

      public int Pellets;
    
    public Material TrailVFX;

    public AudioClip FireSFX;
    public AudioClip BodyshotSFX;
    public AudioClip HeadshotSFX;
    public AudioClip pumpSFX;
    public AudioClip ReloadSFX;

    // public ParticleSystem BulletTrail;

    //  public GameObject SparkleVFX;
    public GameObject BulletHoleVFX;

    public Sprite UIIcon;





}

