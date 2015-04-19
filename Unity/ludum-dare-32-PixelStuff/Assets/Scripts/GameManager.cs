﻿using UnityEngine;
using System.Collections;

public enum GameState {
	Intro, // Game launched
	Start,
	Over
}

public class GameManager : MonoBehaviour {

	public BrothersManager brothersManager;
	public BeaversManager beaversManager;
	public MenuManager menuManager;

	private GameState _state = GameState.Over;
	private GameState state {
		get {
			return _state;
		}
		set {
			
			Debug.Log("[GameManager] Changing from state: " + _state.ToString() + ", to: " + value.ToString());

			if (value == _state) 
				return;

			_state = value;

			switch (_state) {
			case GameState.Intro:
				DoIntro ();
				break;
			case GameState.Start:
				DoRestart ();
				break;
			case GameState.Over:
				DoGameOver ();
				break;
			}
		}
	}

	void Start(){
		if (brothersManager == null) {
			brothersManager = GameObject.FindGameObjectWithTag ("Player").GetComponent<BrothersManager> ();
			brothersManager.brothersDiedAction += BrothersDied;
		}

		if (beaversManager == null) {
			beaversManager = GameObject.FindGameObjectWithTag ("BeaversManager").GetComponent<BeaversManager> ();
		}

		if (menuManager == null) {
			menuManager = GameObject.FindGameObjectWithTag ("MenuManager").GetComponent<MenuManager> ();
		}
	}

	void Awake() {
		state = GameState.Intro;
		beaversManager.onBeaverKilledListener += OnBeaverKilled;
		menuManager.restartClicked += MenuRestartBtnClicked;
	}
	
	// Update is called once per frame
	void Update () {

		// Handle Keyboard inputs

		if (Input.GetKeyDown (KeyCode.Escape)) {
			DoIntro();
		}

		// While playing

		if (state == GameState.Start) {

			if (Input.GetKeyDown (KeyCode.S)) {
				DoAttack();
			}
			
			if (Input.GetKeyDown (KeyCode.Space)) {
				DoChopWood();
			}

			if (Input.GetKeyDown (KeyCode.D)) {
				state = GameState.Over;
			}
		}

		if (Input.GetKey (KeyCode.Return)) {
			if (state == GameState.Intro) {
				state = GameState.Start;
			}
		}

	}

	private void DoIntro() {
		state = GameState.Start;
	}

	private void DoRestart() {
		brothersManager.state = BrotherState.MovingIntoScene;
	}

	private void DoAttack() {
		if (brothersManager.state == BrotherState.PrepareAttack) {
			brothersManager.state = BrotherState.Attack;
		} else {
			brothersManager.state = BrotherState.PrepareAttack;
		}
	}

	private void DoChopWood() {
		brothersManager.state = BrotherState.DropThunk;
	}

	private void DoGameOver() {
		brothersManager.state = BrotherState.Dead;
		menuManager.Reset ();
	}

	void OnBeaverKilled(int totalBeaverKilled) {
		Debug.Log ("[GameManager] " + totalBeaverKilled + " total beavers killed");
		menuManager.IncrementScore ();
	}

	public void MenuRestartBtnClicked() {
		DoIntro ();
	}

	public void BrothersDied() {
		state = GameState.Over;
	}
}
