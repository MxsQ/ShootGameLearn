using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum FireMode { Auto, Burst, Single };
    [SerializeField] public FireMode fireMode;

    [SerializeField] public Transform[] projectileSpawn;
    [SerializeField] public Projectile projectile;
    [SerializeField] public float msBetweenShots = 100;
    [SerializeField] public float muzzleVelocity = 35;
    [SerializeField] public int burstCount;

    [SerializeField] Transform shell;
    [SerializeField] Transform shellEjection;
    MuzzleFlash muzzleFlash;
    float nextShotTime;

    bool triggerReleaseSingceLastShot;
    int shotsRemainingInBurst;

    private void Start()
    {
        muzzleFlash = GetComponent<MuzzleFlash>();
        shotsRemainingInBurst = burstCount;
    }

    void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            if (fireMode == FireMode.Burst)
            {
                if (shotsRemainingInBurst == 0)
                {
                    return;
                }
                shotsRemainingInBurst--;
            }
            else if (fireMode == FireMode.Single)
            {
                if (!triggerReleaseSingceLastShot)
                {
                    return;
                }
            }

            for (int i = 0; i < projectileSpawn.Length; i++)
            {
                nextShotTime = Time.time + msBetweenShots / 1000;
                Projectile newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
            }
            Instantiate(shell, shellEjection.position, shellEjection.rotation);
            muzzleFlash.Activate();

        }

    }

    public void OnTiggerHolde()
    {
        Shoot();
        triggerReleaseSingceLastShot = false;
    }

    public void OnTiggerRelese()
    {
        triggerReleaseSingceLastShot = true;
        shotsRemainingInBurst = burstCount;
    }
}
