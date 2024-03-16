using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("SO")]
    public EnemySO EnemySO;

    [Header("EnemyStats")]
    public float CurrentHp;

    [Header("Range")]
    public float GunAttackRange = 1600f;
    public float GunChasingRange = 2500f;
    public float ChasingRange = 100f;

    [Header("Targets")]
    public Collider[] Targets;
    [field : SerializeField] public GameObject Target { get; private set; }
    [field: SerializeField] public float Distance { get; private set; }

    [Header("GameObjects")]
    public ParticleSystem GunFireParticle;
    [field: SerializeField] public GameObject SwordCollider { get; private set; }



    [SerializeField] public bool IsHit { get; set; }
    public bool IsGun { get; set; }
    public bool IsSword { get; set; }
    public EnemyStateContext _enemyStateContext { get; private set; }
    public Vector3 StartPosition { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public CapsuleCollider Collider { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    private IEnemyState _idleState;
    private IEnemyState _chasingState;
    private IEnemyState _walkState;
    private IEnemyState _hitState;
    private IEnemyState _dieState;
    private IEnemyState _attackState;
    private IEnemyState _gunChasingState;
    private IEnemyState _gunAttackState;

    [SerializeField] private GameObject _sword;
    [SerializeField] private GameObject _gun;
    [HideInInspector] public float OriginGunChasingRange;
    [HideInInspector] public float OriginChasingRange;
    private LayerMask _layerMask;
    [HideInInspector] public Vector3 ShipLocalPosition;
    [HideInInspector] public bool IsFix;
    [HideInInspector] public bool IsInvincibilityTime;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        Collider = GetComponent<CapsuleCollider>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _enemyStateContext = new EnemyStateContext(this);
        _idleState = gameObject.AddComponent<EnemyIdleState>();
        _chasingState = gameObject.AddComponent<EnemyChaisngState>();
        _walkState = gameObject.AddComponent<EnemyWalkState>();
        _hitState = gameObject.AddComponent<EnemyHitState>();
        _dieState = gameObject.AddComponent<EnemyDieState>();
        _attackState = gameObject.AddComponent<EnemyAttackState>();
        _gunChasingState = gameObject.AddComponent<EnemyGunChaisngState>();
        _gunAttackState = gameObject.AddComponent<EnemyGunAttackState>();

        if(transform.CompareTag("Partner"))
        {
            _layerMask = LayerMask.NameToLayer("Enemy");
        }
        else if(transform.CompareTag("Enemy"))
        {
            _layerMask = LayerMask.NameToLayer("Partner");
        }

        StartCoroutine(COTargetting());
        StartPosition = gameObject.transform.position;
        OriginChasingRange = ChasingRange;
        OriginGunChasingRange = GunChasingRange;
        IsHit = false;
        IsGun = false;
        IsSword = false;
        IsFix = false;
        IsInvincibilityTime = false;
        CurrentHp = EnemySO.MaxHp;
        Distance = -1; // Target이 null 일때 임의로 음수 할당

        _enemyStateContext.Transition(_idleState);
    }

    private void Update()
    {
        if (Targets.Length == 0) Distance = -1;
        else Distance = (Target.transform.position - transform.position).sqrMagnitude;

        if (IsFix) transform.localPosition = ShipLocalPosition;

        if(transform.CompareTag("Enemy"))
        {
            if (IsEnemyShip() && transform.parent.parent.GetComponent<Follow_Player>().IsMove && !IsFix)
            {
                ShipLocalPosition = transform.localPosition;
                IsFix = true;
            }
        }
        else if(transform.CompareTag("Partner"))
        {
            if (IsShip() && GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanShipControll
                && !IsFix)
            {
                transform.parent = GameManager.I.ShipManager.Ship.transform;
                ShipLocalPosition = transform.localPosition;
                IsFix = true;
            }
        }
    }

    public void IdleStart()
    {
        _enemyStateContext.Transition(_idleState);
    }

    public void ChasingStart()
    {
        _enemyStateContext.Transition(_chasingState);
    }

    public void WalkStart()
    {
        _enemyStateContext.Transition(_walkState);
    }

    public void HitStart()
    {
        _enemyStateContext.Transition(_hitState);
    }

    public void DieStart()
    {
        _enemyStateContext.Transition(_dieState);
    }
    
    public void AttackStart()
    {
        _enemyStateContext.Transition(_attackState);
    }

    public void GunChasingStart()
    {
        _enemyStateContext.Transition(_gunChasingState);
    }

    public void GunAttackStart()
    {
        _enemyStateContext.Transition(_gunAttackState);
    }

    public bool IsWalk()
    {
        Ray[] rays = new Ray[6]
        {
            new Ray(transform.position + new Vector3(0.3f, 0.3f, 1.5f), transform.forward),
            new Ray(transform.position + new Vector3(0.3f, 1.5f, 1.5f), transform.forward),
            new Ray(transform.position + new Vector3(0.3f, 3f, 1.5f), transform.forward),
            new Ray(transform.position + new Vector3(-0.3f, 0.3f, 1.5f), transform.forward),
            new Ray(transform.position + new Vector3(-0.3f, 1.5f, 1.5f), transform.forward),
            new Ray(transform.position + new Vector3(-0.3f, 3f, 1.5f), transform.forward),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1f, EnemySO.ObstacleLayerMask))
            {
                return false;
            }
        }

        return true;
    }

    public void SwordEquip()
    {
        _gun.SetActive(false);
        _sword.SetActive(true);
    }

    public void GunEquip()
    {
        _sword.SetActive(false);
        _gun.SetActive(true);
    }

    IEnumerator COTargetting()
    {
        while(true)
        {
            Targetting();

            yield return new WaitForSeconds(EnemySO.TargettingTime);
        }
    }

    private void Targetting()
    {
        int layerMask = (1 << _layerMask);
        Targets = Physics.OverlapSphere(transform.position, EnemySO.OverlapSphereRange, layerMask);

        if (Targets.Length > 0) Target = Targets[0].gameObject;
        else return;

        foreach (Collider enemy in Targets)
        { 
            if (SqrDistance(enemy.gameObject) <= SqrDistance(Target.gameObject))
            {
                if (enemy.gameObject.CompareTag("Partner") || enemy.gameObject.CompareTag("Enemy"))
                {
                    if (enemy.gameObject.GetComponent<EnemyController>().CurrentHp > 0) Target = enemy.gameObject;
                }
                else if (enemy.gameObject.CompareTag("Player"))
                {
                    if (enemy.gameObject.GetComponent<PlayerController>().CurrentHp > 0) Target = enemy.gameObject;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, EnemySO.OverlapSphereRange);
    }

    private float SqrDistance(GameObject enemy)
    {
        return (enemy.transform.position - transform.position).sqrMagnitude;
    }

    public bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.5f, EnemySO.GroundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsShip()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.5f, EnemySO.ShipLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public bool IsEnemyShip()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.3f) + (Vector3.up * 0.3f), Vector3.down),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.5f, EnemySO.EnemyShipLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void DropItem()
    {
        foreach (GameObject item in EnemySO.DropItems)
        {
            int randomIndex = Random.Range(0, EnemySO.ItemMaxCount + 1);

            for (int i = 0; i < randomIndex; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 3.5f), Random.Range(-1f, 1f));
                Vector3 randomRotation = new Vector3(Random.Range(-90f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
                Instantiate(item, transform.position + randomPosition, Quaternion.Euler(randomRotation));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.forward * 0.3f) + (Vector3.up * 0.3f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.forward * 0.3f) + (Vector3.up * 0.3f), Vector3.down);
        Gizmos.DrawRay(transform.position + (transform.right * 0.3f) + (Vector3.up * 0.3f), Vector3.down);
        Gizmos.DrawRay(transform.position + (-transform.right * 0.3f) + (Vector3.up * 0.3f), Vector3.down);
    }
}
