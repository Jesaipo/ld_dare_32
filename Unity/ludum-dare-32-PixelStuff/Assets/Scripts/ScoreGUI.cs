﻿using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

	public Font myFont;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnGUI(){
		GUIStyle myStyle = new GUIStyle();
		myStyle.font = myFont;

		GUI.Label(new Rect(Screen.width/2-40, Screen.height/2-50, 100, 20),"Beavers Killed: "+54,myStyle);
	}
}
