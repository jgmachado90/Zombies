using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    public Text waveCounter;

    private void Awake()
    {
        instance = this;
    }


    public void ShowWaveCounter(int wave)
    {
        waveCounter.text = wave.ToString();
    }
}
