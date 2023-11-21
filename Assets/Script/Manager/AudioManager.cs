using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    #region Public Var
    [Header("?????")]
    public AudioClip ambientClip;
    public AudioClip musicClip;
    public AudioClip playerDiedMusicClip;
    public AudioClip wolfMusicClip;
    public AudioClip bossLevelClip;
    public AudioClip explosionClip;

    [Header("???a????")]
    public AudioClip[] walkStepClip;
    public AudioClip[] runStepClip;
    public AudioClip[] attackClip;
    public AudioClip[] fireBallClip;
    public AudioClip getHitClip;
    public AudioClip rotatingWeaponClip;

    [Header("??H????")]
    public AudioClip enemyAttackClip;
    public AudioClip enemyGetHitClip;

    public AudioClip[] zombieGetHurtClip;
    public AudioClip zombieYellClip;
    public AudioClip zombieDeadClip;

    public AudioClip wolfYellClip;
    public AudioClip wolfAttackClip;
    public AudioClip wolfDeadClip;

    [Header("Boss????")]
    public AudioClip bossYellClip;
    public AudioClip bossAttackClip;
    public AudioClip bossAttackSoundClip;
    public AudioClip[] bossGetHurtClip;
    public AudioClip bossDiedClip;
    public AudioClip bossKnockDownClip;
    public AudioClip bossThrowHelmetClip;
    public AudioClip bossKillPlayerClip;
    public AudioClip[] bossRushAttackYellClip;
    public AudioClip bossJumpAttackClip;
    public AudioClip[] bossDeadClip;

    [Header("??????")]
    public AudioSource sourse;
    public AudioClip hoverSFX;
    public AudioClip clickSFX;

    #endregion

    #region Private Var
    //BackGround_Audio_Source
    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource wolfMusicSource;
    AudioSource bossLevelSource;
    AudioSource explosionSource;
    AudioSource playerDiedMusicSource;

    //Player_Audio_Source
    AudioSource walkStepSource;
    AudioSource runStepSource;
    AudioSource attackSource;
    AudioSource fireBallSource;
    AudioSource getHitSource;
    AudioSource rotatingWeaponSource;

    //Enemy_Audio_Source
    AudioSource enemyAttackSource;
    AudioSource enemyGetHitSource;

    AudioSource zombieGetHurtSource;
    AudioSource zombieYellSource;
    AudioSource zombieDeadSource;

    AudioSource wolfYellSource;
    AudioSource wolfAttackSource;
    AudioSource wolfDeadSource;

    //Boss_Audio_Source
    AudioSource bossYellSource;
    AudioSource bossAttackSource;
    AudioSource bossAttackSoundSource;
    AudioSource bossGetHurtSource;
    AudioSource bossDIedSource;
    AudioSource bossKnockDownSource;
    AudioSource bossThrowHelemtSource;
    AudioSource bossKillPlayerSource;
    AudioSource bossRushAttaackYellSource;
    AudioSource bossJumpAttackSource;
    AudioSource bossDeadSource;

    //GameObject
    GameObject wolf;
    GameObject boss;
    
    #endregion

    //Bool
    bool keepFadeIn;
    bool keepFadeOut;
    bool isStop;
    bool playerDead;
    bool wolfAlive;
    bool bossAlive;
    bool playOnce;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        //BackGroundAudio
        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        playerDiedMusicSource = gameObject.AddComponent<AudioSource>();
        wolfMusicSource = gameObject.AddComponent<AudioSource>();
        bossLevelSource = gameObject.AddComponent<AudioSource>();
        explosionSource = gameObject.AddComponent<AudioSource>();

        //PlayerAudio
        walkStepSource = gameObject.AddComponent<AudioSource>();
        runStepSource = gameObject.AddComponent<AudioSource>();
        attackSource = gameObject.AddComponent<AudioSource>();
        fireBallSource = gameObject.AddComponent<AudioSource>();
        getHitSource = gameObject.AddComponent<AudioSource>();
        rotatingWeaponSource = gameObject.AddComponent<AudioSource>();

        //EnemyAudio
        enemyGetHitSource = gameObject.AddComponent<AudioSource>();
        enemyAttackSource = gameObject.AddComponent<AudioSource>();

        zombieGetHurtSource = gameObject.AddComponent<AudioSource>();
        zombieYellSource = gameObject.AddComponent<AudioSource>();
        zombieDeadSource = gameObject.AddComponent<AudioSource>();

        wolfYellSource = gameObject.AddComponent<AudioSource>();
        wolfAttackSource = gameObject.AddComponent<AudioSource>();
        wolfDeadSource = gameObject.AddComponent<AudioSource>();

        //BossAudio
        bossYellSource = gameObject.AddComponent<AudioSource>();
        bossAttackSource = gameObject.AddComponent<AudioSource>();
        bossAttackSoundSource = gameObject.AddComponent<AudioSource>();
        bossDIedSource = gameObject.AddComponent<AudioSource>();
        bossGetHurtSource = gameObject.AddComponent<AudioSource>();
        bossKnockDownSource = gameObject.AddComponent<AudioSource>();
        bossThrowHelemtSource = gameObject.AddComponent<AudioSource>();
        bossKillPlayerSource = gameObject.AddComponent<AudioSource>();
        bossRushAttaackYellSource = gameObject.AddComponent<AudioSource>();
        bossJumpAttackSource = gameObject.AddComponent<AudioSource>();
        bossDeadSource = gameObject.AddComponent<AudioSource>();

        


        StartLevelAudio();

    }

    private void Update()
    {
        wolf = GameObject.FindGameObjectWithTag("Wolf");
        boss = GameObject.FindGameObjectWithTag("Boss");

        if(wolf == null)
        {
            wolfAlive = false;
        }

        if(boss == null)
        {
            bossAlive = false;
        }

        if(SceneManager.GetActiveScene().buildIndex == 8)
        {            
            if (!isStop)
            {
                StopAllMusicAudio();
                isStop = true;
            }            
        }
        else
        {
            isStop = false;
        }

        if(!isStop && !wolfAlive && !bossAlive && !playerDead)
        {
            if (!playOnce)
            {
                PlayMusicAudio();
                playOnce = true;
            }            
        }
    }

    void StartLevelAudio()
    {
        Instance.ambientSource.clip = Instance.ambientClip;
        Instance.ambientSource.loop = true;
        Instance.ambientSource.volume = 0.2f;
        Instance.ambientSource.Play();
        PlayMusicAudio();
    }

    public static void ExplosionAudio()
    {
        Instance.explosionSource.clip = Instance.explosionClip;
        Instance.explosionSource.Play();
    }

    #region BGM

    public static void StopAllMusicAudio()
    {
        Instance.isStop = true;

        Instance.musicSource.Stop();
        Instance.wolfMusicSource.Stop();
        Instance.playerDiedMusicSource.Stop();
        Instance.bossLevelSource.Stop();
    }
    public static void PlayMusicAudio()
    {
        Instance.isStop = false;
        Instance.wolfAlive = false;
        Instance.bossAlive = false;
        Instance.playerDead = false;
        Instance.playOnce = false;

        Instance.wolfMusicSource.Stop();
        Instance.playerDiedMusicSource.Stop();
        Instance.bossLevelSource.Stop();

        Instance.musicSource.clip = Instance.musicClip;
        Instance.musicSource.loop = true;
        Instance.musicSource.Play();
        Instance.StartCoroutine(FadInSound(Instance.musicSource, 0.01f, 0.5f));
    }
    public static void PlayPlayerDiedMusicAudio()
    {
        Instance.playerDead = true;
        Instance.playOnce = false;

        Instance.wolfMusicSource.Stop();
        Instance.musicSource.Stop();
        Instance.bossLevelSource.Stop();
        Instance.playerDiedMusicSource.volume = 1.0f;

        Instance.playerDiedMusicSource.clip = Instance.playerDiedMusicClip;
        Instance.playerDiedMusicSource.loop = true;
        Instance.playerDiedMusicSource.Play();
        Instance.StartCoroutine(FadInSound(Instance.playerDiedMusicSource, 0.01f, 0.5f));
    }

    public static void PlayWolfMusiclAudio()
    {
        Instance.wolfAlive = true;
        Instance.playOnce = false;

        Instance.musicSource.Stop();
        Instance.playerDiedMusicSource.Stop();

        Instance.musicSource.Stop();
        Instance.wolfMusicSource.clip = Instance.wolfMusicClip;
        Instance.wolfMusicSource.loop = true;
        Instance.wolfMusicSource.Play();
        Instance.StartCoroutine(FadInSound(Instance.wolfMusicSource, 0.01f, 1f));

    }

    public static void PlayBossMusiclAudio()
    {
        Instance.bossAlive = true;
        Instance.playOnce = false;

        Instance.musicSource.Stop();
        Instance.playerDiedMusicSource.Stop();
        Instance.wolfMusicSource.Stop();

        Instance.musicSource.Stop();
        Instance.bossLevelSource.clip = Instance.bossLevelClip;
        Instance.bossLevelSource.loop = true;
        Instance.bossLevelSource.Play();
        Instance.StartCoroutine(FadInSound(Instance.bossLevelSource, 0.01f, 0.5f));

    }
    #endregion

    #region Player_Audio
    public static void PlayFootStepAudio()
    {
        int index = Random.Range(0, Instance.walkStepClip.Length);
        Instance.walkStepSource.clip = Instance.walkStepClip[index];
        Instance.walkStepSource.volume = 0.5f;
        Instance.walkStepSource.Play();
    }

    public static void PlayRunStepAudio()
    {
        int index = Random.Range(0, Instance.runStepClip.Length);
        Instance.runStepSource.clip = Instance.runStepClip[index];
        Instance.runStepSource.Play();        
    }

    public static void PlayAttack01Audio()
    {
        int index = Random.Range(0, Instance.attackClip.Length);
        Instance.attackSource.clip = Instance.attackClip[index];
        Instance.attackSource.volume = 1f;
        Instance.attackSource.Play();
    }

    public static void FireBallAudio()
    {
        int index = Random.Range(0, Instance.fireBallClip.Length);
        Instance.fireBallSource.clip = Instance.fireBallClip[index];
        Instance.fireBallSource.Play();
    }

    public static void PlayerGetHitAudio()
    {
        Instance.getHitSource.clip = Instance.getHitClip;
        Instance.getHitSource.Play();
    }

    public static void PlayerRotatingWeaponAudio()
    {
        Instance.rotatingWeaponSource.clip = Instance.rotatingWeaponClip;
        Instance.rotatingWeaponSource.Play();
    }


    #endregion

    #region Enemy_Audio
    public static void EnemyAttackAudio()
    {
        Instance.enemyAttackSource.clip = Instance.enemyAttackClip;
        Instance.enemyAttackSource.Play();
    }

    public static void EnemyGetHitAudio()
    {
        Instance.enemyGetHitSource.clip = Instance.enemyGetHitClip;
        Instance.enemyGetHitSource.Play();
    }

    public static void ZombieGetHurtAudio()
    {
        int index = Random.Range(0, Instance.zombieGetHurtClip.Length);
        Instance.zombieGetHurtSource.clip = Instance.zombieGetHurtClip[index];
        Instance.zombieGetHurtSource.Play();
    }

    public static void ZombieYellAudio()
    {
        Instance.zombieYellSource.clip = Instance.zombieYellClip;
        Instance.zombieYellSource.Play();
    }

    public static void ZombieDeadAudio()
    {
        Instance.zombieDeadSource.clip = Instance.zombieDeadClip;
        Instance.zombieDeadSource.Play();
    }

    public static void WolfYellAudio()
    {
        Instance.wolfYellSource.clip = Instance.wolfYellClip;
        Instance.wolfYellSource.Play();
    }

    public static void WolfAttackAudio()
    {
        Instance.wolfAttackSource.clip = Instance.wolfAttackClip;
        Instance.wolfAttackSource.Play();
    }

    public static void WolfDeadAudio()
    {
        Instance.wolfDeadSource.clip = Instance.wolfDeadClip;
        Instance.wolfDeadSource.Play();
    }

    public static void WolfBackGroundMusic()
    {
        Instance.wolfMusicSource.clip = Instance.wolfMusicClip;
        Instance.wolfMusicSource.Play();
    }

    #endregion

    #region Boss_Audio
    public static void BossYellAudio()
    {
        Instance.bossYellSource.clip = Instance.bossYellClip;
        Instance.bossYellSource.volume = 0.5f;
        Instance.bossYellSource.Play();
    }

    public static void BossAttackAudio()
    {
        Instance.bossAttackSource.clip = Instance.bossAttackClip;
        Instance.bossAttackSource.Play();
    }

    public static void BossAttackSoundAudio()
    {
        Instance.bossAttackSoundSource.clip = Instance.bossAttackSoundClip;
        Instance.bossAttackSoundSource.Play();
    }

    public static void BossGetHurtAudio()
    {
        int index = Random.Range(0, Instance.bossGetHurtClip.Length);
        Instance.bossGetHurtSource.clip = Instance.bossGetHurtClip[index];
        Instance.bossGetHurtSource.Play();
    }

    public static void BossKnockDownAudio()
    {
        Instance.bossKnockDownSource.clip = Instance.bossKnockDownClip;
        Instance.bossKnockDownSource.Play();
    }

    public static void BossDiedAudio()
    {
        Instance.bossDIedSource.clip = Instance.bossDiedClip;
        Instance.bossDIedSource.Play();
    }

    public static void BossThrowHelemtAudio()
    {
        Instance.bossThrowHelemtSource.clip = Instance.bossThrowHelmetClip;
        Instance.bossThrowHelemtSource.Play();
    }

    public static void BossKillPlayerAudio()
    {
        Instance.bossKillPlayerSource.clip = Instance.bossKillPlayerClip;
        Instance.bossKillPlayerSource.volume = 0.5f;
        Instance.bossKillPlayerSource.Play();
    }

    public static void BossRushAttackAudio()
    {
        int index = Random.Range(0, Instance.bossRushAttackYellClip.Length);
        Instance.bossRushAttaackYellSource.clip = Instance.bossRushAttackYellClip[index];
        Instance.bossRushAttaackYellSource.volume = 0.5f;
        Instance.bossRushAttaackYellSource.Play();
    }

    public static void BossJumpAttackAudio()
    {
        Instance.bossJumpAttackSource.clip = Instance.bossJumpAttackClip;
        Instance.bossJumpAttackSource.volume = 0.5f;
        Instance.bossJumpAttackSource.Play();
    }

    public static void BossDeadAudio()
    {
        int index = Random.Range(0, Instance.bossDeadClip.Length);
        Instance.bossDeadSource.clip = Instance.bossDeadClip[index];
        Instance.bossDeadSource.volume = 0.5f;
        Instance.bossDeadSource.Play();
    }
    #endregion

    public static IEnumerator FadInSound(AudioSource audioSource, float speed, float maxValume)
    {
        AudioManager.Instance.keepFadeIn = true;
        AudioManager.Instance.keepFadeOut = false;

        audioSource.volume = 0f;

        while(audioSource.volume < maxValume && AudioManager.Instance.keepFadeIn)
        {
            audioSource.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public static IEnumerator FadOutSound(AudioSource audioSource, float speed)
    {
        AudioManager.Instance.keepFadeIn = false;
        AudioManager.Instance.keepFadeOut = true;

        float audioVolume = audioSource.volume;

        while (audioVolume >= speed && AudioManager.Instance.keepFadeOut)
        {
            audioVolume -= speed;
            audioSource.volume = audioVolume;
            if(audioVolume <= speed )
            {
                audioSource.Stop();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Onhover()
    {
        sourse.PlayOneShot(hoverSFX);
    }

    public void Onclick()
    {
        sourse.PlayOneShot(clickSFX);
    }

}
