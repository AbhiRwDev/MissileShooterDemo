using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissileButton : MonoBehaviour
{
    public enum MissileType
    {
        Missile1,
        Missile2,
        Missile3
    }
    public MissileType missiletype;
    public MissileLauncher launcher;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(missiletype)
        {
            case MissileType.Missile1:
                image.fillAmount = launcher.MaxMissile1;
                break;
            case MissileType.Missile2:
                image.fillAmount = launcher.MaxMissile2;
                break;
            case MissileType.Missile3:
                image.fillAmount = launcher.MaxMissile3;
                break;

        }
    }
}
