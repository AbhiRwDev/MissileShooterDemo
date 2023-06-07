using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissileLauncher : MonoBehaviour
{
    [System.Serializable]
    public class MissileParams
    {
        [SerializeField] public float MissileLoadValue;
        [SerializeField] public float MissileReloadSpeed;
        [SerializeField] public GameObject MissilePrefab;
    }
    [SerializeField] MissileParams[] Missiles;
    public Image[] MissileButtons;
    public Transform[] ShootPoints;
   
    public GameObject targetObject;
    public LayerMask raycastLayer;
   
    public Transform HingeObject;
    public float CurveMagnitude = 15;
    int SelectedMissile;
    int ShootpointIndex=0;
    // Update is called once per frame
    void Update()
    {
        MissileReload();
        MissileAim();
        UpdateMissileButtons();
    }
    public void MissileAim()
    {
        Vector3 s = Vector3.Lerp(ShootPoints[ShootpointIndex].position, targetObject.transform.position, 0.5f) ;
        float yOffset = Mathf.Sin(0.5f * Mathf.PI) * CurveMagnitude;
        s += Vector3.up * yOffset;
        
        HingeObject.LookAt(s);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, raycastLayer))
        {
            targetObject.SetActive(true);
            targetObject.transform.position = hit.point+new Vector3(0,0.001f,0);
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
        GameObject G = null;
        if (Missiles[SelectedMissile].MissileLoadValue >= 1)
        {
            switch(SelectedMissile)
            {
                case 0:
                    G= Objectpool.ObjectPool.GetFromPool(Objectpool.ObjectType.BlackHoleMissile);
                    break;
                case 1:
                    G = Objectpool.ObjectPool.GetFromPool(Objectpool.ObjectType.ChainMissile);
                    break;
                case 2:
                    G = Objectpool.ObjectPool.GetFromPool(Objectpool.ObjectType.FireMissile);
                    break;
            }
            
            
            G.GetComponent<Missile>().curveMagnitude = CurveMagnitude;
            G.transform.position = ShootPoints[ShootpointIndex].position;
            G.GetComponent<Missile>().targetPos = targetObject.transform.position;
            G.SetActive(true);
            ShootpointIndex = Random.Range(0,ShootPoints.Length);
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
    /*
    private void GeneratePath(int resolution)
    {
        Vector3 pos =Vector3.zero;

        pos = HingeObject.transform.position;
        path[resolution - 1] = targetPos;

        for (int i = 1; i < resolution - 1; i++)
        {
            float t = (float)i / (resolution - 1);
            path[i] = CalculateCurvePoint(t);

        }
    }

    private Vector3 CalculateCurvePoint(float t)
    {
        Vector3 point = Vector3.Lerp(startPos, targetPos, t);
        float yOffset = Mathf.Sin(t * Mathf.PI) * ;
        point += Vector3.up * yOffset;
        return point;
    }
    */
}
