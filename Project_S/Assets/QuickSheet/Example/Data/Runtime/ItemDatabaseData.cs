using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class ItemDatabaseData
{
  [SerializeField]
  int id;
  public int ID { get {return id; } set { this.id = value;} }
  
  [SerializeField]
  string description;
  public string DESCRIPTION { get {return description; } set { this.description = value;} }
  
  [SerializeField]
  string itme_name;
  public string ITME_NAME { get {return itme_name; } set { this.itme_name = value;} }
  
  [SerializeField]
  int item_type;
  public int ITEM_TYPE { get {return item_type; } set { this.item_type = value;} }
  
  [SerializeField]
  int amount;
  public int AMOUNT { get {return amount; } set { this.amount = value;} }
  
  [SerializeField]
  bool combination;
  public bool COMBINATION { get {return combination; } set { this.combination = value;} }
  
  [SerializeField]
  string model;
  public string MODEL { get {return model; } set { this.model = value;} }
  
}