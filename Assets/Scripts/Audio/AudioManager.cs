using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioClip WinClip;
    [SerializeField]private AudioClip SwordClip;
    [SerializeField]private AudioClip FireBallClip;
    [SerializeField]private AudioClip EnemySwordClip;
    [SerializeField]private AudioClip DizzyClip;
    [SerializeField]private AudioClip JumpClip;
    [SerializeField]private AudioClip PlayerDieClip;
    [SerializeField]private AudioClip SpecialAttackClip;
    [SerializeField]private AudioClip buttonClick;
    AudioSource audioSource;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Win();
        }
    }
    public void Win()
    {
        audioSource.PlayOneShot(WinClip);
    }
    public void SwordAttack()
    {
        audioSource.PlayOneShot(SwordClip);
    }
    public void FireBallAttack()
    {
        audioSource.PlayOneShot(FireBallClip);
    }
    public void EnemySwordAttack()
    {
        audioSource.PlayOneShot(EnemySwordClip);
    }
    public void PlayerDizzy()
    {
        audioSource.PlayOneShot(DizzyClip);
    }
    public void JumpSound()
    {
        audioSource.PlayOneShot(JumpClip);
    }
    public void PlayerDeadSound()
    {
        audioSource.PlayOneShot(PlayerDieClip);
    }
    public void SpecialAttackSound()
    {
        audioSource.PlayOneShot(SpecialAttackClip);
    }
    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
}
