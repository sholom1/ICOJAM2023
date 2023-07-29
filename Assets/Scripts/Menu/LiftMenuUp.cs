using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LiftMenuUp : MonoBehaviour
{
    public bool start_lifting_menu = false;
    public GameObject menuItems;
    public TextMeshProUGUI player_count;

    private PlayerManager playerManager;

    private float timer = 7.5f;
    public RectTransform image_to_move;

    [Header("Flash settings")]
    public float flash_interval = 1.0f;
    private float flash_current_timer = 0.0f;

    private float flash_back_timer = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerManager.ChangePlayerInput("UI");
        player_count.text = playerManager.players.Count + " Players";

        //Flashing
        if(flash_current_timer < 0.0f)
        {
            menuItems.SetActive(false);
            flash_back_timer -= Time.deltaTime;

            if(flash_back_timer < 0.0f )
            {
                menuItems.SetActive(true);
                flash_current_timer = flash_interval;
                flash_back_timer = 0.1f;
            }
        }
        else
        {
            flash_current_timer -= Time.deltaTime;
        }

        if (start_lifting_menu)
        {
            
            timer = timer - Time.deltaTime;
            image_to_move.localPosition = new Vector3(image_to_move.localPosition.x, image_to_move.localPosition.y + 0.01f, image_to_move.localPosition.z);

            if(timer < 0)
            {
                GameObject.FindObjectOfType<PlayerManager>().ChangePlayerInput("Player");
                menuItems.SetActive(false);
                Destroy(this);
            }
        }
    }

    public void TriggerTransistion()
    {
        start_lifting_menu = true;
    }
}
