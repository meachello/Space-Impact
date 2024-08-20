using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float maxY;
    public float minY;
    
    [SerializeField] private GameObject PlayerBullet;
    [SerializeField] private Transform attackPoint;
    
    public float attackTimer = 0.35f;
    private float _currentAttackTimer;
    private bool _canAttack;
    void Start()
    {
        _currentAttackTimer = attackTimer;
    }

    private void Update()
    {
        MovePlayer();
        AttackPlayer();
    }

    private void MovePlayer()
    {
        if (Input.GetAxisRaw("Vertical") > 0f)
        {
            var temp = transform.position;
            temp.y += speed * Time.deltaTime;
            if (temp.y > maxY)
            {
                temp.y = maxY;
            }
            transform.position = temp;
        }
        if (Input.GetAxisRaw("Vertical") < 0f)
        {
            var temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            if (temp.y < minY)
            {
                temp.y = minY;
            }
            transform.position = temp;
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Enemy"))
        {
            Hearts.HealthPoints--;
        }
    }

    void AttackPlayer()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > _currentAttackTimer)
        {
            _canAttack = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canAttack)
            {
                _canAttack = false;
                attackTimer = 0f;
                Instantiate(PlayerBullet, attackPoint.position, Quaternion.identity);
            }
            
        }
    }
}
