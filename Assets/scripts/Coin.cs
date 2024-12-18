using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    

    [SerializeField]
    private float moveSpeed = 10f;
    private float minX = -10f;

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < minX){
            Destroy(gameObject);
        }
    }
}
