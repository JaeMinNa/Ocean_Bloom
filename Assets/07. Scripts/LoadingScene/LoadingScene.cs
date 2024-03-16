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
            _tipText.text = "���� ������ �� ��, ���� ȿ�����Դϴ�. �Ѿ��� ���ϼ���.";
        }
        else if(random == 1)
        {
            _tipText.text = "�ٴٿ� ������ �������� ������. ��ٸ��� �� ã�ƺ�����.";
        }
        else if (random == 2)
        {
            _tipText.text = "���� ������ �ִ� NPC�� �� ã�ƺ�����. ������ ���԰� ���� ����� �� �� �ֽ��ϴ�.";
        }
        else if (random == 3)
        {
            _tipText.text = "���� ������ �ִ� ���� ���� ����, �ٽ� �����ϸ� ��ġ�� ������ ���� �� �ֽ��ϴ�.";
        }
        else if (random == 4)
        {
            _tipText.text = "���ΰ��� �Ĺ����� ������ �ر��Դϴ�.";
        }
        else if (random == 5)
        {
            _tipText.text = "������ Ȯ���ϸ� �Ĺ��� �ִ� ���� �� �� �ֽ��ϴ�.";
        }
        else if (random == 6)
        {
            _tipText.text = "��ٸ��� ���� �� ��� ���� �Ѿ�� ������ ���� ���� �� �ֽ��ϴ�.";
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
