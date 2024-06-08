using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UI;

public class Destructable : MonoBehaviour
{
    public GameObject explosion;

    [SerializeField] private Slider slider;

    public int health = 3;

    bool CanBeDestroyed = false;
    public float CanBeDestroyedPosition = 10.25f;

    public int ScoreValue = 10;

    public bool isBoss = false;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = health;
        Level.instance.AddDestructable();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < CanBeDestroyedPosition && !CanBeDestroyed) {
            CanBeDestroyed = true;
            Gun[] guns = transform.GetComponentsInChildren<Gun>();
            foreach (Gun gun in guns)
            {
                gun.IsActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CanBeDestroyed) {
            return;
        }

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null) {
            if (!bullet.IsEnemy)
            {
                health--;
                slider.value = health;
                AudioManager.AudioInstance.PlaySfx("explosion");
                if (health <= 0) {
                    Level.instance.AddScore(ScoreValue);
                    DestroyDestructable();
                }
                Destroy(bullet.gameObject);
            }
        }

        if (isBoss) {
            SubMarine subMarine = collision.GetComponent<SubMarine>();
            if (subMarine != null)
            {
                health--;
                slider.value = health;
                if (health <= 0)
                {
                    Destroy(gameObject);
                    Level.instance.AddScore(ScoreValue);
                }
            }
        }
    }

    void DestroyDestructable() {
        Instantiate(explosion, transform.position, Quaternion.identity);
        AudioManager.AudioInstance.PlaySfx("EnemyDie");

        Level.instance.RemoveDestructable();
        Destroy(gameObject);
    }

}
