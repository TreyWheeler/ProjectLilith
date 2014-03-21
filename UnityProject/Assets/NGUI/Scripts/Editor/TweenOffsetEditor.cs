//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TweenOffset))]
public class TweenOffsetEditor : UITweenerEditor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Space(6f);
        NGUIEditorTools.SetLabelWidth(120f);

        TweenOffset tw = target as TweenOffset;
        GUI.changed = false;


        Vector2 from = EditorGUILayout.Vector2Field("From", tw.from);
        Vector2 to = EditorGUILayout.Vector2Field("To", tw.to);

        if (GUI.changed)
        {
            NGUIEditorTools.RegisterUndo("Tween Change", tw);
            tw.from = from;
            tw.to = to;
            UnityEditor.EditorUtility.SetDirty(tw);
        }

        DrawCommonProperties();
    }
}
