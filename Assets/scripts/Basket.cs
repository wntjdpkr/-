using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Basket : MonoBehaviour
{
    [SerializeField]
    private GameObject filled;  // �ٱ��Ͽ� ��� ǥ�ð� �� ������Ʈ
    [SerializeField]
    private TextMeshProUGUI failureText;  // ������ ǥ�� �ؽ�Ʈ
    [SerializeField]
    private Sprite normalSprite;  // �⺻ ��������Ʈ
    [SerializeField]
    private Sprite crackedSprite; // ���� �� ��������Ʈ
    [SerializeField]
    private Sprite shatteredSprite; // ���� ��������Ʈ
    [SerializeField]
    private GameObject foodCluster;

    private SpriteRenderer spriteRenderer;
    private bool isFilled = false;  // �ٱ��Ͽ� ���� ������ Ȯ���ϴ� ����
    private int failureRate = 0;  // ������

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (filled != null)
        {
            filled.SetActive(false);  // ������ ���� ������Ʈ�� ��Ȱ��ȭ
        }
        if (foodCluster != null)
        {
            foodCluster.SetActive(false); // Food Cluster ��Ȱ��ȭ
        }

        UpdateFailureText();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� �浹���� ��
        if (other.gameObject.CompareTag("enemy"))
        {
            isFilled = true;  // �ٱ��Ͽ� ���𰡰� ������� ǥ��
            if (filled != null)
            {
                filled.SetActive(true);  // Ư�� ������Ʈ�� Ȱ��ȭ�Ͽ� ������� ǥ��
            }

            // ������ ���� �� �ؽ�Ʈ ������Ʈ
            failureRate += 1;
            UpdateFailureText();

            // �������� ���� ���� �� ���� ������Ʈ
            if (failureRate == 25 || failureRate == 50 || failureRate == 75 || failureRate == 100)
            {
                UpdateBasketState();
            }

            // ���� ���� ó��
            if (failureRate >= 100)
            {
                
                GameManager.instance.SetGameOver();
            }

            Destroy(other.gameObject);  // �� ������Ʈ ����
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
            // ���� �� ���·� ��������Ʈ ��ü
            spriteRenderer.sprite = crackedSprite;
        }
        else if (failureRate >= 100)
        {
            // ���� ���·� ��������Ʈ ��ü
            spriteRenderer.sprite = shatteredSprite;
            // Food Cluster Ȱ��ȭ
            if (foodCluster != null)
            {
                foodCluster.SetActive(true); // Food Cluster ������Ʈ Ȱ��ȭ
            }
        }
        
    }
}
