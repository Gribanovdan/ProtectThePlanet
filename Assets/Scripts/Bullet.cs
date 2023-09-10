using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float speed;
    
    const float LIFETIME = 8;
    [SerializeField]
    AudioClip sound;

    AudioClip[] boomSounds;
    float timer;
    [SerializeField] GameObject boom;
    AudioSource asc;

    bool canMove;

    

    void Start()
    {
        canMove = true;
        asc = gameObject.AddComponent<AudioSource>();
        asc.volume = 0.5f;
        asc.pitch = Random.Range(0.5f, 1.5f);
        timer = 0;
        boomSounds = GameObject.Find("helper").GetComponent<Helper>().boomSounds;
        asc.clip = sound;
        if(Settings.SoundsAllowed)
        asc.Play();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.isPaused)
        {
            #region die
            timer += Time.deltaTime;
            if (timer >= LIFETIME) Destroy(gameObject);
            #endregion
            #region movement
            if (canMove)
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            else asc.volume -= 0.0005f;
            #endregion
            if (!asc.isPlaying) asc.UnPause();
        }
        else if (asc.isPlaying) asc.Pause();
    }

    void Explose()
    {
        asc.clip = boomSounds[Random.Range(0, boomSounds.Length)];
        if(Settings.SoundsAllowed)
        asc.Play();
        canMove = false;
        GameObject.Instantiate(boom, transform.position, Quaternion.identity);
        transform.position = new Vector3(100, 0, 5); //уносим подальше эту ракету
        GameObject.Destroy(gameObject,5f);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "meteo")
        {
            other.gameObject.GetComponent<Meteo>().Explode();
            GameObject.Find("lm").GetComponent<LooseManager>().count+= GunChoice.Difficulty;
            Explose();
            
        }
    }

}
