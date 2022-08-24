using System;
using UnityEngine;

public class ChunkCalculator
{
    public static Vector2Int CalculateChunkFromPosition(Vector3 position)
    {
        Vector2Int chunk = new()
        {
            x = (int)Math.Floor(position.x / GameConstants.CHUNK_SIZE),
            y = (int)Math.Floor(position.z / GameConstants.CHUNK_SIZE)
        };

        return chunk;
    }
}