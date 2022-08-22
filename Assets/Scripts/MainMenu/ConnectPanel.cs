using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net;

public class ConnectPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField ipInput;
    
    public void Connect()
    {
        string s = ipInput.text;
        try
        {
            IPAddress ip = IPAddress.Parse(s);
            Debug.Log("Connect to: " + ip.ToString());
            GameManager.Instance.ConnectToServer(ip);
        }
        catch
        {
            Debug.Log("Address not valid!");
        }
    }
}
