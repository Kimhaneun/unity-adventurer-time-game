using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky3 : MonoBehaviour
{
    float sky3Speed = 6;

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * sky3Speed;

        if (transform.position.x <= -18)
        {
            transform.position = new Vector3(18, 0, 0);
        }
    }
}
