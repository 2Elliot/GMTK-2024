using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Items", order = 2)]
public class Item : ScriptableObject {
  public string Name;
  public int Weight;
  public Sprite Image;
}