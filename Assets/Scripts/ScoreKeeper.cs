using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    int scoreToWin = 3;
    Dictionary<uint, uint> score = new Dictionary<uint, uint>();
    Dictionary<uint, uint> coins = new Dictionary<uint, uint>();
    PlayerManager playerManager;
    LiftMenuUp liftMenuUp;

    //TMP_Text titleText;

    [SerializeField]
    GameObject countDownAnimation;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.OnPlayerDestroy.AddListener(PlayerDestroyed);

        liftMenuUp = FindObjectOfType<LiftMenuUp>();
        liftMenuUp.OnCompleteLift.AddListener(StartGame);

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
        coins.Clear();
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
        //titleText.text = "First to " + scoreToWin.ToString();
    }

    public void StartRound()
    {
        playerManager.ResetPlayerPositions();
        playerManager.ResetPlayers();

        GameObject canvas = FindObjectOfType<Canvas>().gameObject;
        GameObject countDown = Instantiate(countDownAnimation, canvas.transform);
        CountDownAnimation currentCountDownAnimation = countDown.GetComponent<CountDownAnimation>();
        currentCountDownAnimation.OnAnimationComplete.AddListener(CompleteCountDown);

        StopLight stopLight = FindObjectOfType<StopLight>();
        if (stopLight != null)
        {
            stopLight.SetRedLight();
            stopLight.StopAllCoroutines();
            currentCountDownAnimation.OnAnimationComplete.AddListener(stopLight.SetGreenLight);
        }

    }
    public void CompleteCountDown()
    {
        playerManager.ChangePlayerInput("Player");
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
        Debug.Log("end game");
    }
}
