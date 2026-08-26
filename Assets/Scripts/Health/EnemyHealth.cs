using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static float MaxHealth=1000f;
    [SerializeField]private GameObject FillArea;
    [SerializeField]private Animator EnemyAnimator;
    [SerializeField]private Animator PlayerAnimator;
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private PlayerShoot playerShoot;
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private PlayerHealth playerHealth;
    [SerializeField]private PlayerDamage playerDamage;
    [SerializeField]private UI uI;
    [SerializeField]private SpawnParticles spawnParticles;
    [SerializeField]private AudioManager audioManager;
    [SerializeField]private ParticleSystem bulletHitEffect;
    [SerializeField]private Slider slider;
    [SerializeField]private ModesManager modesManager;
    public GameObject enemyShield;
    public bool enemyShieldActive=false;
    private Modes.modes currentMode;
    public float CurrentHealth;
    public int fireBallDamageCounter;
    private float damageCounter;
    bool isSpecialAttacking=false;
    void Start()
    {
        currentMode = (Modes.modes)PlayerPrefs.GetInt("GameMode", 0);
        MaxHealth=PlayerPrefs.GetFloat("EnemyMaxHealth",1000);
        CurrentHealth=MaxHealth;
        slider.maxValue=CurrentHealth;
        if (currentMode!=Modes.modes.survival)
            modesManager.shockWaveSlider.maxValue=200;
        slider.value=CurrentHealth;
    }
    public void TakeDamage(float Damage)
    {
        if (enemyShieldActive) return;
        CurrentHealth-=Damage;
        
        if (currentMode!=Modes.modes.survival)
        {
            modesManager.shockWaveSlider.value=fireBallDamageCounter;
            if(fireBallDamageCounter >= 200 && !isSpecialAttacking)
            {
                fireBallDamageCounter-=200;
                modesManager.shockWaveSlider.value=fireBallDamageCounter;
                StartCoroutine(SpecialAttack());
            }
            damageCounter+=Damage;
            if (damageCounter >= 700)
            {
                damageCounter=0f;
                SpawnEnemys.instance.SpawnChildEnemy();
            }
        }
        CurrentHealth=Mathf.Clamp(CurrentHealth,0,MaxHealth);
        slider.value=CurrentHealth;
        
        if (CurrentHealth <= 0)
        {
            FillArea.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(Die());
        }

    }
    public void HitEffect()
    {
        bulletHitEffect.gameObject.SetActive(true);
        bulletHitEffect.Play();
    }
    IEnumerator Die()
    {
        playerHealth.enabled=false;
        playerDamage.enabled=false;
        EnemyAnimator.SetTrigger("IsDie");
        playerMovement.enabled=false;
        playerShoot.enabled=false;
        PlayerAnimator.SetBool("IsWin",true);
        audioManager.Win();
        playerMovement.enabled=false;
        uI.ShowLevelComplete();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    public IEnumerator SpecialAttack()
    {
        isSpecialAttacking=true;
        audioManager.SpecialAttackSound();
        EnemyAnimator.SetBool("IsAttack",false);
        EnemyAnimator.SetBool("SpecialAttack",true);
        spawnParticles.SpawnSpikes();
        enemyController.enabled=false;
        yield return new WaitForSeconds(1.5f);
        EnemyAnimator.SetBool("SpecialAttack",false);
        isSpecialAttacking=false;
        if (!(damageCounter >= 700f))
        {
            enemyController.enabled=true;
        }
         
    }
}
