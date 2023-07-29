using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    [SerializeField] private PlayerController_2 _playerCharacterScript;
    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>().x);
        _playerCharacterScript.Turn(Mathf.Sign(context.ReadValue<Vector2>().x));
    }
    public void OnReverse(InputAction.CallbackContext context)
    {
        Debug.Log("reverse");
        if (context.ReadValueAsButton())
        {
            _playerCharacterScript.Reverse();
        }
    }
    public void OnAccelerate(InputAction.CallbackContext context)
    {
        Debug.Log("Accell");
        if (context.ReadValueAsButton())
        {
            _playerCharacterScript.Accelerate();
        }
    }
}
