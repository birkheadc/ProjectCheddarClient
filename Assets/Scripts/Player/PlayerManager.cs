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
    }

    private void CheckPosition()
    {
        if (gameObject.transform.position != position)
        {
            position = gameObject.transform.position;
            Vector2Int chunk = ChunkCalculator.CalculateChunkFromPosition(position);
            if (Vector2Int.Equals(chunk, currentChunk) == false)
            {
                currentChunk = chunk;
                client.SendUpdatedPositionToServer(currentChunk);
            }
        }
    }
}
