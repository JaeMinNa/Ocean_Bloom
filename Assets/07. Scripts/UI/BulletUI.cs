using UnityEngine;
using TMPro;

public class BulletUI : MonoBehaviour
{
    public GunController GunController;
    public TextMeshProUGUI CurrentBulletText;
    public TextMeshProUGUI MaxBulletText;
    public TextMeshProUGUI CarryBulletText;

    void Start()
    {
        
    }

    private void Update()
    {
        UpdateBulletUI();
    }

    private void UpdateBulletUI()
    {
        if (GunController != null && CurrentBulletText != null && MaxBulletText != null && CarryBulletText != null)
        {
            int currentBullet = GunController.CurrentGun.CurrentBulletCount;
            int maxBullet = GunController.CurrentGun.ReloadBulletCount;
            int carryBullet = GunController.CurrentGun.CarryBulletCount;

            // 각각의 TMP(TextMeshPro)에 현재, 최대, 소지한 총알 수를 표시
            CurrentBulletText.text = currentBullet.ToString();
            MaxBulletText.text = maxBullet.ToString();
            CarryBulletText.text = carryBullet.ToString();
        }
    }
}