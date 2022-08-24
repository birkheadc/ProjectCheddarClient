using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private Client client;
    public Client Client { get { return client; } }
    private PlayerManager playerManager;
    public PlayerManager PlayerManager { get { return playerManager; } set { playerManager = value; } }

    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Debug.Log("GameManager already exists, destroying self.");
            Destroy(this.gameObject);
        }
    }
    public void ConnectToServer(IPAddress ip)
    {
        client.Ip = ip;
        client.Port = 30099;
        SceneManager.LoadScene(1);
    }

    public void ConnectClient()
    {
        client.ConnectToServer();
    }

    public void GoToSettings()
    {

    }

    public void QuitGame()
    {

    }
}
