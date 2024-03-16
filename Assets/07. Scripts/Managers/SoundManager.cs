using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("PlayerAudioSource")]
    [SerializeField] private AudioSource _playerBGMAudioSource;
    [SerializeField] private AudioSource _playerSFXAudioSource;
    [SerializeField] private AudioSource _loopSFXAudioSource;

    [Header("ETCAudioSource")]
    [SerializeField] private AudioSource[] _etcSFXAudioSource;

    private Dictionary<string, AudioClip> _bgm;
    private Dictionary<string, AudioClip> _sfx;
    private int _index;
    [SerializeField] private float _maxDistance = 50f;
    [Range(0f, 1f)] public float StartVolume = 0.1f;

    public void Init()
    {
        // 초기 셋팅
        _bgm = new Dictionary<string, AudioClip>();
        _sfx = new Dictionary<string, AudioClip>();

        _playerBGMAudioSource.loop = true;
        _playerBGMAudioSource.volume = StartVolume;
        _playerSFXAudioSource.playOnAwake = false;
        _playerSFXAudioSource.volume = StartVolume;
        _loopSFXAudioSource.loop = true;
        _loopSFXAudioSource.volume = StartVolume;
        _loopSFXAudioSource.playOnAwake = false;
        for (int i = 0; i < _etcSFXAudioSource.Length; i++)
        {
            _etcSFXAudioSource[i].playOnAwake = false;
            _etcSFXAudioSource[i].volume = StartVolume;
        }

        // BGM

        _bgm.Add("BGM1", Resources.Load<AudioClip>("Sound/BGM/bgm1"));
        _bgm.Add("BGM2", Resources.Load<AudioClip>("Sound/BGM/bgm2"));
        _bgm.Add("BGM3", Resources.Load<AudioClip>("Sound/BGM/bgm3"));
        _bgm.Add("BGM4", Resources.Load<AudioClip>("Sound/BGM/bgm4"));
        _bgm.Add("BGM5", Resources.Load<AudioClip>("Sound/BGM/bgm5"));
        _bgm.Add("BGM6", Resources.Load<AudioClip>("Sound/BGM/bgm6"));
        _bgm.Add("BGM7", Resources.Load<AudioClip>("Sound/BGM/bgm7"));
        _bgm.Add("GameOverBGM", Resources.Load<AudioClip>("Sound/BGM/GameOver"));
        _bgm.Add("GameClearBGM", Resources.Load<AudioClip>("Sound/BGM/GameClear"));

        // SFX
        _sfx.Add("PlayerGun", Resources.Load<AudioClip>("Sound/SFX/Player/PlayerHandGun"));
        _sfx.Add("EnemyGun", Resources.Load<AudioClip>("Sound/SFX/Enemy/EnemyHandGun"));
        _sfx.Add("EnemySword", Resources.Load<AudioClip>("Sound/SFX/Enemy/enemySword"));
        _sfx.Add("FineSight", Resources.Load<AudioClip>("Sound/SFX/Player/fineSight"));
        _sfx.Add("PlayerSword", Resources.Load<AudioClip>("Sound/SFX/Player/PlayerSword"));
        _sfx.Add("Reload", Resources.Load<AudioClip>("Sound/SFX/Player/reload"));
        _sfx.Add("AxeTree", Resources.Load<AudioClip>("Sound/SFX/Player/axe"));
        _sfx.Add("AxeNoHit", Resources.Load<AudioClip>("Sound/SFX/Player/axeNoHit"));
        _sfx.Add("Wood", Resources.Load<AudioClip>("Sound/SFX/Item/Wood"));
        _sfx.Add("PlayerHit", Resources.Load<AudioClip>("Sound/SFX/Player/PlayerHit"));
        _sfx.Add("Revive", Resources.Load<AudioClip>("Sound/SFX/Player/Revive"));
        _sfx.Add("Bullet", Resources.Load<AudioClip>("Sound/SFX/Player/bullet"));
        _sfx.Add("BulletZero", Resources.Load<AudioClip>("Sound/SFX/Player/bulletZero"));
        _sfx.Add("Coin", Resources.Load<AudioClip>("Sound/SFX/Item/Coin"));
        _sfx.Add("PartnerSpawn", Resources.Load<AudioClip>("Sound/SFX/Item/PartnerSpawn"));
        _sfx.Add("ButtonNo", Resources.Load<AudioClip>("Sound/SFX/UI/ButtonNo"));
        _sfx.Add("ButtonYes", Resources.Load<AudioClip>("Sound/SFX/UI/ButtonYes"));
        _sfx.Add("CannonBallItem", Resources.Load<AudioClip>("Sound/SFX/Item/CannonBallItem"));
        _sfx.Add("TutorialActive", Resources.Load<AudioClip>("Sound/SFX/UI/TutorialActive"));
        _sfx.Add("TutorialInActive", Resources.Load<AudioClip>("Sound/SFX/UI/TutorialInActive"));
        _sfx.Add("QuiteSea", Resources.Load<AudioClip>("Sound/SFX/Environment/QuiteSea"));
        _sfx.Add("UninhabitedIsland", Resources.Load<AudioClip>("Sound/SFX/Environment/UninhabitedIsland"));
        _sfx.Add("Walk", Resources.Load<AudioClip>("Sound/SFX/Loop/Walk"));
        _sfx.Add("Run", Resources.Load<AudioClip>("Sound/SFX/Loop/Run"));
        _sfx.Add("Ship", Resources.Load<AudioClip>("Sound/SFX/Loop/Ship"));
        _sfx.Add("CannonPlayer", Resources.Load<AudioClip>("Sound/SFX/Player/CannonPlayer"));
        _sfx.Add("CannonEnemy", Resources.Load<AudioClip>("Sound/SFX/Enemy/CannonEnemy"));
        _sfx.Add("CannonDestroy", Resources.Load<AudioClip>("Sound/SFX/Item/CannonDestroy"));
        _sfx.Add("Plant", Resources.Load<AudioClip>("Sound/SFX/Item/Plant"));
        _sfx.Add("ShipMake", Resources.Load<AudioClip>("Sound/SFX/Ship/ShipMake"));
    }

    // 메모리 해제
    public void Release()
    {

    }

    // 다른 오브젝트에서 출력되는 사운드
    public void StartSFX(string name, Vector3 position)
    {
        _index = _index % _etcSFXAudioSource.Length;

        float distance = Vector3.Distance(position, GameManager.I.PlayerManager.Player.transform.position);
        float volume = 1f - (distance / _maxDistance);
        if (volume < 0) volume = 0f;
        _etcSFXAudioSource[_index].volume = Mathf.Clamp01(volume) * StartVolume;
        _etcSFXAudioSource[_index].PlayOneShot(_sfx[name]);

        _index++;
    }

    // Player에서 출력되는 사운드
    public void StartSFX(string name)
    {
        _playerSFXAudioSource.PlayOneShot(_sfx[name]);
    }

    public void StartBGM(string name)
    {
        _playerBGMAudioSource.Stop();
        _playerBGMAudioSource.clip = _bgm[name];
        _playerBGMAudioSource.Play();
    }

    public void StopBGM()
    {
        if (_playerBGMAudioSource != null) _playerBGMAudioSource.Stop();
    }

    public void StartSFXLoop(string name)
    {
        _loopSFXAudioSource.Stop();
        _loopSFXAudioSource.clip = _sfx[name];
        _loopSFXAudioSource.Play();
    }

    public void StopSFXLoop()
    {
        if (_loopSFXAudioSource != null) _loopSFXAudioSource.Stop();
    }


}