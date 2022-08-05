using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New BGN Weapon", menuName = "New BGN Weapon/BGN-Glock")]
public class WeaponDATA : ScriptableObject
{
    public string WeaponName;

   public float FireRate;
    public float BodyDamage;
    public float HeadDamage;
    public float Clip;
    public float Ammo;
    public float ReloadTime;
    public int MaxClip;
    public int MaxAmmo;
    

    public AudioClip FireSFX;
    public AudioClip BodyshotSFX;
    public AudioClip HeadshotSFX;
    public AudioClip ReloadSFX;

    // public ParticleSystem BulletTrail;

    //  public GameObject SparkleVFX;
    public GameObject BulletHoleVFX;

    public Sprite UIIcon;





}

