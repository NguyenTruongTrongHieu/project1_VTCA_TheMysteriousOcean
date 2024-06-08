using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownToUp : MonoBehaviour
{
    public bool isEnemy = false;

    public float MoveSpeed = 2;
    public float DestroyedPosition = -7.83f;
   
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.y += MoveSpeed * Time.fixedDeltaTime;
        if (pos.y > DestroyedPosition)
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
