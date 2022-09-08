using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMode : MonoBehaviour
{
    enum Gunmode
    {
        SemiAuto,
        Inverted,
        Rifle,
        Launcher
    }

    [SerializeField] WeightedChance<Gunmode> GunModeChance;
    [SerializeField] Gunmode currentMode = Gunmode.SemiAuto;

    GameObject projectile;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject rocketPrefab;

    float switchTimer;
    float fireRate;
    float bulletSpeed;
    float bulletDamage;

    public float damageMultiplier = 1f;

    float recoil;
    float currentRecoil;

    bool shooting;
    bool canShoot = true;
    bool modeswitched = true;

    Vector2 dir;
    Vector2 bulletVelocity;
    
    Vector3 offsetPos;
    Vector3 defaultPos;


    private void Start()
    {
        defaultPos = transform.localPosition;
    }

    void Update()
    {
        if (modeswitched)
        {
            StartCoroutine(SwitchCountdown());
        }

        transform.rotation = LookAt2D.LookAtMouse(transform.parent);

        dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

        if (canShoot && shooting)
            StartCoroutine(shootFullAuto());

        ApplyRecoil();

        print("Can shoot: " + canShoot);
    }

    public void Shoot()
    {
        switch (currentMode)
        {
            case Gunmode.SemiAuto:
                SemiAuto();
                break;
            case Gunmode.Inverted:
                Inverted();
                break;
            case Gunmode.Rifle:
                Rifle();
                break;
            case Gunmode.Launcher:
                Launcher();
                break;
            default:
                break;
        }

        if (canShoot)
            StopCoroutine(shootFullAuto());

        print("Started shooting");
        shooting = true;
    }

    public void SemiAuto()
    {
        bulletDamage = 2 * damageMultiplier;
        bulletSpeed = 15f;
        fireRate = 0.3f;
        projectile = bulletPrefab;
        recoil = -0.5f;
    }

    public void Inverted()
    {
        bulletDamage = 2 * damageMultiplier;
        bulletSpeed = 15f;
        fireRate = 0.3f;
        projectile = bulletPrefab;
        recoil = -0.1f;
    }

    void Rifle()
    {
        bulletDamage = 1 * damageMultiplier;
        bulletSpeed = 15f;
        fireRate = 0.1f;
        projectile = bulletPrefab;
        recoil = -1f;
    }

    void Launcher()
    {
        bulletDamage = 5 * damageMultiplier;
        bulletSpeed = 10f;
        fireRate = 1.5f;
        projectile = rocketPrefab;
        recoil = -1f;
    }

    IEnumerator SwitchCountdown()
    {
        modeswitched = false;
        switchTimer = Random.Range(1, 10);
        print("Time until next weapon mode: " + switchTimer);
        yield return new WaitForSeconds(switchTimer);
        currentMode = GunModeChance.GetRandomEntry();

        if(!canShoot)
            Shoot();

        print("Current weapon mode: " + currentMode);
        modeswitched = true;
    }

    IEnumerator shootFullAuto()
    {
        canShoot = false;

        dir.Normalize();

        GameObject bullet;

        switch (currentMode)
        {
            case Gunmode.SemiAuto:
                bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x + Random.Range(-0.05f, 0.05f), dir.y + Random.Range(-0.05f, 0.05f)).normalized * bulletSpeed;
                bullet.GetComponent<BulletScript>().damage = bulletDamage;
                currentRecoil += recoil;
                break;
            case Gunmode.Inverted:
                bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed * -1f;
                bullet.GetComponent<BulletScript>().damage = bulletDamage;
                currentRecoil += recoil;
                break;
            case Gunmode.Rifle:
                bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x + Random.Range(-0.05f, 0.05f), dir.y + Random.Range(-0.05f, 0.05f)).normalized * bulletSpeed;
                bullet.GetComponent<BulletScript>().damage = bulletDamage;
                currentRecoil += recoil;
                break;
            case Gunmode.Launcher:
                bullet = Instantiate(rocketPrefab, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;
                bullet.GetComponent<RocketScript>().damage = bulletDamage;
                currentRecoil += recoil;
                //print("bullet dir: " + dir + " bulletSpeed: " + bulletSpeed);
                break;
        }

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void ApplyRecoil()
    {
        /*        if(currentRecoil < 0)
                {
                    print(currentRecoil);
                    transform.localPosition = Vector2.Lerp(transform.localPosition, dir.normalized * currentRecoil, 0.5f);
                    //transform.localPosition += new Vector3(dir.x, dir.y, 0).normalized * currentRecoil;
                    currentRecoil -= Time.deltaTime;
                }

                if (transform.localPosition != defaultPos)
                {
                    transform.localPosition = Vector2.MoveTowards(transform.localPosition, defaultPos, currentRecoil * -1f);
                    print("Default position: " + defaultPos + " localPosition: " + transform.localPosition);
                }*/
    }

    public void StopShooting()
    {
        shooting = false;
        print("Stopped shooting");
    }
}
