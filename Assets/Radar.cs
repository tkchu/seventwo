using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour {
    public int[] size = new int[2];
    Map map;
    public float imagesize=0.5f;
    public GameObject[] image = new GameObject[4];
    public GameObject[,] radar;
	// Use this for initialization
	void Start () {
        
        map = FindObjectOfType<Map>();
        
    }
	
    public void Initial()
    {
        if (radar != null)
            foreach (GameObject a in radar)
            {
                Destroy(a);

            }
        radar = new GameObject[map.itemMap.GetLength(0), map.itemMap.GetLength(1)];
        Step();
    }
	// Update is called once per frame
	public void Step () {
        
        for (int i = 0; i < map.itemMap.GetLength(0); i++)
        {
            for (int j = 0; j < map.itemMap.GetLength(1); j++)
            {

                Destroy(radar[i, j]);
                if (map.itemMap[i, j])
                {
                    if (map.itemMap[i, j].tag == "Player")
                    {
                        radar[i,j]= Instantiate(image[0], transform.localPosition + new Vector3(i * imagesize, j * imagesize, 0), Quaternion.identity,transform);

                    }
                    else if (map.itemMap[i, j].tag == "enemy" || map.itemMap[i, j].tag == "bomb")
                    {
                        radar[i, j]= Instantiate(image[1], transform.localPosition + new Vector3(i * imagesize, j * imagesize, 0), Quaternion.identity,transform);

                    }
                    else if (map.itemMap[i, j].tag == "spine")
                    {
                        radar[i, j]= Instantiate(image[2], transform.localPosition + new Vector3(i * imagesize, j * imagesize, 0), Quaternion.identity,transform);

                    }
                    else if (map.itemMap[i, j].tag == "wall")
                    {
                        radar[i, j] = Instantiate(image[3], transform.localPosition + new Vector3(i * imagesize, j * imagesize, 0), Quaternion.identity,transform);

                    }
                }

            }
        }

    }
}
