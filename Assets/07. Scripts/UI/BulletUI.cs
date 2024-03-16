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

            // ������ TMP(TextMeshPro)�� ����, �ִ�, ������ �Ѿ� ���� ǥ��
            CurrentBulletText.text = currentBullet.ToString();
            MaxBulletText.text = maxBullet.ToString();
            CarryBulletText.text = carryBullet.ToString();
        }
    }
}