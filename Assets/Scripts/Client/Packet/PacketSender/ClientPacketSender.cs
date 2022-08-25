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

    public void SendPacket(PacketData packetData)
    {

    }

    // Todo: This code is so copy-pasty, is there a better way to do this?
    public void SendWelcomeReceived()
    {
        using (Packet packet = packetBuilder.BuildWelcomeReceivedPacket(client.Id)) SendTcpData(packet);
    }

    public void SendUpdatePlayerPosition(PacketData packetData)
    {
        if (packetData.PacketType != ClientPacket.UpdatePlayerPosition)
        {
            Debug.Log("Received wrong packet data. Aborting request.");
            return;
        }
        UpdatePlayerPositionPacketData data = (UpdatePlayerPositionPacketData)packetData;
        using (Packet packet = packetBuilder.BuildUpdatePlayerPositionPacket(data.LastChunk, data.CurrentChunk)) SendTcpData(packet);
    }

    public void SendPlayerSpawn(PacketData packetData)
    {
        if (packetData.PacketType != ClientPacket.PlayerSpawn)
        {
            Debug.Log("Received wrong packet data. Aborting request.");
            return;
        }
        PlayerSpawnPacketData data = (PlayerSpawnPacketData)packetData;
        using (Packet packet = packetBuilder.BuildPlayerSpawnPacket(data.CurrentChunk)) SendTcpData(packet);
    }
}
