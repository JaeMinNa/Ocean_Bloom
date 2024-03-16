<p align="center">
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/f879a8d6-f77a-4ba3-a96a-837c95b2ca28" width="50%"/>

# 🖥️ Ocean Bloom (내배켐 유니티 2기 최종 팀프로젝트 23조)
### ❗ 유료 에셋 사용으로 에셋 부분은 삭제하였습니다. ❗

+ 식물학을 전공한 해군이 바다를 항해하면서 식물 도감을 모두 채울 수 있도록 도와주세요!
+ 게임 시작 시, 간단한 튜토리얼을 진행하고, [Tab]-조작법 으로 언제든지 조작법을 확인할 수 있습니다!
+ 각각의 섬에 숨어 있는 NPC를 찾아서 아이템을 구매하세요!
+ 혼자서는 힘들어요. 동료를 소환해서 함께 싸우세요!
+ 보물박스를 지나치지 마세요! 게임을 진행하는데 큰 도움이 됩니다!
+ 도감을 열어서 식물이 있는 섬과 식물 채집 난이도를 확인하세요!
+ 동료를 가지고 있으면 종료 시간에 따라 방치형 보상을 받을 수 있습니다!
+ 해적 보스 근처에 식물을 잘 찾아보세요!
<br/>

## 📽️ 프로젝트 소개
 - 게임 이름 : Ocean Bloom
 - 플랫폼 : PC
 - 장르 : 3D 액션 어드벤쳐
 - 개발 기간 : 24.01.10 ~ 24.02.28
<br/>

## ⚙️ Environment

- `Unity 2022.3.2`
- **IDE** : Visual Studio 2019, 2022, MonoDevelop
- **VCS** : Git (GitHub Desktop)
- **Envrionment** : PC `only`
- **Resolution** : 1920 x 1080 `FHD`
<br/>

## 👤 Collaborator - Team Intro
- 팀장  `나재민` - 플레이어 구현, 총기 구현, 저장 구현, 로딩 씬 구현, 오브젝트 풀 구현, 적 구현
- 팀원1 `양인호` - 대포 구현, 맵 디자인, 미니맵 구현, UI 구현
<br/>

## ▶️ 게임 스크린샷

<p align="center">
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/59234705-f74a-4aad-8a55-dacceb656889" width="49%"/>
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/29e69efe-79ec-4f58-9f45-8089e805bcf4" width="49%"/>
</p>
<p align="center">
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/e5781fa5-a078-471f-9c02-87f1b4be045e" width="49%"/>
  <img src="https://github.com/JaeMinNa/Hero-Battle3D/assets/149379194/82def12d-45af-4086-9a89-87754aa670c4" width="49%"/>
  </p>
<br/>

## 🔳 와이어 프레임
![image](https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/55a6fc97-c435-4d0b-8b14-ad77a930ecbb)


## 🧩 클라이언트 구조

### GameManager
![image](https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/9e6cf307-c767-430f-becf-6c92e9706628)
   
### Player   
![image](https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/28588140-a69c-4b72-8199-e2d35313b1ed)

### Enemy
![image](https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/912af85b-4a6f-4cad-96ea-c976dd18cb6d)


## ✏️ 구현 기능

### 1. 상태 패턴 구현

#### 구현 이유
- 다양한 상태를 가진 적과 동료 움직임 구현

#### 구현 방법
- IState 인터페이스 : 구체적인 상태 클래스로 연결할 수 있도록 설정
```C#
public interface IEnemyState
{
    void Handle(EnemyController controller);
}
``` 
​
- Context 스크립트 : 클라이언트가 객체의 내부 상태를 변경할 수 있도록 요청하는 인터페이스를 정의
```C#
public void Transition()
{
    CurrentState.Handle(_enemyController);
}

public void Transition(IEnemyState state)
{
    CurrentState = state;
    CurrentState.Handle(_enemyController);
}
```
​
- State 스크립트 : 각 State를 정의, State 변경 조건 설정
```C#
// Start문과 동일하게 사용
public void Handle(EnemyController enemyController)
{
    if (!_enemyController)
        _enemyController = enemyController;

    Debug.Log("Idle 상태 시작");   
    _idleTime = 3f;
    _time = 0;

    StartCoroutine(COUpdate());
}

// Update문과 동일하게 사용
IEnumerator COUpdate()
{
  while (true)
  {
  	// 각각의 상태 변환 조건 설정
      if (_enemyController.Distance <= 5f)
      {
          _enemyController.AttackStart();
          break;
      }          
      if (_time >= _idleTime)
      {
          _enemyController.WalkStart();
          break;
      }
    
      yield return null;
  }
}
```
<br/>

### 2. ObjectPool 구현

#### 구현 이유
- 미리 생성한 총탄 프리팹을 파괴하지 않고, 재사용을 위해
- 프리팹의 Instantiate, Destroy 함수 사용을 줄이기 위해

#### 구현 방법
- ObjectPoolManager로 ObjectPool들을 관리
- Size만큼 미리 프리팹을 생성하고, 선입선출인 Queue 자료구조로 순차적으로 SetActive(true) 실행
```C#
public GameObject SpawnFromPool(string tag)
{
    if (!PoolDictionary.ContainsKey(tag))
        return null;

    GameObject obj = PoolDictionary[tag].Dequeue();
    PoolDictionary[tag].Enqueue(obj);

    return obj;
}
```
<br/>

### 3. GameManger 구현

#### 구현 이유
- 각각 Manger들을 통합하여 접근 가능한 Manager가 필요

#### 구현 방법
- 어디서든 쉽게 접근이 가능해야 하므로 싱글톤 사용
- GameManger은 Manager들을 관리하는 하나의 역할만 수행
```C#
public static GameManager I;

[field: SerializeField] public DataManager DataManager { get; private set; }
[field: SerializeField] public SoundManager SoundManager { get; private set; }

private void Awake()
{
    if (I == null) I = this;
    else Destroy(gameObject);
    
    Init();
}

private void Init()
{
    DataManager.Init();
    SoundManager.Init();
}

private void Release()
{
    DataManager.Release();
    SoundManager.Release();
}
```
<br/>

### 4. 로딩 씬 구현
  
#### 구현 이유
- 씬이 전환 될 때, 다음 씬에서 사용될 리소스들을 읽어와서 게임을 위한 준비 작업 필요
- 로딩 화면이 없다면 가만히 멈춘 화면이나 까만 화면만 보일 수 있음
- 씬이 전환 될 때, 지루한 대기 시간을 이미지나 Tip으로 지루하지 않게 하기 위해

#### 구현 방법
- 비동기 방식 씬 전환 구현
```csharp
IEnumerator LoadScene()
{
    yield return null;
    AsyncOperation op = SceneManager.LoadSceneAsync(NextScene);
    op.allowSceneActivation = false;
    float timer = 0.0f;
    while (!op.isDone)
    {
        yield return null;
        timer += Time.deltaTime;
        if (op.progress < 0.9f)
        {
            _loadingBar.value = Mathf.Lerp(_loadingBar.value, op.progress, timer);
            if (_loadingBar.value >= op.progress)
            {
                timer = 0f;
            }
        }
        else
        {
            _loadingBar.value = Mathf.Lerp(_loadingBar.value, 1f, timer);
            if (_loadingBar.value == 1.0f)
            {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
```

- allowSceneActivation을 false로 설정
<br/>

  
### 5. 3D 사운드 구현

#### 구현 이유
- 유니티에서 제공하는 3D 사운드 기능을 사용하니 거리에 따른 소리 음량 크기 조절이 한번씩 제대로 적용이 안되는 현상 발생
- 사운드가 깨지거나 이상한 소리로 변질되어 재생되는 현상 발생

#### 구현 방법
- 3D 사운드 기능을 사용하지 않고, 거리에 따라 볼륨을 직접 조절하는 방식으로 직접 구현
- 미리 생성한 몇 개의 AudioSource가 순차적으로 재생하도록 구현

```C#
public void StartSFX(string name, Vector3 position)
{
	_index = _index % _etcSFXAudioSource.Length;
	
	float distance = Vector3.Distance(position, GameManager.I.PlayerManager.Player.transform.position);
	float volume = 1f - (distance / _maxDistance);
	if (volume < 0) volume = 0f;
	_etcSFXAudioSource.volume = Mathf.Clamp01(volume);
	_etcSFXAudioSource.PlayOneShot(_sfx[name]);
	
	_index++;
}
```
<br/>

## 💥 트러블 슈팅

### 1. Input System을 이용한 Player 이동 개선
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/401b8466-c112-43e6-ab26-1a410670b324" width="50%"/>

#### Input 클래스로 Player 이동 구현
- 간편하고 직관적으로 구현 가능
- Update 문에서 매 프레임 실행하기 때문에 성능에 영향
```
private void FixedUpdate()
{
	float moveHorizontal = Input.GetAxis("Horizontal");
	float moveVertical = Input.GetAxis("Vertical");
	
	Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
	_rigidbody.AddForce(movement * speed);
}
```

#### Input System으로 개선
- 입력 이벤트에 대한 바인딩 및 처리를 쉽게 구성
- Update문에서 매 프레임 실행할 필요가 없음
- 다양한 입력 장치를 지원
```
public void OnMoveInput(InputAction.CallbackContext context)
{
	if (context.phase == InputActionPhase.Performed)
	{
	    _curMovementInput = context.ReadValue<Vector2>();
	}
	else if (context.phase == InputActionPhase.Canceled)
	{
	    _curMovementInput = Vector2.zero;
	}
}

private void Move()
{
	Vector3 dir = transform.forward * _curMovementInput.y + transform.right * _curMovementInput.x;
	dir *= MoveSpeed;
	dir.y = _rigidbody.velocity.y;
	
	_rigidbody.velocity = dir;
}
```

#### 결과
- 복잡한 입력 시스템이나 다중 입력 조합을 유연하게 처리
<br/>

### 2. Physics.Raycast를 이용한 총기 구현 개선
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/d736e5a7-8aca-4f6b-b4af-56039f537bb6" width="50%"/>

#### 총알 프리팹을 생성해서 총기 구현
- 실제와 같은 총알 속도, 탄도학 등 적용 가능
- 실제와 유사하게 적용하는 것이 어려움
- 적적한 메모리 관리 방법 필요
```
private void Fire()
{
	Instantiate(bullet, transform.position, Quaternion.identity);
}
```

#### Physics.Raycast로 개선
- 총알 프리팹을 생성할 필요가 없음
- 즉각적으로 대상의 정보를 읽어 올 수 있음
- 별도의 메모리 관리 방법이 필요 없음
```
if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hitInfo, 50f))
{
    Debug.Log(_hitInfo.transform.name);
}
```

#### 결과
- 초당 프레임 개선 (63 FPS → 73 FPS)
<p align="center">
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/5b4e21fc-eaef-4272-986f-ec634f077708" width="49%"/>
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/dee9851e-ed68-4e00-80ca-6c9db30fc122" width="49%"/>
</p>
<br/>

### 3. ObjectPool을 이용한 총기 탄피 구현 개선
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/51eaa960-70bc-4614-8236-36bcd36584bd" width="50%"/>

#### 프리팹 생성, 파괴로 총기 탄피 구현
- 간단하고 직관적으로 구현 가능
- 반복적인 프리팹 생성, 삭제로 성능 저하 초래
- 적절한 메모리 관리 방법 필요
```
private void Fire()
{
	Instantiate(_bulletEffectObj, transform.position, Quaternion.identity);
}
```

#### ObjectPool로 개선
- 프리팹 생성, 파괴를 하지 않음
- 객체를 미리 생성해서 재사용 → 메모리 최적화 가능

ObjectPoolManager
```
public void GunEffect(string poolName ,Vector3 startPosition, Quaternion rotation)
{
	_bulletEffectObj = ObjectPool.SpawnFromPool(poolName);
	
	_bulletEffectObj.transform.position = startPosition;
	_bulletEffectObj.transform.rotation = rotation;
	//RangedAttackController attackController = obj.GetComponent<RangedAttackController>();
	//attackController.InitializeAttack(direction, attackData, this);
	
	_bulletEffectObj.SetActive(true);
	StartCoroutine(COGunEffectInactive());
}

IEnumerator COGunEffectInactive()
{
	GameObject obj = _bulletEffectObj;
	
	yield return new WaitForSeconds(0.5f);
	obj.SetActive(false);
}
```

ObjectPool
```
public GameObject SpawnFromPool(string tag)
{
if (!PoolDictionary.ContainsKey(tag))
    return null;

GameObject obj = PoolDictionary[tag].Dequeue();
PoolDictionary[tag].Enqueue(obj);

return obj;
}
```
<img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/558554b0-f1c7-4bd5-b0d0-334c68ce8041" width="50%"/>

#### 결과
- 초당 프레임 개선 (50 FPS → 76 FPS)
<p align="center">
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/e02299d0-c341-4ce3-9006-d945f44c5431" width="49%"/>
  <img src="https://github.com/JaeMinNa/Ocean_Bloom/assets/149379194/3b3c0f06-0d57-4f9f-ad4a-7f0070b47a9e" width="49%"/>
</p>
<br/>



