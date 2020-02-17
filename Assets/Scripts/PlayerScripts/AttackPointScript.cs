using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointScript : MonoBehaviour
{

  public float damage = 2f;
  public float radius = 1f;
  public LayerMask layerMask;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
    if (hits.Length > 0)
    {
      foreach(Collider hit in hits)
      {
        print("we touched " + hit.gameObject.tag);
      }
    }
  }
}
