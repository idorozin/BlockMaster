
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.UI;

[CustomEditor(typeof(ItemsShopCatalog))]
public class CustomCatalogEditor : Editor
{
   public Item.ItemType types;
   public bool stop = false;
   private string path;
   public override void OnInspectorGUI()
   {
      ItemsShopCatalog itemsShop = (ItemsShopCatalog)target;
      EditorGUILayout.BeginHorizontal();
      path = TextField("Path" , path);
      if (GUILayout.Button("Load From Folder"))
      {
         Sprite[] textures = Resources.LoadAll<Sprite>(path);
         itemsShop.serializedItems.Add(new ListWraper());
         foreach (Sprite texture in textures)
         {
            itemsShop.serializedItems[itemsShop.serializedItems.Count-1].list.Add(new Item(texture.name , texture));
         }
      }

      if (GUILayout.Button("Reset"))
      {
         if(itemsShop.serializedItems.Count > 0 && EditorUtility.DisplayDialog("", "Are you sure?" ,"Reset" , "Cancel"))
            itemsShop.serializedItems.Clear();
      }

      if (GUILayout.Button("Add Category"))
      {
         itemsShop.serializedItems.Add(new ListWraper());
      }
      EditorGUILayout.EndHorizontal();
      GUILayout.Space(20);
      foreach (var category in itemsShop.serializedItems.ToList())
      {
         EditorGUILayout.BeginHorizontal();
         category.type = TextField("Name",category.type);
         if (GUILayout.Button("Add Item"))
         {
            category.list.Add(new Item());
         }
         if (GUILayout.Button("X"))
         {
            itemsShop.serializedItems.Remove(category);
         }
         GUILayout.EndHorizontal();
         foreach (var item in category.list.ToList())
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
               category.list.Remove(item);
            }
            GUILayout.EndHorizontal();
         }

         EditorUtility.SetDirty(target);
         GUILayout.Space(50);
      }
   }

   public static string TextField(string label, string text)
   {
      var textDimensions = GUI.skin.label.CalcSize(new GUIContent(label));
      EditorGUIUtility.labelWidth = textDimensions.x;
      return EditorGUILayout.TextField(label, text);
   }

}
