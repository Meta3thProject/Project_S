using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class DIALOGUE_TABLEData
{
  [SerializeField]
  int id;
  public int ID { get {return id; } set { this.id = value;} }
  
  [SerializeField]
  int npc_id;
  public int NPC_ID { get {return npc_id; } set { this.npc_id = value;} }
  
  [SerializeField]
  string dialogue;
  public string DIALOGUE { get {return dialogue; } set { this.dialogue = value;} }
  
  [SerializeField]
  int link_dialog;
  public int LINK_DIALOG { get {return link_dialog; } set { this.link_dialog = value;} }
  
}