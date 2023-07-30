using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LiftMenuUp : MonoBehaviour
{
    public bool start_lifting_menu = false;
    public GameObject menuItems;
    public TextMeshProUGUI player_count;

    private PlayerManager playerManager;

    private float timer = 5f;
    public RectTransform image_to_move;

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
    }

    // Update is called once per frame
    void Update()
    {
        
        player_count.text = playerManager.players.Count + " Players";

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
                Destroy(this);
            }
        }
    }

    public void TriggerTransistion()
    {
        start_lifting_menu = true;
        playerManager.GetComponent<PlayerInputManager>().DisableJoining();
        
        if(camera_pan != null)
        {
            camera_pan.TiggerTransistion();
        }
    }

    public void OnMove(Vector2 value)
    {
        camera_pan.moveCameraLeft(value);
    }
}
