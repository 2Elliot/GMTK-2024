using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Items", order = 2)]
public class Item : ScriptableObject {
  public string Name;
  public float Weight;
  public Sprite Image;
}