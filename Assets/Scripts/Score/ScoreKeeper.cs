using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    //Game Logic
    Dictionary<uint, uint> score = new Dictionary<uint, uint>();
    Dictionary<uint, uint> coins = new Dictionary<uint, uint>();

    //Using Componenets
    PlayerManager playerManager;

    //UI
    [SerializeField]
    GameObject scoreCard;
    [SerializeField]
    GameObject scoreCardBoard;
    [SerializeField]
    TMP_Text titleText;

    List<ScoreCardDisplay> scoreCardDisplays = new List<ScoreCardDisplay>();

    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.OnPlayerJoin.AddListener(PlayerJoined);
    }
    private void Update()
    {
        UpdateScoreBoard();
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
        scoreCardDisplays.Clear();
        foreach (var player in playerManager.players.Values) {
            GameObject currentScoreDisplay = Instantiate(scoreCard, scoreCardBoard.transform);
            ScoreCardDisplay scoreCardDisplay = currentScoreDisplay.GetComponent<ScoreCardDisplay>();
            scoreCardDisplay.SetPlayer(player);
            scoreCardDisplay.UpdateScore(score[scoreCardDisplay.GetPlayerID()], coins[scoreCardDisplay.GetPlayerID()]);
            scoreCardDisplays.Add(scoreCardDisplay);
        }
        UpdateScoreBoard();
    }
    public void UpdateScoreBoard()
    {
        foreach(var scoreCardDisplay in scoreCardDisplays) 
        {
            scoreCardDisplay.UpdateScore(score[scoreCardDisplay.GetPlayerID()], coins[scoreCardDisplay.GetPlayerID()]);
        }
    }
    public void AddScore(uint playerID, uint amount)
    {
        score[playerID] += amount;
    }
    public void AddCoins(uint playerID, uint amount)
    {
        coins[playerID] += amount;
    }
    public void SetScoreToWin(int amount)
    {
        titleText.text = "First to " + amount.ToString() + " Rounds";
    }
    public List<uint> GetHighestScoreID()
    {
        List<uint> output = new List<uint>();
        uint highestValue = 0;
        foreach(var score in score.Values)
        {
            if(score > highestValue)
            {
                highestValue = score;
            }
        }

        foreach(var player in score)
        {
            if (player.Value == highestValue)
            {
                output.Add(player.Key);
            }
        }

        return output;
    }
    public uint GetScore(uint playerID)
    {
        return score[playerID];
    }
    public uint GetCoins(uint playerID)
    {
        return score[playerID];
    }
}
