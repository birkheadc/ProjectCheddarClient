using System;
using UnityEngine;

public class PacketBuilder
{
    public Packet BuildWelcomeReceivedPacket(Guid clientId)
    {
        Packet packet = new((int)ClientPackets.WelcomeReceived);
        packet.Write(clientId.ToString());
        return packet;
    }

    public Packet BuildUpdatePlayerPositionPacket()
    {
        // Todo: This method should be supplied the players current and last position, for now we just supply dummy values.
        Vector2Int last = new(0, -1);
        Vector2Int current = new(0, 0);

        Packet packet = new((int)ClientPackets.UpdatePlayerPosition);
        packet.Write(last);
        packet.Write(current);
        return packet;
    }
}