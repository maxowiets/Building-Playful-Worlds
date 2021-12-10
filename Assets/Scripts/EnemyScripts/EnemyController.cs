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
    public float aggroRange;
    PlayerControls player;
    public IDamagable target;
    public float attackSpeed;
    public float damage;

    FSM m_fsm = new FSM();

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerControls>();
    }

    private void Start()
    {
        m_fsm.AddState(typeof(Chase), new Chase(m_fsm, this, nav, player));
        //m_fsm.AddState(typeof(Kill), new Kill(m_fsm, this));
        m_fsm.AddState(typeof(DestroyObject), new Chase(m_fsm, this, nav, player));

        m_fsm.SetCurrentState(m_fsm.GetState(typeof(Chase)));
    }

    private void Update()
    {
        m_fsm?.OnUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}

public class Idle : State
{
    EnemyController enemy;
    NavMeshAgent nav;
    float originalNavSpeed;
    PlayerControls player;

    float idleTimer;
    float idleTime;

    public Idle(FSM fsm, EnemyController enemy, NavMeshAgent nav, PlayerControls player) : base(fsm)
    {
        this.enemy = enemy;
        this.nav = nav;
        this.player = player;
    }

    public override void OnEnter()
    {
        idleTimer = Random.Range(2f, 7f);
        idleTime = 0;
        originalNavSpeed = nav.speed;
        nav.speed = 0;
    }

    public override void OnUpdate()
    {
        idleTime += Time.deltaTime;
        if (idleTime >= idleTimer)
        {
            m_fsm.SetCurrentState(m_fsm.GetState(typeof(Patrol)));
        }

        if (Vector3.Distance(enemy.transform.position, player.transform.position) < enemy.aggroRange)
        {
            m_fsm.SetCurrentState(m_fsm.GetState(typeof(Chase)));
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        nav.speed = originalNavSpeed;
    }
}

public class Patrol : State
{
    EnemyController enemy;
    NavMeshAgent nav;
    PlayerControls player;

    public Patrol(FSM fsm, EnemyController enemy, NavMeshAgent nav, PlayerControls player) : base(fsm)
    {
        this.enemy = enemy;
        this.nav = nav;
        this.player = player;
    }

    public override void OnEnter()
    {
        nav.SetDestination(enemy.patrolPoints[enemy.patrolIndex].transform.position);
    }

    public override void OnUpdate()
    {
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            enemy.patrolIndex++;
            if (enemy.patrolIndex >= enemy.patrolPoints.Count)
            {
                enemy.patrolIndex = 0;
            }
            nav.SetDestination(enemy.patrolPoints[enemy.patrolIndex].transform.position);
            m_fsm.SetCurrentState(m_fsm.GetState(typeof(Idle)));
        }

        if (Vector3.Distance(enemy.transform.position, player.transform.position) < enemy.aggroRange)
        {
            m_fsm.SetCurrentState(m_fsm.GetState(typeof(Chase)));
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {

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
        enemy.aggroRange *= 2;
    }

    public override void OnUpdate()
    {
        if (timeSetDestination <= Time.time)
        {
            timeSetDestination = Time.time + timerSetDestination;
            nav.SetDestination(player.transform.position);
        }

        //code for idle state
        //if (Vector3.Distance(enemy.transform.position, player.transform.position) > enemy.aggroRange)
        //{
        //    m_fsm.SetCurrentState(m_fsm.GetState(typeof(Idle)));
        //}

        nav.CalculatePath(nav.destination, nav.path);
        if(nav.path.status == NavMeshPathStatus.PathPartial)
        {
            RaycastHit hit;
            if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, 1f))
            {
                if (hit.collider.GetComponent<ObstacleBlock>() && nav.velocity == Vector3.zero)
                {
                    enemy.target = hit.collider.GetComponent<ObstacleBlock>();
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
        enemy.aggroRange *= 0.5f;
    }
}

public class Kill : State
{
    EnemyController enemy;

    public Kill(FSM fsm, EnemyController enemy) : base(fsm)
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        //walking code
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
    EnemyController enemy;
    float attackTimer;
    float attackTime;

    public DestroyObject(FSM fsm, EnemyController enemy) : base(fsm)
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        attackTime = 0;
        attackTimer = enemy.attackSpeed;
    }

    public override void OnUpdate()
    {
        attackTime += Time.deltaTime;
        if (attackTime >= attackTimer)
        {
            attackTime = 0;
            Attack();
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        enemy.target = null;
    }

    void Attack()
    {
        enemy.target.TakeDamage(enemy.damage);
    }
}