using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public int stick_id;
    public Vector3 start_vector;
    public PlayerController_1 playerController;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Material[] powerUpImages;
    [SerializeField]
    private MeshRenderer powerUpDisplay;
    // Start is called before the first frame update
    void Start()
    {
        start_vector = target.eulerAngles;
    }

    public void onChangeInput(Vector2 value)
    {
        Vector3 rotation = new Vector3(value.y, 0.0f, -value.x);
        target.eulerAngles = start_vector + (rotation * 25);
    }
}
