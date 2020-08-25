using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeObject : MonoBehaviour
{
    public bool active;
    public GameObject activeBarricade;
    public List<GameObject> disabledBarricade;

    private void Start()
    {
        active = true;
    }

    public void EnableActiveBarricade()
    {
        active = true;

        activeBarricade.SetActive(true);

        foreach (GameObject gO in disabledBarricade)
        {
            gO.SetActive(false);
        }
    }

    public void DisableActiveBarricade()
    {
        active = false;

        activeBarricade.SetActive(false);

        foreach (GameObject gO in disabledBarricade)
        {
            gO.SetActive(true);
        }

    }



}
