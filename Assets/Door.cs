using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool opened;
    public int cost;
    public GameObject openDoorModal;
    public GameObject room;
    public Button button;

    private void Start()
    {
        opened = false;
        button = openDoorModal.GetComponentInChildren<Button>();
    }

    private void Update()
    {
        if (CanOpen())
        {            
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!opened)
        {
            if (collision.tag == "Player")
            {
                OpenOpenDoorModal();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(OnOpen);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CloseOpenDoorModal();
        }
    }


    private void CloseOpenDoorModal()
    {
        openDoorModal.SetActive(false);
    }

    private void OpenOpenDoorModal()
    {
        openDoorModal.SetActive(true);
    }

    public bool CanOpen()
    {
        if(PointsManager.instance.currentPoints >= cost)
        {
            return true;
        }
        return false;
    }

    public void OnOpen()
    {
        Debug.Log("opening - "+ cost);
        PointsManager.instance.RemovePoints(cost);
        room.SetActive(true);
        WaveManager.instance.FindSpawns(room);
        Destroy(this.gameObject);
    }



}
