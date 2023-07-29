using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] variations;
    private int index;
    public void swap()
    {
        variations[index].SetActive(false);
        index = Random.Range(0, variations.Length);
        variations[index].SetActive(true);
    }
}
