using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAVE_SYSTEM : MonoBehaviour
{
    [SerializeField] Transform objectParent;
    [SerializeField] Transform ennemyParent;
    [SerializeField] Transform[] points;
    [SerializeField] GameObject[] ennemyPrefab;
    [SerializeField] GameObject[] throwableObjects;
    [SerializeField] List<Transform> ennemiesInArena = new List<Transform>();
    [SerializeField] List<Transform> objectInArena = new List<Transform>();
    [SerializeField] int difficulty;
    [SerializeField] int difficultyLeft;
    public bool everyoneHasSpawn;
    bool newWave = false, waveAppear;
    bool listDone;
    bool spawnMob;
    bool spawnObj;


    void Start()
    {
        difficultyLeft = difficulty;
    }

    void FixedUpdate()
    {
        if (everyoneHasSpawn==false)
        {
            /* for (int i = 0; i < points.Length; i++)
            {
                float spawn = Random.Range(0,2);
                GameObject ennemyGO;

                if (spawn == 0 && difficultyLeft > 2)
                {
                    ennemyGO = Instantiate(ennemyPrefab[Random.Range(0, ennemyPrefab.Length)], points[i].position, Quaternion.identity, ennemyParent);
                    ennemiesInArena.Add(ennemyGO.transform);


                }
                else if (difficultyLeft < 2)
                {difficultyLeft = 0;}
                else
                {
                    Instantiate(throwableObjects[Random.Range(0, throwableObjects.Length)], points[i].position, Quaternion.identity, objectParent);
                    
                }
            }*/



                 List<GameObject> goodDifficultyEnnemy = new List<GameObject>();


                    for (int x = 0; x < ennemyPrefab.Length; x++)
                    {

                        #region THE IF REGION
                        if (ennemyPrefab[x].GetComponent<HeadButtEnemy>() != null && difficulty < 24) 
                        {
                            if (ennemyPrefab[x].GetComponent<HeadButtEnemy>().difficultyLevel <= difficultyLeft)
                            {
                                //difficultyLeft = ennemyPrefab[i].GetComponent<HeadButtEnemy>().difficultyLevel - difficultyLeft;
                                goodDifficultyEnnemy.Add(ennemyPrefab[x]);
                            }
                        }

                        else if (ennemyPrefab[x].GetComponent<Kami>() != null && difficulty < 24) 
                        {
                            if (ennemyPrefab[x].GetComponent<Kami>().difficultyLevel <= difficultyLeft)
                            {
                                //difficultyLeft = ennemyPrefab[i].GetComponent<Kami>().difficultyLevel - difficultyLeft;
                                goodDifficultyEnnemy.Add(ennemyPrefab[x]);
                            }
                        }

                        else if (ennemyPrefab[x].GetComponent<SwordCarrier>() != null) 
                        {
                            if (ennemyPrefab[x].GetComponent<SwordCarrier>().difficultyLevel <= difficultyLeft)
                            {
                                //difficultyLeft = ennemyPrefab[i].GetComponent<SwordCarrier>().difficultyLevel - difficultyLeft;
                                goodDifficultyEnnemy.Add(ennemyPrefab[x]);
                            }
                        }

                        else if (ennemyPrefab[x].GetComponentInChildren<GunCock>() != null) 
                        {
                            if (ennemyPrefab[x].GetComponentInChildren<GunCock>().difficultyLevel <= difficultyLeft)
                            {
                                //difficultyLeft = ennemyPrefab[i].GetComponent<GunCock>().difficultyLevel - difficultyLeft;
                                goodDifficultyEnnemy.Add(ennemyPrefab[x]);
                            }
                        }

                        else if (ennemyPrefab[x].GetComponentInChildren<Scorpio>() != null && difficulty < 24) 
                        {
                            if (ennemyPrefab[x].GetComponentInChildren<Scorpio>().difficultyLevel <= difficultyLeft)
                            {
                                //difficultyLeft = ennemyPrefab[i].GetComponent<Scorpio>().difficultyLevel - difficultyLeft;
                                goodDifficultyEnnemy.Add(ennemyPrefab[x]);
                            }
                        }
                        #endregion
                    }

                    for (int i = 0; i < points.Length; i++)
                    {                         
                        int spawn = Random.Range(0,1);
                        GameObject objectGO; 
                                 
                        if (spawn == 0 && difficultyLeft > 2)
                        {
                            int rightGameObject = Random.Range(0, goodDifficultyEnnemy.Count);
                            GameObject rightPrefab = goodDifficultyEnnemy[rightGameObject];
                            GameObject ennemyGO; 

                            #region THE IF REGION 2 // 
                            if (rightPrefab.GetComponent<HeadButtEnemy>() != null) 
                            {
                                difficultyLeft = difficultyLeft - rightPrefab.GetComponent<HeadButtEnemy>().difficultyLevel;
                                ennemyGO = Instantiate(rightPrefab, points[i].position, Quaternion.identity, ennemyParent);
                                ennemiesInArena.Add(ennemyGO.transform);
                            }

                            else if (rightPrefab.GetComponent<Kami>() != null) 
                            {
                                difficultyLeft = difficultyLeft - rightPrefab.GetComponent<Kami>().difficultyLevel;
                                ennemyGO = Instantiate(rightPrefab, points[i].position, Quaternion.identity, ennemyParent);
                                ennemiesInArena.Add(ennemyGO.transform);
                            }

                            else if (rightPrefab.GetComponent<SwordCarrier>() != null) 
                            {
                                difficultyLeft = difficultyLeft - rightPrefab.GetComponent<SwordCarrier>().difficultyLevel;
                                ennemyGO = Instantiate(rightPrefab, points[i].position, Quaternion.identity, ennemyParent);
                                ennemiesInArena.Add(ennemyGO.transform);
                            }

                            else if (rightPrefab.GetComponentInChildren<GunCock>() != null) 
                            {
                                difficultyLeft = difficultyLeft - rightPrefab.GetComponentInChildren<GunCock>().difficultyLevel;
                                ennemyGO = Instantiate(rightPrefab, points[i].position, Quaternion.identity, ennemyParent);
                                ennemiesInArena.Add(ennemyGO.transform);
                            }

                            else if (rightPrefab.GetComponentInChildren<Scorpio>() != null) 
                            {
                                difficultyLeft = difficultyLeft -rightPrefab.GetComponentInChildren<Scorpio>().difficultyLevel;
                                ennemyGO = Instantiate(rightPrefab, points[i].position, Quaternion.identity, ennemyParent);
                                ennemiesInArena.Add(ennemyGO.transform);
                            }
                            

                            #endregion
                        }
                        else if (objectInArena.Count <= 20)
                        {
                            objectGO = Instantiate(throwableObjects[Random.Range(0, throwableObjects.Length)], points[i].position, Quaternion.identity, objectParent);
                            objectInArena.Add(objectGO.transform);            
                        }


                    }


            newWave = false;
            everyoneHasSpawn = true;
        }

        if (ennemiesInArena.Contains(null))
        {
            ennemiesInArena.Remove(null);
        }
        else if (ennemiesInArena.Count == 0)
        {
            newWave = true;
            waveAppear = false;
            everyoneHasSpawn = false;
        }

        waveDifficulty();

    }

    void waveDifficulty()
    {
        if (newWave == true && waveAppear == false)
        {
            difficulty += 2;
            difficultyLeft = difficulty;
            waveAppear = true;
        }
    }
}
