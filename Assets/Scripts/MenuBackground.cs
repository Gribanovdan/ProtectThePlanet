using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuBackground : MonoBehaviour
{

    const float SPAWNRECT = 10f;
    const float VISIBLERECT = 9f;

    [SerializeField] GameObject cometPrefab;
    GameObject comet;

    [SerializeField] float speed;

    Vector2 target;

    void Start()
    {
        comet = GameObject.Instantiate(cometPrefab, GetSpawnVect(), Quaternion.identity);
        NewComet();
    }


    void Update()
    {
        comet.transform.position = Vector2.MoveTowards(comet.transform.position, target, speed * Time.deltaTime);

        //if (comet.transform.position.x > SPAWNRECT || comet.transform.position.x < -SPAWNRECT ||
        //    comet.transform.position.y > SPAWNRECT || comet.transform.position.y < -SPAWNRECT) NewComet();
        if (comet.transform.position.x == target.x && comet.transform.position.y == target.y) NewComet(); 
    }

    Vector2 GetSpawnVect()
    {
        Vector2 vect = new Vector2(Random.Range(-SPAWNRECT, SPAWNRECT), Random.Range(-SPAWNRECT, SPAWNRECT));
        if (vect.x <= VISIBLERECT && vect.x >= -VISIBLERECT|| vect.y <= VISIBLERECT && vect.y >= -VISIBLERECT) return GetSpawnVect();
        else return vect;
    }

    Vector2 GetTargetVect()
    {
        return new Vector2(Random.Range(-VISIBLERECT, VISIBLERECT), Random.Range(-VISIBLERECT, VISIBLERECT));
    }

    Vector2 DeltaVector2(Vector2 first, Vector2 second)
    {
        return new Vector2(first.x - second.x, first.y - second.y);
    }

    void RotateComet()
    {
        comet.transform.rotation = Quaternion.identity;
        Vector2 v = DeltaVector2(comet.transform.position, target);
        float angle = Vector2.Angle(Vector2.up, v);

        if (comet.transform.position.x > target.x) angle = - angle;
        print(angle);
        comet.transform.Rotate(0, 0, angle + 30); // +30,  обусловлено направлением спрайта
    }

    void NewComet()
    {
        comet.transform.position = GetSpawnVect();
        target = GetSpawnVect();
        RotateComet();
    }

}
