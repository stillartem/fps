using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
  NONE,
  SELF_AIM,
  AIM
}

public enum WeaponFireType
{
  SINGLE,
  MULTIPLE
}

public enum WeaponBulletType
{
  BULLET,
  ARROW,
  SPEAR,
  NONE
}

public class WeaponHandler : MonoBehaviour
{

  private Animator animator;

  private AudioSource audioSource;

  [HideInInspector]
  public bool isAim;

  [SerializeField]
  private GameObject muzzleFlash;

  [SerializeField]
  private AudioClip shootSound, reloadSound;

  public GameObject startPosition;

  public GameObject bulletPrefab;


  public WeaponAim weaponAim;

  public WeaponFireType weaponFireType;

  public WeaponBulletType weaponBulletType;

  public GameObject attackPoint;

  void Awake()
  {
    animator = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();
  }

  // Start is called before the first frame update
  void Start()
  {
    isAim = false;
  }


  public void ShootAnimation()
  {
    animator.SetTrigger(AnimationAction.SHOOT_TRIGGER);
  }

  public void Aim(bool isAim)
  {
    this.isAim = isAim;
    animator.SetBool(AnimationAction.AIM_PARAMETER, isAim);
  }

  public void muzzleFlashTurnOn()
  {
    muzzleFlash.SetActive(true);
  }

  public void muzzleFlashTurnOff()
  {
    muzzleFlash.SetActive(false);
  }

  public void playSoundShoot()
  {
    audioSource.PlayOneShot(shootSound);
  }

  public void playSoundReload()
  {
    audioSource.PlayOneShot(reloadSound);
  }

  public void turnOnAttackPoint()
  {
    attackPoint.SetActive(true);
  }

  public void turnOffAttackPoint()
  {
    if (attackPoint.activeInHierarchy)
    {
      attackPoint.SetActive(false);
    }
  }

  public bool isBulletPrefabExist()
  {
    return bulletPrefab != null;
  }

}
