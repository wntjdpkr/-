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
    private float horizontalMoveSpeed = 1.3f; // �߰��� ���� �̵� �ӵ�
    private float gravity = 2.4f;  // �߰��� �߷� ȿ��

    private FailManager failManager; // FailManager ��ũ��Ʈ�� ���� ����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // �ʱ⿣ �߷��� �������� ����

        failManager = FindObjectOfType<FailManager>();
    }

    void Update()
    {
        if (!isAffectedByGravity)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            if (transform.position.x < -1.9f) // Ư�� ������ �������� ��
            {
                isAffectedByGravity = true;  // �߷��� ������ �޵��� ����
                rb.gravityScale = gravity; // �߷� Ȱ��ȭ
                rb.velocity = new Vector2(-horizontalMoveSpeed, rb.velocity.y); // ���� �̵� �߰�
            }
        }
        else
        {
            // �������� �׸��� �������� �ڿ������� ������ ó��
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
        else if (other.gameObject.tag == "Basket") // Basket�� �浹 ��
        {
            Destroy(gameObject); // �� ����
            if (failManager != null) // FailManager�� null�� �ƴ� ���
            {
                failManager.IncreaseFailRate(); // FailManager�� IncreaseFailRate() �޼��� ȣ��
            }
        }
    }

}

