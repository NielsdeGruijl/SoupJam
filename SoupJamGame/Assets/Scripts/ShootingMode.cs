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
    [SerializeField] GameObject bulletPrefab;

    Gunmode currentMode;

    float switchTimer;
    bool modeswitched = true;

    void Update()
    {
        if (modeswitched)
        {
            StartCoroutine(SwitchCountdown());
        }

        transform.parent.rotation = LookAt2D.LookAtMouse(transform.parent);
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
    }

    public void SemiAuto()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    public void Inverted()
    {

    }

    void Rifle()
    {

    }

    void Launcher()
    {

    }

    IEnumerator SwitchCountdown()
    {
        modeswitched = false;
        switchTimer = Random.Range(1, 10);
        print("Time until next weapon mode: " + switchTimer);
        yield return new WaitForSeconds(switchTimer);
        currentMode = GunModeChance.GetRandomEntry();
        print("Current weapon mode: " + currentMode);
        modeswitched = true;
    }
}
