using Fleck;
using UnityEngine;
using System.Collections.Generic;
using PimDeWitte.UnityMainThreadDispatcher;
using System;
using JetBrains.Annotations;
using Unity.Collections;
using Unity.VisualScripting;

public class WebSocketServerManager : MonoBehaviour
{
    private WebSocketServer _server;
    private List<IWebSocketConnection> _clients = new List<IWebSocketConnection>();
    public int nextId = 1;

    public string client1 = "none";
    public string client2 = "none";

    void Start()
    {
        client1 = "none";
        client2 = "none";
        Debug.Log("Started Websocket");
        // Start server on port 8080
        _server = new WebSocketServer("ws://0.0.0.0:8080");
        _server.Start(socket =>
        {
            socket.OnOpen = () => {
                Debug.Log("Client connected!");
                Debug.Log("Client 1 is: " + client1);
                _clients.Add(socket);
                if (client1 == "none") {
                    client1 = socket.ConnectionInfo.Id.ToString();
                    Debug.Log("Assinged " + client1 + "to client1");
                }
                else if(client2 == "none") {
                    client2 = socket.ConnectionInfo.Id.ToString();
                    Debug.Log("Assinged " + client2 + "to client1");
                    
                }
                else {
                    Debug.Log("Could not assign" + client1 + ":" + client2);
                }

            };

            socket.OnClose = () => {
                Debug.Log("Client disconnected!");
                if (client1 == socket.ConnectionInfo.Id.ToString()) {
                    client1 = "none";
                    Debug.Log("Disconnected client 1");
                }
                else if (client2 == socket.ConnectionInfo.Id.ToString()) {
                    client2 = "none";
                    Debug.Log("Disconnected client 2");
                }
                else {
                    Debug.Log("No client found on disconnection!");
                }
                _clients.Remove(socket);

            };

            socket.OnBinary = bytes => {
                PlayerController player = null;

                if (bytes.Length == 9 && bytes[0] == 99) 
                {
                    // Extract the timestamp sent by the client (8 bytes = long)
                    long clientTimestamp = BitConverter.ToInt64(bytes, 1);
                    // Send back a pong with the same timestamp
                    byte[] pongBytes = new byte[9];
                    pongBytes[0] = 100; // "Pong" marker
                    Buffer.BlockCopy(BitConverter.GetBytes(clientTimestamp), 0, pongBytes, 1, 8);
                    socket.Send(pongBytes);
                    return; // Skip other processing
                }
                UnityMainThreadDispatcher.Instance().Enqueue(() => HandleBinary(bytes, player));
            };
        });
    }

    
    void HandleBinary(byte[] bytes, PlayerController player)
    {

        Debug.Log("Byte length: " + bytes.Length);
        if (bytes.Length < 1) return;
        Debug.Log("Bytes: " + bytes);

        if (bytes.Length >= 2)
        {
            int x = (sbyte)bytes[0];
            int y = (sbyte)bytes[1];

            // Get player input from keyboard or controller
            float horizontalInput = x;
            float verticalInput = y;

            // // Check if diagonal movement is allowed
            // if (player.canMoveDiagonally)
            // {
            //     // Set movement direction based on input
            //     player.movement = new Vector2(horizontalInput, verticalInput);
            //     // Optionally rotate the player based on movement direction
            //     player.RotatePlayer(horizontalInput, verticalInput);
            // }
            // else
            // {
            //     // Determine the priority of movement based on input
            //     if (horizontalInput != 0)
            //     {
            //         player.isMovingHorizontally = true;
            //     }
            //     else if (verticalInput != 0)
            //     {
            //         player.isMovingHorizontally = false;
            //     }

            //     // Set movement direction and optionally rotate the player
            //     if (player.isMovingHorizontally)
            //     {
            //         player.movement = new Vector2(horizontalInput, 0);
            //         player.RotatePlayer(horizontalInput, 0);
            //     }
            //     else
            //     {
            //         player.movement = new Vector2(0, verticalInput);
            //         player.RotatePlayer(0, verticalInput);
            //     }
            // }


        }


    }

    void OnDestroy()
    {
        _server?.Dispose();
    }
}