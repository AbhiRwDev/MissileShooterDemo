using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissileLauncher : MonoBehaviour
{
   
   
    public int SelectedMissile;
   
    public GameObject targetObject;
    public LayerMask raycastLayer;

    public Image[] MissileButtons;

    [System.Serializable]
    public class MissileParams
    {
        [SerializeField]public float MissileLoadValue;
        [SerializeField]public float MissileReloadSpeed;
        [SerializeField]public GameObject MissilePrefab;
    }
    [SerializeField] MissileParams[] Missiles;
    // Update is called once per frame
    void Update()
    {
        MissileReload();
        MissileAim();
        UpdateMissileButtons();
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
        if (Missiles[SelectedMissile].MissileLoadValue >= 1)
        {
            GameObject G= Instantiate(Missiles[SelectedMissile].MissilePrefab);
            G.GetComponent<Missile>().targetPos = targetObject.transform.position;
            Missiles[SelectedMissile].MissileLoadValue = 0;
        }
                  
    }
    public void MissileReload()
    {
        foreach (var item in Missiles)
        {
            item.MissileLoadValue += Time.deltaTime * item.MissileReloadSpeed;
            item.MissileLoadValue = Mathf.Clamp(item.MissileLoadValue, 0, 1);
        }
       
        
    }
    public void UpdateMissileButtons()
    {
        for (int i = 0; i < Missiles.Length; i++)
        {
            MissileButtons[i].fillAmount = Missiles[i].MissileLoadValue;
        }
    }
    public void SelectMissile(int missile)
    {
        SelectedMissile = missile;
    }

}
