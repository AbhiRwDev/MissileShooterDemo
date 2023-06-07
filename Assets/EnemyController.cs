using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Health=20;
    float health;
    public float TimeTillDisable=2;
    public Rigidbody rb;
    public Transform Player;
    public float Speed=10;
    public bool Dead = false;
    public Transform ObjectPool;

    // Start is called before the first frame update
    private void OnEnable()
    {
        health = Health;
        Dead = false;
    }
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        Player = FindObjectOfType<MissileLauncher>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
        {

            rb.constraints = RigidbodyConstraints.None;
           if(!Dead)
            {
                StartCoroutine(Disable(TimeTillDisable));
                Dead = true;
            }
           
        }
        else
        {
            Vector3 r= (Player.position - transform.position).normalized * Speed *100* Time.fixedDeltaTime;
            rb.velocity = new Vector3(r.x,rb.velocity.y,r.z);
            transform.forward += Vector3.forward;
            
        }
    }
    
    public void Damage(float damage)
    {
        health -= damage;
    }
    
    public IEnumerator Disable(float time)
    {
        yield return new WaitForSeconds(time);
        transform.parent = Objectpool.ObjectPool.transform;
        
        Objectpool.ObjectPool.AddToPool(Objectpool.ObjectType.Enemies, gameObject);
        rb.velocity=Vector3.zero;
        gameObject.SetActive(false);

    }
   
    private void OnDisable()
    {
        
        
        //TODO-Make it bac to pool
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("DisableObjects"))
        {
            StartCoroutine(Disable(0)); 
        }
    }
}
