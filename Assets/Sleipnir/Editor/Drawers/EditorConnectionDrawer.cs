﻿using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Sleipnir.Editor
{
    public class EditorConnectionDrawer: OdinValueDrawer<EditorConnection>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (GUIHelper.CurrentWindow.GetType() != typeof(GraphEditor))
            {
                CallNextDrawer(label);
                return;
            }
            
            var editor = (GraphEditor)GUIHelper.CurrentWindow;

            var editorConnection = (EditorConnection)Property.ValueEntry.WeakSmartValue;
            var outputSlotRect = editor.GetSlotRect(editorConnection.Content.Output, SlotDirection.Output);
            var inputSlotRect = editor.GetSlotRect(editorConnection.Content.Input, SlotDirection.Input);
            var startGridPosition = editor.GridToGuiPositionNoClip(outputSlotRect.center);
            var endGridPosition = editor.GridToGuiPositionNoClip(inputSlotRect.center);
            DrawConnection(startGridPosition, endGridPosition);
        }

        public static void DrawConnection(Vector2 startGridPosition, Vector2 endGridPosition)
        {
            if (startGridPosition == endGridPosition)
                return;

            var bezierCurveData = EditorConnection.BezierCurveData(startGridPosition, endGridPosition);

            Handles.DrawBezier(
                bezierCurveData.Item1,
                bezierCurveData.Item2,
                bezierCurveData.Item3,
                bezierCurveData.Item4,
                new Color(0.95f, 0.38f, 0.24f),
                null,
                EditorConnection.ConnectionWidth);
        }
    }

    [DrawerPriority(DrawerPriorityLevel.WrapperPriority)]
    public class EditorConnectionListDrawer<TListType>
        : OdinValueDrawer<TListType> where TListType : IEnumerable<EditorConnection>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            foreach (var propertyChild in ValueEntry.Property.Children)
                    propertyChild.Draw();
        }
    }
}