using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSV_ex : MonoBehaviour
{
    List<Dictionary<string, object>> dataList;
    List<Dictionary<string, object>> data;
    List<Dictionary<string, object>> data2;
    void Awake()
    {
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
    

    public string GetData(int num)
    {
        string str = null;
        if (num==0)
        {
            str= data[4]["Dialogue"].ToString();
        }
        if (num==1)
        {
            str= data2[4]["Dialogue"].ToString();
        }

        

        return str;
    }
    
}