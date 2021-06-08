using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundScript1 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    //Play Global
    private static BGSoundScript1 instance = null;
    public static BGSoundScript1 Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    //Play Gobal End

    // Update is called once per frame
    void Update()
    {

    }
}