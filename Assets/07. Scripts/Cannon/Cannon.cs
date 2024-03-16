using TMPro;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Rigidbody CannonballPrefab;
    public Transform CannonBarrelEnd;
    public Transform CannonBody;
    public float ShellVelocity = 120000f;
    public float RotationSpeed = 50f;
    private Quaternion _originalBarrelEndRot;
    private Quaternion _initialCannonBodyRot;
    public ParticleSystem CannonParticleSystem;
    

    private float currentTime;
    public float FireInterval = 2f;
    private float lastFireTime;
    public TextMeshProUGUI CarryCannonBallText;

    public int CannonCount;

    void Start()
    {
        _originalBarrelEndRot = CannonBarrelEnd.rotation;
        _initialCannonBodyRot = CannonBody.localRotation;
        CannonParticleSystem.transform.position = CannonBarrelEnd.position;
        lastFireTime = -FireInterval;
        CannonCount = 0;
    }

    void Update()
    {
        currentTime = Time.time;
    }

    public void AdjustRotation()
    {
        float xRotationChange = Input.GetKey(KeyCode.UpArrow) ? 1f : (Input.GetKey(KeyCode.DownArrow) ? -1f : 0f);
        float zRotationChange = Input.GetKey(KeyCode.RightArrow) ? 1f : (Input.GetKey(KeyCode.LeftArrow) ? -1f : 0f);

        Vector3 currentRotation = CannonBody.localRotation.eulerAngles;

        currentRotation.x = (currentRotation.x > 180) ? currentRotation.x - 360 : currentRotation.x;
        float clampedXRotation = Mathf.Clamp(currentRotation.x + xRotationChange * RotationSpeed * Time.deltaTime, -3f, 15f);

        currentRotation.z = (currentRotation.z > 180) ? currentRotation.z - 360 : currentRotation.z;
        float clampedZRotation = Mathf.Clamp(currentRotation.z + zRotationChange * RotationSpeed * Time.deltaTime, -20f, 20f);

        CannonBody.localRotation = Quaternion.Euler(clampedXRotation, 0f, clampedZRotation);
    }


    public void Fire()
    {
        if (currentTime - lastFireTime >= FireInterval && GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBall > 0)
        {
            GameManager.I.SoundManager.StartSFX("CannonPlayer", transform.position);
            ShootCannonball();
            CannonParticleSystem.Play();
            GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBall--;

            lastFireTime = currentTime;
            CannonCount++;
        }
    }

    public void ShootCannonball()
    {
        Rigidbody cannonballInstance;
        cannonballInstance = Instantiate(CannonballPrefab, CannonBarrelEnd.position, Quaternion.identity);
        cannonballInstance.AddForce(CannonBarrelEnd.forward * ShellVelocity);

    }

    public void UpdateCannonUI()
    {
        CarryCannonBallText.text = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CannonBall.ToString();
    }
}
