using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class QUEST_TABLEData
{
  [SerializeField]
  int id;
  public int ID { get {return id; } set { this.id = value;} }
  
  [SerializeField]
  string quest_title;
  public string QUEST_TITLE { get {return quest_title; } set { this.quest_title = value;} }
  
  [SerializeField]
  int start_npc;
  public int START_NPC { get {return start_npc; } set { this.start_npc = value;} }
  
  [SerializeField]
  int end_npc;
  public int END_NPC { get {return end_npc; } set { this.end_npc = value;} }
  
  [SerializeField]
  int quest_type;
  public int QUEST_TYPE { get {return quest_type; } set { this.quest_type = value;} }
  
  [SerializeField]
  int value1;
  public int VALUE1 { get {return value1; } set { this.value1 = value;} }
  
  [SerializeField]
  int value2;
  public int VALUE2 { get {return value2; } set { this.value2 = value;} }
  
  [SerializeField]
  int before_dialogue;
  public int BEFORE_DIALOGUE { get {return before_dialogue; } set { this.before_dialogue = value;} }
  
  [SerializeField]
  int doing_dialogue;
  public int DOING_DIALOGUE { get {return doing_dialogue; } set { this.doing_dialogue = value;} }
  
  [SerializeField]
  int complete_dialogue;
  public int COMPLETE_DIALOGUE { get {return complete_dialogue; } set { this.complete_dialogue = value;} }
  
  [SerializeField]
  int reward;
  public int REWARD { get {return reward; } set { this.reward = value;} }
  
}