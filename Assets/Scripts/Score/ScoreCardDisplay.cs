using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreCardDisplay : MonoBehaviour
{
    PlayerController_1 player;
    [SerializeField]
    private TextMeshProUGUI playerText;
    [SerializeField]
    private Image playerImage;
    [SerializeField]
    private TextMeshProUGUI roundsText;
    [SerializeField]
    private TextMeshProUGUI lapsText;
    [SerializeField]
    private TextMeshProUGUI checkPointsText;
    [SerializeField]
    private TextMeshProUGUI coinsText;
    public void SetPlayer(PlayerController_1 _player)
    {
        player = _player;
    }
    public uint GetPlayerID()
    {
        return player.playerID;
    }
    public void UpdateScore(float score)
    {

        string displayText = "";
        displayText += "ID: " + player.playerID.ToString() + 
            " Score: "+ score.ToString() + 
            " Laps: " + player.laps_completed.ToString() +
            " Checkpoints: " + player.check_point_num.ToString();
        GetComponentInChildren<TMP_Text>().text = displayText;
    }
}
