using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaixinhaDaSorte : MonoBehaviour
{
    public PlayerWeapon playerWeapon;
    public int cost;
    public GameObject caixinhaDaSorteModal;
    public List<WeaponInfo> weaponList;
    public GameObject selectedWeaponModal;
    public WeaponInfo selectedWeapon;
    public Button button;
    public bool buyed;


    private void Start()
    {
        buyed = false;
        button = caixinhaDaSorteModal.GetComponentInChildren<Button>();
    }

    private void Update()
    {
        if (CanBuy())
        {
            Debug.Log("canbuy");
            button.interactable = true;
        }
        else
        {
            Debug.Log("cant buy");
            button.interactable = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!buyed)
                OpenBuyModal();
            else
                OpenSelectedModal();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CloseBuyModal();
            CloseSelectedModal();
        }
    }

    public void OpenBuyModal()
    {
        caixinhaDaSorteModal.SetActive(true);
    }

    public void CloseBuyModal()
    {
        caixinhaDaSorteModal.SetActive(false);
    }

    public void OpenSelectedModal()
    {
        selectedWeaponModal.SetActive(true);
        selectedWeaponModal.GetComponent<Image>().sprite = selectedWeapon.weaponSprite;
        
    }

    public void CloseSelectedModal()
    {
        selectedWeaponModal.SetActive(false);
    }


    public void OnBuy()
    {
        PointsManager.instance.RemovePoints(cost);
        int i = UnityEngine.Random.Range(0, weaponList.Count);
        selectedWeapon = weaponList[i];
        CloseBuyModal();
        buyed = true;
        OpenSelectedModal();
        StartCoroutine(CloseSelectedWeaponModalCoroutine());
        

    }

    private bool CanBuy()
    {
        if (PointsManager.instance.currentPoints >= cost)
            return true;
        else
            return false;
    }

    public void OnChangeWeapon()
    {
        CloseSelectedModal();
        buyed = false;
        StopAllCoroutines();
        playerWeapon.OnChangeWeapon(selectedWeapon);
    }

    IEnumerator CloseSelectedWeaponModalCoroutine()
    {
        yield return new WaitForSeconds(5f);
        CloseSelectedModal();
        buyed = false;
    }

}
