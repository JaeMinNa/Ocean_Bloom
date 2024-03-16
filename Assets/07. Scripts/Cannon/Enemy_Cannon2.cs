using UnityEngine;

public class Enemy_Cannon2 : MonoBehaviour
{
    public Transform Target; // Ÿ��
    public Transform LeftCannon; // ���� ����
    public Transform RightCannon; // ������ ����
    public GameObject Cannon_Shot;
    public Transform Cannon_Leftpos;
    public Transform Cannon_Rightpos;
    public float RotationSpeed;    
    public ParticleSystem LeftCannonParticleSystem;
    public ParticleSystem RightCannonParticleSystem;
    [SerializeField] private GameObject Enemys;

    // �߻� ������
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
            // ���� ������ ������ Ÿ���� ���ϴ� �������� ȸ��
            RotateTowardsTarget(LeftCannon);
            // ���� �������� ����
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
            // ������ ������ ������ Ÿ���� ���ϴ� �������� ȸ��
            RotateTowardsTarget(RightCannon);
            // ������ �������� ����
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
        // Ÿ���� ���ϴ� ���� ���͸� ���
        Vector3 direction = Target.position - cannon.position;

        // Ÿ���� ���ϴ� �������� ȸ������ ���
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Y�� �������� 90�� ȸ�� �߰�
        targetRotation *= Quaternion.Euler(90, 0, 0);

        cannon.rotation = targetRotation;
    }

}