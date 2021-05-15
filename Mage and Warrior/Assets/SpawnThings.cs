using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThings : MonoBehaviour
{
    public Transform mageLocation;
    public GameObject[] prefab;
    public GameObject[] Clone;
    int cloneNum = 0;
    private void Start()
    {
        SpawnThings sn = GameObject.Find("spawner").GetComponent<SpawnThings>();
        int x = sn.SpawnGoblin(65, -1);
        x = sn.SpawnGoblin(60, -1);
        x = sn.SpawnGoblin(50, -1);
        x = sn.SpawnGoblin(20, -1);
    }

    public int SpawnDamageProjectile(float Xoffset, float Yoffset)
    {
        Vector3 spawnLocation = new Vector3(mageLocation.position.x + Xoffset, mageLocation.position.y + Yoffset, mageLocation.position.z);
        Clone[cloneNum] = Instantiate(prefab[0], spawnLocation, Quaternion.Euler(0, 0, 0)) as GameObject;
        cloneNum++;
        return cloneNum - 1;
    }
    public int SpawnRockProjectile(float Xoffset, float Yoffset)
    {
        Vector3 spawnLocation = new Vector3(mageLocation.position.x + Xoffset, mageLocation.position.y + Yoffset, mageLocation.position.z);
        Clone[cloneNum] = Instantiate(prefab[1], spawnLocation, Quaternion.Euler(0, 0, 0)) as GameObject;
        cloneNum++;
        return cloneNum - 1;
    }

    public int SpawnGoblin(float x, float y) {
        Vector3 spawnLocation = new Vector3(x, y, 0);
        Clone[cloneNum] = Instantiate(prefab[2], spawnLocation, Quaternion.Euler(0, 0, 0)) as GameObject;
        cloneNum++;
        return cloneNum - 1;
    }
}
