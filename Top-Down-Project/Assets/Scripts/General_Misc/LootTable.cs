using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class LootTable : MonoBehaviour
{
    /// <summary>
    /// inner class
    /// </summary>
    [Serializable]
    public class LootItem
    {
        //variables
        [HideInInspector] string id;
        [SerializeField] GameObject item;
        [SerializeField] float chance;        


        //properties
        public string ID { get {return id; } private set {id = value; } }
        public GameObject Item { get { return item; } private set { item = value; } }
        public float ChanceToDrop { get { return chance; } private set {chance = value; } }

    }

    public List<LootItem> table;
    float fullCDF;
    float[] CDF;

    private void Awake()
    {
        CDF = new float[table.Count];
        int index = 0;
        foreach (var item in table)
        {
            CDF[index] = item.ChanceToDrop;
            fullCDF += item.ChanceToDrop;
            index++;
        }
    }

    public GameObject Select(List<LootItem> items)
    {
        //sort the list
        //items.Sort();  
        int index = UnityEngine.Random.Range(0, items.Count);
        LootItem selected = items[index];
        return selected.Item;
    }

    public GameObject Select()
    {
        int index = System.Array.BinarySearch(CDF, UnityEngine.Random.Range(0, fullCDF) * fullCDF);

        if (index < 0)
        {
            index = ~index;
        }

        return table[index].Item;
    }

}
