using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireForce = 20f;
    [SerializeField] private AudioSource fireSound;
    public Vector2 Pointerposition { get; set; }
    public Vector2 direction { get; set; }

    public int currentClip, maxClipSize = 1, currentAmmo, maxAmmoSize = 2;

    private void Start()
    {
    }

    public void Update()
    {
        direction = (Pointerposition - (Vector2)transform.position).normalized;

        // Will make sure that the weapon will be rotating upright
        Vector2 scale = transform.localScale;
    }
    public void Fire()
    {
        if (currentClip <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        FireBullet();
        PlayFireSound();
        currentClip--;
    }
    public void Fire2()
    {
        if (currentClip <= 0)
        {
            Debug.Log("Out of ammo!");
            return;
        }

        FireBullet2();
        PlayFireSound();
        currentClip--;
    }


    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(-firePoint.up * fireForce, ForceMode2D.Impulse);
    }
    private void FireBullet2()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    private void PlayFireSound()
    {
        fireSound.Play();
    }

    public void Reload()
    {
        int reloadAmount = maxClipSize - currentClip;
        reloadAmount = (currentAmmo - reloadAmount) >= 0 ? reloadAmount : currentAmmo;
        currentClip += reloadAmount;
        currentAmmo -= reloadAmount;
    }

    public void AddAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
        if (currentAmmo > maxAmmoSize)
        {
            currentAmmo = maxAmmoSize;
        }
    }
}
