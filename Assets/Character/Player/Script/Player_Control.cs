using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Control : MonoBehaviour
{
    #region Private Variable
    Rigidbody2D myrig;
    CapsuleCollider2D mycoll;
    CharacterStats characterStats;

    Animator myanim;
    Animator attackAnim;
    Animator deadAnim;
    
    GameObject enemyTarget;
    GameObject playerSpriteObj;
    GameObject attackAnimObj;
    GameObject deadAnimObj;


    //bool isKnockback;
    bool isHurt;
    bool playAudioOnce;

    //按鍵設置    
    bool jumpPressed;
    bool run;
    #endregion

    #region Public Variable
    [Header("角色移動")]
    public float basicSpeed = 5f; //角色基本速度
    public float maxSpeed = 8f; //角色最高速度
    public float speed; //角色速度
    bool canMove = true;

    [Header("跳躍參數")]
    public float jumpForce = 3f; //角色跳躍力度
    public int jumpExtra;
    private float jumpCost = 5f;

    [Header("角色狀態")]
    public bool isOnGround; //角色是否站在地上
    public bool isRun; //角色是否正在跑步
    public bool canRun = true;
    public bool isDead = false;
    public bool isEventActive;
    private float runCost = 15f;

    [Header("Combo系統")]
    public int combo;
    public bool attackCanDo;

    [Header("環境檢測")]
    public LayerMask groundLayer; //檢測地面圖層

    [Header("耐力系統")]
    public float currentStamina;

    [Header("火球技能")]
    public float cooldownTime = 3f;    
    public GameObject fireBall;
    public Transform shootPoint;
    float fireballTimer = 0f;
    public bool startFireballCoolDown = false;
    bool doOnce;

    public float magicMove = 0.3f;
    public bool magicmoveCD = false;

    public float move;
    #endregion

    void Start()
    {
        myrig = GetComponent<Rigidbody2D>();
        mycoll = GetComponent<CapsuleCollider2D>();
        myanim = GetComponent<Animator>();
        characterStats = GetComponent<CharacterStats>();

        attackAnimObj = GameObject.FindGameObjectWithTag("PlayerAttackAnim");
        attackAnim = attackAnimObj.GetComponent<Animator>();

        deadAnimObj = GameObject.FindGameObjectWithTag("PlayerDeadAnim");
        deadAnim = deadAnimObj.GetComponent<Animator>();

        playerSpriteObj = GameObject.FindGameObjectWithTag("PlayerSprite");
        
        speed = basicSpeed;
        canRun = true;

        GameManager.Instance.RigisterPlayer(characterStats);
    }    
    void Update()
    {
        isDead = characterStats.characterData.currentHealth == 0;
        currentStamina = Stamina_Control.Instance.currentStamina;

        if (!isDead)
        {
            if (!isEventActive)
            {                
                Player_Jump();
                Player_Run();
                PlayerRunCost();
                Player_Attack();
                Switch();
                Player_MagicAttack();                
                UseHpPotion();
                UseStaminaPotion();
                Player_SwitchMoveAnimation();
            }
            Player_SwitchJumpAnimation();
        }
        else if (isDead)
        {
            PlayerDead();
        }
        
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (!isEventActive)
            {
                if (canMove)
                {
                    if (!isHurt)
                    {
                        Player_Move();
                    }                    
                }
            }
        }                        
        PhysicsCheck();
    }

    void PhysicsCheck()
    {
        if (mycoll.IsTouchingLayers(groundLayer))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    void Player_SwitchMoveAnimation()
    {
        myanim.SetFloat("speed", Mathf.Abs(move));
        myanim.SetFloat("run", Mathf.Abs(myrig.velocity.x));

    }
    void Player_SwitchJumpAnimation()
    {

        if (myanim.GetBool("jumpping"))
        {
            myanim.SetBool("idle", false);

            if (myrig.velocity.y < 0)
            {
                myanim.SetBool("jumpping", false);
                myanim.SetBool("falling", true);
            }
        }

        if (myanim.GetBool("falling") && isOnGround)
        {
            myanim.SetBool("falling", false);
            myanim.SetBool("idle", true);
        }
    }

    #region Player Movement

    void Player_Move()
    {
        move = Input.GetAxis("Horizontal");

        myrig.velocity = new Vector2( move * speed, myrig.velocity.y);

        Player_Face();
    }
    void Player_Run()
    {
        run = Input.GetKey(KeyCode.LeftShift);

        if (run && canRun)
        {
            speed = maxSpeed; 
        }
        else
        {
            speed = basicSpeed;
        }

        if(currentStamina > 15)
        {
            canRun = true;
        }
        else if(currentStamina <= 15)
        {
            speed = basicSpeed;
            canRun = false;
        }
    }   

    void PlayerRunCost()
    {
        if (Mathf.Abs(move) > 0 && speed == maxSpeed)
        {
            Stamina_Control.Instance.RunStamina(runCost);
        }
    }

    void Player_Face()
    {
        if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Player_Jump()
    {
        jumpPressed = Input.GetKeyDown(KeyCode.Space);

        if (isOnGround)
        {
            jumpExtra = 1;
        }

        if (jumpPressed && jumpExtra > 0)
        {            
            myrig.velocity = Vector2.up * jumpForce;
            jumpExtra--;
            myanim.SetBool("jumpping", true);            
        }
        
        if(currentStamina >= jumpCost)
        {
            if (jumpPressed && jumpExtra == 0 && !isOnGround)
            {
                JumpCost();
                myrig.velocity = Vector2.up * jumpForce;
                myanim.SetBool("jumpping", true);
                jumpExtra = -1;
            }
        }
        else if(currentStamina <= jumpCost)
        {
            jumpExtra = -5;
        }

    }
    void JumpCost()
    {
        Stamina_Control.Instance.UseStamina(jumpCost);
    }

    void PlayerDead()
    {
        playerSpriteObj.SetActive(false);
        deadAnimObj.GetComponent<SpriteRenderer>().enabled =true;
        deadAnim.SetBool("isDead",true);
        myanim.enabled = false;        
        GameManager.Instance.NotifyObservers();
        if (!playAudioOnce)
        {
            AudioManager.PlayPlayerDiedMusicAudio();
            playAudioOnce = true;
        }
        //Destroy(myrig);
        //gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        this.gameObject.layer = 18;
        
    }
    #endregion    
        
    #region Combo System

    public void Player_MagicAttack()
    {      

        if(Input.GetKeyDown(KeyCode.Q) && currentStamina >= 20 && !startFireballCoolDown && SceneController.Instance.canUseFireBall)
        {
            myanim.SetTrigger("MagicAtt");
            startFireballCoolDown = true;
            magicmoveCD = true;
            doOnce = true;
            canMove = false;
        }
        if (magicmoveCD)
        {
            magicMove -= Time.deltaTime;

            if(magicMove < 0f)
            {
                canMove = true;
                magicmoveCD = false;
                magicMove = 0.3f;
            }
        }
        if (startFireballCoolDown)
        {            
            fireballTimer += Time.deltaTime;

            if (doOnce)
            {
                doOnce = false;
                AudioManager.FireBallAudio();
                GameObject fireBall_ = Instantiate(fireBall, shootPoint.position, transform.rotation);
                Stamina_Control.Instance.UseStamina(20);
                fireBall_.GetComponent<FireBall>().face = transform.localScale.x;
                fireBall_.transform.localScale = new Vector3(transform.localScale.x, fireBall.transform.localScale.y, fireBall_.transform.localScale.z);
            }         

            if (fireballTimer >= cooldownTime)
            {
                fireballTimer = 0f;
                startFireballCoolDown = false;
            }
        }
    }
    public void Player_Attack()
    {
        if(Input.GetMouseButtonDown(0) && !attackCanDo)
        {
            if (isOnGround)
            {
                move = 0;
                if (Mathf.Abs(move) < 0.1f && currentStamina >= 10)
                {                    
                    canMove = false;
                    attackCanDo = true;
                    attackAnim.SetTrigger("" + combo);
                    Stamina_Control.Instance.UseStamina(10);
                }
            }
        }
    }
    
    public  void Start_Combo()
    {
        attackCanDo = false;
        if (combo < 3)
        {
            playerSpriteObj.SetActive(false);
            characterStats.isCritical = Random.value < characterStats.attackData.criticalChance;         
            combo++;
        }
    }

    public void AbsoluteCrit()
    {
        characterStats.attackData.criticalChance = 1f;
        characterStats.isCritical = Random.value < characterStats.attackData.criticalChance;
    }

    public void Finish_Animation()
    {
        attackCanDo = false;
        combo = 0;
        canMove = true;
        characterStats.attackData.criticalChance = 0.2f;
        playerSpriteObj.SetActive(true);
    }
    public void Hurt(Collider2D collision)//受傷代碼入口
    {
        if (transform.position.x < collision.gameObject.transform.position.x)
        {
            myrig.velocity = new Vector2(-10, myrig.velocity.y);//反彈
            isHurt = true;
        }
        else
        {
            myrig.velocity = new Vector2(10, myrig.velocity.y);//反彈
            isHurt = true;
        }
    }



    private void HurtByCollision(Collision2D collision)//受傷代碼入口
    {
        if (transform.position.x < collision.gameObject.transform.position.x)
        {
            myrig.velocity = new Vector2(-5, 5);//反彈
            isHurt = true;
        }
        else
        {
            myrig.velocity = new Vector2(5, 5);//反彈
            isHurt = true;
        }
    }
    void Switch()
    {
        if (isHurt)
        {
            if (Mathf.Abs(myrig.velocity.x) < 0.1f)
            {
                isHurt = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("EnemyAttackArea"))
        {
            enemyTarget = GameObject.FindGameObjectWithTag("Enemy");

            var targetStates = enemyTarget.GetComponent<CharacterStats>();

            myanim.SetTrigger("getHit");
            AudioManager.PlayerGetHitAudio();

            characterStats.TakeDamage(targetStates, characterStats);

            Hurt(collision);
        }

        if (collision.CompareTag("BossAttackArea"))
        {
            enemyTarget = GameObject.FindGameObjectWithTag("Boss");

            var targetStats = enemyTarget.GetComponent<CharacterStats>();

            myanim.SetTrigger("getHit");
            AudioManager.PlayerGetHitAudio();

            characterStats.TakeDamage(targetStats, characterStats);
            
            Hurt(collision);
        }

        if (collision.CompareTag("BossFireball"))
        {
            enemyTarget = GameObject.FindGameObjectWithTag("Boss");

            var targetStats = enemyTarget.GetComponent<CharacterStats>();

            myanim.SetTrigger("getHit");
            AudioManager.PlayerGetHitAudio();

            characterStats.TakeDamage(targetStats, characterStats);

            Hurt(collision);
        }



        if (collision.CompareTag("WolfAttackArea"))
        {
            AudioManager.PlayerGetHitAudio();
            characterStats.CurrentHealth = 0f;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "Enemy")
        {
            enemyTarget = GameObject.FindGameObjectWithTag("Enemy");
            
            var target = enemyTarget.GetComponent<CharacterStats>();

            //myanim.SetTrigger("getHit");
            //AudioManager.PlayerGetHitAudio();

            //characterStats.CurrentHealth -= target.attackData.collisionDamge;

            HurtByCollision(collision);

        }

        if(collision.collider.tag == "Wolf")
        {
            characterStats.CurrentHealth = 0f;
        }
    }

    public void GetHurtLayer()
    {
        this.gameObject.layer = 18;
    }

    public void GetHurtLayerFinish()
    {
        this.gameObject.layer = 8;
    }
    #endregion

    #region Audio System

    public void StepAudio()
    {
        AudioManager.PlayFootStepAudio();
    }

    public void RunAudio()
    {
        AudioManager.PlayRunStepAudio();
    }

    #endregion

    void UseHpPotion()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && ItemManager.Instance.hpPotion_Amount > 0)
        {
            if(characterStats.CurrentHealth < characterStats.MaxHealth)
            {
                ItemManager.Instance.hpPotion_Amount -= 1;
                characterStats.CurrentHealth += 10f;
                if(characterStats.CurrentHealth > 100f)
                {
                    characterStats.CurrentHealth = 100f;
                }
            }
            else
            {
                print("無法使用");
            }            
        }
    }
    void UseStaminaPotion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && ItemManager.Instance.staminaPotion_Amount > 0)
        {
            if (Stamina_Control.Instance.currentStamina < Stamina_Control.Instance.maxStamina)
            {
                ItemManager.Instance.staminaPotion_Amount -= 1;
                Stamina_Control.Instance.currentStamina += 15f;
                if (Stamina_Control.Instance.currentStamina > 100f)
                {
                    Stamina_Control.Instance.currentStamina = 100f;
                }
            }
            else
            {
                print("無法使用");
            }
        }
    }
}
