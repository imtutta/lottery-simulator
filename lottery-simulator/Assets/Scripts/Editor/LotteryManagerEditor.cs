using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LotteryManager))]
public class LotteryManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		LotteryManager lm = (LotteryManager)target;
		if(GUILayout.Button("Load Data"))
		{
			lm.LoadDataCoroutine ();
		}
		if(GUILayout.Button("Build Data"))
		{
			lm.BuildData();
		}
		if(GUILayout.Button("Extract Number"))
		{
			if (!lm.ExtractNumber ())
				Debug.LogWarning ("NO MORE NUMBERS TO EXTRACT");
		}
		if(GUILayout.Button("Clean Data"))
		{
			lm.CleanData();
		}
	}
}