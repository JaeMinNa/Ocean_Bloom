using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{ 
    [Header("SO")]
    public PlayerSO PlayerSO;

    [Header("PlayerInfo")]
    public int CurrentCoin = 3;
    public int ReviveCount = 5;
    public int PartnerSpawnCount = 0;
    public int CannonBall = 0;
    [HideInInspector] public float CurrentHp;

    [Header("Weapon")]
    public List<GameObject> HolderList;
    [SerializeField] private GameObject _aim;

    [Header("ETC")]
    public Transform CameraContainer;
    public TMP_Text PartnerSpawnCountText;
    public TMP_Text CannonBallText;
    public TMP_Text ReviveConutText;
    public TMP_Text CurrentCoinText;
    public float LookSensitivity;
    [SerializeField] private GameObject _partnerPrefab;
    private bool _isGun;
    private bool _isSword;
    private bool _isAxe;
    private bool _isRun;
    private bool _isSit;
    private bool _isWalkSound;
    private bool _isRunSound;
    private bool _isIdleSound;
    private bool _isShipSound;
    private Vector2 _curMovementInput;
    private Vector2 _mouseDelta;
    //private Vector3 _shipLocalPosition;
    private SwordController _swordController;
    private AxeController _axeController;
    private CapsuleCollider _capsuleCollider;
    private GameObject _cameraContainer;
    public GunController GunController { get; private set; }
    [HideInInspector] public Rigidbody Rigidbody;
    [HideInInspector] public float CamCurXRot;
    [HideInInspector] public bool CanLook;
    [HideInInspector] public bool IsUI;
    [HideInInspector] public bool CanShipControll;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _swordController = GetComponentInChildren<SwordController>();
        GunController = GetComponentInChildren<GunController>();
        _axeController = GetComponentInChildren<AxeController>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _cameraContainer = transform.Find("CameraContainer").gameObject;
    }

    void Start()
    {
        SwordHolderActive();
        Cursor.lockState = CursorLockMode.Locked;
        CurrentHp = PlayerSO.MaxHp;
        ReviveConutText.text = ReviveCount.ToString();
        CurrentCoinText.text = CurrentCoin.ToString();
        PartnerSpawnCountText.text = PartnerSpawnCount.ToString();
        CannonBallText.text = CannonBall.ToString();
        _isRun = false;
        CanShipControll = false;
        CanLook = true;
        IsUI = false;
        _isSit = false;
        _isWalkSound = false;
        _isRunSound = false;
        _isIdleSound = true;
        _isShipSound = false;

        if (!PlayerPrefs.HasKey("MouseLookSensitivity")) LookSensitivity = 0.1f;
        else LookSensitivity = PlayerPrefs.GetFloat("MouseLookSensitivity");

        SceneSoundStart();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!_isRun) Walk();
        else if (_isSit) Walk();
        else Run();

        MoveSound();
    }

    private void LateUpdate()
    {
        if (CanLook)
        {
            CameraLook();
        }
    }

    // Walk
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!CanShipControll)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                _isWalkSound = true;
                _isRunSound = false;
                _isIdleSound = false;
                _curMovementInput = context.ReadValue<Vector2>();

                if (_isSword) _swordController.Animator.SetBool("Walk", true);
                else if (_isGun) GunController.CurrentGun.Animator.SetBool("Walk", true);
                else if (_isAxe) _axeController.Animator.SetBool("Walk", true);
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                _curMovementInput = Vector2.zero;
                _isRun = false;
                _isWalkSound = false;
                _isRunSound = false;
                _isIdleSound = true;

                if (_isSword)
                {
                    _swordController.Animator.SetBool("Walk", false);
                    _swordController.Animator.SetBool("Run", false);
                }
                else if (_isGun)
                {
                    GunController.CurrentGun.Animator.SetBool("Walk", false);
                    GunController.CurrentGun.Animator.SetBool("Run", false);
                }
                else if (_isAxe)
                {
                    _axeController.Animator.SetBool("Walk", false);
                    _axeController.Animator.SetBool("Run", false);
                }
            }
        }
    }

    private void Walk()
    {
        Move(PlayerSO.WalkSpeed);
    }

    // Run
    public void OnRunInput(InputAction.CallbackContext context)
    {
        if(!CanShipControll)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (!GunController.IsFindSightMode && !_isSit)
                {
                    _isRun = true;
                    _isWalkSound = false;
                    _isRunSound = true;
                    _isIdleSound = false;

                    if (_isSword)
                    {
                        _swordController.Animator.SetBool("Walk", false);
                        _swordController.Animator.SetBool("Run", true);
                    }
                    else if (_isGun)
                    {
                        GunController.CurrentGun.Animator.SetBool("Walk", false);
                        GunController.CurrentGun.Animator.SetBool("Run", true);
                    }
                    else if (_isAxe)
                    {
                        _axeController.Animator.SetBool("Walk", false);
                        _axeController.Animator.SetBool("Run", true);
                    }
                }
            }
        }
    }

    private void Run()
    {
        Move(PlayerSO.RunSpeed);
    }

    private void Move(float speed)
    {
        Vector3 dir = transform.forward * _curMovementInput.y + transform.right * _curMovementInput.x;
        if(!IsGrounded() || _isSit) dir *= speed / 2f;
        else dir *= speed;
        dir.y = Rigidbody.velocity.y;

        Rigidbody.velocity = dir;

    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }

    void CameraLook()
    {
        CamCurXRot += _mouseDelta.y * LookSensitivity;
        CamCurXRot = Mathf.Clamp(CamCurXRot, PlayerSO.MinXLook, PlayerSO.MaxXLook);
        CameraContainer.localEulerAngles = new Vector3(-CamCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, _mouseDelta.x * LookSensitivity, 0);
    }

    // ����
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(!CanShipControll && !IsUI)
        {
            if (context.phase == InputActionPhase.Started)
            {
                if (IsGrounded() && !_isSit)
                {
                    Debug.Log("점프 실행");
                    Rigidbody.AddForce(Vector2.up * PlayerSO.JumpForce, ForceMode.Impulse);
                }
            }
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * 0.3f), Vector3.down);

        if(Physics.Raycast(ray, 0.5f, PlayerSO.GroundLayerMask))
        {
            return true;
        }

        return false;
    }

    public bool IsShip()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * 0.3f), Vector3.down);

        if (Physics.Raycast(ray, 0.5f, PlayerSO.ShipLayerMask))
        {
            return true;
        }

        return false;
    }

    public bool IsEnemyShip()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * 0.3f), Vector3.down);

        if (Physics.Raycast(ray, 20f, PlayerSO.EnemyShipLayerMask))
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position + (Vector3.up * 0.3f), Vector3.down);
    }

    // attack
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if(!CanShipControll && !IsUI)
        {
            if (_isSword)
            {
                if (context.phase == InputActionPhase.Started)
                {
                    _swordController.Animator.SetTrigger("Attack");
                }
            }
            else if (_isGun)
            {
                if (context.phase == InputActionPhase.Started)
                {
                    GunController.TryFire();
                    GunController.CurrentGun.Animator.SetTrigger("Fire");
                    //_sword.Animator.SetTrigger("Attack");
                }
            }
            else if (_isAxe)
            {
                if (context.phase == InputActionPhase.Started)
                {
                    _axeController.IsAttack = true;
                    _axeController.Animator.SetTrigger("Attack");
                }
            }
        }
    }

    // ������
    public void OnFineSightInput(InputAction.CallbackContext context)
    {
        if(!CanShipControll)
        {
            if (_isGun)
            {
                if (context.phase == InputActionPhase.Started)
                {
                    GunController.TryFineSight();
                }

                _isRun = false;
            }
        }
    }

    // Reload
    public void OnReloadInput(InputAction.CallbackContext context)
    {
        if(!CanShipControll)
        {
            if (_isGun)
            {
                if (context.phase == InputActionPhase.Started)
                {
                    GunController.TryReload();
                }
            }
        }
    }

    // Holder ��ü
    public void OnNumber1Input(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(!GunController.IsReload && !_axeController.IsAttack)
            {
                if (GunController.IsFindSightMode)
                {
                    GunController.CancelFineSight();
                    SwordHolderActive();
                }
                else
                {
                    SwordHolderActive();
                }
            }
        }
    }

    public void OnNumber2Input(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (!GunController.IsReload && !_axeController.IsAttack)
            {
                GunHolderActive();
            }
        }
    }

    public void OnNumber3Input(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (!GunController.IsReload && !_axeController.IsAttack)
            {
                if (GunController.IsFindSightMode)
                {
                    GunController.CancelFineSight();
                    AxeHolderActive();
                }
                else
                {
                    AxeHolderActive();
                }
            }
        }
    }

    private void SwordHolderActive()
    {
        _isGun = false;
        _isSword = true;
        _isAxe = false;
        _aim.SetActive(false);
        foreach (GameObject holder in HolderList)
        {
            holder.gameObject.SetActive(false);
        }

        HolderList[0].SetActive(true);
    }

    private void GunHolderActive()
    {
        _swordController._SwordCollider.SetActive(false);
        _isGun = true;
        _isSword = false;
        _isAxe = false;
        _aim.SetActive(true);
        foreach (GameObject holder in HolderList)
        {
            holder.gameObject.SetActive(false);
        }

        HolderList[1].SetActive(true);
    }

    private void AxeHolderActive()
    {
        _swordController._SwordCollider.SetActive(false);
        _isGun = false;
        _isSword = false;
        _isAxe = true;
        _aim.SetActive(false);
        foreach (GameObject holder in HolderList)
        {
            holder.gameObject.SetActive(false);
        }

        HolderList[2].SetActive(true);
    }

    // ParnterSpawn
    public void OnPartnerSpawn(InputAction.CallbackContext context)
    {
        if (!CanShipControll)
        {
            if (context.phase == InputActionPhase.Started)
            {
                Debug.Log("PartnerSpawn");
                if(PartnerSpawnCount > 0) PartnerSpawn();
            }
        }
    }

    // Sit
    public void OnSit(InputAction.CallbackContext context)
    {
        if (!CanShipControll && !IsUI)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                if (!_isSit)
                {
                    Debug.Log("Sit");
                    _isWalkSound = true;
                    _isRunSound = false;
                    _isIdleSound = false;
                    _isSit = true;
                    _capsuleCollider.height = 1.6f;
                    _capsuleCollider.center = new Vector3(0, 0.75f, 0);
                    _cameraContainer.transform.localPosition = new Vector3(0, 0.9f, 0);
                }
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                if (_isSit)
                {
                    Debug.Log("Stand");
                    _isSit = false;
                    _capsuleCollider.height = 3.2f;
                    _capsuleCollider.center = new Vector3(0, 1.518976f, 0);
                    _cameraContainer.transform.localPosition = new Vector3(0, 2.6f, 0);
                }
            }
        }
    }


    public void Die()
    {
        if (ReviveCount > 0)
        {
            ReviveCount--;
            ReviveConutText.text = ReviveCount.ToString();
            GameManager.I.SoundManager.StartSFX("Revive");
            GameManager.I.PlayerManager.Player.GetComponent<PlayerConditions>().Heal(200f);
        }
        else
        {
            GameManager.I.DataManager.GameOver();
        }
    }

    public void StartShipControll()
    {
        CanLook = false;
        CanShipControll = true;
        GameManager.I.ShipManager.ShipControlUI.SetActive(true);
        if(!_isShipSound)
        {
            GameManager.I.SoundManager.StartSFXLoop("Ship");
            _isShipSound = true;
        }
        GameManager.I.CameraManager.ChangeShipCamera();
        GameManager.I.ShipManager.CanShipMove();
    }

    public void StopShipControll()
    {
        CanLook = true;
        CanShipControll = false;
        GameManager.I.ShipManager.ShipControlUI.SetActive(false);
        _isShipSound = false;
        GameManager.I.SoundManager.StopSFXLoop();
        GameManager.I.CameraManager.ChangeMainCamera();
        GameManager.I.ShipManager.CanNotShipMove();
    }

    public void CoinChange(int coin)
    {
        CurrentCoin += coin;
        CurrentCoinText.text = CurrentCoin.ToString();
    }

    public void PartnerSpawn()
    {
        PartnerSpawnCount--;
        PartnerSpawnCountText.text = PartnerSpawnCount.ToString();
        GameManager.I.SoundManager.StartSFX("PartnerSpawn");
        Vector3 spawnPosition = transform.position + transform.forward * 5f;
        GameObject obj = Instantiate(_partnerPrefab, spawnPosition, Quaternion.identity);
    }

    private void MoveSound()
    {
        if(_isWalkSound && !CanShipControll && IsGrounded())
        {
            GameManager.I.SoundManager.StartSFXLoop("Walk");
            _isWalkSound = false;
        }
        else if(_isRunSound && !CanShipControll && IsGrounded())
        {
            GameManager.I.SoundManager.StartSFXLoop("Run");
            _isRunSound = false;
        }
        else if((_isIdleSound && !CanShipControll) || (_isIdleSound && !IsGrounded() && !CanShipControll) || _isIdleSound && CanShipControll)
        {
            GameManager.I.SoundManager.StopSFXLoop();
            _isIdleSound = false;
        }
    }

    private void SceneSoundStart()
    {
        if (GameManager.I.SceneManager.CurrentSceneName == "1")
        {
            GameManager.I.SoundManager.StartSFX("QuiteSea");
        }
    }
}