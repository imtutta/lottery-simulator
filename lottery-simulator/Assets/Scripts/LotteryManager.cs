using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LotteryManager : MonoBehaviour {

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
	public MasterDataObject mdo;
	public List<int> numbersList;
	public List<int> extractedNumbers;
	#endregion

	#region Unity Callbacks
	void Awake ()
	{
		extractedNumbers = new List<int>();
	}

	IEnumerator Start ()
	{
		var path = System.IO.Path.Combine ("file://" + Application.streamingAssetsPath, fileName);
		WWW www = new WWW (path);
		yield return www;

		mdo = JsonUtility.FromJson<MasterDataObject> (www.text);
		BuildNumberList (mdo, ref numbersList);
	}

	void Update ()
	{
	
	}
	#endregion

	#region External functions
	protected int ExtractNumber()
	{
		return 0;
	}
	#endregion

	#region Internal functions
	private void BuildNumberList(MasterDataObject mdo, ref List<int> list)
	{
		for (int i = 0; i < mdo.list.Length; ++i) {
			MasterDataObject.MasterDataEntry mde = mdo.list [i];
			for (int j = mde.start, inc = 0; j <= mde.end; ++j, ++inc)
				list.Add (mde.start + inc);
		}
	}
	#endregion
}
