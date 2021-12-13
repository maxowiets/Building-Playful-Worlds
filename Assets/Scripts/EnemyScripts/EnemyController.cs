using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyPatterns;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent nav;
    public List<PatrolPoint> patrolPoints = new List<PatrolPoint>();
    public int patrolIndex;

    PlayerControls player;
    public ObstacleBlock obstacleTarget;

    public float attackSpeed;
    public float damage;
    public float attackRange;

    FSM m_fsm = new FSM();

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerControls>();
    }

    private void Start()
    {
        m_fsm.AddState(typeof(Chase), new Chase(m_fsm, this, nav, player));
        m_fsm.AddState(typeof(Attack), new Attack(m_fsm, this, nav));

        m_fsm.SetCurrentState(m_fsm.GetState(typeof(Chase)));
    }

    private void Update()
    {
        m_fsm?.OnUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRange, 1.5f);
    }
}

public class Chase : State
{
    EnemyController enemy;
    NavMeshAgent nav;
    PlayerControls player;

    float timerSetDestination = 0.5f;
    float timeSetDestination;

    public Chase(FSM fsm, EnemyController enemy, NavMeshAgent nav, PlayerControls player) : base(fsm)
    {
        this.enemy = enemy;
        this.nav = nav;
        this.player = player;
    }

    public override void OnEnter()
    {
        timeSetDestination = Time.time;
    }

    public override void OnUpdate()
    {
        if (timeSetDestination <= Time.time)
        {
            timeSetDestination = Time.time + timerSetDestination;
            nav.SetDestination(player.transform.position);
        }

        nav.CalculatePath(nav.destination, nav.path);
        if(nav.path.status == NavMeshPathStatus.PathPartial)
        {
            RaycastHit hit;
            if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, enemy.attackRange))
            {
                if (hit.collider.GetComponent<ObstacleBlock>() && nav.velocity == Vector3.zero)
                {
                    enemy.obstacleTarget = hit.collider.GetComponent<ObstacleBlock>();
                    m_fsm.SetCurrentState(m_fsm.GetState(typeof(Attack)));
                }
            }
        }
    }

    public override void OnFixedUpdate()
    {
        //walking code
    }

    public override void OnExit()
    {

    }
}

public class DestroyObject : State
{
    public DestroyObject(FSM fsm, EnemyController enemy) : base(fsm)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }
}

public class Attack : State
{
    EnemyController enemy;
    float attackTimer;
    float attackTime;
    NavMeshAgent nav;
    float navSpeed;

    public Attack(FSM fsm, EnemyController enemy, NavMeshAgent nav) : base(fsm)
    {
        this.enemy = enemy;
        this.nav = nav;
    }

    public override void OnEnter()
    {
        attackTime = 0;
        attackTimer = enemy.attackSpeed;
        navSpeed = nav.speed;
        nav.speed = 0;
    }

    public override void OnUpdate()
    {
        attackTime += Time.deltaTime;

        if (attackTime >= attackTimer)
        {
            Collider[] hits = Physics.OverlapSphere(enemy.transform.position + enemy.transform.forward * enemy.attackRange, enemy.attackRange);
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].GetComponent(typeof(IDamagable)))
                {
                    if (hits[i].gameObject != enemy.gameObject)
                    {
                        hits[i].GetComponent<IDamagable>().TakeDamage(enemy.damage);
                    }
                }
            }
            if (enemy.obstacleTarget != null)
            {
                m_fsm.SetCurrentState(m_fsm.GetState(typeof(Attack)));
            }
            else
            {
                m_fsm.SetCurrentState(m_fsm.GetState(typeof(Chase)));
            }
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        nav.speed = navSpeed;
    }
}