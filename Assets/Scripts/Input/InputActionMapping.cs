using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    [SerializeField] private PlayerController_1 _playerCharacterScript;
    [SerializeField] private LiftMenuUp start_menu;

    private void Start()
    {
        start_menu = GameObject.FindObjectOfType<LiftMenuUp>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //print(context.ReadValue<Vector2>());
        _playerCharacterScript.updateMovement(context.ReadValue<Vector2>());
        print("move");
    }

    public void onUIStart(InputAction.CallbackContext context)
    {
        start_menu.TriggerTransistion();
        print("test");
    }
}
