using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timerloader : MonoBehaviour {

    public string leveltoLoad;
    public float timer = 10f;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SceneManager.LoadScene(leveltoLoad);
        }
    }
}