using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{

    [SerializeField]
    float speed;
    [SerializeField]
    bool isComet;
    [SerializeField]
    int strength;

    Helper helper;

    GameObject boom;
    GameObject boom2;
    GameObject fire;
    Transform fireTrans;
    GameObject boomOnEarth;

    LooseManager lm;

    ParticleSystemRenderer fireRend;
    ParticleSystem ps;

    GameObject earth;

    Vector2 target;
    Transform trans;

    void Start()
    {
        lm = GameObject.Find("lm").GetComponent<LooseManager>();
        earth = GameObject.Find("earth");
        target = earth.transform.position;
        target = new Vector2(target.x + Random.Range(-1.7f, 1.7f), target.y);
        trans = transform;
        speed /= 3;
        helper = GameObject.Find("helper").GetComponent<Helper>();
        boom = helper.meteoBoom;
        boom2 = helper.meteoBoom2;
        fire = helper.fire1;
        fire = GameObject.Instantiate(fire, trans.position, Quaternion.identity);
        fireTrans = fire.transform;
        ps = fire.GetComponent<ParticleSystem>();
        ps.Pause();
        

        // Математика, находит угол между направлением полета метеорита и вертикалью
        Vector2 v = DeltaVector2(trans.position, target);
        float angle = Vector2.Angle(Vector2.up, v);
        if (trans.position.x > target.x) angle = -90-angle;
        else angle = -90+angle;

        if (isComet)
        {
            trans.Rotate(0, 0, angle+120); // +120 = 90+30,  обусловлено направлением спрайта и криворукостью меня
        }

        fireTrans.Rotate(angle, -90, 90);
        fireRend = fire.GetComponent<ParticleSystemRenderer>();
        fireRend.enabled = false;

        boomOnEarth = helper.boomOnEarth;
    }

    void Update()
    {
        if (!PauseManager.isPaused) { 
            #region movement
        trans.position = Vector2.MoveTowards(trans.position, target, speed * Time.deltaTime);
        #endregion
            #region rotation
        if (!isComet) trans.Rotate(trans.rotation.x, trans.rotation.y, trans.rotation.z + 1);
            #endregion
            #region fire
            if (!isComet)
            {
                if (Vector3.Distance(target, trans.position) <= 4.5f) //2 - расстояние до земли тобишь, чтобы загорелся метеорит
                {
                    if (GameObject.Find("earth"))
                    {
                        fireTrans.position = trans.position;
                        fireRend.enabled = true;
                        ps.Play();
                    }
                    else
                    {
                        Destroy(fire);
                    }
                }
                
            }
            #endregion
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "moon")
        {
            GameObject.Find("lm").GetComponent<LooseManager>().MinusLive(100);  //не трогай сотку!
            if (isComet)
            {
                Destroy(transform.Find("whiteFlame").gameObject); // Уничтожение белого огня кометы
                Transform pFlame = trans.Find("purpleFlame");
                pFlame.GetComponent<ParticleSystem>().loop = false;
                trans.DetachChildren();
            }
            Destroy(fire);
            Destroy(gameObject);
        }
        if(other.name == "earth")
        {
            
            if (strength < GameObject.Find("lm").GetComponent<LooseManager>().GetLives())
            {

                GameObject boom = GameObject.Instantiate(boomOnEarth, trans.position, Quaternion.identity);
                float angle = Vector2.Angle(DeltaVector2(new Vector2(0,0), earth.transform.position),
                    DeltaVector2(trans.position, earth.transform.position));
                angle = trans.position.x < earth.transform.position.x ? angle-90 : -angle-90;
                boom.transform.Rotate(angle, -90, 90);
                boom.transform.localScale = new Vector3(0.075f * strength, 0.075f * strength, 0.075f * strength);

                Camera.main.GetComponent<Animator>().Play("boom");

                GameObject.Find("audioEffects").GetComponent<AudioEffects>().BoomOnEarth();

            }
            GameObject.Find("lm").GetComponent<LooseManager>().MinusLive(strength);

            if (isComet)
            {
                Destroy(transform.Find("whiteFlame").gameObject); // Уничтожение белого огня кометы
                Transform pFlame = trans.Find("purpleFlame");
                pFlame.GetComponent<ParticleSystem>().loop = false;
                trans.DetachChildren();


            }
            Destroy(fire);
            Destroy(gameObject);
        }
        if (other.tag == "meteo")
        {
            if (isComet)
            {
                Destroy(transform.Find("whiteFlame").gameObject); // Уничтожение белого огня кометы
                Transform pFlame = trans.Find("purpleFlame");
                pFlame.GetComponent<ParticleSystem>().loop = false;
                trans.DetachChildren();
            }
            Destroy(fire);
            Destroy(gameObject);
            Explode();
        }
    }

    public void Explode()
    {
        if (strength != 1)
        {
            Vector3 v = new Vector3(trans.position.x, trans.position.y, 0.5f);
            GameObject b = GameObject.Instantiate(boom, v, Quaternion.identity);
            b.transform.LookAt(target);
            GameObject.Instantiate(boom2, v, Quaternion.identity);
            if (isComet)
            {
                if(transform.Find("whiteFlame").gameObject) Destroy(transform.Find("whiteFlame").gameObject); // Уничтожение белого огня кометы
                Transform pFlame = trans.Find("purpleFlame");
                pFlame.GetComponent<ParticleSystem>().loop = false;
                trans.DetachChildren();
            }
            Destroy(gameObject);
        }
        else Destroy(gameObject);
        Destroy(fire);
    }



    Vector2 DeltaVector2(Vector2 first, Vector2 second)
    {
        return new Vector2(first.x - second.x, first.y - second.y);
    }

    
}
