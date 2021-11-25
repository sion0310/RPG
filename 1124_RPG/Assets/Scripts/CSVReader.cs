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
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        //data.text를 행이 바뀔때마다 잘라서 lines에 넣는다
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        //lines의 길이가 1이하이면 그냥 리스트로
        if (lines.Length <= 1) return list;

        //lines 첫 행의 칸이 바뀔때마다 잘라서 header에 넣는다
        var header = Regex.Split(lines[0], SPLIT_RE);

        //header를 제외한 나머지 값들 정리
        for (var i = 1; i < lines.Length; i++)
        {
            //lines 1행부터 칸이 바뀔때마다 잘라서 values에 넣는다
            var values = Regex.Split(lines[i], SPLIT_RE);

            //각행의 길이가 0이거나 값이 없을때는 다음단계로
            if (values.Length == 0 || values[0] == "") continue;
            
            var entry = new Dictionary<string, object>();

            //각 값들을 항목별로 넣어주는과정
            for (var j = 0; j < header.Length && j < values.Length; j++)
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
                entry[header[j]] = finalvalue;
            }

            //분류가 끝난애들 리스트에 넣어주는것
            list.Add(entry);
        }
        return list;
    }
}