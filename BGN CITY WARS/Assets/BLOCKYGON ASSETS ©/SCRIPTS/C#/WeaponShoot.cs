using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
public class WeaponShoot : MonoBehaviour
{
    // Start is called before the first frame update


    public bool Fire;
    public Collider collided;

    [SerializeField]
    private LayerMask layermask;
    public Vector3  point;


    public Vector3 pos;
    public Vector3 Dir;

    private RaycastHit hit;

    void Start()
    {
        collided = hit.collider;
}

    // Update is called once per frame
    void Update()

    {


        pos = Camera.main.transform.position;
        Dir = Camera.main.transform.forward;



        if (Fire== true)
        {


            Shoot();
           
        }



    }


    void Shoot()

    {
        



        Physics.Raycast(pos, Dir, out hit, Mathf.Infinity, layermask);
        

        


            collided = hit.collider;

            point = (hit.point);
            Fire = false;

        


            


        



    }



























}
