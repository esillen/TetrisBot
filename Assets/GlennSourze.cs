using UnityEngine;
using System.Collections.Generic;
using System;

public class GlennSourze : MonoBehaviour{

	public GameField theGameField;
	public Wrapper theWrapper;

	private Action left, right, down, drop, spin;
	private float timer = 0;
	private float moveTime = 1;

	private Block currentBlock, nextBlock;
	private List<Action> moveOrder = new List<Action>();




	private void evaluuateAndDecide(){
		bool [,] game_field = GameField.game_field;
		moveOrder.Add (drop);
	}




	private void play(){
		evaluuateAndDecide ();
		getBlocks(theWrapper.playMove (moveOrder));
		moveOrder.Clear ();
	}

	private void getBlocks(GameInfo theInfo){
		currentBlock = theInfo.currentBlock;
		nextBlock = theInfo.nextBlock;
	}


	#region TimerSheeit
	void Update(){
		if (Time.time >= timer) {
			play ();
			setTimer ();
		}
	}
	
	private void setTimer(){
		timer = moveTime + Time.time;
	}
	#endregion
	
	#region Innit
	void Start(){
		initActions();
	}

	void initActions(){
		left = theGameField.moveLeft;
		right = theGameField.moveRight;
		down = theGameField.moveDown;
		drop = theGameField.moveDrop;
		spin = theGameField.moveSpin;
	}
	#endregion
}
