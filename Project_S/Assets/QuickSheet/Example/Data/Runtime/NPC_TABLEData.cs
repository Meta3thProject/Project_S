using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class NPC_TABLEData
{
  [SerializeField]
  int id;
  public int ID { get {return id; } set { this.id = value;} }
  
  [SerializeField]
  string npc_name;
  public string NPC_NAME { get {return npc_name; } set { this.npc_name = value;} }
  
  [SerializeField]
  int npc_initial_quest;
  public int NPC_INITIAL_QUEST { get {return npc_initial_quest; } set { this.npc_initial_quest = value;} }
  
}