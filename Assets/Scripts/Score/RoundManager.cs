using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    //Using Componenets
    int scoreToWin = 5;
    int lapsToEndRound = 5;
    bool endingRound = false;
    PlayerManager playerManager;
    LiftMenuUp liftMenuUp;
    StartLine startLine;
    [SerializeField]
    GameObject countDownAnimation;
    ScoreKeeper scoreKeeper;
    [SerializeField]
    GameObject endMenu;

    [SerializeField]
    float endRoundDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.OnPlayerDestroy.AddListener(PlayerDestroyed);

        liftMenuUp = FindObjectOfType<LiftMenuUp>();
        liftMenuUp.OnCompleteLift.AddListener(StartGame);

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        scoreKeeper.SetScoreToWin(scoreToWin);

        startLine = FindObjectOfType<StartLine>();
        startLine.OnPlayerCrossLine.AddListener(PlayerCrossLine);
    }

    public void StartGame()
    {
        StartRound();
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
    public void PlayerCrossLine(PlayerController_1 player)
    {
        Debug.Log("player crossed line");
        if(player.laps_completed >= lapsToEndRound)
        {
            Debug.Log("ending round");
            StartRoundEnd();
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
        if (alivePlayers <= 1)
        {
            StartRoundEnd();
        }
    }

    public void StartRoundEnd()
    {
        if (!endingRound)
        {
            //Disable player controls
            playerManager.ChangePlayerInput("UI");

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

            //Adding and updating score
            foreach(var player in leadPlayers)
            {
                scoreKeeper.AddScore(player.playerID, 1);
            }
            scoreKeeper.UpdateScoreBoard();

            StartCoroutine(EndRound());
        }
    }

    IEnumerator EndRound()
    {
        endingRound = true;
        yield return new WaitForSeconds(endRoundDelay);
        endingRound = false;
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
        playerManager.ChangePlayerInput("UI");
        endMenu.SetActive(true);

        List<PlayerController_1> winners = new List<PlayerController_1>();
        List<uint> winnerIDs = scoreKeeper.GetHighestScoreID();
        foreach(var id in winnerIDs)
        {
            winners.Add(playerManager.players[id]);
        }

        endMenu.GetComponent<EndGame>().SetWinners(winners);
    }
}
