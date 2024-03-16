using UnityEngine;
using TMPro;

public class ResourceCollector : MonoBehaviour
{
    public int Resources;
    public TextMeshProUGUI ResourceText;

    void Start()
    {
        if (ResourceText == null)
        {
            ResourceText = GameObject.Find("YourUITextObjectName").GetComponent<TextMeshProUGUI>();
        }

        UpdateResourceText();
    }

    public void UpdateResourceText()
    {
        ResourceText.text = Resources.ToString();
    }

    public void CollectResource()
    {
        Resources += 1;
        UpdateResourceText();
    }
}