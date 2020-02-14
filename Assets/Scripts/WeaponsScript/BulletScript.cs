using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BulletScript : MonoBehaviour
{
  private Rigidbody rb;

  public float speed = 30f;
  public float damage = 15f;
  public float deactivateTime = 3f;

  void Awake()
  {
    rb = GetComponent<Rigidbody>();
  }

  // Start is called before the first frame update
  void Start()
  {
 //   Invoke("deactivateGameObject", deactivateTime);
    rb.velocity = Camera.main.transform.forward * speed;
    transform.LookAt(transform.position + rb.velocity);
  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnTriggerEnter(Collider collider)
  {
    if (collider.CompareTag(Tags.ENEMY_TAG))
    {

    }
  }

  private void deactivateGameObject()
  {
    Destroy(gameObject);
  }
}
