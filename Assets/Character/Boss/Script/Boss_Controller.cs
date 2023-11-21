using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Boss_Controller : MonoBehaviour
{
    GameObject player;
    CharacterStats characterStats;
    Rigidbody2D rb;
    Animator anim;
    Transform C_tra;
    Transform Mouth;
    public int FireBallAttackTime;
    public GameObject Fireball;//火球的預置物
    Vector2 Initial;

    public bool isFlipped = false;
    public GameObject explosionEffect;
    public GameObject RushDistance_L;
    public GameObject RushDistance_R;
    public GameObject ball;
    public PlayableDirector playableDirector;



    [Header("JumpAttack")]
    public LayerMask groundLayer;
    public float jumpHeight;
    public BoxCollider2D groundCheack;
    public Vector2 boxSize;
    public float attackRange;


    [Header("Boss_Stats")]
    string[] bossStats = { "Walk", "Jump"};
    string[] bossAngryStats = { "Angry_Walk", "Angry_Jump", "Angry_Rush" };

    public bool isDied;
    bool isJump;
    bool playOnce;
    public bool isEvent;
    public bool playerisDied;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheack = GetComponentInChildren<BoxCollider2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        characterStats = GetComponent<CharacterStats>();
        player.GetComponent<Player_Control>().isEventActive = false;

        C_tra = GetComponent<Transform>();
        Mouth = GameObject.FindGameObjectWithTag("BossHand").transform;
        Initial = C_tra.localScale;
        FireBallAttackTime = 3;
        print(Mouth);

        AudioManager.PlayBossMusiclAudio();
    }

    private void Update()
    {       
        Boss_Died();  
        Player_Died();
        SwitchAnim();
    }

    void SwitchAnim()
    {
        anim.SetBool("isIdle", false);

        if (anim.GetBool("isJump"))
        {
            this.gameObject.layer = 21;
            if(rb.velocity.y < 0)
            {
                anim.SetBool("isJump", false);
                anim.SetBool("isFall", true);
            }
        }else if (groundCheack.IsTouchingLayers(groundLayer))
        {
            anim.SetBool("isFall", false);
            anim.SetBool("isIdle", true);
            this.gameObject.layer = 17;
        }

        if(characterStats.CurrentHealth <= characterStats.MaxHealth / 2 && !anim.GetBool("isAngry"))
        {
            player.GetComponent<Player_Control>().isEventActive = true;
            isEvent = true;
            playableDirector.Play();
            anim.SetBool("isWalk", false);
            anim.SetBool("isFall", false);
            anim.SetBool("isIdle", true);
            anim.enabled = false;
            player.GetComponent<Animator>().SetFloat("speed", 0f);
            player.GetComponent<Animator>().SetFloat("run", 0f);
        }

    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.transform.position.x - transform.position.x;

        if(player.transform.position.x > transform.position.x)
        {
            distanceFromPlayer -= attackRange;
        }else if(player.transform.position.x < transform.position.x)
        {
            distanceFromPlayer += attackRange;
        }

        if (groundCheack.IsTouchingLayers(groundLayer))
        {
            rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
        }        
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.transform.position.x  && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x <player.transform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void RadomStats()
    {
        var playerisDied = player.GetComponent<Player_Control>().isDead;

        if (!isDied)
        {
            if (!playerisDied)
            {
                var bossStatsElement = bossStats[Random.Range(0, bossStats.Length)];

                if (bossStatsElement == "Jump")
                {
                    LookAtPlayer();
                    anim.SetBool("isJump", true);
                    JumpAttack();
                }

                if (bossStatsElement == "Walk")
                {
                    anim.SetBool("isWalk", true);
                }
            }
        }        
    }

    public void RadomStatsAngryType()
    {
        var playerisDied = player.GetComponent<Player_Control>().isDead;

        if (!isEvent)
        {
            if (!isDied)
            {
                if (!playerisDied)
                {
                    var bossStatsElement = bossAngryStats[Random.Range(0, bossAngryStats.Length)];

                    if (bossStatsElement == "Angry_Jump")
                    {
                        LookAtPlayer();
                        anim.SetBool("isJump", true);
                        JumpAttack();
                    }

                    if (bossStatsElement == "Angry_Walk")
                    {
                        anim.SetBool("isWalk", true);
                    }

                    if (bossStatsElement == "Angry_Rush")
                    {
                        anim.SetBool("isRun", true);
                    }                    
                }
            }            
        }
        
    }

    void Boss_Died()
    {
        if(characterStats.CurrentHealth <= 0)
        {
            isDied = true;
            anim.SetBool("isDied", true);
            StartCoroutine(BossDied());
        }
    }

    void Player_Died()
    {
        var playerisDied = player.GetComponent<Player_Control>().isDead;

        if (playerisDied && !playOnce)
        {
            AudioManager.BossKillPlayerAudio();
            playOnce = true;
            print("02");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttackArea"))
        {
            var targetStates = player.GetComponent<CharacterStats>();

            AudioManager.BossGetHurtAudio();
            //Instantiate(bloodEffect, transform.position, Quaternion.identity);

            //getHitTimes--;

            characterStats.TakeDamage(targetStates, characterStats);
        }

        if (collision.CompareTag("FireBall"))
        {
            var targerStates = player.GetComponent<CharacterStats>();

            //Instantiate(bloodEffect, transform.position, Quaternion.identity);
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            characterStats.MagicDamage(targerStates, characterStats);
            Destroy(collision.gameObject);
            CameraShake.Instance.ShakeCamera(5f, 0.1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.blue;
        //Gizmos.DrawCube(groundCheack.position, boxSize);
    }

    public IEnumerator BossDied()
    {        
        yield return new WaitForSeconds(5f);        
        Destroy(gameObject, 5f);
        SceneController.Instance.GoToNextScene();
    }

   public void fireballcreate(){
        for (int i = 0; i < 5; i++)
  {
       int r = Random.Range(-9, 25);//这里的范围是平台的长度
       GameObject ball2 = Instantiate(ball, null);
       ball2.transform.position = new Vector3(r, 6, 0);
  } 
    }

    public void FireBallCreate() //生成火球
    {
        if (C_tra.localScale.x == Initial.x)
        {
            for (int i = -5; i < 0; i++)
            {
                GameObject fireball = Instantiate(Fireball, null);
                Vector3 dir = Quaternion.Euler(0, i * 15, 0) * -transform.right;
                fireball.transform.position = Mouth.position + dir * 1.0f;
                fireball.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }
        }
        else if (C_tra.localScale.x == -Initial.x)
        {
            for (int i = -1; i < 0; i++)
            {
                GameObject fireball = Instantiate(Fireball, null);
                Vector3 dir = Quaternion.Euler(0, i * 15, 0) * transform.right;
                fireball.transform.position = Mouth.position + dir * 1.0f;
                fireball.transform.rotation = Quaternion.Euler(0, 0, i * 15);
            }
        }
        FireBallAttackTime -= 1;
    }



}
