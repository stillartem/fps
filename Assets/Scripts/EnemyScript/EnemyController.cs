using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
  PATROL,
  ATTACK,
  CHASE
}
public class EnemyController : MonoBehaviour
{
  [HideInInspector]
  public EnemyState enemyState;

  public EnemyAnimator enemyAnimator;
  private NavMeshAgent navMeshAgent;
  private Transform target;
  private float patrolTimer;
  private float attackTimer;
  private float currentChaseDistance;

  public GameObject attackPoint;
  public float walkSpeed = 0.5f;
  public float runSpeed = 4f;
  public float attackDistance = 1.8f;
  public float chaseAfterAttackDistance = 2f;
  public float waitBeforeAttack = 1.4f;
  public float chaseDistance = 10f;


  public float patrolRadius = 40f;
  public float patrolForThisTime = 2f;

  void Awake()
  {
    enemyAnimator = GetComponent<EnemyAnimator>();
    navMeshAgent = GetComponent<NavMeshAgent>();
    target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
  }

  void Start()
  {
    attackTimer = waitBeforeAttack;
    ChangeStateToPatrol();
  }

  // Update is called once per frame
  void Update()
  {
    switch (enemyState)
    {
      case EnemyState.PATROL:
        Patrol();
        break;
      case EnemyState.CHASE:
        Chase();
        break;
      case EnemyState.ATTACK:
        Attack();
        break;
    }
  }

  private void ChangeStateToPatrol()
  {
    patrolTimer = patrolForThisTime;
    currentChaseDistance = chaseDistance;
    enemyState = EnemyState.PATROL;
    navMeshAgent.isStopped = false;
    navMeshAgent.speed = walkSpeed;
    enemyAnimator.Walk();
    SetNewRandomDestination();

  }

  public void ChangeStateToChase()
  {
    navMeshAgent.isStopped = false;
    navMeshAgent.speed = runSpeed;
    enemyAnimator.Run();
    enemyState = EnemyState.CHASE;
  }

  public void ChangeStateToAttack()
  {
    navMeshAgent.isStopped = true;
    navMeshAgent.velocity = Vector3.zero;
    enemyAnimator.Stop();
    enemyState = EnemyState.ATTACK;
    chaseDistance = currentChaseDistance;
    navMeshAgent.transform.LookAt(target);
  }

  private void Patrol()
  {
    patrolTimer += Time.deltaTime;
    if (patrolTimer > patrolForThisTime)
    {
      SetNewRandomDestination();
      patrolTimer = 0;
    }
    CheckChaseDistance();
  }

  private void Chase()
  {
    navMeshAgent.SetDestination(target.position);
    CheckAttackDistance();
    CheckStopChaseDistance();
  }

  private void Attack()
  {
    attackTimer += Time.deltaTime;
    if (attackTimer > waitBeforeAttack)
    {
      enemyAnimator.Attack();
      attackTimer = 0;
    }
   
    CheckChaseDistance();
  }

  private void SetNewRandomDestination()
  {
    Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
    randomDirection += transform.position;
    NavMesh.SamplePosition(randomDirection, out NavMeshHit navMeshHit, patrolRadius, NavMesh.AllAreas);

    navMeshAgent.SetDestination(navMeshHit.position);

  }

  private void CheckStopChaseDistance()
  {
    if (Vector3.Distance(transform.position, target.position) > chaseDistance)
    {
      ChangeStateToPatrol();
    }
  }

  private void CheckChaseDistance()
  {
    float distance = Vector3.Distance(transform.position, target.position);
    if (distance <= chaseDistance && distance > attackDistance)
    {
      ChangeStateToChase();
    }
  }


  private void CheckAttackDistance()
  {
    if (Vector3.Distance(transform.position, target.position) <= attackDistance)
    {
      ChangeStateToAttack();
    }
  }

  public void Dead()
  {
    enemyAnimator.Dead();
    Invoke("DestroyEnemy", 2f);
  }

  void DestroyEnemy()
  {
    Destroy(gameObject);
  }

}
