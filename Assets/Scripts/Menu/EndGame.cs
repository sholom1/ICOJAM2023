using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    TMP_Text winnerText;
    bool restarting = false;
    public void SetWinners(List<PlayerController_1> players)
    {
        string output = "Player " + players[0].playerID.ToString();
        for (int i = 1; i < players.Count; i++)
        {
            output += " & " + "Player " + players[i].playerID.ToString();
        }
        winnerText.text = output + " Won!";
    }

    public void RestartGame()
    {
        if (!restarting)
        {
            StartCoroutine(RestartEverything());
        }
    }
    IEnumerator RestartEverything()
    {
        restarting = true;
        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<PlayerManager>().RestartGame();
        FindAnyObjectByType<PlayerDestroyer>().RestartGame();
        FindAnyObjectByType<ScoreKeeper>().RestartGame();
        FindAnyObjectByType<LiftMenuUp>().RestartGame();
        gameObject.SetActive(false);
        restarting = false;
    }
}
