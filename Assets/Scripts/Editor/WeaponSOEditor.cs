using System;
using UnityEditor;

[CustomEditor(typeof(WeaponSO))]
public class WeaponSOEditor : Editor
{
    private WeaponSO script;
    private bool foldout = true;

    private void OnEnable()
    {
        script = (WeaponSO)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var editor = CreateEditor(script.Ammo).serializedObject;

        EditorGUILayout.Separator();

        foldout = EditorGUILayout.InspectorTitlebar(foldout, script.Ammo, true);
        
        using var check = new EditorGUI.ChangeCheckScope();

        if (!foldout) return;

        editor.Update();
        DrawPropertiesExcluding(editor, "m_Script", "BulletDeviation");
        script.Ammo.BulletDeviation = EditorGUILayout.Slider("Bullet Deviation", script.Ammo.BulletDeviation, 0f, 1f);
        editor.ApplyModifiedProperties();
    }
}
