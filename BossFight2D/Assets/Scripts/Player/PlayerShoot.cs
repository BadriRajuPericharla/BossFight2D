using System.Collections;
using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
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
        yield return new WaitForSeconds(5f);
        bulletsFired=0;
    }
}