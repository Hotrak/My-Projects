using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public GameObject b;
	public GameObject t;
	public GameObject l;
	public GameObject r;

    public GameObject tb;
    public GameObject rb;
    public GameObject lb;

    public GameObject tl;
    public GameObject tr;

    public GameObject lr;

    public GameObject rbl;
    public GameObject rtl;
    public GameObject ltb;

    public GameObject block;
    public GameObject start;

	//public GameObject closedRoom;

	//public List<GameObject> rooms;

	//public float waitTime;
	//private bool spawnedBoss;
	//public GameObject boss;

	void Update(){

		//if(waitTime <= 0 && spawnedBoss == false){
		//	for (int i = 0; i < rooms.Count; i++) {
		//		if(i == rooms.Count-1){
		//			Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
		//			spawnedBoss = true;
		//		}
		//	}
		//} else {
		//	waitTime -= Time.deltaTime;
		//}
	}
}
