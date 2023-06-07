using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    public float SpawnDelay;
    bool Spawnned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Spawnned)
        {
            StartCoroutine(Spawn());
            Spawnned = true;
        }
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(SpawnDelay);
        transform.position = new Vector3(Random.Range(-24,36),transform.position.y,transform.position.z);
        if (Objectpool.ObjectPool.Enemies.Count > 0)
        {
            GameObject G = Objectpool.ObjectPool.GetFromPool(Objectpool.ObjectType.Enemies);
            G.transform.position = transform.position;
            G.transform.rotation = Quaternion.identity;
            G.SetActive(true);
            Spawnned = false;
        }
        else
        {
            Debug.Log(Instantiate(Objectpool.ObjectPool.Enemy,transform.position,Quaternion.identity)) ;
            Spawnned = false;
        }
    }
}
