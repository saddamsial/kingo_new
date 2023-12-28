using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SETTINGS", menuName = "saiyan pride/WARRIOR", order = 0)]
public class WARRIOR : ScriptableObject
{
    public AnimatorOverrideController CharacterAnimator;
    public Sprite UIframe;
    public string Name;
    public float DefaultSpeed;
    public float FireSpeed;
    public float RegenrateHPspeed;
    public float HP;
    public float Energy;
    public float MaxHP;
    public float MaxEnergy;
    public int NextForm;
    public int PrevForm;
    public int ChargePower;
    public Vector2 WarriorSize;
    public GameObject Aura;
    public GameObject FullPowerAura;
    public GameObject Energy1;
    public GameObject Energy2;
    public GameObject Energy3;
    public GameObject EnergyS;
    // sfx
    [Header("SFX")] 
 public AudioClip [] Entrances;
public AudioClip [] Wins;
public AudioClip [] attacks;
public AudioClip []Energycalls;
public AudioClip []Charge;
public AudioClip []FullPower;
 public AudioClip[] Hurts;













}


