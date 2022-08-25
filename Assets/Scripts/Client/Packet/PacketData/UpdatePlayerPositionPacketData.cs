using UnityEngine;

public class UpdatePlayerPositionPacketData : PacketData
{
    private Vector2Int lastChunk;
    public Vector2Int LastChunk { get { return lastChunk; } private set { lastChunk = value; } }
    private Vector2Int currentChunk;
    public Vector2Int CurrentChunk { get { return currentChunk; } private set { currentChunk = value; } }
    public UpdatePlayerPositionPacketData(Vector2Int lastChunk, Vector2Int currentChunk) : base(ClientPacket.UpdatePlayerPosition) 
    {
        this.lastChunk = lastChunk;
        this.currentChunk = currentChunk;
    }
}