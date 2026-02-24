using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Shoot();
        }
        void Shoot()
        {
            Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
        }
    }
}