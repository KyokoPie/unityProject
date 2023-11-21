using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBossSound : MonoBehaviour
{
    Boss_Controller bossController;

    private void Start()
    {
        bossController = GetComponent<Boss_Controller>();
    }

    public void PlayBossMusic()
    {
        AudioManager.PlayBossMusiclAudio();
    }

    public void PlayBossYellSound()
    {
        AudioManager.BossYellAudio();
    }

    public void PlayBossAttackSound()
    {
        AudioManager.BossAttackAudio();
    }

    public void PlayBossGetHurtSound()
    {
        AudioManager.BossGetHurtAudio();
    }

    public void PlayBossDiedSound()
    {
        AudioManager.BossDiedAudio();
    }

    public void PlayBossKnockDownSound()
    {
        AudioManager.BossKnockDownAudio();
    }

    public void PlayBossThrowHelmentSound()
    {
        AudioManager.BossThrowHelemtAudio();
    }

    public void PlayBossRushAttackSound()
    {
        AudioManager.BossRushAttackAudio();
    }

    public void PlayBossJumpAttackSound()
    {
        AudioManager.BossJumpAttackAudio();
    }

    public void PlayBossDeadSound()
    {
        AudioManager.BossDeadAudio();
    }

    public void PlayBossAttackSoundAudio()
    {
        AudioManager.BossAttackSoundAudio();
    }
}
