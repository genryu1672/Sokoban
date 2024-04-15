using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManegerScript : MonoBehaviour
{
    private void PrintArray()
    {
        string debugtext = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugtext += map[i].ToString() + ",";
        }
        Debug.Log(debugtext);
    }

    int GetPlayerIndex()
    {
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }
    int[] map = { 0, 0, 0, 2, 0, 1, 0, 2, 0, 0, 0 };
    string debugTXT = "";
    // Start is called before the first frame update
    void Start()
    {
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int PlayerIndex = GetPlayerIndex();

            PrintArray();

            MoveNumber(1, PlayerIndex, PlayerIndex + 1);
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int PlayerIndex = GetPlayerIndex();

            PrintArray();
            MoveNumber(1, PlayerIndex, PlayerIndex - 1);

        }

        bool MoveNumber(int number, int movefarm, int moveTo)
        {
            if (moveTo < 0 || moveTo >= map.Length) { return false; }
            if (map[moveTo] == 2)
            {
                int verocity = moveTo - movefarm;

                bool success = MoveNumber(2, moveTo, moveTo + verocity);
                if (!success) { return false; }

            }
            map[moveTo] = number;
            map[movefarm] = 0;
            return true;
        }
    }
}
