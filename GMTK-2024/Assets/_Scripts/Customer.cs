using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCustomer", menuName = "ScriptableObjects/Customer", order = 1)]
public class Customer : ScriptableObject
{
    public string Name;
    public Sprite Image;
    public List<Item> Items;
    public Dialogue Dialogue;
}

