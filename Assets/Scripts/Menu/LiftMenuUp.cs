using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LiftMenuUp : MonoBehaviour
{
    public bool complete_lift = false;
    public bool start_lifting_menu = false;
    public GameObject menuItems;
    public TextMeshProUGUI player_count;

    private PlayerManager playerManager;

    private float max_time = 5f;
    private float timer;
    public RectTransform image_to_move;
    public Vector3 initial_image_position;

    public CameraPan camera_pan;

    [Header("Flash settings")]
    public float flash_interval = 1.0f;
    private float flash_current_timer = 0.0f;

    private float fade_value = 0.005f;
    private bool flip_fade = false;

    private TextMeshProUGUI[] text;

    public UnityEvent OnCompleteLift;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        text = GetComponentsInChildren<TextMeshProUGUI>();

        initial_image_position = image_to_move.localPosition;
        timer = max_time;
    }

    public void RestartGame()
    {
        timer = max_time;
        start_lifting_menu = false;
        complete_lift = false;
        image_to_move.gameObject.SetActive(true);
        image_to_move.localPosition = initial_image_position;
        playerManager.GetComponent<PlayerInputManager>().EnableJoining();
    }

    // Update is called once per frame
    void Update()
    {
        if (complete_lift == false)
        {
            playerManager.ChangePlayerInput("UI");
            player_count.text = playerManager.players.Count + " Players";
        }
        //Flashing
        if (flash_current_timer < 0.0f)
        {
            foreach (var text_item in text)
            {
                if (!flip_fade)
                {
                    text_item.alpha = text_item.alpha - fade_value;

                    if(text_item.alpha <= 0.1f)
                    { flip_fade = true; }
                }
                else
                {
                    text_item.alpha = text_item.alpha + fade_value;
                    if (text_item.alpha >= 1)
                    {
                        flip_fade = false;
                        flash_current_timer = flash_interval;
                    }
                }
            }
        }
        else
        {
            flash_current_timer -= Time.deltaTime;
        }

        if (start_lifting_menu)
        {
            
            timer = timer - Time.deltaTime;
            image_to_move.localPosition = new Vector3(image_to_move.localPosition.x, image_to_move.localPosition.y + (200f * Time.deltaTime), image_to_move.localPosition.z);

            if(timer < 0)
            {
                OnCompleteLift.Invoke();
                image_to_move.gameObject.SetActive(false);
                start_lifting_menu = false;
                complete_lift = true;
                //Destroy(this);
            }
        }
    }

    public void TriggerTransistion()
    {
        if(complete_lift == false) {
            start_lifting_menu = true;
            playerManager.GetComponent<PlayerInputManager>().DisableJoining();

            if (camera_pan != null)
            {
                camera_pan.TiggerTransistion();
                camera_pan = null;
            }
        }
    }
}
