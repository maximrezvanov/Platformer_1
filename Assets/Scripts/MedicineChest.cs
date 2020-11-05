using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChest : MonoBehaviour
{
    public int bonusHealth;
    [SerializeField] Animator animator;


    private void Start()
    {
        GameManager.Instance.medicContainer.Add(gameObject, this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Enemy"))
        //if(GameManager.Instance.medicContainer.ContainsKey(col.gameObject))
        {
            DestroyMedecineChest();

            Health health = col.gameObject.GetComponent<Health>();
            health.SetHealth(bonusHealth);
            Destroy(gameObject);
        }

    }
    public void DestroyMedecineChest()
    {
        animator.SetTrigger("MedDestroy");
        
    }
}
