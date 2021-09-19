using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificObject : SaveableObject
{
    [SerializeField]
    private float range, damage;
    private int damageLevel, rangeLevel;
    public TurretBehavior tower;
    protected string saveStats;
    public Player player;

    

    public override void Save(int id)
    {
        range = tower.GetRange();
        damage = tower.GetDamage();
        rangeLevel = tower.GetRangeLevel();
        damageLevel = tower.GetDamageLevel();
        save = range.ToString() + "_" + damage.ToString() +"_"+ rangeLevel.ToString() +"_"+ damageLevel.ToString();
        
        base.Save(id);
    }

    public override void Load(List<string> values)
    {
        
        
        range = float.Parse(values[2]);
        tower.SetRange(range);
        damage = float.Parse(values[3]);
        tower.SetDamage(damage);
        // Ugly version control
        if (values.Count == 6)
        {
            rangeLevel = int.Parse(values[4]);
            tower.SetRangeLevel(rangeLevel);
            damageLevel = int.Parse(values[5]);
            tower.SetDamageLevel(damageLevel);
            
        }
        else
        {
            rangeLevel = 0;
            damageLevel = 0;
        }
        base.Load(values);
    }

    public float GetRange()
    {
        return this.range;
    }

    public float GetDamage()
    {
        return this.damage;
    }

    public int GetDamageLevel()
    {
        return this.damageLevel;
    }

    public int GetRangeLevel()
    {
        return this.rangeLevel;
    }
}
