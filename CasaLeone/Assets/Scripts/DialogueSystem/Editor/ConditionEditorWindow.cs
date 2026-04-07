using DialogueSystem.DATA;
using UnityEngine;
using UnityEditor;

public class ConditionEditorWindow : EditorWindow
{
    private ConditionDatabase database;
    private Vector2 scroll;

    private const string PREF_KEY = "ConditionDatabaseGUID";

    [MenuItem("Dialogue/Condition Editor")]
    public static void OpenWindow()
    {
        GetWindow<ConditionEditorWindow>("Condition Editor");
    }

    private void OnEnable()
    {
        string guid = EditorPrefs.GetString(PREF_KEY, "");
        if (!string.IsNullOrEmpty(guid))
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            database = AssetDatabase.LoadAssetAtPath<ConditionDatabase>(path);
        }
    }

    private void OnDisable()
    {
        if (database != null)
        {
            string path = AssetDatabase.GetAssetPath(database);
            string guid = AssetDatabase.AssetPathToGUID(path);
            EditorPrefs.SetString(PREF_KEY, guid);
        }
        else
        {
            EditorPrefs.DeleteKey(PREF_KEY);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Condition Database", EditorStyles.boldLabel);

        database = (ConditionDatabase)EditorGUILayout.ObjectField(
            "Database",
            database,
            typeof(ConditionDatabase),
            false
        );

        if (database != null)
        {
            string path = AssetDatabase.GetAssetPath(database);
            string guid = AssetDatabase.AssetPathToGUID(path);
            EditorPrefs.SetString(PREF_KEY, guid);
        }

        if (database == null)
        {
            EditorGUILayout.HelpBox("Select or create a Condition Database.", MessageType.Info);
            return;
        }

        if (GUILayout.Button("Add Condition"))
        {
            DialogueCondition condition = CreateInstance<DialogueCondition>();
            condition.conditionID = "new_condition";
            condition.description = "Description";

            AssetDatabase.AddObjectToAsset(condition, database);
            database.conditions.Add(condition);

            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        GUILayout.Space(10);
        DrawConditions();
    }

    void DrawConditions()
    {
        scroll = EditorGUILayout.BeginScrollView(scroll);

        for (int i = 0; i < database.conditions.Count; i++)
        {
            DialogueCondition cond = database.conditions[i];

            EditorGUILayout.BeginVertical("box");
            cond.conditionID = EditorGUILayout.TextField("ID", cond.conditionID);
            cond.description = EditorGUILayout.TextField("Description", cond.description);

            if (GUILayout.Button("Remove"))
            {
                database.conditions.RemoveAt(i);
                DestroyImmediate(cond, true);

                EditorUtility.SetDirty(database);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                break;
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();
    }
}