using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class UserDataTest : MonoBehaviour
{
    public UserStatus UserStatus;
    public string PlayerUserName;
    
  
public  void SetPlayerUserName(string field)
 {
    PlayerUserName =field;
  UserStatus.UserName=PlayerUserName;
 }
 
}
