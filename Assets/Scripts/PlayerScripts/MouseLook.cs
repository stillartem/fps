using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

  [SerializeField]
  private Transform playerRoot, cameraRoot;

  [SerializeField]
  private bool invert;

  [SerializeField]
  private bool canUnlock = true;

  [SerializeField]
  private float sensitive = 5f;

  [SerializeField]
  private int smoothStep = 10;

  [SerializeField]
  private float smoothWeight = 0.4f;

  [SerializeField]
  private float rollAngle = 10f;

  [SerializeField]
  private float rollSpeed = 3f;


  [SerializeField]
  private Vector2 defaultLookLimits;

  [SerializeField]
  private Vector2 lookAngles;


  private Vector2 currentMouseLook;

  private Vector2 smoothMove;

  private int lastLookFrame;



  void Awake()
  {
    defaultLookLimits = new Vector2(-70, 80f);
  }

  // Start is called before the first frame update
  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
  }

  void FixedUpdate()
  {
    LockAndUnlockCursor();
    if (isCursorLock())
    {
      LookAround();
    }
  }

  void Update()
  {

  }

  // Update is called once per frame


  private void LockAndUnlockCursor()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (isCursorLock())
      {
        Cursor.lockState = CursorLockMode.None;
      }
      else
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }

    }
  }

  private bool isCursorLock()
  {
    return Cursor.lockState == CursorLockMode.Locked;
  }

  private void LookAround()
  {
    currentMouseLook = new Vector2(
        Input.GetAxis(Axis.MOUSE_Y),
        Input.GetAxis(Axis.MOUSE_X)
    );
    lookAngles.x += currentMouseLook.x * sensitive * (invert ? -1f : 1f);
    lookAngles.y += currentMouseLook.y * sensitive;

    lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);

    cameraRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, 0f);
    playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);

  }
}
