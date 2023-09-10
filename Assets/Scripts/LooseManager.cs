using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LooseManager : MonoBehaviour
{
    [SerializeField]
    GameObject UI;
    [SerializeField]
    GameObject textOfLoose;
    [SerializeField]
    AudioClip looseMusic;
    [SerializeField]
    string[] looseTexts;

    AudioSource asc;

    public bool isLoosed { get; set; }
    public int count { get; set; }

    Helper helper;

    GameObject earthBoom1;
    GameObject earthBoom2;
    GameObject moonBoom1;
    GameObject moonBoom2;

    GameObject earth;

    int lives;

    private float lifeTime;

    void Start()
    {
        if (GunChoice.Basa)
        {
            GameObject moon = GameObject.Find("moon");
            GameObject basa = Instantiate(GunChoice.Basa, moon.transform, false);
            basa.transform.position = new Vector3(0, 0.43f, -1);
        }
        else SceneManager.LoadScene(1);

        asc = GameObject.Find("music").GetComponent<AudioSource>();
        count = 0;
        UI.SetActive(false);
        earth = GameObject.Find("earth");
        isLoosed = false;
        lives = 5;
        helper = GameObject.Find("helper").GetComponent<Helper>();
        earthBoom1 = helper.earhBoom1;
        earthBoom2 = helper.earthBoom2;
        moonBoom1 = helper.moonBoom1;
        moonBoom2 = helper.moonBoom2;

        GameObject.Find("SoundsManager").GetComponent<SoundEnable>().UpdateSoundSources();

        lifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.isPaused)
        {
            if (isLoosed && asc.volume <= 0.1f) asc.volume += 0.005f;
            lifeTime += Time.deltaTime;
            if(isLoosed && Input.GetKeyDown(KeyCode.Escape)){
                Back();
            }
        }
        
    }


    public void MinusLive(int l)
    {
        lives -= l;
        if (lives <= 0)
        {
            if (l == 100)
            {
                ExplodeMoon();
            }
            else ExplodeEarth();
        }
    }

    void Loose() 
    {
        if (!isLoosed)
        {
            Destroy(GameObject.Find("PauseManager"));
            asc.volume = 0;
            asc.gameObject.GetComponent<MusicManager>().SetNewMusic(looseMusic);
            isLoosed = true;
            UI.SetActive(true);
            if (PlayerPrefs.GetInt("record",0) < count)
            {
                textOfLoose.GetComponent<Text>().text = "New record: " + count + "!";
                PlayerPrefs.SetInt("record", count);
            }
            else
                textOfLoose.GetComponent<Text>().text = looseTexts[Random.Range(0, looseTexts.Length)];
            
        }
    }

    public void Restart()
    {
        if (PlayerPrefs.GetInt("record", 0) < count)
        {
            
            PlayerPrefs.SetInt("record", count);
        }
        asc.Stop();
        Ads.ShowAds(lifeTime);
        SceneManager.LoadSceneAsync(3);
        SceneManager.LoadSceneAsync(2);
    }


    void ExplodeMoon()
    {
        GameObject.Find("audioEffects").GetComponent<AudioEffects>().BoomMoon();

        Animator camAnim = Camera.main.GetComponent<Animator>();
        camAnim.Play("explodeMoon");

        Vector3 e = GameObject.Find("moon").transform.position;
        GameObject.Instantiate(moonBoom1, e, Quaternion.identity);
        e = new Vector3(e.x, e.y, -3);
        GameObject.Instantiate(moonBoom2, e, Quaternion.identity);
        Destroy(GameObject.Find("moon"));
        Loose();

    }

    void ExplodeEarth()
    {
        GameObject.Find("audioEffects").GetComponent<AudioEffects>().BoomEarth();

        Animator camAnim = Camera.main.GetComponent<Animator>();
        camAnim.Play("explodeEarth");
        
        //Camera.main.GetComponent<Animator>().SetBool("drag", false);


        Vector3 e = earth.transform.position;
        GameObject.Instantiate(earthBoom1, e, Quaternion.identity);
        e = new Vector3(e.x, e.y, -3);
        GameObject.Instantiate(earthBoom2, e, Quaternion.identity);
        Destroy(earth);
        Loose();
    }


    public int GetLives() { return lives; }

    public void Back()
    {
        if (PlayerPrefs.GetInt("record", 0) < count)
        {
            PlayerPrefs.SetInt("record", count);
        }
        asc.Stop();
        Ads.ShowAds(lifeTime);
        SceneManager.LoadSceneAsync(3);
        SceneManager.LoadSceneAsync(1);
    }

    

}
