using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float deactivateTimer = 10f;
    public float speed = 5f;
    public float maxY;
    public float minY;

    public Animator animator;

    [SerializeField] private GameObject EnemyBullet;
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private Transform enemyAttackPoint;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Transform enemySpawnPoint2;
    [SerializeField] private Transform enemySpawnPoint3;
    
    public float attackTimer = 3f;
    private float _currentAttackTimer;
    private bool _canAttack;

    public float directionTimer = 4f;
    private float _currentDirectionTimer;
    private float _rnd;
    private static readonly int Destroyed = Animator.StringToHash("Destroyed");

    void Start()
    {
        Invoke(nameof(DeactivateGameObject), deactivateTimer);
        EnemyPrefab.GetComponent<Collider2D>().enabled = true;
        if (ScoreManager.Score == 400)
        {
            Instantiate(EnemyPrefab, enemySpawnPoint2.position, Quaternion.identity);
            ScoreManager.Instance.SetScore(1);
        }
        if (ScoreManager.Score == 601)
        {
            Instantiate(EnemyPrefab, enemySpawnPoint3.position, Quaternion.identity);
            ScoreManager.Instance.SetScore(1);
        }
    }

    private void Update()
    {
        MoveEnemy();
        AttackEnemy();
    }

    private void MoveEnemy()
    {
        Vector3 temp1 = transform.position;
        temp1.x -= speed * Time.deltaTime;
        transform.position = temp1;
        _currentDirectionTimer += Time.deltaTime;
        if (_currentDirectionTimer > directionTimer)
        {
            _currentDirectionTimer = 0f;
            _rnd = Random.Range(-1, 3);
        }
        
        if (_rnd > 0)
        {
            var temp = transform.position;
            temp.y += speed * Time.deltaTime;
            if (temp.y > maxY)
            {
                temp.y = maxY;
                _rnd = -1;
            }
            transform.position = temp;
        }
        if (_rnd <= 0)
        {
            var temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            if (temp.y < minY)
            {
                temp.y = minY;
                _rnd = 1;
            }
            transform.position = temp;
        }

    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.6f);
            animator.SetBool(Destroyed, true);
            EnemyPrefab.GetComponent<Collider2D>().enabled = false;
            ScoreManager.Instance.SetScore(100);
            if (ScoreManager.Score < 1302)
            {
                Instantiate(EnemyPrefab, enemySpawnPoint.position, Quaternion.identity);
            }
        }
    }

    void AttackEnemy()
    {
        _currentAttackTimer += Time.deltaTime;
        if (_currentAttackTimer > attackTimer)
        {
            _canAttack = true;
        }
        if (_canAttack)
        {
            _canAttack = false;
            _currentAttackTimer = 0f;
            Instantiate(EnemyBullet, enemyAttackPoint.position, Quaternion.identity);
        }
    }
    
    void DeactivateGameObject()
    {
        if (ScoreManager.Score != 1302)
        {
            Instantiate(EnemyPrefab, enemySpawnPoint.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
