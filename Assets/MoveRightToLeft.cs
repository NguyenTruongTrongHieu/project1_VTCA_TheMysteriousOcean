using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class MoveRightToLeft : MonoBehaviour
{
    public bool isEnemy = false;

    public float MoveSpeed = 2;
    public float DestroyedPosition = -7.83f;

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= MoveSpeed * Time.fixedDeltaTime;
        if (pos.x < DestroyedPosition)
        {
            if (isEnemy)
            {
                Level.instance.RemoveDestructable();
            }
            Destroy(gameObject);
        }
        transform.position = pos;
    }
}
