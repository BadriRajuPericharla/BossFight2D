using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioManager audioManager;
    public Image image;
    int bulletsFired=0;
    public bool bullet;
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.C)||bullet)&&bulletsFired<3)
        {
            Shoot();
            bullet=false;
        }
        void Shoot()
        {
            audioManager.FireBallAttack();
            Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
            bulletsFired++;
            if (bulletsFired == 3)
            {  
                StartCoroutine(Reset());
            }
        }
    }
    IEnumerator Reset()
    {
        image.enabled=true;
        float cooldown = 5f;
        float time = 0;

        image.fillAmount = 0;

        while (time < cooldown)
        {
            time += Time.deltaTime;
            image.fillAmount = time / cooldown;
            yield return null;
        }

        image.fillAmount = 1;
        image.enabled=false;
        bulletsFired = 0;
    }
}