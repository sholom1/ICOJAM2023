using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    [SerializeField] private PlayerController_2 _playerCharacterScript;
    [SerializeField] private float inputSensitivity;
    private Vector2 lastInput;
    private float acceleration = 0;
    private float turn;
    void FixedUpdate()
    {
        acceleration = dampAxis(acceleration, lastInput.y, inputSensitivity);
        turn = dampAxis(turn, lastInput.x, inputSensitivity);
        _playerCharacterScript.Accelerate(acceleration);
        _playerCharacterScript.Turn(turn);
    }
    float dampAxis (float current, float input, float sensitivity)
    {
        if (input != 0)
            return Mathf.Clamp(current + input * sensitivity * Time.deltaTime, Mathf.Min(0, input), Mathf.Max(input, 0));
        return 0;
    }
    public void OnAccelerate(InputAction.CallbackContext context)
    {
        lastInput.y = context.ReadValue<float>();
    }
    public void OnTurn(InputAction.CallbackContext context)
    {
        lastInput.x = context.ReadValue<float>();
    }
}
