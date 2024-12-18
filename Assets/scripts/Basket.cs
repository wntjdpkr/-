using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Basket : MonoBehaviour
{
    [SerializeField]
    private GameObject filled;  // 바구니에 담긴 표시가 될 오브젝트
    [SerializeField]
    private TextMeshProUGUI failureText;  // 실패율 표시 텍스트
    [SerializeField]
    private Sprite normalSprite;  // 기본 스프라이트
    [SerializeField]
    private Sprite crackedSprite; // 금이 간 스프라이트
    [SerializeField]
    private Sprite shatteredSprite; // 깨진 스프라이트
    [SerializeField]
    private GameObject foodCluster;

    private SpriteRenderer spriteRenderer;
    private bool isFilled = false;  // 바구니에 무언가 담겼는지 확인하는 변수
    private int failureRate = 0;  // 실패율

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (filled != null)
        {
            filled.SetActive(false);  // 시작할 때는 오브젝트를 비활성화
        }
        if (foodCluster != null)
        {
            foodCluster.SetActive(false); // Food Cluster 비활성화
        }

        UpdateFailureText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 적과 충돌했을 때
        if (other.gameObject.CompareTag("enemy"))
        {
            isFilled = true;  // 바구니에 무언가가 담겼음을 표시
            if (filled != null)
            {
                filled.SetActive(true);  // 특정 오브젝트를 활성화하여 담겼음을 표시
            }

            // 실패율 증가 및 텍스트 업데이트
            failureRate += 1;
            UpdateFailureText();

            // 실패율에 따른 색상 및 상태 업데이트
            if (failureRate == 25 || failureRate == 50 || failureRate == 75 || failureRate == 100)
            {
                UpdateBasketState();
            }

            // 게임 오버 처리
            if (failureRate >= 100)
            {
                
                GameManager.instance.SetGameOver();
            }

            Destroy(other.gameObject);  // 적 오브젝트 삭제
        }
    }

    private void UpdateFailureText()
    {
        failureText.text = failureRate.ToString() + "%";
    }

    private void UpdateBasketState()
    {
        if (failureRate >= 50 && failureRate < 100)
        {
            // 금이 간 상태로 스프라이트 교체
            spriteRenderer.sprite = crackedSprite;
        }
        else if (failureRate >= 100)
        {
            // 깨진 상태로 스프라이트 교체
            spriteRenderer.sprite = shatteredSprite;
            // Food Cluster 활성화
            if (foodCluster != null)
            {
                foodCluster.SetActive(true); // Food Cluster 오브젝트 활성화
            }
        }
        
    }
}
