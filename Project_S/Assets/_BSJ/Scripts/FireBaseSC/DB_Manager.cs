//using Firebase;
//using Firebase.Database;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class DB_Manager : MonoBehaviour
//{
//    public string DBurl = "https://test-project-s-c765e-default-rtdb.asia-southeast1.firebasedatabase.app/";

//    DatabaseReference reference;

//    유니티 퀵시트 연동
//    public Sheet1 sheet1;

//    void Start()
//    {
//        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);

//        루트 레퍼런스
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }

//    public void WriteDB()
//    {
//        비 로그인시 리턴
//        if (PlayerUser.userID == null) { Debug.Log("로그인을 먼저 해주세요"); return; }

//    Test: 유니티 퀵시트 연동해보기
//   Monster Data1 = new Monster(sheet1.dataArray[0].ID, sheet1.dataArray[0].Description, sheet1.dataArray[0].Hp, sheet1.dataArray[0].Arrive_Time,
//       sheet1.dataArray[0].Deadsizex, sheet1.dataArray[0].Deadsizey, sheet1.dataArray[0].Deadsizez, sheet1.dataArray[0].Deadinstance,
//       sheet1.dataArray[0].Weaknessf, sheet1.dataArray[0].Weaknessl, sheet1.dataArray[0].Weaknessr, sheet1.dataArray[0].Cooltime,
//       sheet1.dataArray[0].Skill1, sheet1.dataArray[0].Skill2, sheet1.dataArray[0].Skill3);

//        string jsondate1 = JsonUtility.ToJson(PlayerUser.userID);
//        reference.Child("User").Child(PlayerUser.userID.ToString()).SetRawJsonValueAsync(jsondate1);

//    LEGACY: DB 테스트용
//         GPSdate Data2 = new GPSdate("busan", 137.0f, 1123.4f, 13.5f);
//        GPSdate Data3 = new GPSdate("daegu", 236.0f, 223.4f, 0.3f);

//    LEGACY: DB 테스트용
//         json으로 파싱해서 db로 보낼 예정
//         string jsondate1 = JsonUtility.ToJson(Data1);
//        string jsondate2 = JsonUtility.ToJson(Data2);
//        string jsondate3 = JsonUtility.ToJson(Data3);

//    LEGACT: DB 테스트용
//         reference.Child("Korea").Child("area1").SetRawJsonValueAsync(jsondate1);
//        reference.Child("Korea").Child("area2").SetRawJsonValueAsync(jsondate2);
//        reference.Child("Korea").Child("area3").SetRawJsonValueAsync(jsondate3);
//    }

//    public void ReadDB()
//    {
//        비 로그인시 리턴
//        if (PlayerUser.userID == null) { Debug.Log("로그인을 먼저 해주세요"); return; }

//        데이터 가져오기
//         reference = FirebaseDatabase.DefaultInstance.GetReference("User");     // korea의 최상위 루트를 가져오겠다.
//        reference = FirebaseDatabase.DefaultInstance.GetReference("User").Child(PlayerUser.userID);
//        reference.GetValueAsync().ContinueWith(task =>
//        {
//            정상적으로 데이터를 가져왔다면 ?
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary UserData = (IDictionary)snapshot.Value;
//                Debug.Log($"아이디 : {UserData["userID"]}" + $"정보 : {UserData["userNickName"]}");


//                foreach (DataSnapshot data in snapshot.Children)
//                {
//                    Debug.Log(data);
//                    IDictionary MonData = (IDictionary)data.Value;
//                    Debug.Log($"아이디 : {MonData["ID"]}" + $"정보 : {MonData["Description"]}");
//                }
//            }
//        });
//    }
//}


//LEGACY: DB 테스트용 예제 2023.11.07 BSJ_
//public class Monster
//{
//    public int ID = 0;
//    public string Description = "";
//    public float HP = 0;
//    public float Arrive_Time = 0;
//    public float DeadSizeX = 0;
//    public float DeadSizeY = 0;
//    public float DeadSizeZ = 0;
//    public float DeadInstance = 0;
//    public float WeaknessF = 0;
//    public float WeaknessL = 0;
//    public float WeaknessR = 0;
//    public float Cooltime = 0;
//    public float Skill1 = 0;
//    public float Skill2 = 0;
//    public float Skill3 = 0;

//    public Monster(int ID, string Description, float HP, float Arrive_Time, float DeadSizeX, float DeadSizeY, float DeadSizeZ,
//        float DeadInstance, float WeaknessF, float WeaknessL, float WeaknessR, float Cooltime, float Skill1, float Skill2, float Skill3)
//    {
//        this.ID = ID;
//        this.Description = Description;
//        this.HP = HP;
//        this.Arrive_Time = Arrive_Time;
//        this.DeadSizeX = DeadSizeX;
//        this.DeadSizeY = DeadSizeY;
//        this.DeadSizeZ = DeadSizeZ;
//        this.DeadInstance = DeadInstance;
//        this.WeaknessF = WeaknessF;
//        this.WeaknessL = WeaknessL;
//        this.WeaknessR = WeaknessR;
//        this.Cooltime = Cooltime;
//        this.Skill1 = Skill1;
//        this.Skill2 = Skill2;
//        this.Skill3 = Skill3;
//    }
//}

//LEGACY: DB 테스트용 예제
//public class GPSdate
//{
//    public string name = "";
//    public float latitude_date = 0;
//    public float longitude_date = 0;
//    public float altitude_date = 0;

//    public GPSdate(string Name, float Lat, float Lon, float ALT)
//    {
//        name = Name;
//        latitude_date = Lat;
//        longitude_date = Lon;
//        altitude_date = ALT;
//    }
//}
