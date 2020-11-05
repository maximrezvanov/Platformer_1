using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollision : MonoBehaviour
{
    public int healthPoint;



    private void OnTriggerEnter2D(Collider2D collision)
   
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy")) // сравнение тегов
        {
            Debug.Log("Collision");
            Health health = collision.gameObject.GetComponent<Health>();
            health.SetHealth(healthPoint);
            Destroy(gameObject);

        }

    }

   








}
