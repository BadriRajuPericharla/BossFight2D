using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ModesManager : MonoBehaviour
{
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private EnemyHealth enemyHealth;
    
    
    void Start()
    {
        Modes.modes currentMode=(Modes.modes)PlayerPrefs.GetInt("GameMode",0);
        switch (currentMode)
        {
            case Modes.modes.survival:

                playerMovement.canAttack=false;
                enemyController.Speed*=2f;
                Modes.instance.StartCoroutine(Modes.instance.Timer());
                StartCoroutine(ParticleSpawner());

            break;

            case Modes.modes.elimination:
                    
                playerMovement.canAttack=true;

            break;

            case Modes.modes.challenge:

                playerMovement.canAttack=true;

            break;
        }
    }
    
    IEnumerator ParticleSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            enemyHealth.StartCoroutine(enemyHealth.SpecialAttack());
        }
        

    }

}
