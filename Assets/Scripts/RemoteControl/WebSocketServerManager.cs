using Fleck;
using UnityEngine;
using System.Collections.Generic;
using PimDeWitte.UnityMainThreadDispatcher;
using System;
using Brawlley;
using UnityEngine.InputSystem;

public class WebSocketServerManager : MonoBehaviour
{
    private WebSocketServer _server;
    private List<IWebSocketConnection> _clients = new List<IWebSocketConnection>();
    public Dictionary<string, PlayerController> connectedPlayers = new Dictionary<string, PlayerController>();

    // Dicionário para armazenar inputs ativos por jogador
    private Dictionary<string, Vector2> activeInputs = new Dictionary<string, Vector2>();

    void Start()
    {
        Debug.Log("Started WebSocket on ws://0.0.0.0:8080");
        _server = new WebSocketServer("ws://0.0.0.0:8080");
        _server.Start(socket =>
        {
            socket.OnOpen = () => {
                Debug.Log($"Client connected: {socket.ConnectionInfo.Id}");
                _clients.Add(socket);
                activeInputs[socket.ConnectionInfo.Id.ToString()] = Vector2.zero;
            };

            socket.OnClose = () => {
                Debug.Log($"Client disconnected: {socket.ConnectionInfo.Id}");
                _clients.Remove(socket);
                connectedPlayers.Remove(socket.ConnectionInfo.Id.ToString());
                activeInputs.Remove(socket.ConnectionInfo.Id.ToString());
            };

            socket.OnMessage = message => {
                UnityMainThreadDispatcher.Instance().Enqueue(() => HandleMessage(socket.ConnectionInfo.Id.ToString(), message));
            };
        });
    }

    void HandleMessage(string clientId, string message)
    {
        if (!connectedPlayers.ContainsKey(clientId))
        {
            // Associa o cliente a um jogador na cena
            PlayerController[] players = FindObjectsOfType<PlayerController>();
            if (players.Length > connectedPlayers.Count)
            {
                connectedPlayers[clientId] = players[connectedPlayers.Count];
                Debug.Log($"Assigned {clientId} to {players[connectedPlayers.Count - 1].gameObject.name}");
            }
            else
            {
                Debug.LogWarning("No available player slots for new connection.");
                return;
            }
        }

        PlayerController player = connectedPlayers[clientId];
        Vector2 direction = activeInputs[clientId];

        // Atualiza direção baseado nos comandos
        switch (message)
        {
            case "MoveUp":
                direction.y = 1;
                break;
            case "MoveDown":
                direction.y = -1;
                break;
            case "MoveLeft":
                direction.x = -1;
                break;
            case "MoveRight":
                direction.x = 1;
                break;
            case "StopMoveUp":
                if (direction.y == 1) direction.y = 0;
                break;
            case "StopMoveDown":
                if (direction.y == -1) direction.y = 0;
                break;
            case "StopMoveLeft":
                if (direction.x == -1) direction.x = 0;
                break;
            case "StopMoveRight":
                if (direction.x == 1) direction.x = 0;
                break;
            case "Jump":
                player.GetComponent<PlayerJump>().OnJump(new InputAction.CallbackContext());
                break;
            case "Dash":
                player.GetComponent<PlayerDash>().OnDash(new InputAction.CallbackContext());
                break;
            case "Parry":
                player.GetComponent<PlayerParry>().OnParry(new InputAction.CallbackContext());
                break;
            case "SpellStart":
                player.OnAiming(new InputAction.CallbackContext());
                break;
            case "SpellRelease":
                player.OnStopAiming(new InputAction.CallbackContext());
                player.GetComponent<PlayerSpell>().OnAttack(new InputAction.CallbackContext());
                break;
            case "Melee":
                player.GetComponent<PlayerMelee>().OnAttack(new InputAction.CallbackContext());
                break;
        }

        // Atualiza direção do jogador
        activeInputs[clientId] = direction;
        player.SetDirection(direction);
    }

    void OnDestroy()
    {
        _server?.Dispose();
    }
}
