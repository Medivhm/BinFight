using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinsManager : MonoBehaviour
{
    public GameObject cycleBinTemp;
    public GameObject uncycleBinTemp; 

    private Pool cPool = new Pool();
    private Pool uPool = new Pool();

    private float timeCBin;
    private float timeUBin;

    private float coldTime;

    private Coroutine coroutine;

    void Start()
    {
        
    }

    public void Init()
    {
        coroutine = StartCoroutine(CreatBins());
    }

    IEnumerator CreatBins()
    {
        while (true)
        {
            coldTime = Random.Range(3f, 5f);
            if (timeCBin > coldTime)
            {
                GameObject go = cPool.GetOne("CycleBin", new Vector3(1f, Random.Range(-0.5f, 0.5f)));
                CycleBin cb = go.GetComponent<CycleBin>();
                cb.blood = 1;
                cb.isDead = false;
                cb.onDeath += cRecycle;
                timeCBin = 0;
            }
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            if (timeUBin > coldTime)
            {
                GameObject go = uPool.GetOne("UncycleBin", new Vector3(1f, Random.Range(-3f,3f)));
                UncycleBin uc = go.GetComponent<UncycleBin>();
                uc.blood = 2;
                uc.isDead = false;
                uc.onDeath += uRecycle;
                timeUBin = 0;
            }
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }

    void Update()
    {
        timeCBin += Time.deltaTime;
        timeUBin += Time.deltaTime;
    }

    void cRecycle(GameObject go)
    {
        cPool.returnOne(go);
        go.GetComponent<CycleBin>().onDeath -= cRecycle;
    }

    void uRecycle(GameObject go)
    {
        uPool.returnOne(go);
        go.GetComponent<UncycleBin>().onDeath -= uRecycle;
    }

    public void DestroyAll()
    {
        StopCoroutine(coroutine);
        uPool.DestroyAll();
        cPool.DestroyAll();
    }
}
