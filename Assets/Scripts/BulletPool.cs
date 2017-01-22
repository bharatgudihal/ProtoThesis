using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    [SerializeField]
    GameObject prefab;

    [SerializeField]
    int poolSize;

    List<GameObject> bulletPool;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(prefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet()
    {
        GameObject bullet = null;
        for(int i = 0; i < poolSize; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                bullet = bulletPool[i];
                break;
            }
        }
        return bullet;
    }    

}

