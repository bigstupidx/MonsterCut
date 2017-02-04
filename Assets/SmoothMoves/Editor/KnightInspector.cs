using UnityEngine;
using UnityEditor;
using System.Collections;
using SmoothMoves;

[CustomEditor(typeof(Knight))]
public class KnightInspector : TextureFunctionInspector {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
	}
}
