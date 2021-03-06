﻿#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RedOwl.Editor;
using RedOwl.GraphFramework;

namespace RedOwl.GraphFramework.Editor
{
	[CustomEditor(typeof(Node), true)]
	public class NodeEditor : RedOwlEditor<Node>
	{
		protected override IEnumerable<string> GetHiddenFields()
		{			
			foreach (var item in Target)
			{
				yield return item.name;
			}
		}
		
		protected override void OnBeforeDefaultInspector()
		{
			EditorGUIUtility.labelWidth = Target.view.labelWidth;
		}

        protected override void OnAfterDefaultInspector()
		{
			EditorGUIUtility.labelWidth = 0;
		}

        protected override void OnChange()
		{
			GraphWindow.MarkDirty();
		}
	}
}
#endif
