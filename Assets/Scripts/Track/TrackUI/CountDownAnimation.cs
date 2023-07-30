using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CountDownAnimation : MonoBehaviour
{
    public UnityEvent OnAnimationComplete;
    [SerializeField]
    float endFontSize;
    [SerializeField]
    float startFontSize;

    [SerializeField]
    float duration;

    [SerializeField]
    TMP_Text number;

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        number.text = Mathf.FloorToInt(duration+1).ToString();
        float timeValue = Mathf.InverseLerp(0f, 0.7f, Mathf.Ceil(duration) - duration);
        number.fontSize = Mathf.Lerp(startFontSize, endFontSize, timeValue);
        if(duration <= 0)
        {
            OnAnimationComplete.Invoke();
            Destroy(gameObject);
        }

    }
}
