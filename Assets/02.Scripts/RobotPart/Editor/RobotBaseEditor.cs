

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(RobotBase), true)]
public class RobotBaseEditor :Editor
{
    RobotBase robotBase;

    private GUIStyle SubTitleGUIStyle;
    private GUIStyle SubSubTitleGUIStyle;

    private SerializedObject robotBaseSO;
    private SerializedProperty robotUniqueIdSP;

    ReorderableList attacehdRobotPartsList;
    SerializedProperty attacehdRobotPartsSP;

    SerializedProperty waitingTimeSP;
    private void Awake()
    {


    }

    private void OnEnable()
    {
        this.SubTitleGUIStyle = new GUIStyle();
        this.SubTitleGUIStyle.fontStyle = FontStyle.Bold;
        this.SubTitleGUIStyle.fontSize = 15;
        this.SubTitleGUIStyle.normal.textColor = Color.white;
        this.SubTitleGUIStyle.hover.textColor = Color.white;

        this.SubSubTitleGUIStyle = new GUIStyle();
        this.SubSubTitleGUIStyle.fontStyle = FontStyle.Normal;
        this.SubSubTitleGUIStyle.fontSize = 12;
        this.SubSubTitleGUIStyle.normal.textColor = Color.white;
        this.SubSubTitleGUIStyle.hover.textColor = Color.white;

        robotBase = target as RobotBase;

        robotBaseSO = new SerializedObject(robotBase);
        robotUniqueIdSP = robotBaseSO.FindProperty("robotUniqueId");
        attacehdRobotPartsSP = robotBaseSO.FindProperty("AttacehdRobotParts");

        waitingTimeSP = robotBaseSO.FindProperty("WaitingTime");
    }
    /*
    TileBaseType selectedTileBaseType;

    private void ShowCreateTileBaseButton(TileObject tileObject)
    {
        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("Create TileBase", EditorStyles.boldLabel);
        selectedTileBaseType = (TileBaseType)EditorGUILayout.EnumPopup("Select Created TileBase Type", selectedTileBaseType);

        if (GUILayout.Button("Create TileBase"))
        {
            Type t = null;

            switch (selectedTileBaseType)
            {
                case TileBaseType.Tile:
                    t = typeof(UnityEngine.Tilemaps.Tile);
                    break;
                case TileBaseType.AnimatedTile:
                    t = typeof(UnityEngine.Tilemaps.AnimatedTile);
                    break;
                case TileBaseType.PipelineTile:
                    t = typeof(UnityEngine.Tilemaps.PipelineTile);
                    break;
                case TileBaseType.RandomTile:
                    t = typeof(UnityEngine.Tilemaps.RandomTile);
                    break;
                case TileBaseType.RuleTile:
                    t = typeof(RuleTile);
                    break;
                case TileBaseType.TerrainTile:
                    t = typeof(UnityEngine.Tilemaps.TerrainTile);
                    break;
                case TileBaseType.WeightedRandomTile:
                    t = typeof(UnityEngine.Tilemaps.WeightedRandomTile);
                    break;
            }

            string assetPath = GetTileBaseAssetPath(tileObject);
            TileBase newTilebase = AssetDatabase.LoadAssetAtPath<TileBase>(assetPath);
            if (newTilebase != null)
            {
                tileObject.TileBaseAsset = newTilebase;
            }
            else
            {
                Directory.CreateDirectory(GetTileBaseAssetAbsolutePath(tileObject));

                newTilebase = ScriptableObject.CreateInstance(t) as TileBase;
                if (newTilebase != null)
                {
                    AssetDatabase.CreateAsset(newTilebase, assetPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    tileObject.TileBaseAsset = newTilebase;

                    //Focus on newTilebase

                    //Init TileObject
                    Tile newTile = newTilebase as Tile;
                    if (newTile != null)
                    {
                        newTile.colliderType = Tile.ColliderType.None;
                    }
                }
            }



        }
    }

    private void SetTileBasePivot(TileBase tileBase)
    {
        Sprite[] sprites = null;


        if (tileBase is AnimatedTile)
        {
            sprites = (tileBase as AnimatedTile).m_AnimatedSprites;
        }
        else if (tileBase is PipelineTile)
        {
            sprites = (tileBase as PipelineTile).m_Sprites;
        }
        else if (tileBase is RandomTile)
        {
            sprites = (tileBase as RandomTile).m_Sprites;
        }
        else if (tileBase is TerrainTile)
        {
            sprites = (tileBase as TerrainTile).m_Sprites;
        }
        else if (tileBase is WeightedRandomTile)
        {
            WeightedRandomTile weightedRandomTile = (tileBase as WeightedRandomTile);
            sprites = new Sprite[weightedRandomTile.Sprites.Length];

            for (int i = 0; i < weightedRandomTile.Sprites.Length; i++)
            {
                sprites[i] = weightedRandomTile.Sprites[i].Sprite;
            }
        }
        else if (tileBase is Tile)
        {
            sprites = new Sprite[1] { (tileBase as Tile).sprite };
        }

        if (sprites != null)
        {
            List<TextureImporter> prevTextureImporterList = null;




            foreach (var sprite in sprites)
            {
                if (sprite == null)
                    continue;

                string path = AssetDatabase.GetAssetPath(sprite);
                TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

                if (textureImporter == null)
                {
                    Debug.LogError("textureImporter is null");
                    return;
                }
                if (prevTextureImporterList != null && prevTextureImporterList.Contains(textureImporter))
                {
                    Debug.Log("Already set textureImporter");
                    continue;
                }

                SpriteMetaData[] spriteMetaData = textureImporter.spritesheet;
                for (int i = 0; i < textureImporter.spritesheet.Length; i++)
                {
                    spriteMetaData[i].alignment = 9; //Set Pivot Mode Custom
                    float pivotX = 1f / ((spriteMetaData[i].rect.width / World.PixelsPerUnit) * 2f);
                    float pivotY = 1f / ((spriteMetaData[i].rect.height / World.PixelsPerUnit) * 2f);
                    spriteMetaData[i].pivot = new Vector2(pivotX, pivotY);
                    Debug.Log(spriteMetaData[i].name + "  " + pivotX.ToString() + " " + pivotY.ToString());
                }

                textureImporter.spritesheet = spriteMetaData;
                EditorUtility.SetDirty(textureImporter);
                textureImporter.SaveAndReimport();

                Debug.Log("improt");
                //AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

                if (prevTextureImporterList == null)
                    prevTextureImporterList = new List<TextureImporter>();

                prevTextureImporterList.Add(textureImporter);
            }
        }
        else
        {
            Debug.LogError("sprites is null");
        }



    }
       
    */


    public override void OnInspectorGUI()
    {

        if (robotBase != null)
        {

            robotBaseSO.Update();

            EditorGUILayout.LabelField("Robot UniqueId : ", robotUniqueIdSP.stringValue, SubTitleGUIStyle);


            EditorGUILayout.Space(10);

            ShowAttachedRobotPartListInEditor();

            EditorGUILayout.Space(10);

            ShowRobotGlobalVariableEditor();

            EditorGUILayout.Space(10);

            ShowCustomBlockParameterVariables();

            EditorGUILayout.Space(10);

            ShowWaitBlockEditor();


            robotBaseSO.ApplyModifiedProperties();
        }
    }

    private void ShowWaitBlockEditor()
    {
        EditorGUILayout.LabelField("Wait Block", SubTitleGUIStyle);

        EditorGUI.indentLevel += 2;

        waitingTimeSP.floatValue = EditorGUILayout.FloatField("Waiting Time", waitingTimeSP.floatValue);
        string waitingBlockType;
        if (robotBase.WaitingBlock != null)
        {
            waitingBlockType = robotBase.WaitingBlock.GetType().Name;
        }
        else
        {
            waitingBlockType = System.String.Empty;
        }
        EditorGUILayout.LabelField("Waiting Block", waitingBlockType);

        EditorGUILayout.LabelField("BlockCallStack", SubSubTitleGUIStyle);
        List<FlowBlock> robotGlobalVariableKeyValuePair = robotBase.BlockCallStackList;

        EditorGUI.indentLevel += 5;
        if (robotGlobalVariableKeyValuePair != null)
        {
            GUIStyle gUIStyle = new GUIStyle();
            gUIStyle.alignment = TextAnchor.MiddleCenter;

            EditorGUILayout.LabelField("Top", gUIStyle);
            for (int i = robotGlobalVariableKeyValuePair.Count - 1; i >= 0; i++)
            {
                EditorGUILayout.LabelField(robotGlobalVariableKeyValuePair[i].GetType().Name, gUIStyle);
            }

            EditorGUILayout.LabelField("Bottom", gUIStyle);
        }
        EditorGUI.indentLevel -= 5;

        EditorGUI.indentLevel -= 2;
    }

    private void ShowCustomBlockParameterVariables()
    {
        EditorGUILayout.LabelField("CustomBlock Parameter Variables", SubTitleGUIStyle);

        List<KeyValuePair<DefinitionCustomBlock, Dictionary<string, string>>> customBlockParameterVariablesKeyValuePair = robotBase.CustomBlockParameterVariablesKeyValuePair;

        if (customBlockParameterVariablesKeyValuePair != null)
        {
            foreach (KeyValuePair<DefinitionCustomBlock, Dictionary<string, string>> pPair in customBlockParameterVariablesKeyValuePair)
            {
                EditorGUI.indentLevel += 2;
                EditorGUILayout.LabelField(pPair.Key.CustomBlockName, SubSubTitleGUIStyle);

                EditorGUI.indentLevel += 2;
                foreach (KeyValuePair<string, string> vPair in pPair.Value)
                {
                    EditorGUILayout.LabelField("Key : " + vPair.Key, "Value : " + vPair.Value);

                }
                EditorGUI.indentLevel -= 2;

                EditorGUI.indentLevel -= 2;
            }
        }
    }

    private void ShowRobotGlobalVariableEditor()
    {
        EditorGUILayout.LabelField("Robot GlobalVariable List", SubTitleGUIStyle);

        List<KeyValuePair<string, string>> robotGlobalVariableKeyValuePair = robotBase.RobotGlobalVariableKeyValuePair;

        if (robotGlobalVariableKeyValuePair != null)
        {
            EditorGUI.indentLevel += 2;
            foreach (KeyValuePair<string, string> pair in robotGlobalVariableKeyValuePair)
            {
                EditorGUILayout.LabelField("Key : " + pair.Key, "Value : " + pair.Value);
            }
            EditorGUI.indentLevel -= 2;
        }
    }

    private void ShowAttachedRobotPartListInEditor()
    {
        EditorGUILayout.LabelField("Attached RobotPart List", SubTitleGUIStyle);

        EditorGUI.indentLevel += 2;
        for (int i = 0; i < attacehdRobotPartsSP.arraySize; i++)
        {
            SerializedProperty sp = attacehdRobotPartsSP.GetArrayElementAtIndex(i);
            if (sp != null && sp.objectReferenceValue == null)
            {
                EditorGUILayout.PropertyField(sp, new GUIContent(sp.objectReferenceValue.GetType().Name));
            }
        }
        EditorGUI.indentLevel -= 2;
    }
}


#endif