using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private UIManager _uiManager;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (player.hasCoin)
                {
                    _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
                    _uiManager.UpdateCoinInvetory(false);
                    player.hasCoin = false;
                    AudioSource _youWin = GetComponent<AudioSource>();
                    _youWin.Play();
                    player.EnableWeapon();
                }
                else
                {
                    Debug.Log("GETTAOUTOFHERE!!!");
                }
            }
        }
    }
}
