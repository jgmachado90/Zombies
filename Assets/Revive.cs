using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Revive : MonoBehaviour
{
    public GameObject defaultBuyModal;
    public int cost;
    public int times;
    public Button button;
    private void Start()
    {
        button = defaultBuyModal.GetComponentInChildren<Button>();

    }

    private void Update()
    {
        if (CanBuy())
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
        if (!PlayerInfo.instance.revive && times > 0)
        {
            if (collision.tag == "Player")
            {
                OpenDefaultBuyModal();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(OnBuy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CloseDefaultBuyModal();
           
        }
    }

    private void OnBuy()
    {
        PointsManager.instance.RemovePoints(cost);
        PlayerInfo.instance.revive = true;
        times--;
        CloseDefaultBuyModal();
    }

    private void CloseDefaultBuyModal()
    {
        defaultBuyModal.SetActive(false);
    }

    private void OpenDefaultBuyModal()
    {
        defaultBuyModal.SetActive(true);
        defaultBuyModal.GetComponent<Text>().text = "Comprar revive " + cost.ToString();
    }

    private bool CanBuy()
    {
        if (PointsManager.instance.currentPoints >= cost)
            return true;
        else
            return false;
    }


}
