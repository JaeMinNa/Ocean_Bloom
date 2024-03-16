using UnityEngine;

public class Enemy_Cannon2 : MonoBehaviour
{
    public Transform Target; // 타겟
    public Transform LeftCannon; // 왼쪽 대포
    public Transform RightCannon; // 오른쪽 대포
    public GameObject Cannon_Shot;
    public Transform Cannon_Leftpos;
    public Transform Cannon_Rightpos;
    public float RotationSpeed;    
    public ParticleSystem LeftCannonParticleSystem;
    public ParticleSystem RightCannonParticleSystem;
    [SerializeField] private GameObject Enemys;

    // 발사 딜레이
    private float currentTime;
    public float FireInterval = 2f;
    private float lastFireTime;

    void Start()
    {
        lastFireTime = -FireInterval;
    }

    void Update()
    {
        currentTime = Time.time;
        float leftDistance = Vector3.Distance(LeftCannon.position, Target.position);
        float rightDistance = Vector3.Distance(RightCannon.position, Target.position);

        if (leftDistance < rightDistance && !Target.GetComponent<PlayerController>().IsEnemyShip() && Enemys.transform.childCount != 0)
        {
            // 왼쪽 대포가 가까우면 타겟을 향하는 방향으로 회전
            RotateTowardsTarget(LeftCannon);
            // 왼쪽 대포에서 공격
            if (currentTime - lastFireTime >= FireInterval)
            {
                Debug.Log("Fire!");
                GameManager.I.SoundManager.StartSFX("CannonEnemy", transform.position);
                Instantiate(Cannon_Shot, Cannon_Leftpos.position, Cannon_Leftpos.rotation);
                LeftCannonParticleSystem.Play();
                lastFireTime = currentTime;

            }
        }
        else if (rightDistance < leftDistance && !Target.GetComponent<PlayerController>().IsEnemyShip() && Enemys.transform.childCount != 0)
        {
            // 오른쪽 대포가 가까우면 타겟을 향하는 방향으로 회전
            RotateTowardsTarget(RightCannon);
            // 오른쪽 대포에서 공격
            if (currentTime - lastFireTime >= FireInterval)
            {
                Debug.Log("Fire!");
                GameManager.I.SoundManager.StartSFX("CannonEnemy", transform.position);
                Instantiate(Cannon_Shot, Cannon_Rightpos.position, Cannon_Rightpos.rotation);
                RightCannonParticleSystem.Play();
                lastFireTime = currentTime;

            }
        }
    }

    private void RotateTowardsTarget(Transform cannon)
    {
        // 타겟을 향하는 방향 벡터를 계산
        Vector3 direction = Target.position - cannon.position;

        // 타겟을 향하는 방향으로 회전값을 계산
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Y축 기준으로 90도 회전 추가
        targetRotation *= Quaternion.Euler(90, 0, 0);

        cannon.rotation = targetRotation;
    }

}