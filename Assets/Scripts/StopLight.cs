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
    private bool countdown = false;
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
        if (remaining < 0)
        {
            if (!hasEnded) StartCoroutine(restart());
            countdown = false;
            Debug.Log(countdown);
        }
        /*else if (remaining < 6 && !countdown)
        {
            AudioManager.instance.Countdown();
            countdown = true;
            Debug.Log(countdown);
        }*/
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
        print("new light: " + Timer.instance.firstTimer);
        hasEnded = false;
        display.text = "";
        if(!Timer.instance.firstTimer)
            Timer.instance.RestartTimer();
        greenLight.material = green;
        redLight.material = black;
    }
}
