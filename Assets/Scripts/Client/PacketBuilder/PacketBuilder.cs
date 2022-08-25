using System;
using UnityEngine;

public class PacketBuilder
{
    // Todo: This code is so copy-pasty, is there a better way to do this?
    public Packet BuildWelcomeReceivedPacket(Guid clientId)
    {
        Packet packet = new((int)ClientPacket.WelcomeReceived);
        packet.Write(clientId.ToString());
        return packet;
    }

    public Packet BuildUpdatePlayerPositionPacket(Vector2Int lastChunk, Vector2Int currentChunk)
    {
        Packet packet = new((int)ClientPacket.UpdatePlayerPosition);
        packet.Write(lastChunk);
        packet.Write(currentChunk);
        return packet;
    }

    public Packet BuildPlayerSpawnPacket(Vector2Int currentChunk)
    {
        Packet packet = new((int)ClientPacket.PlayerSpawn);
        packet.Write(currentChunk);
        return packet;
    }
}