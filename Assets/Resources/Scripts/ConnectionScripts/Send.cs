﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Send : MonoBehaviour                                       //logic for sending packets from client to server
{
    public static void Welcome_Received()                               //creates an client's first packet, after receiving welcome packet from server (like handshake)
    {

        /*Vector3 forward = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().forward;
        Vector3 right = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().right;*/

        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(Client.client.local_client_id);
            packet.Write(UIManager1.manager.username.text);
            packet.Write(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().forward);
            packet.Write(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().right);
            SendTcpData(packet);                                        //send the created packet to the server
        }
    }

    private static void SendTcpData(Packet packet)
    {
        packet.WriteLength();
        Client.client.tcp.SendData(packet);                           //send given packet from client to server (using tcp)
    }

    private static void SendUdpData(Packet packet)
    {
        packet.WriteLength();
        Client.client.udp.SendData(packet);                         //send given packet from client to server (using udp)
    }

    public static void PlayerMovement(float[] inputs)                //send player's inputs (about movement) to the server 
    {
        using (Packet packet = new Packet((int)ClientPackets.player_movement))
        {
            packet.Write(inputs.Length);
            foreach (float input in inputs)
            {
                packet.Write(input);
            }
            packet.Write(GameManager.players[Client.client.local_client_id].transform.rotation);

            SendUdpData(packet);                                    //movement packet will be sent over and over again (can't afford losing some of them)
        }
    }


    public static void Shoot(Vector3 facing_direction)              //specified direction that local player is shooting
    {
        using (Packet packet = new Packet((int) ClientPackets.shoot)) 
        {
            packet.Write(facing_direction);
            SendTcpData(packet);                                    //send packet to server via tcp
        }
    }

    public static void StartGame()
    {
        using (Packet packet = new Packet((int)ClientPackets.startgame))
        {
            SendTcpData(packet);                                    //send packet to server via tcp
        }
    }
}
