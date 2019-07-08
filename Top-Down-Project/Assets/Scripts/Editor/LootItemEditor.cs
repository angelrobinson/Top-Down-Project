using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LootTable.LootItem))]
public class LootItemEditor : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //find properties from the serialized class via relative (Kind of like android development)
        var item = property.FindPropertyRelative("item");
        var chance = property.FindPropertyRelative("chance");
        
        //start the property area
        EditorGUI.BeginProperty(position, label, property);

        //change what label says and add a tooltip
        label.text = "Drop Chance";
        label.tooltip = "Pick your item and put the percent chance (as whole number) of item droping";
        
        //NOTE: this would usually put the label before the property fields and make it to where the label can NOT get keyboard focus

        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);


        //save current indent level of the property
        var indent = EditorGUI.indentLevel;

        //set the indent level to zero while you set up the property
        EditorGUI.indentLevel = 0;

        //calculate width for each item in the property
        float itemWidth = position.width - 60;
        float chanceWidth = position.width - itemWidth;

        var itemPos = new Rect(position.x, position.y, itemWidth, position.height);
        var chancePos = new Rect(position.x + itemWidth, position.y, 50, position.height);

        //properties to use with the main label
        //NOTE: Uncomment if you want to see the changes to the default label I made above
        //EditorGUI.PropertyField(itemPos, item, GUIContent.none);
        //EditorGUI.PropertyField(chancePos, chance, GUIContent.none);

        //properties with their own lables
        //NOTE: Comment this out if you use the base label instead of individual labels
        EditorGUIUtility.labelWidth = 40;
        EditorGUI.PropertyField(itemPos, item, new GUIContent("Item", "Pick the item that you want to add to loot table"));

        EditorGUIUtility.labelWidth = 25;
        EditorGUI.PropertyField(chancePos, chance, new GUIContent("  %  ", "Percent chance of item to drop"));

        //reset the indent level to the original indent
        EditorGUI.indentLevel = indent;

        //end propery
        EditorGUI.EndProperty();
        
    }
}
