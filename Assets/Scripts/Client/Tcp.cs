using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Tcp
{
    private Client client;
    public Client Client
    {
        get
        {
            return client;
        }
        set
        {
            client = value;
        }
    }
    private TcpClient socket;
    private NetworkStream stream;
    private Packet receiveData;
    private byte[] receiveBuffer;
    private delegate void PacketHandler(Packet packet);
    private static Dictionary<int, PacketHandler> packetHandlers;
    private readonly GameManager gameManager;

    public void Connect()
    {
        InitializeClientData();
        socket = new()
        {
            ReceiveBufferSize = Client.DataBufferSize,
            SendBufferSize = Client.DataBufferSize
        };

        receiveBuffer = new byte[Client.DataBufferSize];
        socket.BeginConnect(client.Ip, client.Port, ConnectionCallback, socket);
    }

    private void ConnectionCallback(IAsyncResult result)
    {
        socket.EndConnect(result);
        if (socket.Connected == false)
        {
            return;
        }
        stream = socket.GetStream();

        receiveData = new Packet();

        stream.BeginRead(receiveBuffer, 0, Client.DataBufferSize, ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            int byteLength = stream.EndRead(result);
            if (byteLength <= 0)
            {
                return;
            }

            byte[] data = new byte[byteLength];
            Array.Copy(receiveBuffer, data, byteLength);

            receiveData.Reset(HandleData(data));

            stream.BeginRead(receiveBuffer, 0, Client.DataBufferSize, ReceiveCallback, null);

        }
        catch (Exception e)
        {
            Debug.Log("Error receiving TCP: " + e.ToString());
        }
    }

    private bool HandleData(byte[] data)
    {
        int packetLength = 0;

        receiveData.SetBytes(data);

        if (receiveData.UnreadLength() >= 4)
        {
            packetLength =  receiveData.ReadInt();
            if (packetLength < 1)
            {
                return true;
            }
        }
        while (packetLength > 0  && packetLength <= receiveData.UnreadLength())
        {
            byte[] packetBytes = receiveData.ReadBytes(packetLength);
            client.ExecuteOnMainThread(() =>
            {
                using (Packet packet = new(packetBytes))
                {
                    int packetId = packet.ReadInt();
                    packetHandlers[packetId](packet);
                    // client.ClientHandle.Welcome(packet);
                }
            });

            packetLength = 0;
            if (receiveData.UnreadLength() >= 4)
            {
                packetLength =  receiveData.ReadInt();
                if (packetLength < 1)
                {
                    return true;
                }
            }
        }

        if (packetLength <= 1) return true;
        return false;
    }

    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            {
                (int)ServerPackets.welcome, client.ClientHandle.Welcome
            }
        };
        Debug.Log("Client Data initialized.");
    }

    public void SendData(Packet packet)
    {
        try
        {
            if (socket is null) return;
            stream.BeginWrite(packet.ToArray(), 0, packet.Length, null, null);
        }
        catch
        {
            Debug.Log("Error sending data...");
        }
    }
}