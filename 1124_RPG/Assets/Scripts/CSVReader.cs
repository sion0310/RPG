using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVReader
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var Dialoguelist = new List<Dictionary<string, object>>();
        TextAsset csvData = Resources.Load(file) as TextAsset;

        //data.text를 행이 바뀔때마다 잘라서 lines에 넣는다
        var data = Regex.Split(csvData.text, LINE_SPLIT_RE);

        //lines의 길이가 1이하이면 그냥 리스트로
        if (data.Length <= 1) return Dialoguelist;

        //lines 첫 행의 칸이 바뀔때마다 잘라서 header에 넣는다
        var row = Regex.Split(data[0], SPLIT_RE);

        //header를 제외한 나머지 값들 정리
        for (var i = 1; i < data.Length; i++)
        {
            //lines 1행부터 칸이 바뀔때마다 잘라서 values에 넣는다
            var values = Regex.Split(data[i], SPLIT_RE);

            //각행의 길이가 0이거나 값이 없을때는 다음단계로
            if (values.Length == 0 || values[0] == "") continue;
            
            var entry = new Dictionary<string, object>();

            //각 값들을 항목별로 넣어주는과정
            for (var j = 0; j < row.Length && j < values.Length; j++)
            {
                
                string value = values[j];
                //대충 \" 부터 \" 까지 덩어리로 잘라서 넣는다는거 같긴함(replace는 문자를 바꿔주는것)
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;//각 값들을 자료형에 맞게 바꿔주는 과정
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[row[j]] = finalvalue;
            }

            //분류가 끝난애들 리스트에 넣어주는것
            Dialoguelist.Add(entry);
        }
        return Dialoguelist;
    }
}