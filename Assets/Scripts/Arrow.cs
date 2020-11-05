using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float force;
    [SerializeField] private float lifeTime;
    [SerializeField] private TrigerDamage trigerDamage;
    private Player player;

    public float Force
    {
        get { return force;}
        set { force = value;}
    }

    public void SetImpulse( Vector2 direction, float force, Player player)
    {
        this.player = player;
        trigerDamage.Init(this);
        trigerDamage.Parent = player.gameObject;
        rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
        if (force < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(StartLife());
    }


    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;

    }

    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowToPool(this);
    }

}
