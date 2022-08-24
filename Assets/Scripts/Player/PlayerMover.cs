using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private float speed;

    private Vector2 panVector = new();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (panVector.Equals(Vector2.zero) == false)
        {
            Pan();
        }
    }

    public void UpdatePan(Vector2 v2)
    {
        panVector = v2;
    }

    private void Pan()
    {
        this.gameObject.transform.Translate(new Vector3(panVector.x, 0, panVector.y) * Time.deltaTime * speed);
    }
}
