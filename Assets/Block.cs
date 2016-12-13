using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block {

    public static bool[,] I = new bool[,] { { false, false, false, false }, { true, true, true, true }, { false, false, false, false }, { false, false, false, false } };
    public static bool[,] J = new bool[,] { { true, false, false }, { true, true, true }, { false, false, false } };
    public static bool[,] L = new bool[,] { { false, false, true }, { true, true, true }, { false, false, false } };
    public static bool[,] O = new bool[,] { { true, true }, { true, true } };
    public static bool[,] S = new bool[,] { { false, true, true }, { true, true, false }, { false, false, false } };
    public static bool[,] T = new bool[,] { { false, true, false }, { true, true, true }, { false, false, false } };
    public static bool[,] Z = new bool[,] { { true, true, false }, { false, true, true }, { false, false, false } };

    public bool[,] the_array;
    public List<GameObject> sprites;
    public int xpos = 4, ypos = 5;
    public GameObject block_transform;

	public Block(string type, Transform t_parent, GameObject orig_sprite, int x, int y) {
		xpos = x;
		ypos = y;
        switch (type){
            case "I":
                the_array = I.Clone() as bool[,];
                break;
            case "J":
                the_array = J.Clone() as bool[,];
                break;
            case "L":
                the_array = L.Clone() as bool[,];
                break;
            case "O":
                the_array = O.Clone() as bool[,];
                break;
            case "S":
                the_array = S.Clone() as bool[,];
                break;
            case "T":
                the_array = T.Clone() as bool[,];
                break;
            case "Z":
                the_array = Z.Clone() as bool[,];
                break;
            default:
                Debug.LogError("ERRRROOOOOORRR");
                break;
        }
        block_transform = new GameObject();
        block_transform.name = "current_block";
        block_transform.transform.parent = t_parent;

        sprites = new List<GameObject>();
        for(int i=0; i< the_array.GetLength(0); i++){
            for(int j=0;j<the_array.GetLength(1);j++){
                if (the_array[i,j]){
                    GameObject derrr = GameObject.Instantiate(orig_sprite, new Vector3(xpos + j, ypos + i, 0), Quaternion.identity) as GameObject;
                    derrr.transform.parent = block_transform.transform;
                    sprites.Add(derrr);
                }
            }
        }
    }



    public bool Move(int xdir, int ydir)
    {
        xpos += xdir;
        ypos += ydir;
        //Check if the move is possible
        for (int i = 0; i < the_array.GetLength(0); i++)
        {
            for (int j = 0; j < the_array.GetLength(1); j++)
            {
				if (the_array[i, j] && ypos+i>=0)
                {
                    if (xpos + j < 0 || xpos + j >= GameField.game_field.GetLength(1) || ypos + i >= GameField.game_field.GetLength(0) || GameField.game_field[ypos + i, xpos + j]){
                        //Collission
                        xpos -= xdir;
                        ypos -= ydir;
                        Debug.Log("Collission");
                        return false;
                    }

                }
            }
        }
        return true;

    }

    public bool Rotate(){ //Only clockwise possible??
        //Do some crazy rotations!
        bool[,] transposed_array = new bool[the_array.GetLength(0), the_array.GetLength(1)];
        bool[,] flipped_array = new bool[the_array.GetLength(0), the_array.GetLength(1)];
        //Transpose!
        for (int i = 0; i < the_array.GetLength(0); i++)
        {
            for (int j = 0; j < the_array.GetLength(1); j++)
            {
                transposed_array[i, j] = the_array[j, i];
            }
        }
        //Flip!
        for (int i = 0; i < the_array.GetLength(0); i++)
        {
            for (int j = 0; j < the_array.GetLength(1); j++)
            {
                flipped_array[i, j] = transposed_array[i, the_array.GetLength(1) -1 - j];
				if (flipped_array[i, j] && ypos +i>=0)
                {
                    if (xpos + j < 0 || xpos + j >= GameField.game_field.GetLength(1) || ypos + i >= GameField.game_field.GetLength(0) || GameField.game_field[ypos + i, xpos + j])
                    {
                        Debug.Log("Rotation collission");
                        return false; //Collission!!!
                    }
                }
            }
        }
        //Set as the boss array!
        for (int i = 0; i < the_array.GetLength(0); i++)
        {
            for (int j = 0; j < the_array.GetLength(1); j++)
            {
                the_array[i, j] = flipped_array[i, j];
            }
        }
        return true;
    }

    public void UpdateVisuals(){
        int sprite_counter = 0;
        for (int i = 0; i < the_array.GetLength(0); i++)
        {
            for (int j = 0; j < the_array.GetLength(1); j++)
            {
                if (the_array[i, j]) {
                    sprites[sprite_counter].transform.position = new Vector3( xpos + j, ypos + i, 0);
                    sprite_counter++;
                }
            }
        }
    }


    




}

