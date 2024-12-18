using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FailManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI failRateText; // 실패율을 표시할 TextMeshPro 객체의 이름

    [SerializeField]
    private GameObject Basket; // Basket 오브젝트에 대한 참조

    private int failRate = 0; // 실패율 변수 (정수형)

    public void IncreaseFailRate()
    {
        if (failRate < 100) // 실패율이 100% 미만인 경우에만
        {
            failRate += 1; // 실패율 1% 증가
            UpdateFailRateText(); // 실패율 텍스트 업데이트
            UpdateBasketColor(); // Basket 색상 업데이트

            if (failRate >= 100) // 실패율이 100% 이상이 되면
            {
                GameManager.instance.SetGameOver(); // GameManager의 SetGameOver() 메서드 호출
            }
        }
    }

    private void UpdateFailRateText()
    {
        failRateText.text = $"{failRate}%"; // 실패율을 정수로 표시
    }

    private void UpdateBasketColor()
    {
        if (Basket != null) // Basket 오브젝트가 존재하는 경우에만
        {
            SpriteRenderer basketSpriteRenderer = Basket.GetComponent<SpriteRenderer>();
            if (basketSpriteRenderer != null)
            {
                Color color;

                if (failRate >= 75)
                {
                    color = new Color(1f, 0.5f, 0.5f); // 가장 빨간색
                }

                else if (failRate >= 50)
                {
                    color = new Color(1f, 0.7f, 0.7f); // 중간 빨간색
                }
                else if (failRate >= 25)
                {
                    color = new Color(1f, 0.9f, 0.9f); // 약한 빨간색
                }
                else
                {
                    color = Color.white; // 원래 색상 (흰색)
                }

                basketSpriteRenderer.color = color;
            }
        }
    }
}