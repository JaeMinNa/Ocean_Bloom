using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    public static string NextScene;
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private TMP_Text _tipText;

    private void Start()
    {
        int random = Random.Range(0, 7);

        if(random == 0)
        {
            _tipText.text = "총은 전투를 할 때, 아주 효과적입니다. 총알을 구하세요.";
        }
        else if(random == 1)
        {
            _tipText.text = "바다에 빠져도 걱정하지 마세요. 사다리를 잘 찾아보세요.";
        }
        else if (random == 2)
        {
            _tipText.text = "각각 섬마다 있는 NPC를 잘 찾아보세요. 아이템 구입과 동료 고용을 할 수 있습니다.";
        }
        else if (random == 3)
        {
            _tipText.text = "현재 가지고 있는 동료 수에 따라, 다시 접속하면 방치형 보상을 받을 수 있습니다.";
        }
        else if (random == 4)
        {
            _tipText.text = "주인공은 식물학을 전공한 해군입니다.";
        }
        else if (random == 5)
        {
            _tipText.text = "도감을 확인하면 식물이 있는 섬을 알 수 있습니다.";
        }
        else if (random == 6)
        {
            _tipText.text = "사다리를 통해 적 배로 빨리 넘어가야 대포를 맞지 않을 수 있습니다.";
        }

        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        NextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

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
}
