using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;


public class DataManager :  MonoBehaviour
{
    [Header("Reward")]
    [SerializeField] private GameObject _rewardMenu;
    [SerializeField] private TMP_Text _coinRewardText;
    [SerializeField] private TMP_Text _woodRewardText;
    [SerializeField] private TMP_Text _bulletRewardText;
    [SerializeField] private TMP_Text _cannonRewardText;
    private int _coinSecond = 100;
    private int _woodSecond = 2400;
    private int _bulletSecond = 3000;
    private int _cannonSecond = 2700;
    private int _coinReward;
    private int _woodReward;
    private int _bulletReward;
    private int _cannonReward;
    private DateTime _lastTime;
    private DateTime _currentTime;
    private TimeSpan _timeSpan;
    private double _rewardTime;

    [Header("GameOver")]
    [SerializeField] private GameObject _GameOverPanel;

    [Header("GameClear")]
    [SerializeField] private GameObject _GameClearPanel;

    private PlayerController _playerController;
    private PlayerConditions _playerConditions;
    private ResourceCollector _resourceCollector;
    private PlantBook _plantBook;


    // 초기화
    public void Init()
    {
        _playerController = GameManager.I.PlayerManager.Player.GetComponent<PlayerController>();
        _playerConditions = GameManager.I.PlayerManager.Player.GetComponent<PlayerConditions>();
        _resourceCollector = GameManager.I.PlayerManager.Player.GetComponent<ResourceCollector>();
        _plantBook = GameObject.Find("Canvas").GetComponent<PlantBook>();

        if (PlayerPrefs.HasKey("LastTime"))
        {
            _lastTime = DateTime.Parse(PlayerPrefs.GetString("LastTime"));
            _currentTime = DateTime.Now;
            _timeSpan = _currentTime - _lastTime;
            _rewardTime = _timeSpan.TotalSeconds;
            Debug.Log(_rewardTime);
        }
    }

    // 메모리 해제
    public void Release()
    {

    }

    public void PlayerDataSave()
    {
        PlayerPrefs.SetFloat("Hp", _playerConditions.Health.CurValue);
        PlayerPrefs.SetInt("ReviveCount", _playerController.ReviveCount);
        PlayerPrefs.SetInt("Wood", _resourceCollector.Resources);
        PlayerPrefs.SetInt("CurrentBullet", _playerController.GunController.CurrentGun.CurrentBulletCount);
        PlayerPrefs.SetInt("CarryBullet", _playerController.GunController.CurrentGun.CarryBulletCount);
        PlayerPrefs.SetInt("Coin", _playerController.CurrentCoin);
        PlayerPrefs.SetInt("PartnerSpawnCount", _playerController.PartnerSpawnCount);
        PlayerPrefs.SetInt("CannonBall", _playerController.CannonBall);

        PlayerPrefs.SetInt("Plant1", _plantBook.FindPlants[0]);
        PlayerPrefs.SetInt("Plant2", _plantBook.FindPlants[1]);
        PlayerPrefs.SetInt("Plant3", _plantBook.FindPlants[2]);
        PlayerPrefs.SetInt("Plant4", _plantBook.FindPlants[3]);
        PlayerPrefs.SetInt("Plant5", _plantBook.FindPlants[4]);
        PlayerPrefs.SetInt("Plant6", _plantBook.FindPlants[5]);
    }

    public void PlayerDataLoad()
    {
        _playerConditions.Health.CurValue = PlayerPrefs.GetFloat("Hp");
        _playerController.ReviveCount = PlayerPrefs.GetInt("ReviveCount");
        _resourceCollector.Resources = PlayerPrefs.GetInt("Wood");
        _playerController.GunController.CurrentGun.CurrentBulletCount = PlayerPrefs.GetInt("CurrentBullet");
        _playerController.GunController.CurrentGun.CarryBulletCount = PlayerPrefs.GetInt("CarryBullet");
        _playerController.CurrentCoin = PlayerPrefs.GetInt("Coin");
        _playerController.PartnerSpawnCount = PlayerPrefs.GetInt("PartnerSpawnCount");
        _playerController.CannonBall = PlayerPrefs.GetInt("CannonBall");

        _playerController.ReviveConutText.text = _playerController.ReviveCount.ToString();
        _resourceCollector.UpdateResourceText();
        _playerController.CurrentCoinText.text = _playerController.CurrentCoin.ToString();
        _playerController.PartnerSpawnCountText.text = _playerController.PartnerSpawnCount.ToString();
        _playerController.CannonBallText.text = _playerController.CannonBall.ToString();

        _plantBook.FindPlants[0] = PlayerPrefs.GetInt("Plant1");
        Debug.Log(PlayerPrefs.GetInt("Plant1") + "Plant1");
        if (_plantBook.FindPlants[0] == 1) _plantBook.Slots[0].SetActive(true);
        _plantBook.FindPlants[1] = PlayerPrefs.GetInt("Plant2");
        if (_plantBook.FindPlants[1] == 1) _plantBook.Slots[1].SetActive(true);
        _plantBook.FindPlants[2] = PlayerPrefs.GetInt("Plant3");
        if (_plantBook.FindPlants[2] == 1) _plantBook.Slots[2].SetActive(true);
        _plantBook.FindPlants[3] = PlayerPrefs.GetInt("Plant4");
        if (_plantBook.FindPlants[3] == 1) _plantBook.Slots[3].SetActive(true);
        _plantBook.FindPlants[4] = PlayerPrefs.GetInt("Plant5");
        if (_plantBook.FindPlants[4] == 1) _plantBook.Slots[4].SetActive(true);
        _plantBook.FindPlants[5] = PlayerPrefs.GetInt("Plant6");
        if (_plantBook.FindPlants[5] == 1) _plantBook.Slots[5].SetActive(true);
    }

    public void DataSave()
    {
        PlayerPrefs.SetString("LastTime", DateTime.Now.ToString());

        PlayerPrefs.SetInt("IsData", 1);
        PlayerPrefs.SetFloat("SaveHp", _playerConditions.Health.CurValue);
        PlayerPrefs.SetInt("SaveReviveCount", _playerController.ReviveCount);
        PlayerPrefs.SetInt("SaveWood", _resourceCollector.Resources);
        PlayerPrefs.SetInt("SaveCurrentBullet", _playerController.GunController.CurrentGun.CurrentBulletCount);
        PlayerPrefs.SetInt("SaveCarryBullet", _playerController.GunController.CurrentGun.CarryBulletCount);
        PlayerPrefs.SetInt("SaveCoin", _playerController.CurrentCoin);
        PlayerPrefs.SetInt("SavePartnerSpawnCount", _playerController.PartnerSpawnCount);
        PlayerPrefs.SetInt("SaveCannonBall", _playerController.CannonBall);

        PlayerPrefs.SetString("CurrentScene", GameManager.I.SceneManager.CurrentSceneName);
        PlayerPrefs.SetFloat("PlayerPositionX", GameManager.I.PlayerManager.Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", GameManager.I.PlayerManager.Player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", GameManager.I.PlayerManager.Player.transform.position.z);
        PlayerPrefs.SetFloat("PlayerRotationX", GameManager.I.PlayerManager.Player.transform.rotation.eulerAngles.x);
        PlayerPrefs.SetFloat("PlayerRotationY", GameManager.I.PlayerManager.Player.transform.rotation.eulerAngles.y);
        PlayerPrefs.SetFloat("PlayerRotationZ", GameManager.I.PlayerManager.Player.transform.rotation.eulerAngles.z);

        PlayerPrefs.SetFloat("ShipPositionX", GameManager.I.ShipManager.Ship.transform.position.x);
        PlayerPrefs.SetFloat("ShipPositionY", GameManager.I.ShipManager.Ship.transform.position.y);
        PlayerPrefs.SetFloat("ShipPositionZ", GameManager.I.ShipManager.Ship.transform.position.z);

        PlayerPrefs.SetInt("Plant1", _plantBook.FindPlants[0]);
        PlayerPrefs.SetInt("Plant2", _plantBook.FindPlants[1]);
        PlayerPrefs.SetInt("Plant3", _plantBook.FindPlants[2]);
        PlayerPrefs.SetInt("Plant4", _plantBook.FindPlants[3]);
        PlayerPrefs.SetInt("Plant5", _plantBook.FindPlants[4]);
        PlayerPrefs.SetInt("Plant6", _plantBook.FindPlants[5]);

        if (_playerController.IsShip()) PlayerPrefs.SetInt("OnShip", 1);
        else PlayerPrefs.SetInt("OnShip", 0);
    }

    public void DataLoad()
    {
        _playerConditions.Health.CurValue = PlayerPrefs.GetFloat("SaveHp");
        _playerController.ReviveCount = PlayerPrefs.GetInt("SaveReviveCount");
        _resourceCollector.Resources = PlayerPrefs.GetInt("SaveWood");
        _playerController.GunController.CurrentGun.CurrentBulletCount = PlayerPrefs.GetInt("SaveCurrentBullet");
        _playerController.GunController.CurrentGun.CarryBulletCount = PlayerPrefs.GetInt("SaveCarryBullet");
        _playerController.CurrentCoin = PlayerPrefs.GetInt("SaveCoin") ;
        _playerController.PartnerSpawnCount = PlayerPrefs.GetInt("SavePartnerSpawnCount");
        _playerController.CannonBall = PlayerPrefs.GetInt("SaveCannonBall");

        _playerController.ReviveConutText.text = _playerController.ReviveCount.ToString();
        _resourceCollector.UpdateResourceText();
        _playerController.CurrentCoinText.text = _playerController.CurrentCoin.ToString();
        _playerController.PartnerSpawnCountText.text = _playerController.PartnerSpawnCount.ToString();
        _playerController.CannonBallText.text = _playerController.CannonBall.ToString();

        _plantBook.FindPlants[0] = PlayerPrefs.GetInt("Plant1");
        if (_plantBook.FindPlants[0] == 1) _plantBook.Slots[0].SetActive(true);
        _plantBook.FindPlants[1] = PlayerPrefs.GetInt("Plant2");
        if (_plantBook.FindPlants[1] == 1) _plantBook.Slots[1].SetActive(true);
        _plantBook.FindPlants[2] = PlayerPrefs.GetInt("Plant3");
        if (_plantBook.FindPlants[2] == 1) _plantBook.Slots[2].SetActive(true);
        _plantBook.FindPlants[3] = PlayerPrefs.GetInt("Plant4");
        if (_plantBook.FindPlants[3] == 1) _plantBook.Slots[3].SetActive(true);
        _plantBook.FindPlants[4] = PlayerPrefs.GetInt("Plant5");
        if (_plantBook.FindPlants[4] == 1) _plantBook.Slots[4].SetActive(true);
        _plantBook.FindPlants[5] = PlayerPrefs.GetInt("Plant6");
        if (_plantBook.FindPlants[5] == 1) _plantBook.Slots[5].SetActive(true);

        if (GameManager.I.SceneManager.CurrentSceneName != "2" ||
            (GameManager.I.SceneManager.CurrentSceneName == "2" && PlayerPrefs.GetInt("IsShip") == 1))
        {
            GameManager.I.ShipManager.CanShipMove();
            //GameManager.I.ShipManager.ShipSetRotation(new Vector3(PlayerPrefs.GetFloat("ShipRotationX"), PlayerPrefs.GetFloat("ShipRotationY")
            //    , PlayerPrefs.GetFloat("ShipRotationZ")));
            GameManager.I.ShipManager.ShipSetPosition(new Vector3(PlayerPrefs.GetFloat("ShipPositionX"), PlayerPrefs.GetFloat("ShipPositionY")
                , PlayerPrefs.GetFloat("ShipPositionZ")));
            GameManager.I.ShipManager.CanNotShipMove();
        }

        if (PlayerPrefs.GetInt("OnShip") == 1)
        {
            GameManager.I.PlayerManager.PlayerSetPosition(GameObject.Find("outof").transform.position);
        }
        else
        {
            GameManager.I.PlayerManager.PlayerSetPosition(new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY") + 1f,
            PlayerPrefs.GetFloat("PlayerPositionZ")));
        }
        GameManager.I.PlayerManager.PlayerSetRotation(new Vector3(PlayerPrefs.GetFloat("PlayerRotationX"), PlayerPrefs.GetFloat("PlayerRotationY"),
            PlayerPrefs.GetFloat("PlayerRotationZ")));

        
        RewardActive();
    }

    private void RewardActive()
    {
        GameManager.I.SoundManager.StartSFX("TutorialActive");
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = false;
        _rewardMenu.SetActive(true);
        _coinReward = (int)(_rewardTime / _coinSecond) * _playerController.PartnerSpawnCount;
        _woodReward = (int)(_rewardTime / _woodSecond) * _playerController.PartnerSpawnCount;
        _bulletReward = (int)(_rewardTime / _bulletSecond) * _playerController.PartnerSpawnCount;
        _cannonReward = (int)(_rewardTime / _cannonSecond) * _playerController.PartnerSpawnCount;
        _coinRewardText.text = _coinReward.ToString();
        _woodRewardText.text = _woodReward.ToString();
        _bulletRewardText.text = _bulletReward.ToString();
        _cannonRewardText.text = _cannonReward.ToString();
    }

    public void GetReward()
    {
        GameManager.I.SoundManager.StartSFX("ButtonYes");

        _playerController.CurrentCoin = PlayerPrefs.GetInt("SaveCoin") + _coinReward;
        _resourceCollector.Resources = PlayerPrefs.GetInt("SaveWood") + _woodReward;
        _playerController.GunController.CurrentGun.CarryBulletCount = PlayerPrefs.GetInt("SaveCarryBullet") + _bulletReward;
        _playerController.CannonBall = PlayerPrefs.GetInt("SaveCannonBall") + _cannonReward;
        _playerController.CurrentCoinText.text = _playerController.CurrentCoin.ToString();
        _resourceCollector.UpdateResourceText();
        _playerController.CannonBallText.text = _playerController.CannonBall.ToString();

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = true;
        _rewardMenu.SetActive(false);
    }

    public void GameOver()
    {
        GameManager.I.SoundManager.StartBGM("GameOverBGM");
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = false;
        _GameOverPanel.SetActive(true);
    }

    public void GameClear()
    {
        GameManager.I.SoundManager.StartSFX("TutorialActive");
        GameManager.I.SoundManager.StartBGM("GameClearBGM");
        GameManager.I.PlayerManager.Player.GetComponent<PlayerController>().CanLook = false;
        _GameClearPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }
}
