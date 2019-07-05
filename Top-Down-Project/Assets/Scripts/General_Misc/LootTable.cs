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
    float[] CDF;

    /// <summary>
    /// Select and Return a Random loot from the loot table. 
    /// </summary>
    /// <param name="items">List from loot table</param>
    /// <returns></returns>
    public GameObject Select(List<LootItem> items)
    {

        //create array of just the weights
        CDF = new float[items.Count];
        int index = 0;
        foreach (var item in items)
        {
            if (index == 0)
            {
                CDF[index] = item.ChanceToDrop;
            }
            else
            {
                CDF[index] = item.ChanceToDrop + CDF[index - 1];
            }

            
            index++;
        }

        //find random index via binary search
        index = System.Array.BinarySearch(CDF, UnityEngine.Random.Range(0, CDF[CDF.Length-1]));

        //if didn't find identical item, reverse the binary ticks
        if (index < 0)
        {
            index = ~index;
        }

        //return index number
        return items[index].Item;
    }

}
