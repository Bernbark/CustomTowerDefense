using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBar;

    public void SetHealth(float health, float maxHealth)
    {
        float scaleX = (health / maxHealth) * 10;
        if (scaleX < 0)
        {
            scaleX = 0;
        }
        healthBar.transform.localScale = new Vector3(scaleX, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
