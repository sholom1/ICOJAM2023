using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    int scoreToWin;
    Dictionary<uint, uint> score = new Dictionary<uint, uint>();
    Dictionary<uint, uint> coins = new Dictionary<uint, uint>();
    PlayerManager playerManager;

    TMP_Text titleText;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerManager.OnPlayerDestroy.AddListener(PlayerDestroyed);
        ResetPlayerScore();
        ResetPlayerCoins();
    }
    private void OnDestroy()
    {
        playerManager.OnPlayerDestroy.RemoveListener(PlayerDestroyed);
    }
    private void ResetPlayerScore()
    {
        score.Clear();
        foreach (KeyValuePair<uint, PlayerController_1> player in playerManager.players)
        {
            score.Add(player.Value.playerID, 0);
        }
    }
    private void ResetPlayerCoins()
    {
        score.Clear();
        foreach (KeyValuePair<uint, PlayerController_1> player in playerManager.players)
        {
            coins.Add(player.Value.playerID, 0);
        }
    }
    public void StartGame()
    {
        ResetPlayerScore();
        ResetPlayerCoins();
        StartRound();

        titleText.text = "First to " + scoreToWin.ToString();
    }
    public void StartRound()
    {
        playerManager.ResetPlayerPositions();
        playerManager.ResetPlayers();

        StopLight stopLight = FindObjectOfType<StopLight>();
        if(stopLight != null)
        {
            stopLight.StartRedLight();
        }
    }

    public void PlayerDestroyed()
    {
        int alivePlayers = 0;

        foreach (KeyValuePair<uint, PlayerController_1> player in playerManager.players)
        {
            if (player.Value.dead == false)
            {
                alivePlayers++;
            }
        }
        if(alivePlayers <= 1)
        {
            EndRound();
        }
    }

    public void EndRound()
    {
        //Searching for the leading player
        PlayerController_1 leadPlayer = null;
        foreach (KeyValuePair<uint, PlayerController_1> player in playerManager.players)
        {
            if (leadPlayer == null)
            {
                leadPlayer = player.Value;
            }
            else
            {
                if (leadPlayer.laps_completed < player.Value.laps_completed && leadPlayer.check_point_num < player.Value.check_point_num)
                {
                    leadPlayer = player.Value;
                }
            }
        }

        //Adding points and checking end of round
        score[leadPlayer.playerID] += 1;
        if(score[leadPlayer.playerID] >= scoreToWin)
        {
            EndGame();
        }
        else
        {
            StartRound();
        }
    }

    public void EndGame()
    {

    }
}
