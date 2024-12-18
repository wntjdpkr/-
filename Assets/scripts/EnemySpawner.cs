using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject boss;
    private float[] arrPosY = { 3.6f, 1.6f, -0.4f, -2.4f };
    // Start is called before the first frame update
    [SerializeField]
    private float spawnInterval = 1.5f;
    void Start()
    {
        StartEnemyRoutine();
    }
    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }
    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(2f);

        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;

        while (true)
        {
            foreach (float posY in arrPosY)
            {

                SpawnEnemy(posY, enemyIndex, moveSpeed);
            }
            spawnCount += 1;
            if (spawnCount % 10 == 0)
            {
                enemyIndex += 1;
                moveSpeed += 2;
            }
            if (enemyIndex >= enemies.Length)
            {
                SpawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }

    }
    // Update is called once per frame
    void SpawnEnemy(float posY, int index, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(transform.position.x, posY, transform.position.z);

        if (Random.Range(0, 5) == 0)
        {
            index += 1;
        }
        if (index >= enemies.Length)
        {
            index = enemies.Length - 1;
        }

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }
    void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}