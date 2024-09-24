using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;

    public Bullet bullet;
    Vector2 Direction;

    public bool AutoShoot = false;
    public float ShootIntervalSeconds = 0.5f;
    public float ShootDelaySeconds = 0;

    float ShootTimer = 0;
    float DelayTimer = 0;

    public bool IsActive = false;

    Quaternion pos;

    // Update is called once per frame
    void Update()
    {
        if (!IsActive) { 
            return;
        }

        Direction = (transform.localRotation * Vector2.right).normalized;

        
        if (AutoShoot)
        {
            if (DelayTimer >= ShootDelaySeconds)
            {
                if (ShootTimer >= ShootIntervalSeconds)
                {
                    Shoot();
                    AudioManager.AudioInstance.PlaySfx("Shoot");
                    ShootTimer = 0;
                }
                else
                {
                    ShootTimer += Time.deltaTime;
                }
            }
            else
            {
                DelayTimer += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        pos = transform.rotation;

        transform.rotation = pos;
    }

    public void Shoot() { 
        GameObject go = Instantiate(bullet.gameObject, transform.position, pos);

        Bullet goBullet = go.GetComponent<Bullet>();
        goBullet.Direction = Direction;
    }
}
