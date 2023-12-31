using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //Gun Stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonhold;

    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;

    //Grapics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;


    //bug fixing
    public bool allowInvoke = true;

    private void Awake()
    {
        //make sure magazin is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        //Set ammo display, if it exists
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);
    }
    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonhold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting= Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload automatically when trying to shoot without ammo
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        
        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft >0)
        {
            //Set bullet shot to 0
            bulletsShot = 0;

            Shoot();
        }
    
    }


    private void Shoot()
    {
        readyToShoot = false;

        //Fined hit position
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWitoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread=directionWitoutSpread + new Vector3(x, y, 0); //Just add Spread to last digit

        //Instatntiate bullet/projectile
        GameObject currentBullet=Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized*shootForce,ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked)
        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //if more than one bulletsPerTap make sure to repeat shoot funktion
        if(bulletsShot<bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }


}