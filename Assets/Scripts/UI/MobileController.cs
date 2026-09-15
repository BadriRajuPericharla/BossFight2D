using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private PlayerShoot playerShoot;
    [SerializeField]private UI uI;
    void Update()
    {
        if (!Application.isMobilePlatform)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                uI.ShowPausePanel();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                uI.ShowSettings();
            }
        }
        else
            return;
    }

    public void leftmove()
    {
        playerMovement.MoveInput=-1;
    }
    public void rightmove()
    {
        playerMovement.MoveInput=1;
    }
    public void stopmove()
    {
        playerMovement.MoveInput=0;
    }
    public void jump()
    {
        playerMovement.JumpFromMobile();
    }
    public void attckKnife()
    {
        if (playerMovement.canAttack)
        {
            playerMovement.knifeAttack=true;
        }
    }
    public void BulletsAttack()
    {
        playerShoot.bullet=true;
    }
    public void StopBulletsAttack()
    {
        playerShoot.bullet=false;
    }
}
