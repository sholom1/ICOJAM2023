using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    //Using Componenets
    int scoreToWin = 3;
    PlayerManager playerManager;
    LiftMenuUp liftMenuUp;
    [SerializeField]
    GameObject countDownAnimation;
    ScoreKeeper scoreKeeper;


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
        if (alivePlayers <= 1)
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
                if (leadPlayer.laps_completed < player.Value.laps_completed && 
                    leadPlayer.check_point_num < player.Value.check_point_num)
                {
                    leadPlayer = player.Value;
                }
            }
        }
        scoreKeeper.AddScore(leadPlayer.playerID, 1);
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
