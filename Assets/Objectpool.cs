using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectpool : MonoBehaviour
{
    public int MaxPoolCount;

    public GameObject FireMissile,BlackHoleMissile,ChainMissile,BlackHoleEffect,FireEffect,ChainEffect,Enemy;
    public List<GameObject> FireMissiles ;
    public List<GameObject> BlackHoleMissiles;
    public List<GameObject> ChainMissiles;
    public List<GameObject> BlackHoleEffects;
    public List<GameObject> FireEffects;
    public List<GameObject> ChainEffects;
    public List<GameObject> Enemies;

    public static Objectpool ObjectPool { get; private set; }
    public enum ObjectType
    {
        FireMissile,
        BlackHoleMissile,
        ChainMissile,
        BlackHoleEffects,
        FireEffects,
        ChainEffects,
        Enemies
    }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (ObjectPool != null && ObjectPool != this)
        {
            Destroy(this);
        }
        else
        {
            ObjectPool = this;
        }



    }
    private void Start()
    {
       
        for (int i = 0; i < MaxPoolCount; i++)
        {
            GameObject G = null;
            G = Instantiate(FireMissile, transform);
            G.SetActive(false);
            FireMissiles.Add(G);
            G = Instantiate(ChainMissile, transform);
            G.SetActive(false);
            ChainMissiles.Add(G);
            G = Instantiate(BlackHoleMissile, transform);
            G.SetActive(false);
            BlackHoleMissiles.Add(G);
            G = Instantiate(BlackHoleEffect, transform);
            G.SetActive(false);
            BlackHoleEffects.Add(G);
            G = Instantiate(FireEffect, transform);
            G.SetActive(false);
            FireEffects.Add(G);
            G = Instantiate(ChainEffect, transform);
            G.SetActive(false);
            ChainEffects.Add(G);
            G = Instantiate(Enemy, transform);
            G.SetActive(false);
            Enemies.Add(G);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           GameObject g= GetFromPool(ObjectType.Enemies);
        }
    }
    public GameObject GetFromPool(ObjectType Objecttype)
    {
        GameObject G = null;
        switch (Objecttype)
        {
            case ObjectType.FireMissile:
                if(FireMissiles.Count==0)
                {
                    G = Instantiate(FireMissile,transform.position,Quaternion.identity);
                    AddToPool(ObjectType.FireMissile,G);
                    G.SetActive(false);
                }
                G = FireMissiles[0];
                G.transform.parent = null;
                FireMissiles.Remove(G);
                return G;
                break;
            case ObjectType.BlackHoleMissile:
                if (BlackHoleMissiles.Count ==0)
                {
                    G = Instantiate(BlackHoleMissile, transform.position, Quaternion.identity);
                    AddToPool(ObjectType.BlackHoleMissile, G);
                    G.SetActive(false);
                }
                G = BlackHoleMissiles[0];
                G.transform.parent = null;
                BlackHoleMissiles.Remove(G);
                return G;
                break;
            case ObjectType.ChainMissile:
                if (ChainMissiles.Count == 0)
                {
                    G = Instantiate(ChainMissile, transform.position, Quaternion.identity);
                    AddToPool(ObjectType.ChainMissile, G);
                    G.SetActive(false);
                }
                G = ChainMissiles[0];
                G.transform.parent = null;
                ChainMissiles.Remove(G);
                return G;
                break;
            case ObjectType.BlackHoleEffects:
                if (BlackHoleEffects.Count == 0)
                {
                    G = Instantiate(BlackHoleEffect, transform.position, Quaternion.identity);
                    AddToPool(ObjectType.BlackHoleEffects, G);
                    G.SetActive(false);
                }
                G = BlackHoleEffects[0];
                G.transform.parent = null;
                BlackHoleEffects.Remove(G);
                return G;
                break;
            case ObjectType.FireEffects:
                if (FireEffects.Count == 0)
                {
                    G = Instantiate(FireEffect, transform.position, Quaternion.identity);
                    AddToPool(ObjectType.FireEffects, G);
                    G.SetActive(false);
                }
                G = FireEffects[0];
                G.transform.parent = null;
                FireEffects.Remove(G);
                return G;
                break;
            case ObjectType.ChainEffects:
                if (ChainEffects.Count == 0)
                {
                    G = Instantiate(ChainEffect, transform.position, Quaternion.identity);
                    AddToPool(ObjectType.ChainEffects, G);
                    G.SetActive(false);
                }
                G = ChainEffects[0];
                G.transform.parent = null;
                ChainEffects.Remove(G);
                return G;
                break;
            case ObjectType.Enemies:
                if (Enemies.Count == 0)
                {
                    G = Instantiate(Enemy, transform.position, Quaternion.identity);
                    AddToPool(ObjectType.Enemies, G);
                    G.SetActive(false);
                }
                G = Enemies[0];
                G.transform.parent = null;
                Enemies.Remove(G);
                return G;
                break;
        }
        return null;
    }
    public void AddToPool(ObjectType Objecttype,GameObject g)
    {
        switch(Objecttype)
        {
            case ObjectType.FireMissile:
                // g.transform.parent = transform;
                g.SetActive(false);
                FireMissiles.Add(g);
                break;
            case ObjectType.BlackHoleMissile:
                //  g.transform.parent = transform;
                g.SetActive(false);
                BlackHoleMissiles.Add(g);
                break;
            case ObjectType.ChainMissile:
                //  g.transform.parent = transform;
                g.SetActive(false);
                ChainMissiles.Add(g);
                break;
            case ObjectType.BlackHoleEffects:
                //  g.transform.parent = transform;
                g.SetActive(false);
                BlackHoleEffects.Add(g);
                break;
            case ObjectType.FireEffects:
                //  g.transform.parent = transform;
                g.SetActive(false);
                FireEffects.Add(g);
                break;
            case ObjectType.ChainEffects:
                //  g.transform.parent = transform;
                g.SetActive(false);
                ChainEffects.Add(g);
                break;
            case ObjectType.Enemies:
                //  g.transform.parent = transform;
                g.SetActive(false);
                Enemies.Add(g);
                break;
        }
    }
}
