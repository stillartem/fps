using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{
  private AudioSource footStepSound;
  private CharacterController characterController;
  private float accumulateDistance;


  [SerializeField]
  private AudioClip[] footStepClip;

  [HideInInspector]
  public float volume;

  [HideInInspector]
  public float stepDistance;

  void Awake()
  {
    footStepSound = GetComponent<AudioSource>();
    characterController = GetComponentInParent<CharacterController>();
  }

  void LateUpdate()
  {
    CheckToPlayFootStepSound();
  }

  private void CheckToPlayFootStepSound()
  {
    if (!characterController.isGrounded) return;

    if (characterController.velocity.sqrMagnitude > 0)
    {
      accumulateDistance += Time.deltaTime;
      if (accumulateDistance > stepDistance)
      {
        footStepSound.volume = volume;
        footStepSound.clip = footStepClip[Random.Range(0, footStepClip.Length)];
        footStepSound.Play();
        accumulateDistance = 0;
      }
    }
    else
    {
      accumulateDistance = 0;
    }
  }
}
