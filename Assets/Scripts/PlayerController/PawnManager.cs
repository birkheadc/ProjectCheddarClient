using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    private Dictionary<Guid, Pawn> pawns = new();
    private Pawn currentPawn;
    [SerializeField] private GameObject pawnPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPawnAtLocation(Vector3 location)
    {
        GameObject pawn = Instantiate(pawnPrefab);
        currentPawn = pawn.GetComponent<Pawn>();
    }
}
