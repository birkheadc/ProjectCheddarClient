using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Client : MonoBehaviour
{
    public static int DataBufferSize = 4096;
    private IPAddress ip;
    public IPAddress Ip { get { return ip; } set { ip = value; Debug.Log(ip); } }
    private int port;
    public int Port { get { return port; } set { port = value; } }
    private Guid id = Guid.Empty;
    public Guid Id { get { return id; } set { id = value; } }
    private Tcp tcp;
    //public Tcp Tcp { get { return tcp; } set { tcp = value; } }
    [SerializeField] private ClientPacketHandler clientHandle;
    [SerializeField] private ThreadManager threadManager;
    public ClientPacketHandler ClientHandle
    {
        get
        {
            return clientHandle;
        }
    }

    private void Start()
    {
        tcp = new();
        tcp.Client = this;
    }

    public void ConnectToServer()
    {
        tcp.Connect();
    }

    public void SendData(Packet packet)
    {
        tcp.SendData(packet);
    }

    public void ExecuteOnMainThread(Action action)
    {
        threadManager.ExecuteOnMainThread(action);
    }
}
