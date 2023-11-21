using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterData_SO templateData;

    public CharacterData_SO characterData;

    public AttackData_SO attackData;

    [HideInInspector] public bool isCritical;

    private void Awake()
    {
        if(templateData != null)
        {
            characterData = Instantiate(templateData);
        }
    }

    #region Read from Data_SO
    public float MaxHealth
    {
        get
        {
            if (characterData != null)
                return characterData.maxHealth;
            else return 0f;
        }
        set
        {
            characterData.maxHealth = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            if (characterData != null)
                return characterData.currentHealth;
            else return 0f;
        }
        set
        {
            characterData.currentHealth = value;
        }

    }

    public float BaseDefence
    {
        get
        {
            if (characterData != null)
                return characterData.baseDefence;
            else return 0f;
        }
        set
        {
            characterData.baseDefence = value;
        }
    }

    public float CurrentDefence
    {
        get
        {
            if (characterData != null)
                return characterData.currentDefence;
            else return 0f;
        }
        set
        {
            characterData.currentDefence = value;
        }
    }
    
    #endregion

    #region Character Combat
    public void TakeDamage(CharacterStats attacker, CharacterStats defener)
    {
        int damage = Mathf.Max(attacker.CurrentDamage() - (int)defener.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        //TODO: Updata UI
    }

    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamge, attackData.maxDamge);

        if (isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            CameraShake.Instance.ShakeCamera(5f, 0.1f);
            Debug.Log("Critical" + coreDamage);
        }

        return (int)coreDamage;
    }

    public void MagicDamage(CharacterStats attacker, CharacterStats defener)
    {
        float damage = Mathf.Max(attacker.attackData.magicDamge - defener.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
    }


    #endregion
}
