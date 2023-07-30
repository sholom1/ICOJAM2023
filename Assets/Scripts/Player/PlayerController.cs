using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController_1 : MonoBehaviour
{
    [SerializeField] 
    private Rigidbody2D rb;

    public GameObject deathMark;

    private Vector2 move_Position;
    private bool isBreakDepressed;
    public float max_speed;
    public float speed_modifier;
    public float acceleration_mod;

    public float rotation_sensitivity;

    private PlayerManager playerManager;

    public uint playerID;

    public Joystick players_stick;

    public Material p_material;

    [Header("TrackVars")]
    public int check_point_num = 0;
    public int laps_completed = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerID = GetComponent<PlayerInput>().user.id;
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerManager.OnJoin(this);

        foreach(Joystick stick in GameObject.FindObjectsOfType<Joystick>())
        {
            if(stick.stick_id == playerID)
            {
                stick.playerController = this;
                players_stick = stick;
                
                break;
            }
        }
    }
    private void OnDestroy()
    {
        playerManager.OnLeave(this);
    }

    private void FixedUpdate()
    {
        MovePlayer();
        UpdateRotation();
    }

    private void MovePlayer()
    {
        if (isBreakDepressed)
            rb.AddForce(-rb.velocity);
        else if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) < max_speed)
        {
                Vector2 speed = move_Position *
               (speed_modifier + (Mathf.Abs(rb.velocity.x * acceleration_mod) + Mathf.Abs(rb.velocity.y * acceleration_mod)));
                rb.AddForce(speed , ForceMode2D.Force);
        }
    }

    private void UpdateRotation()
    {
        if (move_Position != Vector2.zero)
        {
            float currentAngle = transform.rotation.eulerAngles.z;
            float targetAngle = Mathf.Atan2(-move_Position.x, move_Position.y) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.AngleAxis(Mathf.LerpAngle(currentAngle, targetAngle, rotation_sensitivity), Vector3.forward);
        }
    }

    public void updateMovement(Vector2 value)
    {
        //value.x = -value.x;
        move_Position = value;
        players_stick.onChangeInput(value);
    }
    public void HandleBreak(InputAction.CallbackContext ctx)
    {
        isBreakDepressed = ctx.ReadValueAsButton();
    }
    
    public void setMaterial()
    {
        foreach(SpriteRenderer p_sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            p_sprite.material = p_material;
        }
    }
}
