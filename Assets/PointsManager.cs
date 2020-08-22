using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public static PointsManager instance;

    public Text pointsText;

    public int currentPoints;


    private void Start()
    {
        instance = this;
        pointsText.text = "0";
    }

    public void AddPoints(int points)
    {
        currentPoints += points;
        pointsText.text = "Points:" + currentPoints.ToString();
    }

    public void RemovePoints(int points)
    {
        currentPoints -= points;
        pointsText.text = "Points:" + currentPoints.ToString();
    }


}
