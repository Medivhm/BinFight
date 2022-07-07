using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pool : MonoBehaviour
{

    List<GameObject> work = new List<GameObject>();
    Queue<GameObject> wait = new Queue<GameObject>();

    public GameObject GetOne(String name,Vector3 vector3)
    {
        lock (work)
        {
            
            if (wait.Count > 0)
            {
                //wait还有
                GameObject t = wait.Dequeue();
                t.transform.position = vector3;
                t.SetActive(true);
                work.Add(t);
                return t;
            }
            else
            {
                //wait没有
                String path = "Prefabs/" + name;
                GameObject temp = (GameObject)Resources.Load(path);
                GameObject t1 = Instantiate(temp);
                t1.transform.position = vector3;
                t1.SetActive(true);
                work.Add(t1);
                return t1;
            }
        }
    }

    public void returnOne(GameObject t)
    {
        lock(wait){
            t.SetActive(false);
            int i = work.IndexOf(t);
            work.RemoveAt(i);
            wait.Enqueue(t);
        }
    }

    public void DestroyAll()
    {
        foreach (GameObject t in work)
        {
            Destroy(t);
        }
        foreach (GameObject t in wait)
        {
            Destroy(t);
        }
    }
}
