using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnBase))]
public class SpawnNodeEditor : Editor
{
    SerializedProperty nodeTypeProp;
    SerializedProperty spawnRadiusProp;

    private void OnEnable()
    {
        nodeTypeProp = serializedObject.FindProperty("nodeType");
        spawnRadiusProp = serializedObject.FindProperty("spawnRadius");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.HelpBox("스폰 노드 설정 창", MessageType.Info);
        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(nodeTypeProp, new GUIContent("노드 종류"));
        EditorGUILayout.Space(5);

        if (nodeTypeProp.enumValueIndex == (int)NodeType.EnemyUnit)
        {
            EditorGUILayout.LabelField("적 스폰 설정", EditorStyles.boldLabel);
            spawnRadiusProp.floatValue = EditorGUILayout.Slider("스폰 반경", spawnRadiusProp.floatValue, 1f, 20f);

            // 적 스폰 관련 추가 설정 가능
        }
        else if (nodeTypeProp.enumValueIndex == (int)NodeType.ItemLootBox)
        {
            EditorGUILayout.LabelField("파밍 상자 설정", EditorStyles.boldLabel);
            spawnRadiusProp.floatValue = EditorGUILayout.Slider("스폰 반경", spawnRadiusProp.floatValue, 0f, 5f);
            
            // 파밍 상자 관련 추가 설정 가능
        }

        EditorGUILayout.Space(10);

        if (GUILayout.Button("기즈모 크기 초기화"))
        {
            spawnRadiusProp.floatValue = 5f;
            Debug.Log("스폰 반경이 초기화되었습니다.");
        }

        // 수정한 값들을 원본 스크립트에 최종 반영
        serializedObject.ApplyModifiedProperties();

    }
}
