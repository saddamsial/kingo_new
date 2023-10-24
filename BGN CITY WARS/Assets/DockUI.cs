using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockUI : MonoBehaviour

{
 public void Close()
     {
        transform.localScale =new Vector3 (0, 0, 0);
    }

    public void Open()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
