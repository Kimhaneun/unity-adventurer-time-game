using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky1 : MonoBehaviour
{
    float sky1Speed = 2;

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * sky1Speed;

        if (transform.position.x <= -18)
        {
            transform.position = new Vector3(18, 0, 0);
        }
    }
}
