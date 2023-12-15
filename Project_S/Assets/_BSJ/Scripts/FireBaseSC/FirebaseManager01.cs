//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Firebase.Auth;
//using UnityEngine.UI;
//using TMPro;
//using Unity.VisualScripting;
//using Firebase.Database;
//using Firebase;
//using System;
//using System.IO;
//using Firebase.Extensions;
//using UnityEngine.SceneManagement;

////! 유저 정보를 담고 있는 클래스
//public class Userdata
//{
//    // 파이어 베이스에 등록된 유저 UID
//    public string UserID;

//    // 등록시킬 닉네임
//    public string UserNickName;

//    public Userdata(string userID, string userNickName)
//    {
//        UserID = userID;
//        UserNickName = userNickName;
//    }
//}

//public class FirebaseManager : MonoBehaviour
//{
//    // { 키 값 상수
//    const string User = "User";
//    const string UserNickname = "UserNickName";
//    const string INVENTORY = "Inventory";
//    const string ITEMTYPE0 = "ItemType0";
//    const string ITEMTYPE1 = "ItemType1";
//    const string ITEMTYPE2 = "ItemType2";
//    const string ITEMTYPE3 = "ItemType3";
//    const string ITEMTYPE4 = "ItemType4";
//    const string ITEMTYPE5 = "ItemType5";
//    const string ITEMTYPE6 = "ItemType6";
//    const string QUEST = "Quest";
//    const string ZONE5 = "Zone5";
//    // } 키 값 상수

//    // 싱글톤
//    public static FirebaseManager instance;

//    // 파이어 베이스
//    private DatabaseReference reference;    // 루트 레퍼런스
//    private FirebaseAuth auth;              // 로그인 | 회원가입 등에 사용 => 인증을 관리할 객체
//    private FirebaseUser user;              // 인증이 완료된 유저 정보

//    [SerializeField] private TMP_InputField emailField;         // 이메일 입력받을 필드
//    [SerializeField] private TMP_InputField passField;          // 비밀번호 입력받을 필드
//    [SerializeField] private TMP_InputField nickNameField;      // 닉네임 입력받을 필드

//    [SerializeField]
//    private readonly string DBurl = "https://test-project-s-c765e-default-rtdb.asia-southeast1.firebasedatabase.app/";        // DB URL 주소

//    // 유저 데이터 클래스
//    [SerializeField]
//    private Userdata userData;

//    // 로그인 관련 캔버스
//    [SerializeField]
//    private Canvas loginCanvas;

//    // 아이템 박스 오브젝트들
//    [SerializeField]
//    private GameObject itemBoxes;

//    void Awake()
//    {
//        // { 싱글톤
//        if (null == instance)
//        {
//            instance = this;

//            DontDestroyOnLoad(this.gameObject);
//        }
//        else
//        {
//            Destroy(this.gameObject);
//        }
//        // } 싱글톤

//        // 파이어 베이스 객체 초기화
//        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
//    }

//    void Start()
//    {
//        // DB URL 지정
//        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);

//        // 루트 레퍼런스
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }

//    /// <summary>
//    /// 회원가입하는 함수
//    /// </summary>
//    public void register()
//    {
//        // 제공되는 함수 : 이메일과 비밀번호로 회원가입 시켜 줌
//        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWith(
//            task => {
//                if (task.IsCanceled)
//                {
//                    Debug.Log("회원가입 캔슬됨.\n");
//                    return;
//                }

//                if (task.IsFaulted)
//                {
//                    Debug.Log("오류 발생\n" + task.Exception);
//                    return;
//                }

//                AuthResult result = task.Result;
//                Debug.Log($"회원가입 성공 : {result.User.DisplayName}, {result.User.UserId}");

//                // 첫 회원 가입 시 ( 플레이어 정보 생성 + 인벤토리 부여 + ...)
//                // 회원가입 성공 시 유저 데이터 초기값 셋팅
//                userData = new Userdata(result.User.UserId.ToString(), nickNameField.text);

//                // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//                reference = FirebaseDatabase.DefaultInstance.RootReference;

//                // 유저 DB 추가
//                // WriteDB();

//                // 인벤토리 DB 추가 ( 처음 DB 추가 ) 6은 아이템 타입
//                for(int i = 0; i <= 6; i++)
//                {
//                    UpdateFirebaseInventory(i);
//                }

//                // 퀘스트 DB 추가 ( 처음 DB 추가 ) 5는 zone5 테스트
//                UpdateFirebaseQuest(5);

//                // 완료 되었다면 유저데이터 비우기
//                userData = new Userdata(null, null);
//            }
//            );
//    }

//    /// <summary>
//    /// 로그인 하는 함수.
//    /// </summary>
//    public void login()
//    {
//        // 제공되는 함수 : 이메일과 비밀번호로 로그인 시켜 줌
//        auth.SignInWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWith(
//            task => {
//                if (task.IsCanceled)
//                {
//                    Debug.Log("로그인 캔슬됨.\n");
//                    return;
//                }

//                if (task.IsFaulted)
//                {
//                    Debug.Log("로그인 오류 발생\n" + task.Exception);
//                    return;
//                }

//                AuthResult result = task.Result;
//                Debug.Log($"로그인 성공 : {emailField.text}, {result.User.UserId}");

//                // 로그인 성공 시 유저 데이터 초기값 셋팅
//                userData = new Userdata(result.User.UserId.ToString(), nickNameField.text);

//                // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//                reference = FirebaseDatabase.DefaultInstance.RootReference;

//                // 로그인 시 파이어 베이스 DB에서 아이템 받아오기
//                InitInventoryFromDB();

//                // 로그인 시 파이어 베이스 DB에서 퀘스트 정보 받아오기
//                InitQuestFromDB();

//                // 인게임 내 인벤토리로 동기화 (첫 동기화 이기 떄문에 모든 아이템 동기화해주어야 한다.) 6은 아이템 타입
//                for(int i = 0; i <= 6; i++)
//                {
//                    PlayerInventory.instance.ItemDictionaryInit(i);
//                    PlayerInventory.instance.SetItemsDictionry(i);
//                }
//            }
//        );

//        // 아이템 박스 활성화
//        itemBoxes.SetActive(true);
        

//        // 캔버스 비활성화 전환
//        loginCanvas.transform.gameObject.SetActive(false);
//    }

//    /// <summary>
//    /// 로그아웃하는 함수.
//    /// </summary>
//    public void LogOut()
//    {
//        auth.SignOut(); // 로그아웃이 자동으로 된다.
//        Debug.Log("로그아웃");

//        // 유저데이터 비우기
//        userData = new Userdata(null, null);

//        // 아이템 갯수 초기화
//        PlayerInventory.instance.ItemInit();

//        // 모든 아이템 딕셔너리 클리어 ( 6은 아이템 타입 )
//        for(int i = 0; i < 6; i++)
//        {
//            PlayerInventory.instance.ItemDictionaryInit(i);
//        }
//    }

//    /// <summary>
//    /// Json 파일로 변환해서 새로운 DB 생성 후 저장
//    /// </summary>
//    public void WriteDB()
//    {
//        // 비 로그인시 리턴
//        if (userData.UserID == null) { Debug.Log("로그인을 먼저 해주세요"); return; }

//        // 이미 유저데이터가 있다면 반환
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID.ToString());
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                // Json 파일로 변환해서
//                string jsondate1 = JsonUtility.ToJson(userData);

//                // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//                reference = FirebaseDatabase.DefaultInstance.RootReference;

//                // User 안에 유저데이터 안에 json 파일로 데이터를 DB에 저장
//                reference.Child(User).Child(userData.UserID.ToString()).SetRawJsonValueAsync(jsondate1);
//            }
//        });
//    }

//    /// <summary>
//    /// 닉네임 DB 변경 함수
//    /// </summary>
//    /// <param name="_nickName"></param>
//    public void SetDB_NickName(string _nickName)
//    { 
//        reference.Child(User).Child(userData.UserID.ToString()).Child(UserNickname).SetValueAsync(_nickName);

//        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }

//    /// <summary>
//    /// 파이어베이스 DB에 플레이어 인벤토리를 저장시키는 함수.
//    /// </summary>
//    /// <param name="_itemType"></param>
//    public void UpdateFirebaseInventory(int _itemType)
//    {
//        // 딕셔너리 초기화 -> 현재 인게임 내 아이템 동기화 한 후에 저장
//        PlayerInventory.instance.ItemDictionaryInit(_itemType);
//        PlayerInventory.instance.SetItemsDictionry(_itemType);

//        switch (_itemType)
//        {
//            case 0:
//                var dataToSave00 = PlayerInventory.instance.itemType0;
//                reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE0).SetValueAsync(dataToSave00);
//                break;

//            case 1:
//                var dataToSave01 = PlayerInventory.instance.itemType1;
//                reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE1).SetValueAsync(dataToSave01);
//                break;

//            case 2:
//                var dataToSave02 = PlayerInventory.instance.itemType2;
//                reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE2).SetValueAsync(dataToSave02);
//                break;

//            case 3:
//                var dataToSave03 = PlayerInventory.instance.itemType3;
//                reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE3).SetValueAsync(dataToSave03);
//                break;

//            case 4:
//                var dataToSave04 = PlayerInventory.instance.itemType4;
//                reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE4).SetValueAsync(dataToSave04);
//                break;

//            case 5:
//                var dataToSave05 = PlayerInventory.instance.itemType5;
//                reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE5).SetValueAsync(dataToSave05);
//                break;

//            case 6:
//                var dataToSave06 = PlayerInventory.instance.itemType6;
//                reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE6).SetValueAsync(dataToSave06);
//                break;

//            default: break;
//        }
//    }

//    /// <summary>
//    /// 파이어베이스 DB에 플레이어 퀘스트를 저장시키는 함수.
//    /// </summary>
//    /// <param name="_zoneType"></param>
//    public void UpdateFirebaseQuest(int _zoneType)
//    {
//        // 딕셔너리 초기화 -> 현재 인게임 내 퀘스트 동기화 한 후에 저장
//        PlayerQuest.Instance.QuestDictionaryInit(_zoneType);
//        PlayerQuest.Instance.SetQuestDictionry(_zoneType);

//        // 5는 zone 5 테스트
//        switch (_zoneType)
//        {
//            case 5:
//                var questToSave05 = PlayerQuest.Instance.Zone5_QuestDictionary;
//                reference.Child(User).Child(userData.UserID.ToString()).Child(QUEST).Child(ZONE5).SetValueAsync(questToSave05);
//                break;
//            default:
//                break;
//        }
//    }

//    /// <summary>
//    /// 파이어베이스에서 DB 데이터 가져오는 함수. TEST: v.02
//    /// </summary>
//    public void UpdatePlayerInventory()
//    {
//        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//        reference = FirebaseDatabase.DefaultInstance.RootReference;

//        // 데이터 가져오기 (유저 ID의 인벤토리 항목 스냅샷으로 가져옴)
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE1);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if(task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary itemData = (IDictionary)snapshot.Value;

//                // 테스트용
//                Debug.Log($"{itemData["apple"]}");

//                // TODO: 잘 Read 되었으면 다음 할 일
//            }
//        });

//        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }

//    /// <summary>
//    /// 파이어베이스에서 퀘스트 DB 데이터 가져오는 함수. TEST: v.02
//    /// </summary>
//    public void UpdatePlayerQuest()
//    {
//        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//        reference = FirebaseDatabase.DefaultInstance.RootReference;

//        // 데이터 가져오기 (유저 ID의 퀘스트 항목 스냅샷으로 가져옴)
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(QUEST).Child(ZONE5);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary questData = (IDictionary)snapshot.Value;

//                // 테스트용
//                Debug.Log($"{questData[301500]}");

//                // TODO: 잘 Read 되었으면 다음 할 일
//            }
//        });

//        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }

//    /// <summary>
//    /// 파이어 베이스에서 데이터 읽어오는 함수. TEST: v.01
//    /// </summary>
//    public void ReadDB()
//    {
//        // 비 로그인시 리턴
//        if (userData.UserID == null) { Debug.Log("로그인을 먼저 해주세요"); return; }

//        // 데이터 가져오기 (유저 ID를 찾아서 스냅샷으로 가져옴)
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID.ToString());
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            // 정상적으로 데이터를 가져왔다면?
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary UserData = (IDictionary)snapshot.Value;
//                Debug.Log($"아이디 : {UserData["UserID"]}" + $"정보 : {UserData["UserNickName"]}");
//            }
//        });

//        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }

//    /// <summary>
//    /// 파이어 베이스에 저장된 DB 지우는 함수.
//    /// </summary>
//    public void RemoveDB()
//    {
//        FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).RemoveValueAsync();

//        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }

//    /// <summary>
//    /// Json 파일로 생성하는 함수.
//    /// </summary>
//    public void SaveJsonFile()
//    {
//        File.WriteAllText(Application.dataPath + "/TestJson.json", JsonUtility.ToJson(userData));
//    }

//    /// <summary>
//    /// 로그인 시 파이어 베이스 DB에서 모든 퀘스트 받아오는 함수.
//    /// </summary>
//    public void InitQuestFromDB()
//    {
//        #region Zone5
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID.ToString()).Child(QUEST).Child(ZONE5);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary QuestData = (IDictionary)snapshot.Value;

//                // Zone5 퀘스트
//                Debug.Log(QuestData[301500.ToString()]);
//                Debug.Log(Convert.ToBoolean(QuestData[301500.ToString()]));
//                PlayerQuest.Instance.zone5_Quest00 = Convert.ToBoolean(QuestData[301500.ToString()]);
//                PlayerQuest.Instance.zone5_Quest01 = Convert.ToBoolean(QuestData[301501.ToString()]);
//                PlayerQuest.Instance.zone5_Quest02 = Convert.ToBoolean(QuestData[301502.ToString()]);
//                PlayerQuest.Instance.zone5_Quest03 = Convert.ToBoolean(QuestData[301503.ToString()]);
//                PlayerQuest.Instance.zone5_Quest04 = Convert.ToBoolean(QuestData[301504.ToString()]);
//                PlayerQuest.Instance.zone5_Quest05 = Convert.ToBoolean(QuestData[301505.ToString()]);
//                PlayerQuest.Instance.zone5_Quest06 = Convert.ToBoolean(QuestData[301506.ToString()]);
//                PlayerQuest.Instance.zone5_Quest07 = Convert.ToBoolean(QuestData[301507.ToString()]);
//                PlayerQuest.Instance.zone5_Quest08 = Convert.ToBoolean(QuestData[301508.ToString()]);
//                PlayerQuest.Instance.zone5_Quest09 = Convert.ToBoolean(QuestData[301509.ToString()]);
//                PlayerQuest.Instance.zone5_Quest10 = Convert.ToBoolean(QuestData[301510.ToString()]);
//            }
//            else
//            {   /*Do Nothing*/    }
//        });
//        #endregion

//        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }

//    /// <summary>
//    /// 로그인 시 파이어 베이스 DB에서 모든 아이템 받아오는 함수.
//    /// </summary>
//    public void InitInventoryFromDB()
//    {
//        // 데이터 가져오기 (유저 ID의 인벤토리 항목 스냅샷으로 가져옴)
//        #region 아이템 타입 0
//        // 아이템 타입 0
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE0);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary ItemData = (IDictionary)snapshot.Value;

//                // 아이템 타입 0
//                PlayerInventory.instance.wood = Convert.ToInt32(ItemData["wood"]);    // ItemData["apple"]이 long 타입이라 Convert로 정수형으로 변환해야함.
//                PlayerInventory.instance.stone = Convert.ToInt32(ItemData["stone"]);
//                PlayerInventory.instance.iron = Convert.ToInt32(ItemData["iron"]);
//            }
//            else
//            {   /*Do Nothing*/    }
//        });
//        #endregion

//        #region 아이템 타입 1
//        // 아이템 타입 1
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE1);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary itemData = (IDictionary)snapshot.Value;

//                // 아이템 타입 1은 아직 추가되지 않음.
//            }
//            else
//            {   /*Do Nothing*/    }
//        });
//        #endregion

//        #region 아이템 타입 2
//        // 아이템 타입 2
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE2);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary itemData = (IDictionary)snapshot.Value;

//                // 아이템 타입 2
//                PlayerInventory.instance.ice = Convert.ToInt32(itemData["ice"]);
//            }
//            else
//            {   /*Do Nothing*/    }
//        });
//        #endregion

//        #region 아이템 타입 3
//        // 아이템 타입 3
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE3);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary itemData = (IDictionary)snapshot.Value;

//                // 아이템 타입 3
//                PlayerInventory.instance.oil = Convert.ToInt32(itemData["oil"]);
//            }
//            else
//            {   /*Do Nothing*/    }
//        });
//        #endregion

//        #region 아이템 타입 4
//        // 아이템 타입 4
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE4);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary itemData = (IDictionary)snapshot.Value;

//                // 아이템 타입 4
//                PlayerInventory.instance.torch = Convert.ToInt32(itemData["torch"]);
//                PlayerInventory.instance.flashlight = Convert.ToInt32(itemData["flashlight"]);
//                PlayerInventory.instance.brush = Convert.ToInt32(itemData["brush"]);
//            }
//            else
//            {   /*Do Nothing*/    }
//        });
//        #endregion

//        #region 아이템 타입 5
//        // 아이템 타입 5
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE5);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary itemData = (IDictionary)snapshot.Value;

//                // 아이템 타입 5
//                PlayerInventory.instance.toyParts = Convert.ToInt32(itemData["toyParts"]);
//                PlayerInventory.instance.bond = Convert.ToInt32(itemData["bond"]);
//                PlayerInventory.instance.repairedToy = Convert.ToInt32(itemData["repairedToy"]);
//                PlayerInventory.instance.cookie = Convert.ToInt32(itemData["cookie"]);
//                PlayerInventory.instance.wrappingPaper = Convert.ToInt32(itemData["wrappingPaper"]);
//                PlayerInventory.instance.deliciousSnack = Convert.ToInt32(itemData["deliciousSnack"]);
//                PlayerInventory.instance.thickThread = Convert.ToInt32(itemData["thickThread"]);
//                PlayerInventory.instance.knittingNeedles = Convert.ToInt32(itemData["knittingNeedles"]);
//                PlayerInventory.instance.warmShawl = Convert.ToInt32(itemData["warmShawl"]);
//                PlayerInventory.instance.film = Convert.ToInt32(itemData["film"]);
//                PlayerInventory.instance.cameraWithoutFilm = Convert.ToInt32(itemData["cameraWithoutFilm"]);
//                PlayerInventory.instance.camera = Convert.ToInt32(itemData["camera"]);
//            }
//            else
//            {   /*Do Nothing*/    }
//        });
//        #endregion

//        #region 아이템 타입 6
//        // 아이템 타입 6
//        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE6);
//        reference.GetValueAsync().ContinueWithOnMainThread(task =>
//        {
//            if (task.IsCompleted)
//            {
//                DataSnapshot snapshot = task.Result;
//                IDictionary itemData = (IDictionary)snapshot.Value;

//                // 아이템 타입 6
//                PlayerInventory.instance.spiritAxe = Convert.ToInt32(itemData["spiritAxe"]);
//                PlayerInventory.instance.magicMap = Convert.ToInt32(itemData["magicMap"]);
//            }
//            else
//            {   /*Do Nothing*/    }
//        });
//        #endregion

//        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
//        reference = FirebaseDatabase.DefaultInstance.RootReference;
//    }
//}