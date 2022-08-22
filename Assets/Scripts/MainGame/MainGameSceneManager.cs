using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameSceneManager : MonoBehaviour
{
    private void Awake()
    {
        LoadWorld();
    }

    private void LoadWorld()
    {
        GameManager.Instance.ConnectClient();
    }
}
