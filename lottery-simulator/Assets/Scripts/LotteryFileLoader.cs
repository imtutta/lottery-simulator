using UnityEngine;
using System.Collections;

public class LotteryFileLoader : MonoBehaviour {

	#region Data
	[System.Serializable]
	public struct MasterDataObject
	{
		[System.Serializable]
		public struct MasterDataEntry
		{
			public int start;
			public int end;
			public string owner;
		}

		public MasterDataEntry[] list;
	}
	#endregion

	#region Public Fields
	public string fileName;
	#endregion

	#region Unity Callbacks
	IEnumerator Start ()
	{
		var path = System.IO.Path.Combine ("file://" + Application.streamingAssetsPath, fileName);
		WWW www = new WWW (path);
		yield return www;

		MasterDataObject mdo = JsonUtility.FromJson<MasterDataObject> (www.text);
		Debug.Log ("First: start " + mdo.list [0].start + ", end " + mdo.list [0].end + ", owner " + mdo.list [0].owner);

	}

	void Update ()
	{
	
	}
	#endregion
}
