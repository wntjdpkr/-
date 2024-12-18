using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI progressText; // 달성률을 표시할 TextMeshPro UI
    private int progressPercentage = 0; // 초기 달성률 0%
    private int maxProgress = 100; // 최대 달성률 100%

    // 싱글톤 패턴으로 ProgressManager를 다른 스크립트에서 쉽게 참조할 수 있게 함
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

    // 적이 죽을 때 호출되어 달성률을 1% 증가시킴
    public void IncrementProgress()
    {
        if (progressPercentage < maxProgress)
        {
            progressPercentage += 1; // 1% 증가

            UpdateProgressUI();

            if (progressPercentage >= maxProgress)
            {
                GameManager.instance.SetGameOver(); // 달성률이 100%가 되면 게임 오버
            }
        }
    }

    // TextMesh Pro UI에 현재 달성률을 업데이트
    void UpdateProgressUI()
    {
        progressText.SetText(progressPercentage.ToString() + "%");
    }
}
