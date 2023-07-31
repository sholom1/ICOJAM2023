using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private int rotations;
    private int currentIndex;
    [SerializeField]
    private MeshRenderer powerUpDisplay;
    [SerializeField]
    private float delay;

    public TextMeshPro costText;
    // Start is called before the first frame update
    void Start()
    {
        start_vector = target.eulerAngles;
        powerUpDisplay.material = powerUpImages[currentIndex];

        costText = GetComponentInChildren<TextMeshPro>();
    }

    public void onChangeInput(Vector2 value)
    {
        Vector3 rotation = new Vector3(value.y, 0.0f, -value.x);
        target.eulerAngles = start_vector + (rotation * 25);
    }
    public void selectPowerUp(int index, Action callBack)
    {
        StartCoroutine(rotateThroughImages(index, callBack));
    }
    public void resetPowerUpImage()
    {
        currentIndex = 0;
        powerUpDisplay.material = powerUpImages[0];
    }
    IEnumerator rotateThroughImages(int index, Action callBack)
    {
        for (int i = 0; i < rotations; i++)
        {
            for (int j = 0; j < powerUpImages.Length; j++)
            {
                powerUpDisplay.material = powerUpImages[j];
                yield return new WaitForSeconds(delay);
            }
        }
        powerUpDisplay.material = powerUpImages[index + 1];
        callBack();
    }
}
