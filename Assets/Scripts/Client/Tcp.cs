using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Tcp
{
    private Client client;
    private TcpClient socket;
    private NetworkStream stream;
    private Packet receiveData;
    private byte[] receiveBuffer;
    private delegate void PacketHandleDelegate(Packet packet);
    private static Dictionary<int, PacketHandleDelegate> packetHandleDelegates;
    // private delegate void PacketSendDelegate();
    // private static Dictionary<int, PacketSendDelegate> packetSendDelegates;
    private static Dictionary<ClientPacket, Action<PacketData>> packetSendActions;
    private readonly ClientPacketHandler packetHandler;
    private readonly ClientPacketSender packetSender;

    public Tcp(Client client)
    {
        this.client = client;
        packetHandler = new(client, this);
        packetSender = new(client, this);
        InitializeClientData();
    }

    public void Connect()
    {
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
                    packetHandleDelegates[packetId](packet);
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
        packetHandleDelegates = new()
        {
            {
                (int)ServerPacket.Welcome, packetHandler.HandleWelcome
            }
        };

        packetSendActions = new()
        {
            {
                ClientPacket.WelcomeReceived, o => packetSender.SendWelcomeReceived()
            },
            {
                ClientPacket.UpdatePlayerPosition, o => packetSender.SendUpdatePlayerPosition(o)
            },
            {
                ClientPacket.PlayerSpawn, o => packetSender.SendPlayerSpawn(o)
            }
        };
        Debug.Log("Client Data initialized.");
    }

    public void SendPacket(ClientPacket packetType, PacketData packetData)
    {
        packetSendActions[packetType].Invoke(packetData);
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