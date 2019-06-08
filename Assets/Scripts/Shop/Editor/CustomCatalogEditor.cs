
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.UI;

[CustomEditor(typeof(Category))]
public class CustomCatalogEditor : Editor
{
   public Item.ItemType types;
   public bool stop = false;
   private string path;
   public override void OnInspectorGUI()
   {
      Category category = (Category)target;

      EditorGUILayout.BeginHorizontal();
      path = TextField("Path" , path);
      if (GUILayout.Button("Load From Folder"))
      {
         Sprite[] textures = Resources.LoadAll<Sprite>(path);
         foreach (Sprite texture in textures)
         {
            category.serializedItems.Add(new Item(texture.name , texture));
         }
      }

      if (GUILayout.Button("Reset"))
      {
         if(category.serializedItems.Count > 0 && EditorUtility.DisplayDialog("", "Are you sure?" ,"Reset" , "Cancel"))
            category.serializedItems.Clear();
      }

      EditorGUILayout.EndHorizontal();
      GUILayout.Space(20);
     // category.type = TextField("Name",category.type);
      if (GUILayout.Button("Add Item"))
      {
         category.serializedItems.Add(new Item());
      }
      
      foreach (var item in category.serializedItems)
      {
         EditorGUILayout.BeginHorizontal();
         item.Name = TextField("Name",item.Name);
         item.Gold = EditorGUILayout.IntField("Price",item.Gold);
         item.Diamonds = EditorGUILayout.IntField("Gems",item.Diamonds);
         item.Score = EditorGUILayout.IntField("Score",item.Score);
         item.Icon = (Sprite)EditorGUILayout.ObjectField("Icon", item.Icon, typeof(Sprite), allowSceneObjects: true);
         //item.type = category.type;
         if (GUILayout.Button("X"))
         {
            category.serializedItems.Remove(item);
         }
         GUILayout.EndHorizontal();
       }
      EditorUtility.SetDirty(target);

   }


   public static string TextField(string label, string text)
   {
      var textDimensions = GUI.skin.label.CalcSize(new GUIContent(label));
      EditorGUIUtility.labelWidth = textDimensions.x;
      return EditorGUILayout.TextField(label, text);
   }

}
