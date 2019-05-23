using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WAVE_SYSTEM : MonoBehaviour
{
    Player plScript;

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

    public bool TransitionScene;
    bool CoroutineOnce;
    float timeToNextScene;

    bool newWave = false, waveAppear;
    bool listDone;
    bool spawnMob;
    bool spawnObj;
    int spawnChance = 1;

    Text waveInfo; //////// Waves
    string waveWrite;
    float numberOFWave;


    void Start()
    {
        plScript = FindObjectOfType<Player>();
        difficultyLeft = difficulty;
        waveInfo = GetComponentInChildren<Text>();
        waveWrite = "WAVE : ";
        waveInfo.text = waveWrite + 0.ToString();
    }

    void FixedUpdate()
    {
        if (TransitionScene == true && CoroutineOnce == false)
        {
            timeToNextScene = Random.Range(30, 40);
            StartCoroutine(SwitchScene());
            CoroutineOnce = true;
        }

        if (everyoneHasSpawn == false)
        {

            List<GameObject> goodDifficultyEnnemy = new List<GameObject>();

            if (difficulty > 8 && difficulty <= 20)
            { spawnChance = Random.Range(1, 2); }
            else if (difficulty > 20)
            { spawnChance = Random.Range(1, Random.Range(1, 2)); }


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
                int spawn = Random.Range(0, spawnChance);
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
                        difficultyLeft = difficultyLeft - rightPrefab.GetComponentInChildren<Scorpio>().difficultyLevel;
                        ennemyGO = Instantiate(rightPrefab, points[i].position, Quaternion.identity, ennemyParent);
                        ennemiesInArena.Add(ennemyGO.transform);
                    }


                    #endregion
                }
                else if (difficultyLeft > 6)
                {
                    difficultyLeft = 0;
                }
                else if (objectInArena.Count <= 20)
                {
                    objectGO = Instantiate(throwableObjects[Random.Range(0, throwableObjects.Length)], points[i].position, Quaternion.identity, objectParent);
                    objectInArena.Add(objectGO.transform);
                }

                Debug.Log(spawn);


            }


            newWave = false;
            everyoneHasSpawn = true;
        }

        if (ennemiesInArena.Contains(null))
        { ennemiesInArena.Remove(null); }

        if (objectInArena.Contains(null))
        { objectInArena.Remove(null); }


        else if (ennemiesInArena.Count == 0)
        {
            newWave = true;
            waveAppear = false;
            everyoneHasSpawn = false;
            waveDifficulty();
        }

        if (waveInfo.rectTransform.localScale.y >= 3)
        { WaveChill(); }

    }

    void waveDifficulty()
    {
        if (newWave == true && waveAppear == false)
        {
            waveInfo.rectTransform.localScale = new Vector3(6, 6, 6);
            waveInfo.color = new Color(1, 0, 0);

            numberOFWave += 1;
            waveInfo.text = waveWrite + numberOFWave.ToString();
            difficulty += 2;
            difficultyLeft = difficulty;
            waveAppear = true;
        }
    }

    void WaveChill()
    {
        Debug.Log("argent");
        waveInfo.rectTransform.localScale = new Vector3(Mathf.Clamp(waveInfo.rectTransform.localScale.x - 0.05f, 4, 6), Mathf.Clamp(waveInfo.rectTransform.localScale.y - 0.05f, 4, 6), Mathf.Clamp(waveInfo.rectTransform.localScale.z - 0.05f, 4, 6));
        waveInfo.color = new Color(1, Mathf.Clamp(waveInfo.color.g + 0.01f, 0, 1), Mathf.Clamp(waveInfo.color.b + 0.01f, 0, 1));
    }

    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(timeToNextScene);
        plScript.levelEnd = true;
    }
}
