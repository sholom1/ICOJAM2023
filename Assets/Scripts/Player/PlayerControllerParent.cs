using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerParent : MonoBehaviour
{
    public GameObject deathMark;
    public uint playerID;
    [SerializeField] protected Rigidbody2D rb;
    private PlayerManager playerManager;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerID = GetComponent<PlayerInput>().user.id;
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerManager.OnJoin(this);
    }
    private void OnDestroy()
    {
        playerManager.OnLeave(this);
    }
}
