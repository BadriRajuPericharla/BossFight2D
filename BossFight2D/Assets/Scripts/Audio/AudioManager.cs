using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioClip WinClip;
    [SerializeField]private AudioClip SwordClip;
    [SerializeField]private AudioClip FireBallClip;
    [SerializeField]private AudioClip EnemySwordClip;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwordAttack();
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
}
