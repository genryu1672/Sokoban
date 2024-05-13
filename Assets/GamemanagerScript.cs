using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class GameManegerScript : MonoBehaviour
{
    //�ǉ�
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;

    //�ǋL
    public GameObject clearText;


    //�錾�̗�
    int[,] map;
    GameObject[,] field;//�Q�[���Ǘ��p�̔z��
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
        if (Input.GetKeyUp(KeyCode.RightArrow))//�E�ړ�
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(1, 0));
            //PrintArray();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))//���ړ�
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(-1, 0));
            //PrintArray();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))//��ړ�
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(0, -1));
            //PrintArray();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))//���ړ�
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(
                playerIndex,
                playerIndex + new Vector2Int(0, 1));
            //PrintArray();
            if(IsCleard())
            {
                clearText.SetActive(true);
            }
        }
    }

    bool IsCleard()
    {
        //Vector2Int�^�̉ϒ��z��̍쐬
        List<Vector2Int> goals = new List<Vector2Int>();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //�i�[�ꏊ���ۂ��𔻒f
                if (map[y, x] == 3)
                {
                    goals.Add(new Vector2Int(x, y));
                }
                //�v�f����goals.Count�Ŏ擾
                for (int i = 0; i < goals.Count; i++)
                {
                    GameObject f = field[goals[i].y, goals[i].x];
                    if (f == null || f.tag != "Box")
                    {
                        //��ł������Ȃ�������������B��
                        return false;
                    }
                }
                
            }
        }
        //�������B���łȂ���Ώ����B��
        return true;
    }




    bool MoveNumber( Vector2Int moveFrom, Vector2Int moveTo)
    {
        //�c�������̔z��O�Q�Ƃ����Ă��Ȃ���
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }
        //Box�^�O�������Ă�����ċA�֐�
        if (field[moveTo.y,moveTo.x]!=null&& field[moveTo.y, moveTo.x].tag=="Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber( moveTo, moveTo+velocity);
            if (!success) { return false; }
        }

        //GameObject�̍��W(position)���ړ������Ă���C���f�b�N�X�̓���ւ�
        field[moveFrom.y, moveFrom.x].transform.position = new Vector3(moveTo.x, field.GetLength(0) - moveTo.y, 0);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;


        //�ړ�
        //field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];

        return true;
    }

        //






    //int[] map = { 0, 0, 0, 2, 0, 1, 0, 2, 0, 0, 0 };
    //string debugTXT = "";
    //// Start is called before the first frame update
    void Start()
    {//�ύX�B������₷���R���T�T�C�Y
        map = new int[,]
        {
            {0,0,0,0,0 },     //�R���S�[���g
            {0,3,1,3,0 },
            {0,0,2,0,0 },
            {0,2,3,2,0 },
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
                if (map[y, x] == 3)
                {
                    field[y, x] = Instantiate(
                        goalPrefab,
                        new Vector3(x, map.GetLength(0) - y, 0.1f),
                        Quaternion.identity
                        );
                }
            }
        }
    



        //�ǉ�
        //GameObject instance = Instantiate(
        //    playerPrefab,
        //    new Vector3(0, 0, 0),
        //    Quaternion.identity
        //   );


        string debugText = "";
        //�ύX�B��dfor���œ񎟌��z��̏����o��
        for(int y=0;y<map.GetLength(0);y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";//���s
        }
        //Debug.Log(debugText);
        
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
