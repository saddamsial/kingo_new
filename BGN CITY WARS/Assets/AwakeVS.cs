using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class AwakeVS : MonoBehaviour
{
    public GameObject gameObject;


     void Awake() 
    {
       CustomEvent.Trigger(gameObject,"awake");
    }
}
