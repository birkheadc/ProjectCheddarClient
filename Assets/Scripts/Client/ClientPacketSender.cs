using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientPacketSender : MonoBehaviour
{
    [SerializeField] private Client client;
    private void SendTcpData(Packet packet)
    {
        packet.WriteLength();
        client.SendData(packet);

    }

    public void welcomeReceived()
    {
        using (Packet packet = new((int)ClientPackets.welcomeReceived))
        {
            packet.Write(client.Id.ToString());
            SendTcpData(packet);
        }
    }
}
