using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    //Game Logic
    int scoreToWin = 3;
    Dictionary<uint, uint> score = new Dictionary<uint, uint>();
    Dictionary<uint, uint> coins = new Dictionary<uint, uint>();

    //Using Componenets
    PlayerManager playerManager;
    LiftMenuUp liftMenuUp;

    //UI
    [SerializeField]
    GameObject scoreCard;
    [SerializeField]
    GameObject scoreCardBoard;
    [SerializeField]
    TMP_Text titleText;

    [SerializeField]
    GameObject countDownAnimation;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.OnPlayerDestroy.AddListener(PlayerDestroyed);
        playerManager.OnPlayerJoin.AddListener(PlayerJoined);

        liftMenuUp = FindObjectOfType<LiftMenuUp>();
        liftMenuUp.OnCompleteLift.AddListener(StartGame);
    }

    private void PlayerJoined()
    {
        //Adding scores
        foreach (var player in playerManager.players.Values)
        {
            //Adding player to score
            if (!score.ContainsKey(player.playerID))
            {
                score.Add(player.playerID, 0);
            }
            if (!coins.ContainsKey(player.playerID))
            {
                coins.Add(player.playerID, 0);
            }
        }
        InitalizeScoreCards();
    }
    private void InitalizeScoreCards()
    {
        //Deleting all children
        for(int i = scoreCardBoard.transform.childCount-1; i >= 0; i--)
        {
            Destroy(scoreCardBoard.transform.GetChild(i).gameObject);
        }

        //Adding scores
        foreach (var player in playerManager.players.Values) {
            GameObject currentScoreDisplay = Instantiate(scoreCard, scoreCardBoard.transform);
            ScoreCardDisplay scoreCardDisplay = currentScoreDisplay.GetComponent<ScoreCardDisplay>();
            scoreCardDisplay.SetPlayer(player);
            scoreCardDisplay.UpdateScore(score[scoreCardDisplay.GetPlayerID()]);
        }
        //UpdateScoreBoard();
    }

    private void UpdateScoreBoard()
    {
        for (int i = 0; i < scoreCardBoard.transform.childCount; i++)
        {
            GameObject currentScoreDisplay = scoreCardBoard.transform.GetChild(i).gameObject;
            ScoreCardDisplay scoreCardDisplay = currentScoreDisplay.GetComponent<ScoreCardDisplay>();
            scoreCardDisplay.UpdateScore(score[scoreCardDisplay.GetPlayerID()]);
        }
    }
    public void StartGame()
    {
        StartRound();
        titleText.text = "First to " + scoreToWin.ToString();
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
        UpdateScoreBoard();

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
