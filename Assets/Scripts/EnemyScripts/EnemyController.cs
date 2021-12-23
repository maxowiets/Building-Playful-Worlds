using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyPatterns;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamagable
{
    NavMeshAgent nav;
    PlayerControls player;
    public ObstacleBlock obstacleTarget;
    public EnemyType enemyType;
    public GameObject attackProjectile;

    Animator anim;
    public AnimationClip attackAnimation;

    float attackSpeed;
    public float damage;
    public float attackRange;
    public float despawnTimer;

    public float health;
    public float Health { get; set; }

    public GameObject pickUpPrefab;
    public float pickUpChance;

    FSM m_fsm = new FSM();

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerControls>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        attackSpeed = attackAnimation.length;
        Health = health;

        m_fsm.AddState(typeof(Chase), new Chase(m_fsm, this, nav, player, anim));
        m_fsm.AddState(typeof(Death), new Death(m_fsm, nav, anim));
        switch (enemyType)
        {
            case EnemyType.NORMAL:
                m_fsm.AddState(typeof(Attack), new Attack(m_fsm, this, nav, attackSpeed, anim));
                break;
            case EnemyType.FIRE:
                m_fsm.AddState(typeof(FireAttack), new FireAttack(m_fsm, this, nav, attackSpeed, anim));
                break;
        }

        m_fsm.SetCurrentState(m_fsm.GetState(typeof(Chase)));
    }

    private void Update()
    {
        m_fsm?.OnUpdate();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            m_fsm.SetCurrentState(m_fsm.GetState(typeof(Death)));
            UIManager.Instance.waveManagerUI.DecreaseEnemiesToKill();
            GetComponent<CapsuleCollider>().enabled = false;
            if (Random.Range(0,100f) < pickUpChance)
            {
                Instantiate(pickUpPrefab, transform.position + Vector3.up + transform.forward * 0.5f, Quaternion.identity);
            }
            Invoke("DespawnEnemy", despawnTimer);
        }
    }

    void DespawnEnemy()
    {
        Destroy(gameObject);
    }

    public void ProjectileAttack()
    {
        Instantiate(attackProjectile, transform.position + Vector3.up + Vector3.forward * 1.5f, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent(typeof(IDamagable)))
        {
            if (other.gameObject.GetInstanceID() != gameObject.GetInstanceID())
            {
                other.GetComponent<IDamagable>().TakeDamage(damage);
            }
        }
    }
}

public class Chase : State
{
    EnemyController enemy;
    NavMeshAgent nav;
    PlayerControls player;
    Animator anim;

    float timerSetDestination = 0.5f;
    float timeSetDestination;

    public Chase(FSM fsm, EnemyController enemy, NavMeshAgent nav, PlayerControls player, Animator anim) : base(fsm)
    {
        this.enemy = enemy;
        this.nav = nav;
        this.player = player;
        this.anim = anim;
    }

    public override void OnEnter()
    {
        timeSetDestination = Time.time;
        anim.SetTrigger("Walk");
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

        float distance = enemy.transform.position.CheckPlaneDistanceTo(player.transform.position);
        if (distance <= enemy.attackRange)
        {
            switch (enemy.enemyType)
            {
                case EnemyType.NORMAL:
                    m_fsm.SetCurrentState(m_fsm.GetState(typeof(Attack)));
                    break;
                case EnemyType.FIRE:
                    if (enemy.enemyType == EnemyType.FIRE && Vector3.Angle(enemy.transform.forward, player.transform.position - (enemy.transform.position + Vector3.up)) < 3)
                    {
                        m_fsm.SetCurrentState(m_fsm.GetState(typeof(FireAttack)));
                    }
                    break;
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

public class Attack : State
{
    EnemyController enemy;
    float attackTimer;
    float attackTime;
    NavMeshAgent nav;
    float navSpeed;
    Animator anim;

    public Attack(FSM fsm, EnemyController enemy, NavMeshAgent nav, float attackSpeed, Animator anim) : base(fsm)
    {
        this.enemy = enemy;
        this.nav = nav;
        this.anim = anim;
        attackTimer = attackSpeed;
    }

    public override void OnEnter()
    {
        attackTime = 0;
        navSpeed = nav.speed;
        nav.speed = 0;
        anim.SetTrigger("Attack");
    }

    public override void OnUpdate()
    {
        attackTime += Time.deltaTime;

        if (attackTime >= attackTimer)
        {
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
public class FireAttack : State
{
    EnemyController enemy;
    float attackTimer;
    float attackTime;
    NavMeshAgent nav;
    float navSpeed;
    Animator anim;
    GameObject fireBall;
    bool shotProjectile;

    public FireAttack(FSM fsm, EnemyController enemy, NavMeshAgent nav, float attackSpeed, Animator anim) : base(fsm)
    {
        this.enemy = enemy;
        this.nav = nav;
        this.anim = anim;
        attackTimer = attackSpeed;
    }

    public override void OnEnter()
    {
        attackTime = 0;
        navSpeed = nav.speed;
        nav.speed = 0;
        anim.SetTrigger("Attack");
    }

    public override void OnUpdate()
    {
        attackTime += Time.deltaTime;

        if (attackTime >= attackTimer * 0.25f && !shotProjectile)
        {
            enemy.ProjectileAttack();
            shotProjectile = true;
        }
        if (attackTime >= attackTimer)
        {
            m_fsm.SetCurrentState(m_fsm.GetState(typeof(Chase)));
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        nav.speed = navSpeed;
        shotProjectile = false;
    }
}

public class Death : State
{
    EnemyController enemy;
    NavMeshAgent nav;
    Animator anim;

    public Death(FSM fsm, NavMeshAgent nav, Animator anim) : base(fsm)
    {
        this.nav = nav;
        this.anim = anim;
    }

    public override void OnEnter()
    {
        anim.SetTrigger("Death");
        nav.enabled = false;
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