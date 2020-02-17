using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
  private Animator animator;

  void Awake()
  {
    animator = GetComponent<Animator>();
  }



  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Walk()
  {
    animator.SetBool(AnimationAction.WALK_PARAMETER, true);
    animator.SetBool(AnimationAction.RUN_PARAMETER, false);
  }

  public void Run()
  {
    animator.SetBool(AnimationAction.RUN_PARAMETER, true);
  }

  public void Stop()
  {
    animator.SetBool(AnimationAction.WALK_PARAMETER, false);
    animator.SetBool(AnimationAction.RUN_PARAMETER, false);
  }

  public void Attack()
  {
    animator.SetTrigger(AnimationAction.ATTACK_TRIGGER);
  }

  public void Dead()
  {
      animator.SetTrigger(AnimationAction.DEAD_TRIGGER);
  }

  public void GetHit(bool hardHit)
  {
    if (hardHit)
    {
      animator.SetTrigger(AnimationAction.HIT_HARD_PARAMETER);

    }
    else
    {
      animator.SetTrigger(AnimationAction.HIT_NORMAL_PARAMETER);
    }
  }
}
