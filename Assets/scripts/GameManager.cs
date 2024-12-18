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
    private GameObject startScreen; // ���� ȭ�� �г�
    [SerializeField]
    private GameObject playButton; // Play ��ư
    [SerializeField]
    private GameObject dialogueScreen; // ��ȭ ȭ�� �г�
    [SerializeField]
    private TextMeshProUGUI buttonText; // ��ư �ؽ�Ʈ

    [HideInInspector]
    public bool isGameStarted = false; // ���� ���� ���� üũ

    [HideInInspector]
    public bool isGameOver = false;

    private string[] dialogues = {
        "��� ����: �̺�, ���� �װ� Į���̶�� �ϰ� �ֳ�?",
        "�ʺ� ����: �˼��մϴ� ������, ������ �ϰڽ��ϴ�.",
        "��� ����: ������ �ؼ� �� ���̸� �����￡ ������. �������� Ư���̴�.",
        "�ʺ� ����: ��? Ư���̿�?",
        "��� ����: �� �����̾Ʈ�� ��. �پ��� �������� ������ ������. �װ� �װ� ���� ȥ�� ������ �ž�.",
        "�ʺ� ����: ���� ���� ȥ�ڼ� �ؿ�?",
        "��� ����: ���ϴ°� ���� ����, �̽��� ��Ÿ ���� �ǰ� �ʹ���? �׷� ���� ��.",
        "��� ����: Į���� �丮�� �⺻�̾�. ���� ������ ���� ���� �ڰ��� ���� �Ŵ�. ��Ƽ�� �Ǵϱ� ���ߺ�.",
        "�ʺ� ����: ���� �ϸ� ���� ���� ��� ����, �ݵ�� �����ϰڽ��ϴ� ����!"
    }; // ��ȭ ������

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
        Time.timeScale = 0; // ���� ���� �� ���� ����
        ShowStartScreen();
    }

    // ���� ȭ�� Ȱ��ȭ
    void ShowStartScreen()
    {
        startScreen.SetActive(true);
        dialogueScreen.SetActive(false);
    }

    // Play ��ư Ŭ�� �� ȣ��
    public void StartGame()
    {
        startScreen.SetActive(false);
        ShowDialogueScreen(); // ��ȭ ����
    }
    void ShowDialogueScreen()
    {
        dialogueScreen.SetActive(true);
        UpdateButtonText(); // ù ��° ��ȭ ǥ��
    }
    public void NextDialogue()
    {
        dialogueIndex++;
        if (dialogueIndex < dialogues.Length)
        {
            UpdateButtonText(); // ���� ��ȭ ǥ��
        }
        else
        {
            EndDialogue(); // ��ȭ ���� �� ���� ����
        }
    }

    void UpdateButtonText()
    {
        buttonText.text = dialogues[dialogueIndex]; // ��ư �ؽ�Ʈ ������Ʈ
    }

    void EndDialogue()
    {
        dialogueScreen.SetActive(false);
        Time.timeScale = 1; // ���� �簳
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

        // ��� �� ����
        DestroyAllEnemies();

        // �� ���� �ߴ�
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopEnemyRoutine();
        }

        // ���� ���� �г� Ȱ��ȭ
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

    // ���� ���� ��� Enemy ������Ʈ ����
    public void DestroyAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); // �� �� ��� Enemy ������Ʈ ã��
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject); // �� Enemy ������Ʈ ����
        }
    }
    public void ExitGame()
    {
        Debug.Log("Exit button clicked. Quitting the game."); // Unity Editor Ȯ�ο�
        Application.Quit(); // ���� ����
    }
}
