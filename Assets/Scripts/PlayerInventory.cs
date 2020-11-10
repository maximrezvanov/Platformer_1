using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    public int coinsCount;

    public static PlayerInventory Instanse;
    [SerializeField] private Text coinsText;
    public BuffReciever buffReciever;
    private List<Item> items;
    public List<Item> Items
    {
        get { return items; }
    }
   

    private void Start()
    {
        GameManager.Instance.inventory = this;
        coinsText.text = coinsCount.ToString();
        items = new List<Item>();

    }

    private void Awake()
    {
        Instanse = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.coinContainer.ContainsKey(collision.gameObject))
        {
            var coin = GameManager.Instance.coinContainer[collision.gameObject];
            coin.StartDestroy();

            coinsCount++;
            coinsText.text = coinsCount.ToString();
            
        }
        if (GameManager.Instance.itemsContainer.ContainsKey(collision.gameObject))
        {
            var itemComponent = GameManager.Instance.itemsContainer[collision.gameObject];
            items.Add(itemComponent.Item);
            itemComponent.Destroy(collision.gameObject);

        }

    }

   
    

  

   
       

    


}


