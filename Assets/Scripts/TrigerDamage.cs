using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerDamage : MonoBehaviour
{
    [SerializeField]private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField] private bool isDestroyingAfterCollision;

    private GameObject parent;
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    private IObjectDestroyer destroyer;

   public void Init(IObjectDestroyer destroyer)
    {
        this.destroyer = destroyer;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == parent)
            return;

        if(GameManager.Instance.healthContainer.ContainsKey(collision.gameObject))

        {
            var health = GameManager.Instance.healthContainer[collision.gameObject];

            health.TakeHit(damage);
        }
        if (isDestroyingAfterCollision)
        {
            //if (destroyer == null)
            //    Destroy(gameObject);
            //else destroyer.Destroy(gameObject);
           Destroy(gameObject);

        }
    }


}

public interface IObjectDestroyer
{
    void Destroy(GameObject gameObject);
}
