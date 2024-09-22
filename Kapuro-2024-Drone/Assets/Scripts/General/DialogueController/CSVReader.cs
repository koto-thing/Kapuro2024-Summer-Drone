using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    [SerializeField] private List<String> csvFileNames; // CSVファイル名
    [SerializeField] private TextAsset csvFile; // CSVファイル
    [SerializeField] private int currentCSVIndex = 0;
    [SerializeField] private int lastRow;
    [SerializeField] private List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;
    
    public int LastRow { get { return lastRow; } }

    public async void LoadCSV()
    {
        csvDatas.Clear();
        csvFile = Resources.Load(csvFileNames[currentCSVIndex]) as TextAsset; // Resouces下のCSV読み込み
        using (var reader = new StringReader(csvFile.text))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            while(await csv.ReadAsync())
            {
                var record = csv.Parser.Record;
                csvDatas.Add(record);
            }
        }

        currentCSVIndex++;
        lastRow = csvDatas.Count;
    }
    
    // @brief CSVデータの取得
    // @param row 行, col 列
    public String GetCSVData(int row, int col)
    {
        return csvDatas[row][col];
    }
}