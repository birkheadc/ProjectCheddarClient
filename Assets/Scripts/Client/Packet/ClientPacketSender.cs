using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPacketSender
{
    private Client client;
    private Tcp tcp;

    public ClientPacketSender(Client client, Tcp tcp)
    {
        this.client = client;
        this.tcp = tcp;
    }
    private void SendTcpData(Packet packet)
    {
        packet.WriteLength();
        tcp.SendData(packet);
    }

    public void SendWelcomeReceived()
    {
        using (Packet packet = new((int)ClientPackets.WelcomeReceived))
        {
            packet.Write(client.Id.ToString());
            SendTcpData(packet);
        }
    }

    public void SendUpdatePlayerPosition()
    {
        
    }
}
