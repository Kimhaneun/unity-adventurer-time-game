using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky2 : MonoBehaviour
{
    float sky2Speed = 4;

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * sky2Speed;

        if (transform.position.x <= -18)
        {
            transform.position = new Vector3(18, 0, 0);
        }
    }
}
