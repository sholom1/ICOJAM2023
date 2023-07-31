using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    //Using Componenets
    int scoreToWin = 5;
    PlayerManager playerManager;
    LiftMenuUp liftMenuUp;
    [SerializeField]
    GameObject countDownAnimation;
    ScoreKeeper scoreKeeper;
    public PlayerController_1 leadPlayer = null;


    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.OnPlayerDestroy.AddListener(PlayerDestroyed);

        liftMenuUp = FindObjectOfType<LiftMenuUp>();
        liftMenuUp.OnCompleteLift.AddListener(StartGame);

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.SetScoreToWin(scoreToWin);
    }

    public void StartGame()
    {
        StartRound();
    }

    public void StartRound()
    {
        playerManager.ResetPlayerPositions();
        playerManager.ResetPlayers();
        playerManager.ChangePlayerInput("UI");

        GameObject canvas = FindObjectOfType<Canvas>().gameObject;
        GameObject countDown = Instantiate(countDownAnimation, canvas.transform);
        CountDownAnimation currentCountDownAnimation = countDown.GetComponent<CountDownAnimation>();
        currentCountDownAnimation.OnAnimationComplete.AddListener(CompleteCountDown);

        StopLight stopLight = FindObjectOfType<StopLight>();

        print("Start round");

        stopLight.SetRedLight();
        currentCountDownAnimation.OnAnimationComplete.AddListener(() =>
        {
            Timer.instance.RestartTimer();
            stopLight.SetGreenLight();
            stopLight.enabled = true;
        });
        

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
        if (alivePlayers == 1)
        {
            EndRound();
        }
    }

    public void EndRound()
    {
        //Award coins
        foreach (var player in playerManager.players)
        {
            scoreKeeper.AddCoins(player.Key, (uint)(player.Value.check_point_num + player.Value.laps_completed * 5));
        }
        //Searching for the leading player
        foreach (KeyValuePair<uint, PlayerController_1> player in playerManager.players)
        {
            if (leadPlayer == null)
            {
                leadPlayer = player.Value;
            }
            else
            {
                if (leadPlayer.laps_completed == player.Value.laps_completed)
                {
                    if (leadPlayer.check_point_num < player.Value.check_point_num)
                    {
                        leadPlayer = player.Value;
                    }
                }
                else if(leadPlayer.laps_completed < player.Value.laps_completed)
                {
                    leadPlayer = player.Value;
                }
            }
        }

        //Checking for lead ties
        List<PlayerController_1> leadPlayers = new List<PlayerController_1>();
        foreach (KeyValuePair<uint, PlayerController_1> player in playerManager.players)
        {
            if (leadPlayer.laps_completed == player.Value.laps_completed &&
                leadPlayer.check_point_num == player.Value.check_point_num)
            {
                leadPlayers.Add(player.Value);
            }
        }

        foreach(var player in leadPlayers)
        {
            scoreKeeper.AddScore(player.playerID, 1);
        }
        scoreKeeper.UpdateScoreBoard();
        //Adding points and checking end of round
        uint highestScore = scoreKeeper.GetScore(scoreKeeper.GetHighestScoreID()[0]);
        if (highestScore >= scoreToWin)
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
