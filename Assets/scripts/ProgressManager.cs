using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI progressText; // �޼����� ǥ���� TextMeshPro UI
    private int progressPercentage = 0; // �ʱ� �޼��� 0%
    private int maxProgress = 100; // �ִ� �޼��� 100%

    // �̱��� �������� ProgressManager�� �ٸ� ��ũ��Ʈ���� ���� ������ �� �ְ� ��
    public static ProgressManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateProgressUI();
    }

    // ���� ���� �� ȣ��Ǿ� �޼����� 1% ������Ŵ
    public void IncrementProgress()
    {
        if (progressPercentage < maxProgress)
        {
            progressPercentage += 1; // 1% ����

            UpdateProgressUI();

            if (progressPercentage >= maxProgress)
            {
                GameManager.instance.SetGameOver(); // �޼����� 100%�� �Ǹ� ���� ����
            }
        }
    }

    // TextMesh Pro UI�� ���� �޼����� ������Ʈ
    void UpdateProgressUI()
    {
        progressText.SetText(progressPercentage.ToString() + "%");
    }
}
