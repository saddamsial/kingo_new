using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class AwakeVS : MonoBehaviour
{
    public GameObject gameObjectToAwake;


     void Awake() 
    {
       CustomEvent.Trigger(gameObject,"awake");
    }
}
