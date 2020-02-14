using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  private CharacterController characterController;
  private Vector3 moveDirection;
  private readonly float gravity = 20f;
  private float verticalVelocity;

  public float speed = 5f;
  [SerializeField] private float jumpForce = 10f;

  void Awake()
  {
    characterController = GetComponent<CharacterController>();

  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void FixedUpdate()
  {
    MovePlayer();
  }


  private void MovePlayer()
  {
    moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0, Input.GetAxis(Axis.VERTICAL));
    moveDirection = transform.TransformDirection(moveDirection);
    moveDirection = moveDirection * speed * Time.deltaTime;
    ApplyGravity();

    characterController.Move(moveDirection); 

  }

  private void ApplyGravity()
  {
    verticalVelocity -= gravity * Time.deltaTime;
    if (characterController.isGrounded)
    {
      PlayerJump();
    }

    moveDirection.y = verticalVelocity * Time.deltaTime;


  }

  private void PlayerJump()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      verticalVelocity = jumpForce;
    }

   
  }

}
