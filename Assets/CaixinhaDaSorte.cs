using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaixinhaDaSorte : MonoBehaviour
{
    public PlayerWeapon playerWeapon;
    public GameObject caixinhaDaSorteModal;
    public List<WeaponInfo> weaponList;
    public GameObject selectedWeaponModal;
    public WeaponInfo selectedWeapon;
    public bool buyed;


    private void Start()
    {
        buyed = false;
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
        int i = Random.Range(0, weaponList.Count);
        selectedWeapon = weaponList[i];
        CloseBuyModal();
        buyed = true;
        OpenSelectedModal();
        StartCoroutine(CloseSelectedWeaponModalCoroutine());
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
