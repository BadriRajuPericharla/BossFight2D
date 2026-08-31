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
    [SerializeField]private GameObject timerImage;
    [SerializeField]private UI uI;
    private float currentTime;
    public Modes.modes currentMode;
    public float duration=170f;
    public Slider shockWaveSlider;
    public Coroutine timerCouroutine;
    
    
    void Start()
    {
        currentMode=(Modes.modes)PlayerPrefs.GetInt("GameMode",0);
        switch (currentMode)
        {
            case Modes.modes.def:
                return;
            
            case Modes.modes.survival:
                Time.timeScale=1f;
                uI.CloseSurvivalWarning();
                playerMovement.canAttack=false;
                knifeAttackBtn.SetActive(false);
                bulletAttackBtn.SetActive(false);
                enemyController.Speed*=2f;
                shockWaveSlider.gameObject.SetActive(true);
                timerImage.SetActive(true);
                timerText.enabled=true;
                timerCouroutine=StartCoroutine(Timer());
                StartCoroutine(EnemyDamage());
                StartCoroutine(ParticleSpawner());
            break;
            case Modes.modes.challenge:
                Time.timeScale=1f;
                uI.CloseChallengeWarning();
                playerMovement.canAttack=true;
                knifeAttackBtn.SetActive(true);
                bulletAttackBtn.SetActive(true);
                duration=120f;
                timerImage.SetActive(true);
                timerText.enabled=true;
                timerCouroutine=StartCoroutine(Timer());
            break;
            case Modes.modes.elimination:
                Time.timeScale=1f;
                uI.CloseEliminationWarning();
                playerMovement.canAttack=true;
                knifeAttackBtn.SetActive(true);
                bulletAttackBtn.SetActive(true);
            break;

        }
    }
    public void PlayerDied()
    {
        Time.timeScale=0f;
        if (enemyHealth != null)
        {
            enemyHealth.StopAllCoroutines();
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


    public IEnumerator ParticleSpawner()
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
    public IEnumerator Timer()
    {
        currentTime = duration;
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
