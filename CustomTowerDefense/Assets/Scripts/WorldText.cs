
using UnityEngine;
using TMPro;

public class WorldText : MonoBehaviour
{
    public TextMeshPro goldText;
    private MeshRenderer mesh;
    public Player player;
    float statUpdateCooldown = .2f;

    // Update is called once per frame
    void Update()
    {
        statUpdateCooldown -= Time.deltaTime;
        if(statUpdateCooldown <= 0f)
        {
            statUpdateCooldown = .2f;
            goldText.text = "Gold: "+player.GetGold().ToString();
        }
    }
}
