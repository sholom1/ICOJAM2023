using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public int stick_id;
    public Vector3 start_vector;
    public PlayerController_1 playerController;
    // Start is called before the first frame update
    void Start()
    {
        start_vector = transform.eulerAngles;
    }

    public void onChangeInput(Vector2 value)
    {
        Vector3 rotation = new Vector3(value.y, 0.0f, value.x);
        transform.eulerAngles = start_vector + (rotation * 5);

        print("Test" + playerController.playerID);
    }
}
