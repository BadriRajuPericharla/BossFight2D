using System.Collections;
using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    int bulletsFired=0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)&&bulletsFired<3)
        {
            Shoot();
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