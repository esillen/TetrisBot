using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameField : MonoBehaviour {

	public Wrapper theWrapper;

    public static bool [,] game_field = new bool[20,10];
    public List<GameObject> sprites;
    public GameObject orig_sprite;
    public Block selected_block, nextBlock;
    public GameObject t_game_field;
    public GameObject t_surrounding;
    private static List<string> letters = new List<string>() {"I","J","L","O","T","Z","S"};

    public void Start(){
        t_game_field = new GameObject();
        t_game_field.name = "game_field";
        t_game_field.transform.parent = transform.parent;
        t_surrounding = new GameObject();
        t_surrounding.name = "surrounding";
        t_surrounding.transform.parent = transform.parent;
        selected_block = new Block("J", transform, orig_sprite, 4, -1);
		nextBlock = new Block("J", transform, orig_sprite, 4, -1);

        for (int i = -1; i< game_field.GetLength(0)+1; i++){
            for (int j = -1; j< game_field.GetLength(1)+1; j++){
                if (i < 0 || i == game_field.GetLength(0) || j < 0 || j == game_field.GetLength(1)) {
                    GameObject g = Instantiate(orig_sprite, new Vector3(j, i, 0), Quaternion.identity) as GameObject;
                    g.transform.parent = t_surrounding.transform;
                    g.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
        }
    }

	public void moveDown(){
		if (selected_block.Move(0, 1)){
			selected_block.UpdateVisuals();
		}
		else {
			setNewBlock ();
		}
	}

	public void moveDrop(){
		while (selected_block.Move(0, 1));
		setNewBlock ();
	}

	public void moveLeft(){
		if (selected_block.Move(-1, 0)) {
			selected_block.UpdateVisuals();
		}
	}

	public void moveRight(){
		if (selected_block.Move(1, 0)){
			selected_block.UpdateVisuals();
		}

	}

	public void moveSpin(){
		if (selected_block.Rotate()){
			selected_block.UpdateVisuals();
		}
	}

	public void setNewBlock(){
		MergeBlockWithField(selected_block);
		selected_block = nextBlock;
		nextBlock = new Block(letters[Random.Range(0,6)],transform, orig_sprite,4, -1);
		theWrapper.updateBlock(selected_block, nextBlock);
	}

    public void Update() {
        sprites = new List<GameObject>();
    }

    public void MergeBlockWithField(Block b) {
        Destroy(b.block_transform);

        b.sprites.Clear();
        for (int i = 0; i < b.the_array.GetLength(0); i++)
        {
            for (int j = 0; j < b.the_array.GetLength(1); j++)
            {
                if (b.the_array[i,j]){
                    game_field[b.ypos + i, b.xpos + j] = true;
                }
            }
        }
        //Collapse rows if possible
        int current_row = game_field.GetLength(0)-1;
        int temp_score = 0;
        while (current_row > 0) {
            //If entire row is full, move everything down.
            int counter = 0;
            for (int j = 0; j < game_field.GetLength(1); j++) { 
                if (game_field[current_row, j]){
                    counter++;
                }
            }
            if (counter == game_field.GetLength(1))
            {
                temp_score++;
                //Move everything above down!
                for (int i = current_row; i > 0; i--)
                {
                    for (int j = 0; j < game_field.GetLength(1); j++)
                    {
                        game_field[i, j] = game_field[i - 1, j];
                    }
                }
                //Uppermost row is cleared
                for (int j = 0; j < game_field.GetLength(1); j++)
                {
                    game_field[0, j] = false;
                }
            }
            else {
                current_row--;
            }
        }
        Debug.Log("Scored " + temp_score + " rows!");
        UpdateVisuals();
        
    }

    private void UpdateVisuals() { 
        //Stupid solution
        foreach (Transform child in t_game_field.transform) {
            Destroy(child.gameObject);
        }
        sprites.Clear();
        for (int i = 0; i < game_field.GetLength(0); i++) {
            for (int j = 0; j < game_field.GetLength(1); j++)
            {
                if (game_field[i, j])
                {
                    GameObject sprite = Instantiate(orig_sprite, new Vector3(j, i, 0), Quaternion.identity) as GameObject;
                    sprites.Add(sprite);
                    sprite.GetComponent<SpriteRenderer>().color = Color.gray;
                    sprite.transform.parent = t_game_field.transform;
                }

            }
        }


    
    }




}
