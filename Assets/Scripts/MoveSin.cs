using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSin : MonoBehaviour
{
    float SinCenterY;

    public float amplitude = 3;
    public float frequency = 0.5f;

    [SerializeField]private bool Inverted;

    private void Awake()
    {
        int inverted = Random.Range(0, 1);
        if (inverted == 0)
        {
            Inverted = false;
        }
        else
        {
            Inverted = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SinCenterY = transform.position.y;
    }

    private void FixedUpdate()
    {
        Vector2 Pos = transform.position;

        float sin = Mathf.Sin(Pos.x * frequency) * amplitude;
        if (Inverted) {
            sin *= -1;
        }
        Pos.y = SinCenterY + sin;

        transform.position = Pos;
    }
}
