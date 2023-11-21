using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,IEndGameObserver
{
    #region Public Variables
     //攻擊範圍
    public float moveSpeed;
    public float speed;
    public float maxSpeed;
    public int getHitTimes = 0;
    public bool getHit;
    public bool playerIsDead;
    public float timer; //攻擊冷卻
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //檢查玩家是否在範圍內
    public GameObject hotZone;
    public GameObject triggerArea;
    public GameObject Nomove;
    public GameObject bloodEffect;
    public GameObject explosionEffect;
    #endregion

    #region Private Variables
    private Animator anim;
    private CharacterStats characterStats;
    private GameObject playerTarget;
    private Rigidbody2D rigi;

    private float attackDistance;
    private float distance; //儲存敵人與玩家之間的距離        
    private bool cooling; //檢查敵人冷卻時間
    private bool attackMode;
    private bool isDead;
    private float intTimer;
    #endregion

    private void Start()
    {
        SelectTarget();
        intTimer = timer; //儲存初始時間
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        characterStats =GetComponent<CharacterStats>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        GameManager.Instance.AddObserver(this);

        attackDistance = characterStats.attackData.attackRange;

    }

    /*private void OnEnable()
    {
        GameManager.Instance.AddObserver(this);
    }*/

    private void OnDisable()
    {
        if (!GameManager.IsInitialized) return;
        GameManager.Instance.RemoveObserver(this);
    }

    void Update()
    {
        if(characterStats.CurrentHealth <= 0)
        {
            isDead = true;
            Destroy(rigi);
            Destroy(Nomove);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            triggerArea.SetActive(false);
            hotZone.SetActive(false);
            anim.SetBool("attack", false);
            anim.SetBool("isDead", true);
            Destroy(gameObject, 1.5f);
        }

        if (!isDead)
        {
            if (!getHit)
            {
                if (!attackMode)
                {
                    Move();
                }

                if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("ANIME_Zombie_ATK"))
                {
                    SelectTarget();
                }

                if (inRange)
                {
                    EnemyLogic();
                }
            }
        }
        

        

        //ResetGetHit();
    }


    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackDistance)
        {
            //StopAttack();
        }
        else if(attackDistance >= distance && !cooling)
        {
            if (!playerIsDead)
            {
                Attack();
            }
            
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("ANIME_Zombie_ATK"))
        {
            if (inRange)
            {
                speed = maxSpeed;
            }
            else
            {
                speed = moveSpeed;
            }

            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //當玩家進入範圍，重新設定計時器
        attackMode = true; //檢查敵人是否能夠進行攻擊

        anim.SetBool("canWalk", false);
        anim.SetBool("attack", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttackArea"))
        {
            var targetStates = playerTarget.GetComponent<CharacterStats>();

            anim.SetTrigger("getHit");
            AudioManager.EnemyGetHitAudio();
            Instantiate(bloodEffect, transform.position, Quaternion.identity);

            //getHitTimes--;

            characterStats.TakeDamage(targetStates, characterStats);
        }

        if (collision.CompareTag("FireBall"))
        {
            var targerStates = playerTarget.GetComponent<CharacterStats>();
            anim.SetTrigger("getHit");
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);            
            characterStats.MagicDamage(targerStates, characterStats);
            Destroy(collision.gameObject);
            CameraShake.Instance.ShakeCamera(5f, 0.1f);
        }
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    public void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("attack", false);
    }

  
    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.transform.position.x)
        {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }

        transform.eulerAngles = rotation;
    }

    public void StopActive()
    {
        getHit = true;
    }

    public void StartActive()
    {
        getHit = false;
    }

   public void EndNotify()
    {
        //停止敵人追擊
        playerIsDead = true;
        StopAttack();
    }

    public void PlayEnemyAttack()
    {
        AudioManager.EnemyAttackAudio();
    }

    public void PlayZombieGetHurtAudio()
    {
        AudioManager.ZombieGetHurtAudio();
    }

    public void PlayZombieYellAudio()
    {
        AudioManager.ZombieYellAudio();
    }

    public void PlayZombieDeadAudio()
    {
        AudioManager.ZombieDeadAudio();
    }

}
