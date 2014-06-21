using UnityEngine;
using System.Collections;

public class UiInventoryCommodity : MonoBehaviour {
	public string trackedCommodity = "not defined";

	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update()
	{
	}
	
	public void trackCommodity(string commodity)
	{
		trackedCommodity = commodity;
		
		updateCommodityInfo(trackedCommodity);
	}
	
	private void updateCommodityInfo(string commodity)
	{
		gameObject.transform.FindChild("name").GetComponent<UILabel>().text =
			Economy.commodityInfo[commodity].name;
		gameObject.transform.FindChild("amount").GetComponent<UILabel>().text =
			Universe.singleton.player.cargo.commodities[commodity].ToString();
		
		GameObject.Find("statusDesc").GetComponent<UILabel>().text =
			"Cargo: " + Universe.singleton.player.cargo.getUsedCargoSpace().ToString() + " / " +
				Universe.singleton.player.cargo.maxCargoSpace.ToString() + "\n" +
				"Credits: " + Universe.singleton.player.cargo.credits.ToString();
	}

}
