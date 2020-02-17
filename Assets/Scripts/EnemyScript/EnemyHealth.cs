using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  private EnemyController enemyController;

  [SerializeField]
  public int health;

  void Awake()
  {
    enemyController = GetComponent<EnemyController>();
  }


  public void ApplyDamage(int damage)
  {
    if (enemyController.enemyState == EnemyState.PATROL)
    {
      enemyController.ChangeStateToAttack();
    }
    health = health - damage;
    if (health <= 0)
    {
      health = 0;
      enemyController.Dead();
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
