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

	#region Protected Fields
	public bool loadFlag = false;
	#endregion

	#region Unity Callbacks
	void Awake ()
	{
		numbersList = new List<int>();
		extractedNumbers = new List<int>();
	}

	IEnumerator Start ()
	{
		StartCoroutine (LoadData ());
		while (!loadFlag)
			yield return new WaitForEndOfFrame();
		
		BuildData ();
	}
	#endregion

	#region Coroutines
	IEnumerator LoadData()
	{
		var path = System.IO.Path.Combine ("file://" + Application.streamingAssetsPath, fileName);
		WWW www = new WWW (path);
		yield return www;
		mdo = JsonUtility.FromJson<MasterDataObject> (www.text);
		loadFlag = true;
	}
	#endregion

	#region External functions
	public bool ExtractNumber()
	{
		if (numbersList.Count == 0)
			return false;
		int randomIndex = Random.Range (0, 1000000) % numbersList.Count;
		extractedNumbers.Add (numbersList[randomIndex]);
		numbersList.RemoveAt (randomIndex);
		return true;
	}

	public void LoadDataCoroutine()
	{
		StartCoroutine (LoadData ());
	}

	public void BuildData()
	{
		BuildNumberList (mdo, ref numbersList);
	}

	public void CleanData()
	{
		Awake ();
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
