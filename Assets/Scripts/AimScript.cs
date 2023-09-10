using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    [SerializeField]
    GameObject aim;

    GameObject curAim;

    LooseManager lm;

    float gunY;
    Gun gun;

    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("gun").GetComponent<Gun>();
        gunY = GameObject.FindGameObjectWithTag("gun").transform.position.y;
        lm = GameObject.Find("lm").GetComponent<LooseManager>();
    }

    void Update()
    {
        if (!PauseManager.isPaused)
        {
            #region aim
            if (!lm.isLoosed)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, mousePos.y + 4.4f);
                    if (!curAim) curAim = GameObject.Instantiate(aim, mousePos, Quaternion.identity);
                    Vector3 mVect = new Vector3(mousePos.x,
                        mousePos.y, -2);
                    curAim.transform.position = mVect;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    GameObject.Destroy(curAim);
                }
            }
            else Destroy(curAim);
            #endregion
            #region reloading
            if (!gun.canShoot && curAim) curAim.transform.Find("reload").GetComponent<SpriteRenderer>().enabled = true;
            else if (gun.canShoot && curAim) curAim.transform.Find("reload").GetComponent<SpriteRenderer>().enabled = false;
            #endregion
            #region check for angle
            if (curAim)
                if (curAim.transform.position.y < gunY)
                {
                    curAim.transform.position = new Vector3(curAim.transform.position.x, gunY,
                        curAim.transform.position.z);
                }
            #endregion
        }
        else if (curAim) Destroy(curAim);
    }


}
