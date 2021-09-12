using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOverlay : MonoBehaviour
{
    private TurretBehavior tower;
    public GameObject rangeBtn, damageBtn, closeBtn;
    private static UpgradeOverlay Instance { get; set; }
    private void Awake()
    {
        Instance = this;

        Button range = rangeBtn.GetComponent<Button>();
        range.onClick.AddListener(UpgradeRange);
        Button damage = damageBtn.GetComponent<Button>();
        damage.onClick.AddListener(UpgradeDamage);
        Button close = closeBtn.GetComponent<Button>();
        close.onClick.AddListener(Hide);

        Hide();
    }

    private void OnMouseExit()
    {
        Hide();
    }

    public static void Show_Static(TurretBehavior tower)
    {
        Instance.Show(tower);
    }
    public static void Hide_Static()
    {
        Instance.Hide();
    }

    private void Show(TurretBehavior tower)
    {
        this.tower = tower;
        RefreshRangeVisual();
        gameObject.SetActive(true);

        transform.position = tower.transform.position;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpgradeRange()
    {
        tower.UpgradeRange();
        RefreshRangeVisual();
    }

    private void UpgradeDamage()
    {
        tower.UpgradeDamage();
    }

    private void RefreshRangeVisual()
    {
        transform.Find("Range").localScale = Vector3.one * tower.GetRange();
    }
}
