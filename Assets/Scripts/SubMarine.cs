using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class SubMarine : MonoBehaviour
{
    public GameObject explosion;
    public GameObject hit;

    Vector2 initialPosition;

    public Image [] Hearts;

    public int health = 3;
    public bool invincible = false;
    float invincibleTimer = 0;
    float invincibleTime = 2;

    SpriteRenderer spriteRenderer;

    float MoveSpeed = 4;

    bool MoveUp;
    bool MoveDown;
    bool MoveLeft;
    bool MoveRight;

    Gun[] guns;
    float CooldownTimer = 0;
    float fireDelay = 0.25f;

    bool shoot;

    GameObject shield;
    public int powerUpGunLevel = 0;

    private void Awake()
    {
        initialPosition = transform.position;
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
        foreach (Gun gun in guns) {
            gun.IsActive = true;
            if (gun.powerUpLevelRequirement != 0) { 
                gun.gameObject.SetActive(false);
            }
        }

        shield = transform.Find("Shield").gameObject;
        DeActivateShield();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        MoveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        MoveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        MoveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        shoot = Input.GetKey(KeyCode.Space);
        CooldownTimer -= Time.deltaTime;
        if (shoot && CooldownTimer <= 0) {
            foreach (Gun gun in guns) {
                if (gun.gameObject.activeSelf) {
                    gun.Shoot();
                }
            }
            AudioManager.AudioInstance.PlaySfx("Shoot");
            CooldownTimer = fireDelay;
        }

        if (invincible) {
            if (invincibleTimer >= invincibleTime)
            {
                invincibleTimer = 0;
                invincible = false;
                spriteRenderer.enabled = true;
            }
            else
            {
                invincibleTimer += Time.deltaTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float MoveAmount = MoveSpeed * Time.fixedDeltaTime;
        Vector2 Move = Vector2.zero;

        if (MoveUp)
        {
            Move.y += MoveAmount;
        }
        if (MoveDown)
        {
            Move.y -= MoveAmount;
        }
        if (MoveLeft)
        {
            Move.x -= MoveAmount;
        }
        if (MoveRight)
        {
            Move.x += MoveAmount;
        }

        float MoveMagnitude = Mathf.Sqrt(Move.x * Move.x + Move.y * Move.y);
        if (MoveMagnitude > MoveAmount) { 
            float ratio = MoveAmount / MoveMagnitude;
            Move *= ratio;
        }
        
        pos += Move;

        if (pos.x <= -7.5f) {
            pos.x = -7.5f;
        }
        if (pos.x >= 7.5f) {
            pos.x = 7.5f;
        }
        if (pos.y >= 4.5f) { 
            pos.y= 4.5f;
        }
        if (pos.y <= -4.5f) {
            pos.y = -4.5f;
        }

        transform.position = pos;

        
    }

    void ActivateShield() { 
        shield.SetActive(true);
    }

    void DeActivateShield() { 
        shield.SetActive(false);
    }

    bool HasShield(){ 
        return shield.activeSelf;
    }

    void AddGuns() {
        powerUpGunLevel++;
        foreach (Gun gun in guns) {
            if (gun.powerUpLevelRequirement <= powerUpGunLevel)
            {
                gun.gameObject.SetActive(true);
            }
            else { 
                gun.gameObject.SetActive(false);
            }
        }
    }

    public void ResetSubMarine () { 
        transform.position = initialPosition;
        DeActivateShield();
        powerUpGunLevel = -1;
        AddGuns();
        health = 3;
        foreach (Image heart in Hearts)
        {
            heart.enabled = true;
        }
        Level.instance.ResetLevel();
    }


    void Hit()
    {
        if (HasShield())
        {
            DeActivateShield();
        }
        else
        {
            if (!invincible)
            {
                health--;
                AudioManager.AudioInstance.PlaySfx("EnemyDie");
                if (health <= 0)
                {
                    Hearts[health].enabled = false;
                    ResetSubMarine();
                }
                else
                {
                    powerUpGunLevel = -1;
                    AddGuns();
                    Hearts[health].enabled = false;
                    invincible = true;
                }
            }
        }
    }

    void Hit(GameObject gameObjectHit) {
        Hit();

        Destroy(gameObjectHit);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!invincible)
            {
                if (bullet.IsEnemy)
                {
                    if (bullet.CanBeDestroyed)
                    {
                        Hit(bullet.gameObject);
                    }
                    else
                    {
                        Hit();
                    }
                }
            }
        }

        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
        {
            if (!invincible) {
                if (!destructable.isBoss)
                {
                    Level.instance.RemoveDestructable();
                    Hit(destructable.gameObject);
                }
                else
                {
                    Hit();
                }
                Instantiate(hit, transform.position, Quaternion.identity);
            }
        }
        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp) {
            AudioManager.AudioInstance.PlaySfx("PowerUP");

            Destroy(powerUp.gameObject);

            if (powerUp.ActivateShield)
            { 
                ActivateShield();
            }
            if (powerUp.AddGuns) { 
                AddGuns();
            }

            Level.instance.AddScore(powerUp.PointValue);
        }

    }
}
