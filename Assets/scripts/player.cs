using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;
    [SerializeField]
    private Transform shootTransform;
    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;
    // Update is called once per frame
    void Update()
    {
        // 게임이 시작된 경우에만 이동 및 공격 가능
        if (GameManager.instance.isGameStarted)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float toX = Mathf.Clamp(mousePos.x, -8.5f, -4f);
            float toY = Mathf.Clamp(mousePos.y, -4.35f, 4.35f);
            transform.position = new Vector3(toX, toY, 0);

            if (GameManager.instance.isGameOver == false)
            {
                Shoot();
            }
        }
    }
    void Shoot(){
        if (Time.time - lastShotTime > shootInterval){
            // 원래 무기의 회전 값 유지
            Instantiate(weapons[weaponIndex], shootTransform.position, weapons[weaponIndex].transform.rotation);
            lastShotTime = Time.time;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "enemy" || other.gameObject.tag == "Boss") {
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Coin") {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }
    public void Upgrade() {
        weaponIndex += 1;
        if (weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length - 1;
        }
    }
}
