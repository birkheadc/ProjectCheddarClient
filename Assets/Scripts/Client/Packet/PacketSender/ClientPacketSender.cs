using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPacketSender
{
    private Client client;
    private Tcp tcp;
    private PacketBuilder packetBuilder;

    public ClientPacketSender(Client client, Tcp tcp)
    {
        this.client = client;
        this.tcp = tcp;
        packetBuilder = new();
    }
    private void SendTcpData(Packet packet)
    {
        packet.WriteLength();
        tcp.SendData(packet);
    }

    public void SendWelcomeReceived()
    {
        using (Packet packet = packetBuilder.BuildWelcomeReceivedPacket(client.Id)) SendTcpData(packet);
    }

    public void SendUpdatePlayerPosition()
    {
        using (Packet packet = packetBuilder.BuildUpdatePlayerPositionPacket()) SendTcpData(packet);
    }
}
