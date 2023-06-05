using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissileLauncher : MonoBehaviour
{
    public GameObject[] Missiles;
    public float MaxMissile1, MaxMissile2, MaxMissile3;
    public float Missile1ReloadSpeed, Missile2ReloadSpeed, Missile3ReloadSpeed;
    public int SelectedMissile;
   
    public GameObject targetObject;
    public LayerMask raycastLayer;

    public Image[] MissileButtons;

    [System.Serializable]
    public class cc
    {
        [SerializeField]public float ti;
       [SerializeField] public float csc;
    }
    [SerializeField] cc[] ccs;
    // Update is called once per frame
    void Update()
    {
        MissileReload();
        MissileAim();
        
    }
    public void MissileAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayer))
        {
            targetObject.transform.position = hit.point;
            if (Input.GetMouseButtonDown(0))
            {
                ShootMissile();
            }
        }
        else
        {
            targetObject.SetActive(false);
        }
    }
    
    public void ShootMissile()
    {
        if (SelectedMissile > -1)
        {
            GameObject g = Instantiate(Missiles[SelectedMissile], transform.position, Quaternion.identity);
            switch (SelectedMissile)
            {
                case 0:

                    g.GetComponent<Missile>().targetPos = targetObject.transform.position;
                    SelectedMissile = -1;
                    MaxMissile1 = 0;
                    break;
                case 1:

                    g.GetComponent<Missile>().targetPos = targetObject.transform.position;
                    SelectedMissile = -1;
                    MaxMissile2 = 0;
                    break;
                case 2:

                    g.GetComponent<Missile>().targetPos = targetObject.transform.position;
                    SelectedMissile = -1;
                    MaxMissile3 = 0;
                    break;
            }
        }
    }
    public void MissileReload()
    {
        MaxMissile1 += Time.deltaTime * Missile1ReloadSpeed;
        MaxMissile2 += Time.deltaTime * Missile2ReloadSpeed;
        MaxMissile3 += Time.deltaTime * Missile3ReloadSpeed;
        MaxMissile1= Mathf.Clamp(MaxMissile1,0,1);
        MaxMissile2= Mathf.Clamp(MaxMissile2,0,1);
        MaxMissile3= Mathf.Clamp(MaxMissile3,0,1);
    }
    public void SelectMissile(int missile)
    {

        switch(missile)
        {
            case 0:
                if (MaxMissile1 >= 1)
                {
                    SelectedMissile = missile;
                }
                break;
            case 1:
                if (MaxMissile2 >= 1)
                {
                    SelectedMissile = missile;
                }
                break;
            case 2:
                if (MaxMissile3 >= 1)
                {
                    SelectedMissile = missile;
                }
                break;
        }
      
        

    }

}
