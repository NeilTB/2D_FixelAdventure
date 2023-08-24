 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int item = 0;
    [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectItem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collectItem.Play();
            Destroy(collision.gameObject);
            item++;
            cherriesText.text = "Cherries : " + item;
        }
    }
}
