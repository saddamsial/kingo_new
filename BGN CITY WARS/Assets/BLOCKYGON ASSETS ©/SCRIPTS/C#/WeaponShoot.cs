using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    // Start is called before the first frame update


    public bool Fire;
    public Collider collided;

    [SerializeField]
    private LayerMask layermask;
    public Vector3  point;





    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
     

        if (Fire== true)
        {
        



        }



    }


    void Shoot()

    {
        Vector3 pos = Camera.main.transform.position;
        Vector3 Dir = Camera.main.transform.forward;


           RaycastHit hit;

        

        

      if(  Physics.Raycast(pos, Dir, out hit,Mathf.Infinity,layermask))
        {
            collided = hit.collider;

            point = (hit.point);
            Fire = false;

            


        }



    }



























}
