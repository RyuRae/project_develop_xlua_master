using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace UISceneModule
{
    /**********************************************
    * Copyright (C) 2019 讯飞幻境（北京）科技有限公司
    * 模块名: UIComponentEditor.cs
    * 创建者：RyuRae
    * 修改者列表：
    * 创建日期：
    * 功能描述：
    ***********************************************/
    [CustomEditor(typeof(UIComponent), true)]
    [CanEditMultipleObjects]
    public class UIComponentEditor : Editor
    {
        protected Object monoScript;
        protected GUIStyle paddingStyle;

        protected virtual void OnEnable()
        {
            paddingStyle = new GUIStyle();
            paddingStyle.padding = new RectOffset(15, 0, 0, 0);
            this.monoScript = MonoScript.FromMonoBehaviour(target as MonoBehaviour);
        }

        protected void DrawMonoScript()
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", this.monoScript, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();
        }

        public override void OnInspectorGUI()
        {
            this.DrawMonoScript();

            EditorGUILayout.Space();
            UIComponent com = target as UIComponent;

            com.showFields = OneFlyGUITools.BeginFoldOut("Public Fields", com.showFields);
            if (com.showFields)
            {
                EditorGUILayout.BeginVertical(paddingStyle);
                EditorGUILayout.Space();
                com.uiType = (UIType)EditorGUILayout.EnumPopup("UIType(组件类型)", com.uiType);
                switch (com.uiType)
                {
                    case UIType.DEFAULT:
                        //EditorGUILayout.LabelField("UIComponent为默认形式支持所");
                        break;
                    case UIType.LONGPRESS:
                        com._longPressTime = EditorGUILayout.FloatField("Long Press Time", com._longPressTime);
                        break;
                    case UIType.DOUBLECLICK:
                        break;
                    case UIType.DRAG:
                        break;
                    case UIType.MOVE:
                        break;
                    default:
                        break;
                }
               
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
            }
         
            com.showEvents = OneFlyGUITools.BeginFoldOut("Unity Events", com.showEvents);
            if (com.showEvents)
            {

                //点击事件
                serializedObject.Update();
                SerializedProperty onMouseClick = serializedObject.FindProperty("onMouseClick");
                EditorGUILayout.PropertyField(onMouseClick, true);
                serializedObject.ApplyModifiedProperties();

                //悬浮事件
                serializedObject.Update();
                SerializedProperty onHover = serializedObject.FindProperty("onHover");
                EditorGUILayout.PropertyField(onHover, true);
                serializedObject.ApplyModifiedProperties();             

                //双击事件
                serializedObject.Update();
                SerializedProperty onDoubleClick = serializedObject.FindProperty("onDoubleClick");
                EditorGUILayout.PropertyField(onDoubleClick, true);
                serializedObject.ApplyModifiedProperties();

                //选择事件
                serializedObject.Update();
                SerializedProperty onSelect = serializedObject.FindProperty("onSelect");
                EditorGUILayout.PropertyField(onSelect, true);
                serializedObject.ApplyModifiedProperties();

                //长按事件
                serializedObject.Update();
                SerializedProperty onLongPress = serializedObject.FindProperty("onLongPress");
                EditorGUILayout.PropertyField(onLongPress, true);
                serializedObject.ApplyModifiedProperties();

                //抬起事件
                serializedObject.Update();
                SerializedProperty onRelease = serializedObject.FindProperty("onRelease");
                EditorGUILayout.PropertyField(onRelease, true);
                serializedObject.ApplyModifiedProperties();

                //退出事件
                serializedObject.Update();
                SerializedProperty onMouseExit = serializedObject.FindProperty("onMouseExit");
                EditorGUILayout.PropertyField(onMouseExit, true);
                serializedObject.ApplyModifiedProperties();

                //移动事件
                serializedObject.Update();
                SerializedProperty onMove = serializedObject.FindProperty("onMove");
                EditorGUILayout.PropertyField(onMove, true);
                serializedObject.ApplyModifiedProperties();

                //开始拖拽事件
                serializedObject.Update();
                SerializedProperty onBeginDrag = serializedObject.FindProperty("onBeginDrag");
                EditorGUILayout.PropertyField(onBeginDrag, true);
                serializedObject.ApplyModifiedProperties();

                //拖拽事件
                serializedObject.Update();
                SerializedProperty onDrag = serializedObject.FindProperty("onDrag");
                EditorGUILayout.PropertyField(onDrag, true);
                serializedObject.ApplyModifiedProperties();

                //结束拖拽事件
                serializedObject.Update();
                SerializedProperty onEndDrag = serializedObject.FindProperty("onEndDrag");
                EditorGUILayout.PropertyField(onEndDrag, true);
                serializedObject.ApplyModifiedProperties();
            }

            if (GUI.changed)
                EditorUtility.SetDirty(com);
        }
    }
}