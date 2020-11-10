using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] GameObject bowlSound;
    [SerializeField] private Animator animator;
    [SerializeField] SpriteRenderer sprite;
    private Health health;
    public int damage = 10;
    private float direction;
    public float Direction
    {
        get
        {
            return direction;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))

        health = GameManager.Instance.healthContainer[col.gameObject];
        if (health != null)
        {
            direction = (col.transform.position - transform.position).x;
            animator.SetFloat("Direction", Mathf.Abs(direction));


        }
    }

    public void SetDamage()
    {
        if (health != null)
        {
            health.TakeHit(damage);
        }
        health = null;
        direction = 0;
        animator.SetFloat("Direction", 0f);

    }

    public void testSound()
    {
        animator.SetTrigger("SetDamage");
        bowlSound.SetActive(true);

    }
}
