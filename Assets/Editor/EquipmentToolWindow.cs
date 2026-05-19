using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;

public class EquipmentToolWindow : EditorWindow
{
    string newWeaponName = "New Weapon";

    [MenuItem("Tools/Equipment Tool")]
    public static void ShowWindow()
    {
        GetWindow<EquipmentToolWindow>("Equipment Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("장비 데이터 생성기", EditorStyles.boldLabel);

        newWeaponName = EditorGUILayout.TextField("무기 이름", newWeaponName);

        if (GUILayout.Button("새 무기 데이터 생성"))
        {
            CreateWeaponSO();
        }

        GUILayout.Space(20);
        GUILayout.Label("현재 프로젝트의 무기 목록", EditorStyles.boldLabel);

        string[] guids = AssetDatabase.FindAssets("t:WeaponSO");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            WeaponSO weapon = AssetDatabase.LoadAssetAtPath<WeaponSO>(path);
            if (GUILayout.Button(weapon.WeaponName))
            {
                Selection.activeObject = weapon;
            }
        }
    }

    private void CreateWeaponSO()
    {
        WeaponSO newWeapon = ScriptableObject.CreateInstance<WeaponSO>();
        newWeapon.WeaponName = newWeaponName;

        AssetDatabase.CreateAsset(newWeapon, $"Assets/Resources/{newWeaponName}.asset");
        AssetDatabase.SaveAssets();

        Debug.Log($"새 무기 데이터 생성: {newWeaponName}");
        Selection.activeObject = newWeapon;
    }
}
