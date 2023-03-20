using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New BGN Weapon", menuName = "New BGN Weapon/BGN-Glock")]
public class WeaponDATA : ScriptableObject
{
    public string WeaponName;
    public int Weapontype;

   public float FireRate;
   public float WeaponRange;
    public float BodyDamage;
    public float HeadDamage;
    public int CurrentClip;
    public int Ammo;
    public float ReloadTime;
    public int MaxClip;
    public int MaxAmmo;
    public bool MaxedAmmo;
    public bool Scope;
    public bool IsMelee;
    
    public Material TrailVFX;

    public AudioClip FireSFX;
    public AudioClip BodyshotSFX;
    public AudioClip HeadshotSFX;
    public AudioClip ReloadSFX;

    // public ParticleSystem BulletTrail;

    //  public GameObject SparkleVFX;
    public GameObject BulletHoleVFX;

    public Sprite UIIcon;





}

