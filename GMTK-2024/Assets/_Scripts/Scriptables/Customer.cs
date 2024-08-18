using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCustomer", menuName = "ScriptableObjects/Customer", order = 1)]
public class Customer : ScriptableObject
{
    public string Name;
    [ColorUsage(false, false)] public Color NameColor;
    public Sprite Image;
    public List<Item> Items;
    public List<int> Weights;
    public Dialogue StartDialogue;
    public Dialogue EndDialogueSuccess;
    public Dialogue EndDialogueFailure;
}

