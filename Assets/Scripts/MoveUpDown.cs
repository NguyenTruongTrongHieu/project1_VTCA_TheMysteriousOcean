using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public float MoveSpeed = 2;
    public float DestroyedPosition = -7.83f;

    public GameObject PointA;
    public GameObject PointB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (pos.y == PointA.transform.position.y)
        {
            pos.y -= MoveSpeed * Time.fixedDeltaTime;
        }

        if (pos.y == PointB.transform.position.y) {
            pos.y += MoveSpeed * Time.fixedDeltaTime;
        }

        transform.position = pos;
    }
}
