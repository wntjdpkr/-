using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanal;

    private int coin = 0;

    [SerializeField]
    private GameObject startScreen; // 시작 화면 패널
    [SerializeField]
    private GameObject playButton; // Play 버튼
    [SerializeField]
    private GameObject dialogueScreen; // 대화 화면 패널
    [SerializeField]
    private TextMeshProUGUI buttonText; // 버튼 텍스트

    [HideInInspector]
    public bool isGameStarted = false; // 게임 시작 여부 체크

    [HideInInspector]
    public bool isGameOver = false;

    private string[] dialogues = {
        "헤드 쉐프: 이봐, 지금 그걸 칼질이라고 하고 있나?",
        "초보 쉐프: 죄송합니다 쉐프님, 열심히 하겠습니다.",
        "헤드 쉐프: 열심히 해서 될 일이면 애진즉에 고쳤지. 이제부터 특훈이다.",
        "초보 쉐프: 예? 특훈이요?",
        "헤드 쉐프: 저 컨베이어벨트를 봐. 다양한 식재료들이 끝없이 나오지. 네가 그걸 전부 혼자 손질할 거야.",
        "초보 쉐프: 저걸 제가 혼자서 해요?",
        "헤드 쉐프: 못하는게 말이 많네, 미슐랭 스타 쉐프 되고 싶댔지? 그럼 가서 해.",
        "헤드 쉐프: 칼질은 요리의 기본이야. 저걸 끝내면 팬을 잡을 자격이 생길 거다. 버티면 되니까 버텨봐.",
        "초보 쉐프: 저걸 하면 드디어 팬을 잡는 군요, 반드시 성공하겠습니다 쉐프!"
    }; // 대화 데이터

    private int dialogueIndex = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 0; // 게임 시작 전 멈춘 상태
        ShowStartScreen();
    }

    // 시작 화면 활성화
    void ShowStartScreen()
    {
        startScreen.SetActive(true);
        dialogueScreen.SetActive(false);
    }

    // Play 버튼 클릭 시 호출
    public void StartGame()
    {
        startScreen.SetActive(false);
        ShowDialogueScreen(); // 대화 시작
    }
    void ShowDialogueScreen()
    {
        dialogueScreen.SetActive(true);
        UpdateButtonText(); // 첫 번째 대화 표시
    }
    public void NextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogues.Length)
        {
            UpdateButtonText(); // 다음 대화 표시
        }
        else
        {
            EndDialogue(); // 대화 종료 후 게임 시작
        }
    }

    void UpdateButtonText()
    {
        buttonText.text = dialogues[dialogueIndex]; // 버튼 텍스트 업데이트
    }

    void EndDialogue()
    {
        dialogueScreen.SetActive(false);
        Time.timeScale = 1; // 게임 재개
        isGameStarted = true;
    }
    public void IncreaseCoin()
    {
        coin += 1;
        text.SetText(coin.ToString());

        if (coin % 40 == 0)
        {
            player player = FindObjectOfType<player>();
            if (player != null)
            {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;

        // 모든 적 제거
        DestroyAllEnemies();

        // 적 생성 중단
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopEnemyRoutine();
        }

        // 게임 오버 패널 활성화
        Invoke("ShowGameOverPanal", 0.5f);
    }

    void ShowGameOverPanal()
    {
        gameOverPanal.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // 현재 씬의 모든 Enemy 오브젝트 제거
    public void DestroyAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // 씬 내 모든 Enemy 오브젝트 찾기
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject); // 각 Enemy 오브젝트 제거
        }
    }
    public void ExitGame()
    {
        Debug.Log("Exit button clicked. Quitting the game."); // Unity Editor 확인용
        Application.Quit(); // 게임 종료
    }
}
