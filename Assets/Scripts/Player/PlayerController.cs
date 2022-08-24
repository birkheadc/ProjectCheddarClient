using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
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

    [SerializeField] private Camera cam;
    [SerializeField] private PlayerMover mover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeDebug(CallbackContext context)
    {
        if (context.performed == true)
        {
            RaycastHit hit;
            if (RaycastAtCursor(out hit))
            {
                // Do something at that location...
            }
        }
    }

    public void InvokePan(CallbackContext context)
    {
        mover.UpdatePan(context.ReadValue<Vector2>());
    }
    

    private bool RaycastAtCursor(out RaycastHit hit)
    {
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit))
        {
            return true;
        }
        return false;
    }
}
