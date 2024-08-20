using UnityEngine;

[CreateAssetMenu(fileName = "BuyableItem", menuName = "ScriptableObjects/PurchaseableItems", order = 2)]
public class PurchasableItem : ScriptableObject
{
	public string ItemName;
	public int UnlockIndex;
	public Sprite Image;
	public int Price;
	public string Description;
	public enum ItemType
	{
		Consumable,
		Counterweight,
	}
	public ItemType Type;
}
