using UnityEngine;
using Photon.Pun;
using TMPro;

public class UIOverHeadManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI UINameDisplay;
    void Start()
    {
        UINameDisplay.text = PhotonNetwork.NickName;
    }
    public void UpdateUINameDisplay()
    {
        UINameDisplay.text = PhotonNetwork.NickName;
    }


}
