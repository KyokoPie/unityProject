using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina_Control : Singleton<Stamina_Control>
{
    GameObject playerObj;
    CharacterStats characterStats;
    public Slider staminaBar;


    public float maxStamina;
    public float currentStamina;
    private Coroutine regen;

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        characterStats = playerObj.GetComponent<CharacterStats>();
        maxStamina = characterStats.characterData.maxStamina;
        currentStamina = characterStats.characterData.currentStamina;

        currentStamina = maxStamina;

        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void RunStamina(float amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount / 100;
            staminaBar.value = currentStamina;

            if(regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }    
    }

    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
            {
                StopCoroutine(regen);
            }

            regen = StartCoroutine(RegenStamina());
        }
    }


    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return new WaitForSeconds(0.1f);
        }

        regen = null;
    }
    
}
