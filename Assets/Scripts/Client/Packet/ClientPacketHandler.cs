using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPacketHandler
{
    private Client client;
    private Tcp tcp;

    public ClientPacketHandler(Client client, Tcp tcp)
    {
        this.client = client;
        this.tcp = tcp;
    }
    public void HandleWelcome(Packet packet)
    {
        string msg = packet.ReadString();
        Guid clientId = Guid.Parse(packet.ReadString());

        Debug.Log("Server: " + msg);
        client.Id = clientId;

        tcp.SendPacket((int)ClientPackets.WelcomeReceived);
    }
}
