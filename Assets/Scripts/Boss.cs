using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Boss : MonoBehaviour
{
    public float speed = 2f;
    public float maxY;
    public float minY;
    private const float PosX = 7.27f;

    private float _bossHp = 100;
    public Animator animator;
    [SerializeField] private TextMeshProUGUI bossHpText;
    
    [SerializeField] private GameObject BossBullet;
    [SerializeField] private GameObject BossCharge;
    [SerializeField] private Transform bossAttackPoint1;
    [SerializeField] private Transform bossAttackPoint2;
    [SerializeField] private Transform bossChargePoint1;
    [SerializeField] private Transform bossChargePoint2;
    
    public float attackTimer = 2f;
    public float chargeTimer = 7f;
    private float _currentAttackTimer;
    private float _currentChargeTimer;
    private bool _canAttack;
    private bool _canCharge;

    public float directionTimer = 4f;
    private float _currentDirectionTimer;
    private float _rnd;
    private static readonly int Destroyed = Animator.StringToHash("Destroyed");

    void Start()
    {
        if (_bossHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_bossHp <= 0)
        {
            Destroy(gameObject, 1.083f*2 );
            gameObject.GetComponent<Collider2D>().enabled = false;
            animator.SetBool(Destroyed, true);
            _canAttack = false;
            _canCharge = false;
            speed = 0f;
            attackTimer = 100f;
            chargeTimer = 100f;
        }
        MoveBoss();
        AttackBoss();
    }

    private void MoveBoss()
    {
        Vector3 temp1 = transform.position;
        temp1.x -= speed * Time.deltaTime;
        if (temp1.x <= PosX)
        {
            temp1.x = PosX;
        }
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

    void BossHpUpdating(float value)
    {
        _bossHp += value;
        bossHpText.text = $"Boss Hp:{_bossHp}";
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            BossHpUpdating(-5);
        }
    }

    void AttackBoss()
    {
        _currentAttackTimer += Time.deltaTime;
        _currentChargeTimer += Time.deltaTime;
        if (_currentAttackTimer > attackTimer)
        {
            _canAttack = true;
        }
        if (_canAttack)
        {
            _canAttack = false;
            _currentAttackTimer = 0f;
            Instantiate(BossBullet, bossAttackPoint1.position, Quaternion.identity);
            Instantiate(BossBullet, bossAttackPoint2.position, Quaternion.identity);
        }
        
        if (_currentChargeTimer > chargeTimer)
        {
            _canCharge = true;
        }
        if (_canCharge)
        {
            _canCharge = false;
            _currentChargeTimer = 0f;
            var random = Random.Range(-1, 3);
            if (random > 0)
            {
                Instantiate(BossCharge, bossChargePoint1.position, Quaternion.identity);
            }
            if (random <= 0)
            {
                Instantiate(BossCharge, bossChargePoint2.position, Quaternion.identity);
            }
        }
    }
    
}
