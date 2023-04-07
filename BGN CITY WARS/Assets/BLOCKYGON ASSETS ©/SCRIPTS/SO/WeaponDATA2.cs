using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New BGN Weapon", menuName = "New BGN Weapon/BGN Rocket")]
public class WeaponDATA2 : ScriptableObject
{
    public string WeaponName;
    public int Weapontype;

   public float FireRate;
   public float RocketRange;
    public int MaxDamage;
    public int MidDamage;
    public int MinDamage;
    public int CurrentClip;
    public int Ammo;
    public float ReloadTime;
    public int MaxClip;
    public int MaxAmmo;
    public bool MaxedAmmo;
    public bool Scope;
    public GameObject Rocket;
    

    public AudioClip BodyshotSFX;
    public AudioClip ReloadSFX;

    public Sprite UIIcon;





}

