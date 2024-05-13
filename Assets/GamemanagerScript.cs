using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameManegerScript : MonoBehaviour
{
    //追加
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    //宣言の例
    int[,] map;
    GameObject[,] field;//ゲーム管理用の配列
    GameObject obj;

    //private void PrintArray()
    //{
    //    string debugtext = "";
    //    for (int i = 0; i < map.Length; i++)
    //    {
    //        debugtext += map[i].ToString() + ",";
    //    }
    //    Debug.Log(debugtext);
    //}

    Vector2Int GetPlayerIndex()
    {
      for(int y=0;y<field.GetLength(0);y++)
        {
            for(int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y, x].tag== "Player") { return new Vector2Int(x, y); }
            }
        }
        return new Vector2Int(-1, -1);
    }

     void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))//右移動
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(1, 0));
            //PrintArray();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))//左移動
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(-1, 0));
            //PrintArray();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))//上移動
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(0, -1));
            //PrintArray();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))//下移動
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(0, 1));
            //PrintArray();
        }
    }

    bool MoveNumber( Vector2Int moveFrom, Vector2Int moveTo)
    {
        //縦軸横軸の配列外参照をしていないか
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }
        //Boxタグを持っていたら再帰関数
        if (field[moveTo.y,moveTo.x]!=null&& field[moveTo.y, moveTo.x].tag=="Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber( moveTo, moveTo+velocity);
            if (!success) { return false; }
        }

        //GameObjectの座標(position)を移動させてからインデックスの入れ替え
        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;


        //移動
        //field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];

        return true;
    }

        //






    //int[] map = { 0, 0, 0, 2, 0, 1, 0, 2, 0, 0, 0 };
    //string debugTXT = "";
    //// Start is called before the first frame update
    void Start()
    {//変更。分かりやすく３ｘ５サイズ
        map = new int[,]
        {
            {0,0,0,0,0 },
            {0,2,1,0,0 },
            {0,0,0,0,0 },
        };

        field = new GameObject[map.GetLength(0), map.GetLength(1)];
        




        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y,x] == 1)
                {
                    field[y,x]= Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }
                if (map[y,x]==2)
                {
                    field[y, x] = Instantiate(
                        boxPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0),
                        Quaternion.identity
                        );
                }
            }
        }
    



        //追加
        //GameObject instance = Instantiate(
        //    playerPrefab,
        //    new Vector3(0, 0, 0),
        //    Quaternion.identity
        //   );


        string debugText = "";
        //変更。二重for文で二次元配列の情報を出力
        for(int y=0;y<map.GetLength(0);y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";//改行
        }
        Debug.Log(debugText);
        
        //PrintArray();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.RightArrow))
    //    {
    //        //int PlayerIndex = GetPlayerIndex();

    //        //PrintArray();

    //        //MoveNumber(1, PlayerIndex, PlayerIndex + 1);
    //    }


    //    if (Input.GetKeyDown(KeyCode.LeftArrow))
    //    {
    //        //int PlayerIndex = GetPlayerIndex();

    //        //PrintArray();
    //        //MoveNumber(1, PlayerIndex, PlayerIndex - 1);

    //    }

        
    //}
}
