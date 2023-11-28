using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class SpawnChatItem : MonoBehaviour
{
    [SerializeField]
   private  Transform ParentItem;
    [SerializeField]
    private Transform Inputfield;

    private PhotonView PV;
    private GameObject MessageItem;

    private void Start()
    {
        PV = this.GetComponent<PhotonView>();
    }
    public void SpawnChat()
    {
       MessageItem =  PhotonNetwork.Instantiate("MESSAGE ITEM_", transform.position, transform.rotation);
      MessageItem.transform.parent = ParentItem;
        #region SetTXTmessage
        {
            PV.RPC("SetTXTmessage", RpcTarget.AllBuffered);
       if(PV.IsMine)
            {
                Inputfield.GetComponent<TMP_InputField>().text = ""; // rerset field after send
            }

        }
        #endregion
    }
    [PunRPC]
    public void SetTXTmessage()
    {
        MessageItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName + " : " + Inputfield.GetComponent<TMP_InputField>().text;  // set chat item logic
    }
}
