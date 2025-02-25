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
    private void HandlePlayerElimination(GameObject eliminatedPlayer)
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

    // Lógica para atualizar a GUI quando o player tomar dano
    // Passando o valor do dano para poder usar ele como parâmetro para ajustar a cor
    public void HandlePlayerDamage(float damage)
    {

    }

    // Lógica para atualizar a GUI quando o player morrer 
    public void HandlePlayerDeath(GameObject player, int lives)
    {
        if (lives <= 0) { HandlePlayerElimination(player); }
    }

    
}
