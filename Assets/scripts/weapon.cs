using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 9f;
    public float damage = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}
