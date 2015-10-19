using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	public List<GameObject> templateList;
    public float interval, SpawnCircleDistanceMin, SpawnCircleDistanceMax;
    public int maxSpawnNum;
    private Camera cam;
    private float lastSpawnTime;
    private int currentSpawned;

    // Use this for initialization
    void Start () {
        lastSpawnTime = Time.time;
        cam = GameObject.Find("Main Camera Left").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 pos;
        if(Time.time - lastSpawnTime > interval)
        {
            if (templateList.Count > 0 && cam != null && currentSpawned < maxSpawnNum)
            {
                Random.seed = (int)System.DateTime.Now.Ticks;
                Quaternion qot = new Quaternion();
                qot.eulerAngles = new Vector3(0,Random.Range(0,360),0);
                pos = cam.transform.position + qot * (Vector3.forward * (SpawnCircleDistanceMin + Random.value * (SpawnCircleDistanceMax - SpawnCircleDistanceMin))) + new Vector3(0,Random.Range(-0.3f,0.3f),0);
                Spawn(templateList[Random.Range(0, templateList.Count)], 1, pos, new Quaternion(), 0.2f, ref currentSpawned);
                lastSpawnTime = Time.time;
            }
        }
	
	}

    public static GameObject Spawn(GameObject prefab, int num, Vector3 loc, Quaternion rot, float radius, ref int spawnedNum)
    {
        Random.seed = (int)Time.time;
        for (int i = 0; i < num; i++)
        {
            GameObject obj = Instantiate(prefab);

            if (obj != null)
            {
                obj.transform.position = loc + new Vector3(radius * Random.Range(-1f, 1f), loc.y, radius * Random.Range(-1f, 1f));
                obj.transform.rotation = rot;
                spawnedNum++;
                obj.name = prefab.name + spawnedNum.ToString();
            }
        }
        return null;
    }

}
