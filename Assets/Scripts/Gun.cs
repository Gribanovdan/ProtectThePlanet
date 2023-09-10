using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float rotSpeed;

    Vector2 mousePos;

    [SerializeField]
    GameObject bullet;

    [SerializeField]
    float reloadTime;

    AudioSource asc;
    bool soundEnabled;

    LooseManager lm;

    float timerReload;

    public bool canShoot { get; set; }

    void Start()
    {
        soundEnabled = Settings.SoundsAllowed;
        asc = GameObject.Find("ReloadSounds").GetComponent<AudioSource>();
        timerReload = 0;
        canShoot = true;
        lm = GameObject.Find("lm").GetComponent<LooseManager>();
    }

    void Update()
    {
        if (!PauseManager.isPaused)
        {
            #region triggering
            if (!lm.isLoosed)
            {
                if (Input.GetMouseButton(0))
                {
                    mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos = new Vector2(mousePos.x, mousePos.y + 4.4f);
                    float angle = Vector2.Angle(transform.position, DeltaVector2(mousePos, transform.position));
                    if (angle <= 90)
                    {
                        Quaternion q = Quaternion.Euler(0, 0, mousePos.x < transform.position.x ? angle : -angle);
                        if (Quaternion.Angle(transform.rotation, q) > 0.01f)
                        {
                            transform.rotation = Quaternion.Lerp(transform.rotation, q, rotSpeed * Time.deltaTime);
                        }
                    }
                    else
                    {
                        transform.rotation = Quaternion.Lerp(transform.rotation,
                            Quaternion.Euler(0, 0, mousePos.x < transform.position.x ? 90 : -90), rotSpeed * Time.deltaTime);
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Shoot();
                }
            }
            #endregion
            #region reloading
            if (!canShoot) timerReload += Time.deltaTime;
            if (timerReload >= reloadTime)
            {
                if (!asc.enabled && soundEnabled) asc.enabled = true;
                canShoot = true;
            }
            #endregion
        }
    }



    void Shoot()
    {
        if (canShoot)
        {
            Vector3 nVect = new Vector3(transform.position.x, transform.position.y, 0);
            GameObject.Instantiate(bullet, nVect, transform.rotation);
            canShoot = false;
            timerReload = 0;
            asc.enabled = false;
        }
    }


    Vector2 DeltaVector2(Vector2 first, Vector2 second)
    {
        return new Vector2(first.x - second.x, first.y - second.y);
    }

}
