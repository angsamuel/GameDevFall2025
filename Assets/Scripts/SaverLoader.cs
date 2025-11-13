using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

using System.IO;

public static class SaverLoader
{
    public static string fileName = "default.txt";
    private static string saveFolder = "saves";
    private static char saveToken = '=';
    private static Dictionary<string, string> dataDict;
    private static string directoryPath = "";


    static SaverLoader(){
        directoryPath = Directory.GetParent(Application.dataPath).FullName;
        directoryPath = Path.Combine(directoryPath, saveFolder);
        if(!Directory.Exists(directoryPath)){
            Directory.CreateDirectory(directoryPath);
        }
        Debug.Log($"Save directory set to {directoryPath}");
        dataDict = new Dictionary<string, string>();
        LoadFromFile();
    }



    private static void SaveToFile(){
        string filePath = Path.Combine(directoryPath, fileName);
        try{
            using(StreamWriter writer = new StreamWriter(filePath, false)){
                foreach(var pair in dataDict){
                    writer.WriteLine($"{pair.Key}{saveToken}{pair.Value}");
                }
                Debug.Log($"Saved to file: {fileName}");
            }
        }catch{
            Debug.LogError("Couldn't save to the file for some reason. Teehee!");
        }
    }

    private static void LoadFromFile(){
        string filePath = Path.Combine(directoryPath, fileName);
        try{
            if(File.Exists(filePath)){
                string[] lines = File.ReadAllLines(filePath);
                foreach(string line in lines){
                    string[] parts = line.Split(saveToken);
                    if(parts.Length == 2){
                        dataDict[parts[0]] = parts[1];
                    }
                }
            }
        }catch{
            Debug.LogError("Uh oh! Couldn't load from file! Teehee! >:3");
        }
    }

    public static void Flush(){
        SaveToFile();
    }


    public static void SaveString(string key, string value)
    {
        dataDict[key] = value;
    }
    public static string LoadString(string key, string defaultValue = "")
    {
        if (!dataDict.ContainsKey(key))
        {
            return defaultValue;
        }
        return dataDict[key];
    }

    public static void SaveInt(string key, int value)
    {
        dataDict[key] = value.ToString();
    }

    public static int LoadInt(string key, int defaultValue = 0)
    {
        if (!dataDict.ContainsKey(key))
        {
            return defaultValue;
        }
        string value = dataDict[key];

        if (int.TryParse(value, out int resultInt))
        {
            return resultInt;
        }
        return defaultValue;
    }

    public static void SaveVector3(string key, Vector3 vector){
        string vectorString = $"{vector.x},{vector.y},{vector.z}";
        dataDict[key] = vectorString;
    }

    public static Vector3 LoadVector3(string key, Vector3 defaultValue){
        if(dataDict.TryGetValue(key,out string value)){
            string[] components = value.Split(',');

            if(
                float.TryParse(components[0],out float x)
                && float.TryParse(components[1], out float y)
                && float.TryParse(components[2], out float z)
            )
            {
                return new Vector3(x, y, z);
            }
        }
        Debug.LogWarning("Returning default vector value on load vector3");
        return defaultValue;
    }


}
