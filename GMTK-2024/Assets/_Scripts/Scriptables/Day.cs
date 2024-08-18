using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDay", menuName = "ScriptableObjects/Day", order = 3)]
public class Day : ScriptableObject {
  public int DayNumber;
  public List<Customer> Customers;
}