using UnityEngine;

public class PlayerSpawnPacketData : PacketData
{
    private Vector2Int currentChunk;
    public Vector2Int CurrentChunk { get { return currentChunk; } private set { currentChunk = value; } }
    public PlayerSpawnPacketData(Vector2Int currentChunk) : base(ClientPacket.PlayerSpawn)
    {
        this.currentChunk = currentChunk;
    }
}