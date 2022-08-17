using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{

    private Guid playerId;
    public Guid PlayerId
    {
        get
        {
            return playerId;
        }
        set
        {
            playerId = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerId = Guid.Empty;
        Debug.Log(playerId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
