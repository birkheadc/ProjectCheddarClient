using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] PawnManager pawnManager;

    private Client client;

    private Vector3 position;
    private Vector2Int currentChunk;

    private void Start()
    {
        Initialize();
    }
    
    private void Update()
    {
        CheckPosition();
    }

    private void Initialize()
    {
        client = GameManager.Instance.Client;
        GameManager.Instance.PlayerManager = this;
        InitializePosition();
        client.SendPlayerSpawnToServer(currentChunk);
    }

    private void InitializePosition()
    {
        position = gameObject.transform.position;
        currentChunk = ChunkCalculator.CalculateChunkFromPosition(position);
    }

    private void CheckPosition()
    {
        if (gameObject.transform.position != position)
        {
            position = gameObject.transform.position;
            Vector2Int chunk = ChunkCalculator.CalculateChunkFromPosition(position);
            if (Vector2Int.Equals(chunk, currentChunk) == false)
            {
                Vector2Int lastChunk = currentChunk;
                currentChunk = chunk;
                client.SendUpdatedPositionToServer(lastChunk, currentChunk);
            }
        }
    }
}
