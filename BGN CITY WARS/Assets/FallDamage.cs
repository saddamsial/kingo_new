using UnityEngine;


public class FallDamage : MonoBehaviour
{
    private CharacterController CH;
    void Start()
    {
     CH =   gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
