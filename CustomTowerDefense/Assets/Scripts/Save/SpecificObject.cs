using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificObject : SaveableObject
{
    [SerializeField]
    private float range, damage;
    public TurretBehavior tower;
    protected string saveStats;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Save(int id)
    {
        range = tower.GetRange();
        damage = tower.GetDamage();
        save = range.ToString() + "_" + damage.ToString();
        
        base.Save(id);
    }

    public override void Load(string[] values)
    {
        
        
        range = float.Parse(values[2]);
        tower.SetRange(range);
        damage = float.Parse(values[3]);
        tower.SetDamage(damage);
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
}
