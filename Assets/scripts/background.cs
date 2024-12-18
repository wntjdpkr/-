using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgrounds : MonoBehaviour
{
    private float moveSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < -18.45) {
            transform.position += new Vector3(36.9f, 0, 0);
        }

    }
}
