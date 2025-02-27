using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro; // Para usar o TextMeshPro

public class GameManager : MonoBehaviour
{
    private List<GameObject> players;
    [SerializeField] private float time = 60;

    // Referências para a UI
    [SerializeField] private TMP_Text timerText; // Para mostrar o tempo restante
    [SerializeField] private List<TMP_Text> playerLivesTexts; // Para mostrar as vidas de cada jogador

    private void Start()
    {
        // Inicializa a lista de jogadores buscando todos os objetos com a tag "Player"
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        StartCoroutine(TimerCoroutine());
        UpdatePlayerLivesUI();
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
            HandleGameOver();
        }

        UpdatePlayerLivesUI(); // Atualiza a UI após a eliminação
    }

    private IEnumerator TimerCoroutine()
    {
        while (time > 0)
        {
            timerText.text = "Time: " + Mathf.Round(time);
            yield return null;
            time -= Time.deltaTime;
        }

        timerText.text = "Time: 0";
        HandleTimeout();
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over! O último jogador venceu.");
    }

    private void HandleTimeout()
    {
        Debug.Log("Tempo esgotado!");
        // Implementar lógica para finalizar o jogo
    }

    public void HandlePlayerDamage(GameObject player, float damage)
    {
        // Implementar lógica se necessário
    }

    public void HandlePlayerDeath(GameObject player, int lives)
    {
        if (lives <= 0) { HandlePlayerElimination(player); }
        UpdatePlayerLivesUI(); // Atualiza a UI ao lidar com a morte
    }

    private void UpdatePlayerLivesUI()
    {
        for (int i = 0; i < playerLivesTexts.Count; i++)
    {
        if (i < players.Count && players[i].activeSelf) 
        {
            PlayerHealth playerHealth = players[i].GetComponent<PlayerHealth>();
            playerLivesTexts[i].text = "Vidas: " + playerHealth.Lives;
            playerLivesTexts[i].gameObject.SetActive(true);
        }
        else
        {
            playerLivesTexts[i].gameObject.SetActive(false);
        }
    }
    }
}
