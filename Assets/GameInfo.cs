using UnityEngine;
using System.Collections.Generic;

public class GameInfo {

	public Block currentBlock, nextBlock;

	public GameInfo(Block currentBlock, Block nextBlock){
		this.currentBlock = currentBlock;
		this.nextBlock = nextBlock;
	}

}
