using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hit;

    private GameObject player;
    private Rigidbody2D rb;
    public bool isShootToPlayer = false;
    public bool CanBeDestroyed = true;

    public Vector2 Direction = new Vector2(1, 0);
    public float speed = 10;
    public float AliveTimer = 3;
    public Vector2 Velocity;

    public bool IsEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isShootToPlayer)
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");

            Vector3 direction = player.transform.position - transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        }
        Destroy(gameObject, AliveTimer);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShootToPlayer)
        {
            Velocity = Direction * speed;

        }
    }

    private void FixedUpdate()
    {
        if (!isShootToPlayer)
        {
            Vector2 pos = transform.position;

            pos += Velocity * Time.fixedDeltaTime;

            transform.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet2 = collision.GetComponent<Bullet>();
        if (bullet2 != null)
        {
            if (bullet2.IsEnemy != this.IsEnemy)
            {
                if (CanBeDestroyed)
                {
                    Destroy(gameObject);
                }

                if (bullet2.CanBeDestroyed)
                {
                    Destroy(bullet2.gameObject);
                }
                Instantiate(hit, transform.position, Quaternion.identity);
                AudioManager.AudioInstance.PlaySfx("explosion");
            }
        }

        SubMarine subMarine = collision.GetComponent<SubMarine>();
        if (subMarine != null)
        {
            if (IsEnemy && !subMarine.invincible)
            {
                Instantiate(hit, transform.position, Quaternion.identity);
            }
        }

        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
        {
            if (!IsEnemy)
            {
                Instantiate(hit, transform.position, Quaternion.identity);
            }
        }
    }
}