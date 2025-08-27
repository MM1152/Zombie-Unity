using UnityEngine;
using UnityEngine.AI;


public class Zombie : LivingEntity
{
    static readonly int IdHasTarget = Animator.StringToHash("HasTarget");
    static readonly int IdDie = Animator.StringToHash("Die");
    public enum Status
    {
        Idle,
        Trace,
        Attack,
        Die,
    }

    private Status currentStatus;
    public Status CurrentStatus
    {
        get => currentStatus;
        set
        {
            var prevStatus = currentStatus;
            currentStatus = value;

            switch(currentStatus)
            {
                case Status.Idle:
                    animator.SetBool(IdHasTarget, false);
                    navMeshAgent.isStopped = true;
                    break;
                case Status.Trace:
                    animator.SetBool(IdHasTarget, true);
                    navMeshAgent.isStopped = false;
                    break;
                case Status.Attack:
                    animator.SetBool(IdHasTarget, false);
                    navMeshAgent.isStopped = true;
                    break;
                case Status.Die:
                    animator.SetTrigger(IdDie);
                    navMeshAgent.isStopped = true;
                    break;
            }
        }
    }

    public float traceDistance;
    public float attackDistance;

    public float damage = 10f;
    public float lastAttackTime;
    public float attackInterval = 0.5f;
    
    public Transform target;
    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            navMeshAgent.SetDestination(new Vector3(10f,0f,0f));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            navMeshAgent.SetDestination(new Vector3(0f, 0f, 0f));
        }

        switch (currentStatus)
        {
            case Status.Idle:
                UpdateIdle();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
            case Status.Attack:
                UpdateAttack();
                break;
            case Status.Die:
                UpdateDie();
                break;
        }
    }
    private void UpdateDie()
    {

    }
    private void UpdateAttack()
    {
        if (target == null ||
            (target != null && Vector3.Distance(target.position, transform.position) > attackDistance))
        {
            CurrentStatus = Status.Trace;
            return;
        }

        var lookAt = target.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);

        if(lastAttackTime + attackInterval < Time.time)
        {
            lastAttackTime = Time.time;
            var damageAble = target.GetComponent<IDamageAble>();
            if(damageAble != null)
            {
                damageAble.OnDamage(damage , transform.position , -transform.forward);
            }
        }
    }
    private void UpdateTrace()
    {
        if (target != null &&
            Vector3.Distance(target.position, transform.position) < attackDistance)
        {
            CurrentStatus = Status.Attack;
            return;
        }

        if (target == null ||
           Vector3.Distance(target.position , transform.position) > traceDistance)
        {
            CurrentStatus = Status.Idle;
            return;
        }

        navMeshAgent.SetDestination(target.position);
    }
    private void UpdateIdle()
    {
        if(target != null && 
            Vector3.Distance(transform.position , target.position) < traceDistance)
        {
            CurrentStatus = Status.Trace;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    protected override void Die()
    {
        base.Die();
        CurrentStatus = Status.Die;
    }
}
