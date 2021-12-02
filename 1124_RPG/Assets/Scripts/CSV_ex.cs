using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSV_ex : MonoBehaviour
{
    List<Dictionary<string, object>> questlist;
    List<Dictionary<string, object>> talk01;
    List<Dictionary<string, object>> talk02;

    int index;

    void Awake()
    {
        index = 0;

        questlist = CSVReader.Read("quest");
        talk01 = CSVReader.Read("dialogue01");
        talk02 = CSVReader.Read("dialogue02");

        for (var i = 0; i < questlist.Count; i++)
        {
            print(questlist[i]["index"] + " " +
                  questlist[i]["npc"] + " " +
                  questlist[i]["questName"] + " " +
                  questlist[i]["quest"]);

        }

        for (var i = 0; i < talk01.Count; i++)
        {
            print(talk01[i]["Index"] + " " +
                  talk01[i]["Name"] + " " +
                  talk01[i]["Dialogue"]);
        }

        for (var i = 0; i < talk02.Count; i++)
        {
            print(talk02[i]["Index"] + " " +
                  talk02[i]["Name"] + " " +
                  talk02[i]["Dialogue"]);
        }
    }
    

    public string GetDialogue()
    {
        string str;
        str = talk01[index]["Dialogue"].ToString();
        index++;
        if (index >= talk01.Count)
        {
            index = 0;
            str = null;
        }
        


        

        return str;
    }

   
    
}