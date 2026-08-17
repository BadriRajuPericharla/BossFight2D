using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModesManager : MonoBehaviour
{
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private EnemyHealth enemyHealth;
    [SerializeField]private GameObject knifeAttackBtn;
    [SerializeField]private GameObject bulletAttackBtn;
    
    
    void Start()
    {
        Modes.modes currentMode=(Modes.modes)PlayerPrefs.GetInt("GameMode",0);
        switch (currentMode)
        {
            case Modes.modes.survival:
                Time.timeScale=1f;
                playerMovement.canAttack=false;
                knifeAttackBtn.SetActive(false);
                bulletAttackBtn.SetActive(false);
                enemyController.Speed*=2f;
                Modes.instance.timerText.enabled=true;
                Modes.instance.StartCoroutine(Modes.instance.Timer());
                StartCoroutine(EnemyDamage());
                StartCoroutine(ParticleSpawner());
            break;
            case Modes.modes.challenge:
                Time.timeScale=1f;
                playerMovement.canAttack=true;
                knifeAttackBtn.SetActive(true);
                bulletAttackBtn.SetActive(true);
                Modes.instance.duration=300f;
                Modes.instance.timerText.enabled=true;
                Modes.instance.StartCoroutine(Modes.instance.Timer());
            break;
            case Modes.modes.elimination:
                Time.timeScale=1f;
                playerMovement.canAttack=true;
                knifeAttackBtn.SetActive(true);
                bulletAttackBtn.SetActive(true);
            break;

        }
    }
    
    IEnumerator EnemyDamage()
    {
        float damagePerSecond = enemyHealth.CurrentHealth / Modes.instance.duration;

        while (enemyHealth.CurrentHealth > 0)
        {
            enemyHealth.TakeDamage(damagePerSecond * Time.deltaTime);

            yield return null;
        }
    }


    IEnumerator ParticleSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            
            enemyHealth?.StartCoroutine(enemyHealth.SpecialAttack());
        }
        

    }

}
