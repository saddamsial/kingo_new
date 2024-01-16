using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NameinputField : MonoBehaviour
{
    public InputField yourInputField;

    [SerializeField]
    private string UserName;

    [SerializeField]
    private UIOverHeadManager UIOverhead;

    private void Start()
    {
        // Add a listener to the input field's value changed event
        yourInputField.onValueChanged.AddListener(OnInputFieldValueChanged);
    }

    private void OnInputFieldValueChanged(string value)
    {
         PlayerPrefs.SetString("UserStats_PlayerName", value);
        UserName = PlayerPrefs.GetString("UserStats_PlayerName");

    }


    private void OnEnable()
    {
    Updatefield();

    }
    private void OnDisable()
    {

   Updatefield();
    }

    void Updatefield()
    {
        UserName = PlayerPrefs.GetString("UserStats_PlayerName");
        PhotonNetwork.NickName = UserName;
        yourInputField.text = UserName;
        UIOverhead.UpdateUINameDisplay();
    }
}
