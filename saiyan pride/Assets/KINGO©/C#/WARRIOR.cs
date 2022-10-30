using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SETTINGS", menuName = "saiyan pride/WARRIOR", order = 0)]
public class WARRIOR : ScriptableObject
 {
public Sprite UIframe;
public string Name;
public float DefaultSpeed;
public float FireSpeed;
public float HP;
public float Energy;
public int NextForm;
public int PrevForm;
public Vector2 WarriorSize;
public GameObject Aura;
public GameObject FullPowerAura;
public GameObject Energy1;
public GameObject Energy2;
public GameObject Energy3;
public GameObject EnergyS;
public bool Regenerate;
// sfx
public AudioClip [] Entrances;
public AudioClip [] Wins;
public AudioClip [] attacks;
public AudioClip []Energycalls;
public AudioClip []Charge;
public AudioClip []FullPower;













 }


