﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private ItemType type;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] GameObject soundEffect;
    private Item item;

    public Item Item
    {
        get { return item; }
    }

    public void Destroy(GameObject gameObject)
    {
        animator.SetTrigger("Destroy");
        soundEffect.SetActive(true);
    }

    void Start()
    {
        item = GameManager.Instance.itemBase.GetItemOfID((int)type);
        spriteRenderer.sprite = item.Icon;
        GameManager.Instance.itemsContainer.Add(gameObject.gameObject, this);


    }

   

}

 

public enum ItemType
{
    DamagePotion = 1, ArmorPotion = 2, ForcePotion = 3
}
