using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> players;

    private void Start()
    {
        // Inicializa a lista de jogadores buscando todos os objetos com a tag "Player"
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
    }

    public void HandlePlayerElimination(GameObject eliminatedPlayer)
    {
        // Desativa o jogador eliminado
        eliminatedPlayer.SetActive(false);

        // Remove da lista de jogadores ativos
        players.Remove(eliminatedPlayer);

        // Verifica quantos jogadores ainda estão ativos
        int activePlayers = players.Count(p => p.activeSelf);

        if (activePlayers == 1)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! O último jogador venceu.");
    }
}
