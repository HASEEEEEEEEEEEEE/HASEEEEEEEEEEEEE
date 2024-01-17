using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Map : EditorWindow
{
    private GameObject _parent;
    // = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/VrMap.prefab");
    private GameObject _prefab;
    private GameObject _prefab_corner;
    private GameObject _prefab_side;
    private GameObject _prefab_1;
    private GameObject _prefab_1_corner;
    private GameObject _prefab_1_side;
    private GameObject _mapCanvas;
    private Text _mapText;
    private GameObject _warp;
    private int x = 1;
    private int z = 1;

    [MenuItem("VRMap/VrMap", false, 1)]
    private static void ShowWindow()
    {
        GetWindow<Map>();
        //VrMap window = GetWindow<VrMap>();
        //window.titleContent = new GUIContent("VRMap Window");
    }

    private void OnEnable()
    {
        _parent = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/VrMapParent.prefab");
        _prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/Room.prefab");
        _prefab_corner = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/Room_Corner.prefab");
        _prefab_side = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/Room_Side.prefab");
        _prefab_1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/Room1.prefab");
        _prefab_1_corner = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/Room1_Corner.prefab");
        _prefab_1_side = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/Room1_Side.prefab");
        _mapCanvas = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/MapTextCanvas.prefab");
        _mapText = AssetDatabase.LoadAssetAtPath<Text>("Assets/VrMapCreate/Prefabs/MapText.prefab");
        _warp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/VrMapCreate/Prefabs/Warp.prefab");
    }

    private void OnGUI()
    {
        //ScriptableObject target = this;
        //SerializedObject so = new SerializedObject(target);
        //SerializedProperty stringsProperty = so.FindProperty("x");
        //EditorGUILayout.PropertyField(stringsProperty, true);
        //so.ApplyModifiedProperties();

        //SerializedObject so2 = new SerializedObject(target);
        //SerializedProperty stringsProperty2 = so2.FindProperty("z");
        //EditorGUILayout.PropertyField(stringsProperty2, true);
        //so2.ApplyModifiedProperties();
        GUILayout.Label("VRMap生成");
        try
        {
            //_parent = EditorGUILayout.ObjectField("Parent", _parent, typeof(GameObject), true) as GameObject;
            //_prefab = EditorGUILayout.ObjectField("Prefab", _prefab, typeof(GameObject), true) as GameObject;

            //GUILayout.Label("X : ", EditorStyles.boldLabel);
            x = int.Parse(EditorGUILayout.TextField("x", x.ToString()));
            z = int.Parse(EditorGUILayout.TextField("z", z.ToString()));

            //GUILayout.Label("", EditorStyles.boldLabel);
            if (GUILayout.Button("MapCreate")) Create();
        }
        catch (System.FormatException) { }

        GUILayout.Label("");
        GUILayout.Label("ワープ生成");
        if (GUILayout.Button("WarpCreate")) WarpCreate();

        //GUILayout.Label("説明文");
        //if (GUILayout.Button("ここをクリック"))
        //{
        //    Debug.Log(x);
        //}
    }

    private void Create()
    {
        int i, j;

        if (x == 0 || z == 0) return;

        var parent_obj = Instantiate(_parent, new Vector3(0, 0, 0), Quaternion.identity);
        parent_obj.name = "VrMap";

        var mapCanvas_obj = Instantiate(_mapCanvas, new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0));
        mapCanvas_obj.transform.parent = parent_obj.transform;
        mapCanvas_obj.name = "MapCanvas";

        for (j = 0; j < z; j++)
        {
            for (i = 0; i < x; i++)
            {
                var mapText_obj = Instantiate(_mapText, new Vector3(15f * i, -2.0f, 15f * j), Quaternion.Euler(90, 0, 0));
                mapText_obj.transform.parent=mapCanvas_obj.transform;
                mapText_obj.name = "MapText";
                mapText_obj.text = "";

                for (int l = (z - 1); l > -1; l--)
                {
                    for (int k = 0; k < x; k++)
                    {
                        if (i == k && j == l) mapText_obj.text += "■";
                        else mapText_obj.text += "□";
                    }
                    mapText_obj.text += "\n";
                }
            }
        }




        if (x == 1 && z == 1)
        {
            var obj = Instantiate(_prefab_1, new Vector3(0, 0, 0), Quaternion.identity);
            obj.transform.parent = parent_obj.transform;
            obj.name = "Room";
        }
        else if (x == 1)
        {
            for (j = 0; j < z; j++)
            {
                if (j == 0)
                {
                    var obj = Instantiate(_prefab_1_corner, new Vector3(0, 0, 15f * j), Quaternion.Euler(0, -90, 0));
                    obj.transform.parent = parent_obj.transform;
                    obj.name = "Room0" + "_" + j;
                }
                else if (j == (z - 1))
                {
                    var obj = Instantiate(_prefab_1_corner, new Vector3(0, 0, 15f * j), Quaternion.Euler(0, 90, 0));
                    obj.transform.parent = parent_obj.transform;
                    obj.name = "Room0" + "_" + j;
                }
                else
                {
                    var obj = Instantiate(_prefab_1_side, new Vector3(0, 0, 15f * j), Quaternion.Euler(0, 90, 0));
                    obj.transform.parent = parent_obj.transform;
                    obj.name = "Room0" + "_" + j;
                }
            }
        }
        else if (z == 1)
        {
            for (i = 0; i < x; i++)
            {
                if (i == 0)
                {
                    var obj = Instantiate(_prefab_1_corner, new Vector3(15f * i, 0, 0), Quaternion.identity);
                    obj.transform.parent = parent_obj.transform;
                    obj.name = "Room" + i + "_0";
                }
                else if (i == (x - 1))
                {
                    var obj = Instantiate(_prefab_1_corner, new Vector3(15f * i, 0, 0), Quaternion.Euler(0, 180, 0));
                    obj.transform.parent = parent_obj.transform;
                    obj.name = "Room" + i + "_0";
                }
                else
                {
                    var obj = Instantiate(_prefab_1_side, new Vector3(15f * i, 0, 0), Quaternion.identity);
                    obj.transform.parent = parent_obj.transform;
                    obj.name = "Room" + i + "_0";
                }
            }
        }
        else
        {
            for (i = 0; i < x; i++)
            {
                for (j = 0; j < z; j++)
                {
                    //_prefab_corner
                    if (i == 0 && j == 0)
                    {
                        var obj = Instantiate(_prefab_corner, new Vector3(15f * i, 0, 15f * j), Quaternion.identity);
                        obj.transform.parent = parent_obj.transform;
                        obj.name = "Room" + i + "_" + j;
                    }
                    else if (i == 0 && j == (z - 1))
                    {
                        var obj = Instantiate(_prefab_corner, new Vector3(15f * i, 0, 15f * j), Quaternion.Euler(0, 90, 0));
                        obj.transform.parent = parent_obj.transform;
                        obj.name = "Room" + i + "_" + j;
                    }
                    else if (i == (x - 1) && j == 0)
                    {
                        var obj = Instantiate(_prefab_corner, new Vector3(15f * i, 0, 15f * j), Quaternion.Euler(0, -90, 0));
                        obj.transform.parent = parent_obj.transform;
                        obj.name = "Room" + i + "_" + j;
                    }
                    else if (i == (x - 1) && j == (z - 1))
                    {
                        var obj = Instantiate(_prefab_corner, new Vector3(15f * i, 0, 15f * j), Quaternion.Euler(0, 180, 0));
                        obj.transform.parent = parent_obj.transform;
                        obj.name = "Room" + i + "_" + j;
                    }
                    //_prefab_side
                    else if (i == 0)
                    {
                        var obj = Instantiate(_prefab_side, new Vector3(15f * i, 0, 15f * j), Quaternion.identity);
                        obj.transform.parent = parent_obj.transform;
                        obj.name = "Room" + i + "_" + j;
                    }
                    else if (i == (x - 1))
                    {
                        var obj = Instantiate(_prefab_side, new Vector3(15f * i, 0, 15f * j), Quaternion.Euler(0, 180, 0));
                        obj.transform.parent = parent_obj.transform;
                        obj.name = "Room" + i + "_" + j;
                    }
                    else if (j == 0)
                    {
                        var obj = Instantiate(_prefab_side, new Vector3(15f * i, 0, 15f * j), Quaternion.Euler(0, -90, 0));
                        obj.transform.parent = parent_obj.transform;
                        obj.name = "Room" + i + "_" + j;
                    }
                    else if (j == (z - 1))
                    {
                        var obj = Instantiate(_prefab_side, new Vector3(15f * i, 0, 15f * j), Quaternion.Euler(0, 90, 0));
                        obj.transform.parent = parent_obj.transform;
                        obj.name = "Room" + i + "_" + j;
                    }
                    //_prefab
                    else
                    {
                        var obj = Instantiate(_prefab, new Vector3(15f * i, 0, 15f * j), Quaternion.identity);
                        obj.transform.parent = parent_obj.transform;
                        obj.name = "Room" + i + "_" + j;
                    }
                }
            }
        }
    }
    private void WarpCreate()
    {
        var warp_obj = Instantiate(_warp, new Vector3(0, 0.4f, 0), Quaternion.identity);
        warp_obj.name = "Warp";
    }
}