using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ChildEnemyHealth : MonoBehaviour
{
    public float MaxHealth=500f;
    public GameObject FillArea;
    [SerializeField]private Animator EnemyAnimator;



    [SerializeField]private EnemyController enemyController;

    [SerializeField]private AudioManager audioManager;
    [SerializeField]private ParticleSystem bulletHitEffect;
    public Slider slider;
    [SerializeField]private Animator childAnimator;
    public float CurrentHealth;
    
    void Start()
    {
        MaxHealth=PlayerPrefs.GetFloat("EnemyMaxHealth",500);
        CurrentHealth=MaxHealth;
        slider.maxValue=CurrentHealth;
        slider.value=CurrentHealth;
    }
    public void TakeDamage(float Damage)
    {
        CurrentHealth-=Damage;
        CurrentHealth=Mathf.Clamp(CurrentHealth,0,MaxHealth);
        slider.value=CurrentHealth;
        if (CurrentHealth <= 0)
        {
            if (!gameObject.activeInHierarchy)
                return;
            FillArea.SetActive(false);
            StopAllCoroutines();
            SpawnEnemys.instance.ChildEnemyDied(gameObject);
            
            return;
        }

    }
    public void HitEffect()
    {
        bulletHitEffect.gameObject.SetActive(true);
        bulletHitEffect.Play();
    }
    // IEnumerator Die()
    // {
    //     EnemyAnimator.SetTrigger("IsDie");
    //     yield return new WaitForSeconds(1f);
    //     SpawnEnemys.instance.ChildEnemyDied(gameObject);
    // }
    
}
