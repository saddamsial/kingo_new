using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTile : MonoBehaviour
{
    
   public Transform tf;
public float Size;
public float SizeSet;
public Vector3 scale;

    void Update()
    {
     Size = Size *SizeSet;
        scale =  new Vector3(Size,0,0);

      tf.localScale = scale;

       


    }







}
