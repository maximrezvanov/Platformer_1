using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.5f;
    public float force;
    public Rigidbody2D rigidboby;
    public float minimalHeight;
    public bool isCheatMode;
    public GroundDetection groundDetection;
    private Vector3 direction;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool isJumping;
    private bool isReadyToShoot;
    private Arrow currentArrow;
    private List<Arrow> arrowPool;

    [SerializeField] Arrow arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private TrigerDamage trigerDamage;
    [SerializeField] private int shootForce = 5;
    [SerializeField] private float timeToPause = 0.5f;
    [SerializeField] private int arrowCount = 5;
    [SerializeField] private Health health;
    public Health Health { get { return health; } }
    [SerializeField] public Item item;
    [SerializeField] private BuffReciever buffReciever;




    public void Start()
    {
        AddCoins();
        СreateArrows();

        buffReciever.OnBuffsChanged += TestBuffs;

    }


    public void TestBuffs()
    {
        Debug.Log("сработал buff");
    }

    void FixedUpdate()
    {
        if (animator != null)
            animator.SetBool("isGrounded", groundDetection.isGrounded);

        isJumping = isJumping && !groundDetection.isGrounded;
        direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            direction = Vector3.left; // (-1, 0)
        if (Input.GetKey(KeyCode.D))
            direction = Vector3.right; // (1, 0)
        direction *= speed;
        direction.y = rigidboby.velocity.y;
        rigidboby.velocity = direction;

        if (Input.GetKeyDown(KeyCode.Space) && groundDetection.isGrounded)
        {
            rigidboby.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetTrigger("StartJump");
            isJumping = true;
        }

        if (direction.x > 0)
            spriteRenderer.flipX = false;
        if (direction.x < 0)
            spriteRenderer.flipX = true;

        animator.SetFloat("Speed", Mathf.Abs(rigidboby.velocity.x));
        CheckFall();
    }

    private void Update()
    {
        CheckShoot();

    }

    #region CheckFall
    void CheckFall()
    {
        if (transform.position.y < minimalHeight && isCheatMode)
        {
            rigidboby.velocity = new Vector2(0, 0);
            transform.position = new Vector2(0, 0);
        }
        else if (transform.position.y < minimalHeight && !isCheatMode)
            Destroy(gameObject);
    }

    #endregion

    public void AddCoins()
    {

        if (PlayerInventory.Instanse.gameObject.CompareTag("Coin"))
        {
            PlayerInventory.Instanse.coinsCount++;
            Destroy(PlayerInventory.Instanse.gameObject);

        }
    }

    public void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0) && groundDetection.isGrounded && !isReadyToShoot)
        {

            animator.SetTrigger("Attack");

        }

    }


    public void InitArrow()
    {
        currentArrow = GetArrowFromPool();
    }

    private IEnumerator PauseToShoot()
    {
        isReadyToShoot = true;
        yield return new WaitForSeconds(timeToPause);
        isReadyToShoot = false;
        yield break;
    }


    private void TimeToShoot()// для подгона времени анимации и вылета стрелы
    {


        currentArrow = Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
        currentArrow.SetImpulse(Vector2.right, spriteRenderer.flipX ? -force * shootForce : force * shootForce, this);
        StartCoroutine(PauseToShoot());

    }
    private void СreateArrows()
    {
        arrowPool = new List<Arrow>();

        for (int i = 0; i < arrowCount; i++)
        {
            var arrowTemp = Instantiate(arrow, arrowSpawnPoint);
            arrowPool.Add(arrowTemp);
            arrowTemp.gameObject.SetActive(false);
        }

    }
    private Arrow GetArrowFromPool()
    {
        if (arrowPool.Count > 0)
        {
            var arrowTemp = arrowPool[0];
            arrowPool.Remove(arrowTemp);
            arrowTemp.gameObject.SetActive(true);
            arrowTemp.transform.parent = null;
            arrowTemp.transform.position = arrowSpawnPoint.transform.position;
            return arrowTemp;
        }
        return Instantiate(arrow, arrowSpawnPoint.position, Quaternion.identity);
    }

    public void ReturnArrowToPool(Arrow arrowTemp)
    {
        if (!arrowPool.Contains(arrowTemp))
            arrowPool.Add(arrowTemp);

        arrowTemp.transform.parent = arrowSpawnPoint;
        arrowTemp.transform.position = arrowSpawnPoint.transform.position;
        arrowTemp.gameObject.SetActive(false);
    }

}
