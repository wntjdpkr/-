using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private float moveSpeed = 10f;
    private float minX = -10f;
    private bool isAffectedByGravity = false;
    private Rigidbody2D rb;

    [SerializeField]
    private float hp = 1f;
    private float horizontalMoveSpeed = 1.3f; // 추가된 수평 이동 속도
    private float gravity = 2.4f;  // 추가된 중력 효과

    private FailManager failManager; // FailManager 스크립트에 대한 참조

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 초기엔 중력을 적용하지 않음

        failManager = FindObjectOfType<FailManager>();
    }

    void Update()
    {
        if (!isAffectedByGravity)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x < -1.9f) // 특정 지점에 도달했을 때
            {
                isAffectedByGravity = true;  // 중력의 영향을 받도록 설정
                rb.gravityScale = gravity; // 중력 활성화
                rb.velocity = new Vector2(-horizontalMoveSpeed, rb.velocity.y); // 수평 이동 추가
            }
        }
        else
        {
            // 포물선을 그리며 내려오는 자연스러운 움직임 처리
            if (transform.position.x < minX)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "weapon")
        {
            weapon weapon = other.gameObject.GetComponent<weapon>();
            hp -= weapon.damage;
            if (hp <= 0)
            {
                if (gameObject.tag == "Boss")
                {
                    GameManager.instance.SetGameOver();
                }
                ProgressManager.instance.IncrementProgress();
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Basket") // Basket과 충돌 시
        {
            Destroy(gameObject); // 적 삭제
            if (failManager != null) // FailManager가 null이 아닌 경우
            {
                failManager.IncreaseFailRate(); // FailManager의 IncreaseFailRate() 메서드 호출
            }
        }
    }

}

