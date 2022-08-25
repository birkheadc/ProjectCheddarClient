using UnityEngine;

public abstract class PacketData
{
    protected ClientPacket packetType;
    public ClientPacket PacketType { get { return packetType; } set { packetType = value; } }

    public PacketData(ClientPacket packetType)
    {
        this.packetType = packetType;
        Debug.Log(packetType);
    }
}