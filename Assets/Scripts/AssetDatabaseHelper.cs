

using System.Collections.Generic;
using UnityEngine;

    public static class AssetDatabaseHelper
    {
        public static List<Category> categories = new List<Category>();

        public static int GetUniqueId()
        {
            int i = 1;
            while (IdExists(i))
            {
                i++;
            }

            return i;
        }

        [ContextMenu("SetIds")]
        public static void SetIds()
        {
            foreach (var c in categories)
            {
                SetIds(c);
            }
        }

        private static void SetIds(Category c)
        {
            ResetIds(c);
            if (c == null)
                return;
            foreach (Item item in c.serializedItems)
            {
                item.Id = GetUniqueId();
            }
        }

        private static bool IdExists(int id)
        {
            foreach (var c in categories)
            {
                if (IdExists(c, id))
                    return true;
            }

            return false;
        }

        private static bool IdExists(Category c, int id)
        {
            if (c == null)
                return false;
            foreach (Item item_ in c.serializedItems)
            {
                if (item_.Id == id)
                {
                    return true;
                }
            }

            return false;
        }


        [ContextMenu("PrintIds")]
        public static void PrintIds()
        {
            foreach (var c in categories)
            {
                PrintIds(c);
            }
        }

        private static void PrintIds(Category c)
        {
            if (c == null)
                return;
            foreach (Item item in c.serializedItems)
            {
                Debug.Log(item.Id + " : " + item.Name);
            }
        }

        private static void ResetIds(Category c)
        {
            if (c == null)
                return;
            foreach (Item item in c.serializedItems)
            {
                item.Id = 0;
            }
        }

    }