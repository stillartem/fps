using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrounch : MonoBehaviour
{

  public float sprintSpeed = 10f;
  public float crouchSpeed = 3f;

  private PlayerMovement playerMovement;
  private Transform lookTransform;
  private PlayerFootSteps playerFootSteps;

  private readonly float sprintVolume = 1f;
  private readonly float walkVolume = 0.5f;
  private readonly float crouchVolume = 0.1f;

  private readonly float walkStepDistance = 0.4f;
  private readonly float sprintStepDistance = 0.25f;
  private readonly float crouchStepDistance = 0.5f;
  

  private float standHeight;
  private float crouchHeight;
  private bool isCrouch;
  private float moveSpeed;

  void Awake()
  {
    playerMovement = GetComponent<PlayerMovement>();
    playerFootSteps = GetComponentInChildren<PlayerFootSteps>();
    lookTransform = transform.GetChild(0);
  }
  // Start is called before the first frame update
  void Start()
  {
    isCrouch = false;
    standHeight = lookTransform.localPosition.y;
    crouchHeight = standHeight - standHeight * 0.4f;
    moveSpeed = playerMovement.speed;
    playerFootSteps.volume = walkVolume;
    playerFootSteps.stepDistance = walkStepDistance;
  }

  // Update is called once per frame
  void Update()
  {
    Sprint();
    Crouch();
  }

  void FixedUpdate()
  {

  }

  private void Sprint()
  {
    if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouch)
    {
      playerMovement.speed = sprintSpeed;
      playerFootSteps.stepDistance = sprintStepDistance;
      playerFootSteps.volume = crouchVolume;

    }

    if (Input.GetKeyUp(KeyCode.LeftShift))
    {
      playerMovement.speed = moveSpeed;
      playerFootSteps.stepDistance = walkStepDistance;
      playerFootSteps.volume = walkVolume;
    }
  }

  private void Crouch()
  {
    if (Input.GetKeyDown(KeyCode.C))
    {
      if (isCrouch)
      {
        StandUp();
      }
      else
      {
        StandDown();
      }

    }
  }

  private void StandUp()
  {
    isCrouch = false;
    lookTransform.localPosition = new Vector3(0f, standHeight, 0f);
    playerMovement.speed = moveSpeed;
    playerFootSteps.stepDistance = walkStepDistance;
    playerFootSteps.volume = walkVolume;
  }

  private void StandDown()
  {
    isCrouch = true;
    lookTransform.localPosition = new Vector3(0f, crouchHeight, 0f);
    playerMovement.speed = crouchSpeed;
    playerFootSteps.stepDistance = crouchStepDistance;
    playerFootSteps.volume = crouchVolume;

  }
}
