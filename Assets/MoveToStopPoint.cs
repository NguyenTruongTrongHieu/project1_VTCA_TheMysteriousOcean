using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToStopPoint : MonoBehaviour
{
    public float MoveSpeed = 2;
    public float DestroyedPosition = -7.83f;

    public GameObject StopPoint;

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

        if (Vector2.Distance(pos, StopPoint.transform.position) >= 0.5f)
        {
            pos.x -= MoveSpeed * Time.deltaTime;
        }

        transform.position = pos;
    }
}
