
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(ItemsShop))]
public class CustomCatalogEditor : Editor
{
   public override void OnInspectorGUI()
   {
      ItemsShop itemsShop = (ItemsShop)target;
      EditorGUILayout.BeginHorizontal();
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
         category.name = EditorGUILayout.TextField(category.name);
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
            item.Sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", item.Sprite, typeof(Sprite), allowSceneObjects: true);   
            if (GUILayout.Button("X"))
            {
               category.list.Remove(item);
            }
            GUILayout.EndHorizontal();
         }
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
