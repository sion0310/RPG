using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSV_ex : MonoBehaviour
{
    List<Dictionary<string, object>> dataList;
    List<Dictionary<string, object>> data;
    List<Dictionary<string, object>> data2;

    int index;

    void Awake()
    {
        index = 0;
        dataList = CSVReader.Read("RPG - talkList");

        for (var i = 0; i < dataList.Count; i++)
        {
            print(dataList[i]["ID"] + " " +
                  dataList[i]["questName"]);

        }

        data = CSVReader.Read("RPG - talk");

        for (var i = 0; i < data.Count; i++)
        {
            //string[] scripts = ((string)data[i]["Dialogue"]).Split('$');

            print(data[i]["Index"] + " " +
                  data[i]["Name"] + " " +
                  data[i]["Dialogue"] + " " +
                  data[i]["EventNum"] + " " +
                  data[i]["SkipLine"]);

        }
        
        data2 = CSVReader.Read("RPG - talk2");

        for (var i = 0; i < data2.Count; i++)
        {
            print(data2[i]["Index"] + " " +
                  data2[i]["Name"] + " " +
                  data2[i]["Dialogue"] + " " +
                  data2[i]["EventNum"] + " " +
                  data2[i]["SkipLine"]);

        }
    }
    

    public string GetData(int _npcID)
    {
        string str = null;
        //아래 조건문은 대화창을 닫는것으로 바꾼다.
        if (data2.Count <= index) index = 0;
        if (_npcID==0)
        {
            str= data[index]["Dialogue"].ToString();
            index++;
        }
        if (_npcID==1)
        {
            str= data2[index]["Dialogue"].ToString();
        }

        

        return str;
    }
    
}