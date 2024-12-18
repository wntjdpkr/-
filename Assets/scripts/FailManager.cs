using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FailManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI failRateText; // �������� ǥ���� TextMeshPro ��ü�� �̸�

    [SerializeField]
    private GameObject Basket; // Basket ������Ʈ�� ���� ����

    private int failRate = 0; // ������ ���� (������)

    public void IncreaseFailRate()
    {
        if (failRate < 100) // �������� 100% �̸��� ��쿡��
        {
            failRate += 1; // ������ 1% ����
            UpdateFailRateText(); // ������ �ؽ�Ʈ ������Ʈ
            UpdateBasketColor(); // Basket ���� ������Ʈ

            if (failRate >= 100) // �������� 100% �̻��� �Ǹ�
            {
                GameManager.instance.SetGameOver(); // GameManager�� SetGameOver() �޼��� ȣ��
            }
        }
    }

    private void UpdateFailRateText()
    {
        failRateText.text = $"{failRate}%"; // �������� ������ ǥ��
    }

    private void UpdateBasketColor()
    {
        if (Basket != null) // Basket ������Ʈ�� �����ϴ� ��쿡��
        {
            SpriteRenderer basketSpriteRenderer = Basket.GetComponent<SpriteRenderer>();
            if (basketSpriteRenderer != null)
            {
                Color color;

                if (failRate >= 75)
                {
                    color = new Color(1f, 0.5f, 0.5f); // ���� ������
                }

                else if (failRate >= 50)
                {
                    color = new Color(1f, 0.7f, 0.7f); // �߰� ������
                }
                else if (failRate >= 25)
                {
                    color = new Color(1f, 0.9f, 0.9f); // ���� ������
                }
                else
                {
                    color = Color.white; // ���� ���� (���)
                }

                basketSpriteRenderer.color = color;
            }
        }
    }
}