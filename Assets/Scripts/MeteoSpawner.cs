using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoSpawner : MonoBehaviour
{

    
    const int METEOSTODIFUP = 2;

    [SerializeField]
    GameObject[] meteos = new GameObject[9];

    LooseManager lm;

    int dif;   //уровень сложности
    float spawnTime;
    int spawnedMeteos;
    float minSpawnTime;
    int maxDifLevel;
    float timer;

    void Start()
    {
        timer = -2f; // Чтобы метеориты спавнились после начальной анимации
        dif = GunChoice.Difficulty;
        spawnedMeteos = 0;
        if(dif == 1)
        {
            spawnTime = 3f;
            minSpawnTime = 0.5f;
            maxDifLevel = 2;
        }else if(dif == 2)
        {
            spawnTime = 3f;
            minSpawnTime = 2f;
            maxDifLevel = 6;
        }
        else if(dif ==3)
        {
            spawnTime = 2.5f;
            minSpawnTime = 1.5f;
            maxDifLevel = 7;
        }
        dif *= 2;
        lm = GameObject.Find("lm").GetComponent<LooseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseManager.isPaused)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0;
                SpawnMeteo();

            }
        }
    }


    void SpawnMeteo()
    {
        if (!lm.isLoosed)
        {
            Vector2 sVect = new Vector2(Random.Range(-2f, 2f), 6);
            int meteoLevel = Random.Range(dif - 2, dif + 2);
            GameObject meteo = GameObject.Instantiate(meteos[meteoLevel], sVect, Quaternion.identity);
            spawnedMeteos++;
            if (spawnedMeteos >= dif * METEOSTODIFUP)
            {
                if (dif < meteos.Length - 2 && dif < maxDifLevel)
                {
                    if(minSpawnTime < spawnTime)
                    spawnTime -= 0.5f;
                    dif++;
                    
                }
                if (meteoLevel == meteos.Length - 1) spawnTime = 2.5f; //Если вылетает комета, то время между спавнами увелчивается
            }
            if(spawnedMeteos >= 30 && GunChoice.Difficulty == 1)
            {
                spawnTime = 3f;
                minSpawnTime = 2f;
                maxDifLevel = 6;
                GunChoice.Difficulty = 2; //Переход на второй уровень сложности после 30 очков
            }
        }
    }
}
