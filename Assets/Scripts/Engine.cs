using Model;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Engine : MonoBehaviour
{
    public static Tilemap tileMap;


	// Use this for initialization
	public void Start()
    {
        GameObject tmObj = GameObject.Find("Tilemap");
        tileMap = tmObj.GetComponent<Tilemap>();

        // Load up the first level
        //GameObject.Instantiate(Resources.Load<GameObject>(Constants.PREFAB_LVL01));
	}
	
	// Update is called once per frame
	public void Update()
    {
		
	}

   
}
