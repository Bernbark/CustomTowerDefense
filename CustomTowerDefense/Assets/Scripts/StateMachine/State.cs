using System.Collections;

using UnityEngine;

public abstract class State : MonoBehaviour
{
    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator BloodMage()
    {
        yield break;
    }
    public virtual IEnumerator KillMonger()
    {
        yield break;
    }

    public virtual IEnumerator GoldHoarder()
    {
        yield break;
    }
}
