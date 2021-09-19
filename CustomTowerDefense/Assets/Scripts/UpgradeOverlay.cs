using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UpgradeOverlay : MonoBehaviour
{
    public BuildingManager buildingManager;
    private TurretBehavior tower;
    public GameObject rangeBtn, damageBtn, closeBtn, destroyBtn;
    private string rangetoolTip, damagetoolTip, closetoolTip, destoytoolTip;
    private static UpgradeOverlay Instance { get; set; }
    private void Start()
    {
        Instance = this;
        closetoolTip = "Close overlay";
        destoytoolTip = "Destroy (refunds build cost,\n not upgrade cost)";
        Button range = rangeBtn.GetComponent<Button>();
        range.onClick.AddListener(UpgradeRange);
        range.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(rangetoolTip);
        range.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
        Button damage = damageBtn.GetComponent<Button>();
        damage.onClick.AddListener(UpgradeDamage);
        damage.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(damagetoolTip);
        damage.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
        Button close = closeBtn.GetComponent<Button>();
        close.onClick.AddListener(Hide);
        close.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(closetoolTip);
        close.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
        Button destroy = destroyBtn.GetComponent<Button>();
        destroy.onClick.AddListener(DestroyThisObject);
        destroy.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(destoytoolTip);
        destroy.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();

        Hide();
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        Hide();
    }

    public static void Show_Static(TurretBehavior tower, float rangeCost, float damageCost)
    {
        Instance.Show(tower, rangeCost, damageCost);
        
    }
    public static void Hide_Static()
    {
        Instance.Hide();
    }

    private void Show(TurretBehavior tower, float rangeCost, float damageCost)
    {
        this.tower = tower;
        RefreshRangeVisual();
        rangetoolTip = "Range Upgrade = " + rangeCost;
        damagetoolTip = "Damage Upgrade = " + damageCost;
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
        rangetoolTip = "Range Upgrade = " + (tower.GetRangeLevel() * 20 + 10);
        RefreshTooltip(rangetoolTip);
    }

    private void UpgradeDamage()
    {
        tower.UpgradeDamage();
        damagetoolTip = "Damage Upgrade = " + (tower.GetDamageLevel() * 20 + 10);
        RefreshTooltip(damagetoolTip);
    }

    public void RefreshRangeVisual()
    {
        transform.Find("Range").localScale = Vector3.one * tower.GetRange();
    }

    private void DestroyThisObject()
    {
        Hide();
        buildingManager.DestroyTurret(tower);
        Hide();
    }

    public void RefreshTooltip(string tooltip)
    {
        Tooltip.HideTooltip_Static();
        Tooltip.ShowTooltip_Static(tooltip);

    }
}
