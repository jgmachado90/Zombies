using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool opened;
    public GameObject openDoorModal;

    private void Start()
    {
        opened = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!opened)
        {
            if (collision.tag == "Player")
            {
                OpenOpenDoorModal();
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




}
