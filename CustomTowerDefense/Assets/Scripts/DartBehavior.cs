using UnityEngine;
public class DartBehavior : MonoBehaviour
{
    
    private bool isInCollision = false;
    private bool hit;
    float damage;
    public TurretBehavior spawnOrigin;
    
    //private Animator anim;
    private BoxCollider2D boxCollider;
    private float liveTime;
    // Start is called before the first frame update
    void Start()
    {
        this.damage = spawnOrigin.GetDamage();
        hit = false;
        //AudioManager.instance.PlaySound("laser_small");
        liveTime = 0;
        //anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        liveTime += Time.deltaTime;

        if (liveTime > 1)
        {
            Deactivate();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBehavior e = collision.gameObject.GetComponent<EnemyBehavior>();
        GameObject otherObj = collision.gameObject;
        if (e && isInCollision == false)
        {
            hit = true;

            //boxCollider.enabled = false;
            isInCollision = true;
            
            e.TakeHit(damage);
            Deactivate();
        }

        //play sound when striking enemies
        
        //AudioManager.instance.PlaySound("fireball_explosion");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        EnemyBehavior e = collision.gameObject.GetComponent<EnemyBehavior>();
        GameObject otherObj = collision.gameObject;

        if (e && isInCollision == false)
        {
            hit = true;

            //boxCollider.enabled = false;
            isInCollision = true;
            
            e.TakeHit(damage);
            Deactivate();
        }

        //play sound when striking enemies
        
        //AudioManager.instance.PlaySound("fireball_explosion");
    }

    public void Deactivate()
    {
        Destroy(this.gameObject);
    }
}
