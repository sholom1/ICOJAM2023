using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLight : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro display;
    [SerializeField]
    private MeshRenderer redLight;
    [SerializeField]
    private MeshRenderer greenLight;
    [SerializeField]
    private Material black;
    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;
    [SerializeField]
    private float resetDelay;
    [SerializeField]
    private AudioSource beeper;
    private bool hasEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        Timer.instance.onTimerUpdate.AddListener(CheckTime);
    }
    private void OnDestroy()
    {
        Timer.instance.onTimerUpdate.RemoveListener(CheckTime);
    }
    void CheckTime(float remaining)
    {
        //print(remaining);
        if (remaining < 0)
        {
            if (!hasEnded) StartCoroutine(restart());
        }
        else if (remaining < 4 && remaining > 3 && !beeper.isPlaying)
        {
            beeper.Play();
        }
        else if (remaining < 3)
        {
            display.text = Mathf.FloorToInt(remaining + 1).ToString();
        }
    }
    IEnumerator restart()
    {
        SetRedLight();
        yield return new WaitForSeconds(resetDelay);
        SetGreenLight();
    }
    public void SetRedLight()
    {
        greenLight.material = black;
        redLight.material = red;
        display.text = "X";
        hasEnded = true;
    }
    public void SetGreenLight()
    {
        hasEnded = false;
        display.text = "";
        Timer.instance.RestartTimer();
        greenLight.material = green;
        redLight.material = black;
    }
}
