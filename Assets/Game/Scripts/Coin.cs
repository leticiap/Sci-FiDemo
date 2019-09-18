using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    private UIManager _uiManager;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
           Player player = other.GetComponent<Player>();
           if (player != null)
           {
               _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
               _uiManager.UpdateCoinInvetory(true);
               player.hasCoin = true;
               AudioSource.PlayClipAtPoint(_clip, transform.position, 1f);
               Destroy(this.gameObject);
           }
        }
    }
}
