using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{
    [SerializeField] private PlayerController_1 _playerCharacterScript;

    private void Start()
    {
        _playerCharacterScript = GetComponent<PlayerController_1>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        print(context.ReadValue<Vector2>());
        _playerCharacterScript.updateMovement(context.ReadValue<Vector2>());
    }
}
