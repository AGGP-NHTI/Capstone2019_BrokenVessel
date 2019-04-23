using System.Collections;
using System.Collections.Generic;
using System.IO;
using BrokenVessel.RoomSystem;
using UnityEditor;
using UnityEngine;

namespace BrokenVessel.Utility
{
    [CustomEditor(typeof(RoomManager))]
    public class RoomManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RoomManager scr = (RoomManager)target;
            if (GUILayout.Button("Load from File"))
            {
                string path = EditorUtility.OpenFilePanel("Load from File", $"{Application.dataPath}/Resources/Rooms/", "txt");
                if (path.Length != 0)
                {
                    path = Path.GetFileName(path);
                    RoomManager.LoadFromFile(path.Remove(path.Length - 4));
                }
            }
            /*if (GUILayout.Button("Clear Rooms"))
            {
                RoomManager.Rooms.Clear();
            }*/
        }
    }
}