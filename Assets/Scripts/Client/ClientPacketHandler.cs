using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPacketHandler : MonoBehaviour
{
    [SerializeField] private Client client;
    [SerializeField] private ClientPacketSender clientSend;
    public void Welcome(Packet packet)
    {
        string msg = packet.ReadString();
        Guid clientId = Guid.Parse(packet.ReadString());

        Debug.Log("Server: " + msg);
        client.Id = clientId;

        clientSend.welcomeReceived();
    }
}
