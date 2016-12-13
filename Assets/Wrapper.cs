using UnityEngine;
using System;
using System.Collections.Generic;

public class Wrapper : MonoBehaviour {

	public GameField theGameField;
	private Block selected_block, nextBlock;
	
	public void updateBlock(Block theNewBlock, Block nextBlock){
		selected_block = theNewBlock;
		this.nextBlock = nextBlock;
	}


	public GameInfo playMove(List<Action> currentMove){
		playActions (currentMove);
		return new GameInfo (selected_block, nextBlock);
	}

	private void playActions(List<Action> currentMove){
		foreach (Action a in currentMove)
			a.Invoke ();
	}
}
