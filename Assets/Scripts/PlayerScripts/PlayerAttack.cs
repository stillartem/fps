using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  private WeaponManager weaponManager;
  private Animator zoomCameraAnimator;
  private Camera mainCamera;
  private GameObject crosshair;



  private float nextTimeToFire;

  public float fireRate = 15f;
  public float damage = 20f;

  void Awake()
  {
    weaponManager = GetComponent<WeaponManager>();
    zoomCameraAnimator = GameObject.FindGameObjectWithTag(Tags.ZOOM_CAMERA)
      .GetComponent<Animator>();
    crosshair = GameObject.FindGameObjectWithTag(Tags.CROSSHAIR);

  }

  // Update is called once per frame
  void Update()
  {
    var weaponHandler = weaponManager.GetCurrentSelectedWeapon() as WeaponHandler;

    Zoom(weaponHandler);
    WeaponShoot(weaponHandler);

  }

  private void WeaponShoot(WeaponHandler weaponHandler)
  {
    if (!Input.GetMouseButtonDown(0)) return;

    switch (weaponHandler.weaponFireType)
    {
      case WeaponFireType.SINGLE:
        SingleShoot(weaponHandler);
        break;
      case WeaponFireType.MULTIPLE:
        MultipleShoot(weaponHandler);
        break;
    }
  }

  private void SingleShoot(WeaponHandler weaponHandler)
  {

    switch (weaponHandler.weaponBulletType)
    {
      case WeaponBulletType.BULLET:
        BulletShoot(weaponHandler);
        break;
      case WeaponBulletType.ARROW:
        if (weaponHandler.isAim)
        {
          ArrowShoot(weaponHandler);
        }
        break;
      case WeaponBulletType.SPEAR:
        if (weaponHandler.isAim)
        {
          SpearShoot(weaponHandler);
        }
        break;
      case WeaponBulletType.NONE:
        AxeShoot(weaponHandler);
        break;
    }
  }

  private void MultipleShoot(WeaponHandler weaponHandler)
  {
    if (Input.GetMouseButtonDown(0) && Time.time > nextTimeToFire)
    {
      nextTimeToFire = Time.time + 1f / fireRate;
      weaponHandler.ShootAnimation();
    }
  }


  private void BulletShoot(WeaponHandler weaponHandler)
  {
    weaponHandler.playSoundShoot();
    weaponHandler.muzzleFlashTurnOn();
    weaponHandler.ShootAnimation();
    if (weaponHandler.isBulletPrefabExist())
    {
      Instantiate(weaponHandler.bulletPrefab, weaponHandler.startPosition.transform.position, weaponHandler.startPosition.transform.rotation);
    }
  }

  private void ArrowShoot(WeaponHandler weaponHandler)
  {
    weaponHandler.ShootAnimation();
    if (weaponHandler.isBulletPrefabExist())
    {
      Instantiate(weaponHandler.bulletPrefab, weaponHandler.startPosition.transform.position, weaponHandler.startPosition.transform.rotation);
    }
  }

  private void SpearShoot(WeaponHandler weaponHandler)
  {
    weaponHandler.ShootAnimation();
    if (weaponHandler.isBulletPrefabExist())
    {
      Instantiate(weaponHandler.bulletPrefab, weaponHandler.startPosition.transform.position, weaponHandler.startPosition.transform.rotation);
    }
  }

  private void AxeShoot(WeaponHandler weaponHandler)
  {
    weaponHandler.ShootAnimation();
  }

  private void Zoom(WeaponHandler weaponHandler)
  {
    if (weaponHandler.weaponAim == WeaponAim.NONE) return;

    switch (weaponHandler.weaponAim)
    {
      case WeaponAim.AIM:
        ZoomGun();
        break;
      case WeaponAim.SELF_AIM:
        AimWeapon(weaponHandler);
        break;
      case WeaponAim.NONE:
        return;
    }
  }

  private void ZoomGun()
  {
    if (Input.GetMouseButtonDown(1))
    {
      crosshair.SetActive(false);
      zoomCameraAnimator.Play(AnimationAction.ZOOM_IN_ANIM);
    }

    if (Input.GetMouseButtonUp(1))
    {
      crosshair.SetActive(true);
      zoomCameraAnimator.Play(AnimationAction.ZOOM_OUT_ANIM);
    }
  }

  private void AimWeapon(WeaponHandler weaponHandler)
  {
    if (Input.GetMouseButtonDown(1))
    {
      weaponHandler.Aim(true);
    }

    if (Input.GetMouseButtonUp(1))
    {
      weaponHandler.Aim(false);
    }

  }
}
