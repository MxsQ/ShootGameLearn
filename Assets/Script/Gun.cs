using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] public Transform muzzle;
    [SerializeField] public Projectile projectile;
    [SerializeField] public float msBetweenShots = 100;
    [SerializeField] public float muzzleVelocity = 35;

    [SerializeField] Transform shell;
    [SerializeField] Transform shellEjection;

    MuzzleFlash muzzleFlash;

    float nextShotTime;

    private void Start()
    {
        muzzleFlash = GetComponent<MuzzleFlash>();
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);

            Instantiate(shell, shellEjection.position, shellEjection.rotation);
            muzzleFlash.Activate();
        }

    }
}
