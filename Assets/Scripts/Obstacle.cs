using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] variations;
    private int index;
    public void swapRandom()
    {
        variations[index].SetActive(false);
        index = Random.Range(0, variations.Length);
        variations[index].SetActive(true);
    }
    public void Swap()
    {
        if (variations[0].activeInHierarchy)
        {
            variations[0].SetActive(false);
            variations[1].SetActive(true);
        } else if (variations[1].activeInHierarchy)
        {
            variations[1].SetActive(false);
            variations[0].SetActive(true);
        }
    }
}
