using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ModesManager : MonoBehaviour
{
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private EnemyHealth enemyHealth;
    [SerializeField]private GameObject knifeAttackBtn;
    [SerializeField]private GameObject bulletAttackBtn;
    [SerializeField]private TextMeshProUGUI timerText;
    [SerializeField]private UI uI;
    private float currentTime;
    private Modes.modes currentMode;
    public float duration=170f;
    public Slider shockWaveSlider;
    
    
    void Start()
    {
        currentMode=(Modes.modes)PlayerPrefs.GetInt("GameMode",0);
        switch (currentMode)
        {
            case Modes.modes.def:
                return;
            
            case Modes.modes.survival:
                Time.timeScale=1f;
                playerMovement.canAttack=false;
                knifeAttackBtn.SetActive(false);
                bulletAttackBtn.SetActive(false);
                enemyController.Speed*=2f;
                shockWaveSlider.gameObject.SetActive(true);
                timerText.enabled=true;
                StartCoroutine(Timer());
                StartCoroutine(EnemyDamage());
                StartCoroutine(ParticleSpawner());
            break;
            case Modes.modes.challenge:
                Time.timeScale=1f;
                playerMovement.canAttack=true;
                knifeAttackBtn.SetActive(true);
                bulletAttackBtn.SetActive(true);
                duration=120f;
                timerText.enabled=true;
                StartCoroutine(Timer());
            break;
            case Modes.modes.elimination:
                Time.timeScale=1f;
                playerMovement.canAttack=true;
                knifeAttackBtn.SetActive(true);
                bulletAttackBtn.SetActive(true);
            break;

        }
    }
    public void PlayerDied()
    {
        StopAllCoroutines();

        if (enemyHealth != null)
        {
            enemyHealth.StopAllCoroutines();
        }

        if (timerText != null)
        {
            timerText.enabled = false;
        }

        if (shockWaveSlider != null)
        {
            shockWaveSlider.gameObject.SetActive(false);
        }
        uI.ShowGameOver();
    }

    IEnumerator EnemyDamage()
    {
        float damagePerSecond = enemyHealth.CurrentHealth / duration;

        while (enemyHealth.CurrentHealth > 0)
        {
            if(currentMode==Modes.modes.survival)
                enemyHealth.TakeDamage(damagePerSecond * Time.deltaTime);
            yield return null;
        }
    }


    IEnumerator ParticleSpawner()
    {
        shockWaveSlider.value = 0;
        shockWaveSlider.maxValue = 15f;

        while (enemyHealth != null && enemyHealth.gameObject.activeInHierarchy)
        {
            
            shockWaveSlider.value += Time.deltaTime;
            if (shockWaveSlider.value >= shockWaveSlider.maxValue)
            {
                shockWaveSlider.value = 0f;

                enemyHealth.StartCoroutine(enemyHealth.SpecialAttack());
            }

            yield return null;
        }
        shockWaveSlider.gameObject.SetActive(false);
    }
    IEnumerator Timer()
    {
        currentTime = duration;
        Debug.Log("timer started");
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);

            timerText.text = $"{minutes:00}:{seconds:00}";

            yield return null;
        }
        if(currentMode==Modes.modes.survival)
            uI.ShowLevelComplete();
        else
        {
            uI.ShowGameOver();
            playerMovement.enabled=false;
            enemyController.enabled=false;
        }   
        timerText.text = "00:00";
    }

}
