
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UpgradeOverlay : MonoBehaviour
{
    private int buyAmount;
    private float rangeCost, damageCost;
    public BuildingManager buildingManager;
    private TurretBehavior tower;
    public GameObject rangeBtn, damageBtn, destroyBtn;
    public Button buyOne, buyTen, buyTwentyFive, buyOneHundred;
    private string rangetoolTip, damagetoolTip, destoytoolTip;
    private static UpgradeOverlay Instance { get; set; }
    private void Start()
    {
        Instance = this;
        buyAmount = 1;
        
        destoytoolTip = "Destroy (refunds build cost,\n not upgrade cost)";

        Button range = rangeBtn.GetComponent<Button>();
        range.onClick.AddListener(UpgradeRange);
        range.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(rangetoolTip);
        range.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();

        Button damage = damageBtn.GetComponent<Button>();
        damage.onClick.AddListener(UpgradeDamage);
        damage.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(damagetoolTip);
        damage.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();      

        Button destroy = destroyBtn.GetComponent<Button>();
        destroy.onClick.AddListener(DestroyThisObject);
        destroy.GetComponent<Button_UI>().MouseOverOnceFunc = () => Tooltip.ShowTooltip_Static(destoytoolTip);
        destroy.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();

        // Button for each buy amount
        buyOne.onClick.AddListener(BuyOne);
        buyTen.onClick.AddListener(BuyTen);
        buyTwentyFive.onClick.AddListener(BuyTwentyFive);
        buyOneHundred.onClick.AddListener(BuyOneHundred);

        Hide();
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
        this.rangeCost = rangeCost;
        this.damageCost = damageCost;
        RefreshRangeVisual();
        RefreshToolTipText();
        
        gameObject.SetActive(true);
        transform.position = tower.transform.position;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpgradeRange()
    {
        tower.UpgradeRange(buyAmount);
        RefreshRangeVisual();
        RefreshToolTipText();
        
        RefreshTooltip(rangetoolTip);
    }

    private void UpgradeDamage()
    {
        tower.UpgradeDamage(buyAmount);
        RefreshToolTipText();
        
        RefreshTooltip(damagetoolTip);
    }

    private void RefreshToolTipText()
    {
        
        if (tower.tag == "LaserTurret")
        {
            rangetoolTip = "Range Upgrade = " + (tower.GetRangeLevel() * 2000 + 1000) * buyAmount;
            damagetoolTip = "Damage Upgrade = " + (tower.GetDamageLevel() * 2000 + 1000) * buyAmount;
        }
        else
        {
            rangetoolTip = "Range Upgrade = " + (tower.GetRangeLevel() * 20 + 10) * buyAmount;
            damagetoolTip = "Damage Upgrade = " + (tower.GetDamageLevel() * 20 + 10) * buyAmount;
        }
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

    private TurretBehavior GetTurret()
    {
        return this.tower;
    }

    public static TurretBehavior GetTurret_Static()
    {
        return Instance.GetTurret();
    }

    public void RefreshTooltip(string tooltip)
    {
        Tooltip.HideTooltip_Static();
        Tooltip.ShowTooltip_Static(tooltip);

    }

    private void BuyOne()
    {
        buyAmount = 1;
        //tower.SetBuyAmount(buyAmount);
        RefreshToolTipText();
    }

    private void BuyTen()
    {
        buyAmount = 10;
       // tower.SetBuyAmount(buyAmount);
        RefreshToolTipText();
    }

    private void BuyTwentyFive()
    {
        buyAmount = 25;
        //tower.SetBuyAmount(buyAmount);
        RefreshToolTipText();
    }

    private void BuyOneHundred()
    {
        buyAmount = 100;
        //tower.SetBuyAmount(buyAmount);
        RefreshToolTipText();
    }
}
