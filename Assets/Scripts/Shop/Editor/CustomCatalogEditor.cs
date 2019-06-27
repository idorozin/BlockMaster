
using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Experimental.UIElements;
using UnityEngine.Assertions.Comparers;
using UnityEngine.UI;

[CustomEditor(typeof(Category))]
public class CustomCatalogEditor : Editor
{
   public Item.ItemType types;
   public bool stop = false;
   public bool animation;
   public bool trai;
   private string path;

   private GameObject passer;
   public override void OnInspectorGUI()
   {
      Category category = (Category)target;

      foreach (var v in category.serializedItems)
      {
         if (v.Animator != null)
         {
            animation = true;
            break;
         }
      }

      EditorGUILayout.BeginHorizontal();
      path = TextField("Path" , path);
      if (GUILayout.Button("Load From Folder") && category.serializedItems.Count > 0 && EditorUtility.DisplayDialog("", "Are you sure?" ,"Load" , "Cancel"))
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
      EditorGUILayout.BeginHorizontal();
      EditorGUIUtility.LookLikeControls(50.0f);
      passer = (GameObject)EditorGUILayout.ObjectField("rect", passer, typeof(GameObject) , allowSceneObjects:true);
      if (GUILayout.Button("Update Values"))
      {
         RectTransform rect = passer.GetComponent<RectTransform>();
         category.x = rect.position.x;
         category.y = rect.position.y;
         category.width = rect.sizeDelta.x;
         category.height = rect.sizeDelta.y;
         category.rotation = rect.eulerAngles.z;
      }
      animation = EditorGUILayout.Toggle("animation", animation);
      category.type = (Item.ItemType)EditorGUILayout.EnumPopup("Type", category.type);
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
         item.type = category.type;
         if (animation)
            item.Animator = (RuntimeAnimatorController) EditorGUILayout.ObjectField("Controller", item.Animator, 
               typeof(RuntimeAnimatorController), allowSceneObjects: true);
         else
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


   private static string TextField(string label, string text)
   {
      var textDimensions = GUI.skin.label.CalcSize(new GUIContent(label));
      EditorGUIUtility.labelWidth = textDimensions.x;
      return EditorGUILayout.TextField(label, text);
   }

}
