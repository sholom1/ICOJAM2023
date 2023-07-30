using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    [SerializeField] private PlayerController_1 _playerCharacterScript;
    [SerializeField] private LiftMenuUp start_menu;
    [SerializeField] private EndGame end_menu;

    private void Start()
    {
        start_menu = GameObject.FindObjectOfType<LiftMenuUp>();
        end_menu = FindObjectOfType<EndGame>(true);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //print(context.ReadValue<Vector2>());
        _playerCharacterScript.updateMovement(context.ReadValue<Vector2>());
    }

    public void onUIStart(InputAction.CallbackContext context)
    {
        if (end_menu.isActiveAndEnabled)
        {
            end_menu.RestartGame();
        }
        else
        {
            start_menu.TriggerTransistion();
        }
    }
}
