using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TableController : MonoBehaviour
{
    public GameObject rowGrid;
    public Text tableTittle;
    public GameObject rowContainer;

    public class JsonTest
    {
        public string Title { get; set; }
        public List<string> ColumnHeaders { get; set; }
        public List<Dictionary<string, string>> Data { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {

        StreamReader file = File.OpenText(Application.dataPath+"/StreamingAssets/JsonChallenge.json");

        JsonTest json = JsonConvert.DeserializeObject<JsonTest>(file.ReadToEnd());

        tableTittle.text = json.Title;
        tableTittle.color = Color.black;
       GameObject gameObjectRow;
        gameObjectRow = (GameObject)Instantiate(rowGrid);
        gameObjectRow.transform.SetParent(rowContainer.transform);
        foreach (string header in json.ColumnHeaders)
        {
            GameObject row = new GameObject(header);
            row.transform.SetParent(gameObjectRow.transform);
            row.AddComponent<Text>().text = header;
            row.GetComponent<Text>().color = Color.black;
            row.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        }

        
        foreach (Dictionary<string, string> dictionary in json.Data)
        {
            gameObjectRow = (GameObject)Instantiate(rowGrid);
            gameObjectRow.transform.SetParent(rowContainer.transform);
            foreach (KeyValuePair<string, string> kvp in dictionary)
            {
                GameObject row = new GameObject(kvp.Value);
                row.transform.SetParent(gameObjectRow.transform);
                row.AddComponent<Text>().text = kvp.Value;
                row.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            }
        }

    }
}
