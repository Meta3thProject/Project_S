using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Firebase.Database;
using Firebase;
using System;
using System.IO;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor;
using BNG;

public class FirebaseManager : MonoBehaviour
{
    // 싱글톤
    public static FirebaseManager instance;

    #region 필드
    // 다음 스테이지 이름
    const string LOGINSCENE = "Lobby";
    const string MAINSCENE = "BSJ_MainSceneCopy";
    // bsj 추가
    const string BSJTESTSCENE = "Main Scene Copy _ BSJ";
    const string TESTSCENE = "PJK_MainSceneCopy";
    const string JSHTestScene = "Main Scene Copy";

    // { 키 값 상수
    const string User = "User";
    const string Puzzle = "Puzzle";
    const string POSITION = "Position";
    const string MBTI = "Mbti";
    const string QUEST = "Quest";
    const string X = "x";
    const string Y = "y";
    const string Z = "z";
    #region LEGACY : 안쓰는 상수
    // LEGACY : 유저의 닉네임 사용 여부 확실치 않음. 인벤토리 사라짐.
    //const string UserNickname = "UserNickName";
    //const string INVENTORY = "Inventory";
    //const string ITEMTYPE0 = "ItemType0";
    //const string ITEMTYPE1 = "ItemType1";
    //const string ITEMTYPE2 = "ItemType2";
    //const string ITEMTYPE3 = "ItemType3";
    //const string ITEMTYPE4 = "ItemType4";
    //const string ITEMTYPE5 = "ItemType5";
    //const string ITEMTYPE6 = "ItemType6";
    #endregion
    // } 키 값 상수

    // 파이어 베이스
    private DatabaseReference reference;        // 루트 레퍼런스
    private FirebaseAuth auth;                  // 로그인 | 회원가입 등에 사용 => 인증을 관리할 객체
    private FirebaseUser user;                  // 인증이 완료된 유저 정보
    private string userID;                      // 인증이 완료된 유저 ID

    [SerializeField] private GameObject loginObjects;       // 로그인 관련 오브젝트들
    [SerializeField] private UnityEngine.UI.Button loginButton;            // 로그인 버튼
    [SerializeField] private UnityEngine.UI.Button registerButton;         // 회원가입 버튼

    [SerializeField] private InputField emailField;         // 이메일 입력받을 필드
    [SerializeField] private InputField passField;          // 비밀번호 입력받을 필드
    [SerializeField] private Text infoTextField;            // 상태 메시지 텍스트

    [SerializeField] private GameObject loadingUIPannel;    // 로딩 UI 패널
    [SerializeField] private GameObject loadingObjects;     // 로딩 관련 오브젝트들
    [SerializeField] private Text loadingTitleText;         // 로딩 타이틀 텍스트
    [SerializeField] private Text loadingGaugeText;         // 로딩 게이지 텍스트
    [SerializeField] private Text tipText;                  // 팁 텍스트

    [SerializeField] private WaitForSecondsRealtime loadingGauge = new WaitForSecondsRealtime(0.02f);  // 로딩 게이지 올라가는 연출을 위한 시간
    [SerializeField] private WaitForSecondsRealtime loadingTitleTextChangeTime = new WaitForSecondsRealtime(1.0f);  // 로딩 텍스트 변환시간

    [SerializeField]
    private readonly string DBurl = "https://test-project-s-c765e-default-rtdb.asia-southeast1.firebasedatabase.app/";        // DB URL 주소
    
    // 로그인 관련 캔버스
    [SerializeField]
    private Canvas loginCanvas;

    // 아이템 박스 오브젝트들
    [SerializeField]
    private GameObject itemBoxes;

    // 플레이어의 마지막 위치를 DB에 저장시키기 위한 딕셔너리 ( -10, 9, 72 ) => 플레이어 초기 위치임.
    private float playerXPos = -10;
    private float playerYPos = 9;
    private float playerZPos = 72;

    // 플레이어 위치를 바꾸기 위한 값
    public Vector3 lastPlayerPos { get; private set; }

    // 처음 시작할때 MBTI DB를 가져오기 위한 필드
    public float MBTI_E {  get; private set; }
    public float MBTI_I { get; private set; }
    public float MBTI_N { get; private set; }
    public float MBTI_S { get; private set; }
    public float MBTI_T { get; private set; }
    public float MBTI_F { get; private set; }
    public float MBTI_J { get; private set; }
    public float MBTI_P { get; private set; }

    // 처음 시작할때 퀘스트 DB를 가져오기 위한 필드
    [SerializeField] QUEST_TABLE quest_Table;
    public Dictionary<int, bool> QuestClearDictionary { get; private set; } 

    #endregion

    #region 메서드
    void Awake()
    {
        // { 싱글톤
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        // } 싱글톤

        // 파이어 베이스 객체 초기화
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        // 상태 메세지 초기화
        infoTextField.text = "상태 : 대기 중 ...";

        // 로딩 관련 텍스트 캐싱받기
        loadingUIPannel = transform.transform.GetChild(0).gameObject;
        loadingTitleText = loadingObjects.transform.GetChild(0).GetComponent<Text>();
        loadingGaugeText = loadingObjects.transform.GetChild(1).GetComponent<Text>();
        tipText = loadingObjects.transform.GetChild(1).GetComponent<Text>();
        loadingObjects.SetActive(false);
    }

    void Start()
    {
        // DB URL 지정
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new Uri(DBurl);

        // 루트 레퍼런스
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        // Test : 로그아웃 테스트 BSJ_240102
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            LogOut(true);
        }

        // Y 버튼을 누르면 저장이 잘 됨.
        if(InputBridge.Instance.YButtonDown)
        {
            // 인게임 데이터 저장
            SaveIngameData();
        }
    }

    /// <summary>
    /// 회원가입하는 메서드.
    /// </summary>
    public void Register()
    {
        infoTextField.text = "상태 : 회원가입 시도 중 ...";
        registerButton.interactable = false;
        // 제공되는 함수 : 이메일과 비밀번호로 회원가입 시켜 줌
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWithOnMainThread(
            task => {
                if (task.IsCanceled)
                {
                    infoTextField.text = "상태 : 회원가입 캔슬됨.";
                    registerButton.interactable = true;
                    return;
                }

                if (task.IsFaulted)
                {
                    infoTextField.text = "상태 : 회원가입 캔슬됨." + task.Exception;
                    registerButton.interactable = true;
                    return;
                }
                
                if (task.IsCompleted)
                {
                    AuthResult result = task.Result;
                    infoTextField.text = "상태 : 회원가입 성공";
                    Debug.Log($"회원가입 성공 : {result.User.UserId}");
                    registerButton.interactable = true;

                    // 회원가입 성공 시 유저 데이터 초기값 셋팅
                    userID = result.User.UserId.ToString();

                    // 플레이어 포지션 DB의 초기 노드 생성
                    MakePlayerPosDB();

                    // 퍼즐 DB의 초기 노드 생성
                    MakePuzzleDB();

                    // MBTI DB의 초기 노드 생성
                    MakeMbtiDB();

                    // Quest DB의 초기 노드 생성
                    MakeQuestDB();
                }
            }
            );
    }

    /// <summary>
    /// 로그인 하는 메서드.
    /// </summary>
    public void Login()
    {
        infoTextField.text = "상태 : 로그인 시도 중 ...";
        loginButton.interactable = false;
        auth.SignInWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWithOnMainThread(
            task =>
            {
                if (task.IsCanceled)
                {
                    infoTextField.text = "상태 : 로그인 캔슬됨.";
                    loginButton.interactable = true;
                    return;
                }

                if (task.IsFaulted)
                {
                    infoTextField.text = "상태 : 로그인 오류 발생." + task.Exception;
                    loginButton.interactable = true;
                    return;
                }

                if (task.IsCompleted)
                {
                    AuthResult result = task.Result;

                    // 유저 아이디 받아오기
                    userID = task.Result.User.UserId.ToString();

                    infoTextField.text = "상태 : 로그인 성공!!!";
                    Debug.Log($"로그인 성공 : {emailField.text}, {result.User.UserId}");
                    loginButton.interactable = true;

                    // 로딩 관련 UI 활성화
                    loginObjects.gameObject.SetActive(false);
                    loadingObjects.gameObject.SetActive(true);

                    // 퀘스트 딕셔너리 초기화
                    QuestUpdateFromDB();

                    // 비동기 로딩 실행
                    StartCoroutine(LoadTitleText());
                    StartCoroutine(LoadSceneAsync());
                }
            });
    }

    //! 비동기 로딩
    IEnumerator LoadSceneAsync()
    {
        // BSJ 테스트 씬으로 변경했음.
        AsyncOperation AsyncLoad = SceneManager.LoadSceneAsync(JSHTestScene);
        AsyncLoad.allowSceneActivation = false;

        int loadingGaugeCount = 0;

        while (loadingGaugeCount <= 99)
        {
            yield return loadingGauge;

            loadingGaugeText.text = loadingGaugeCount.ToString() + "%";
            loadingGaugeCount++;

            if (loadingGaugeCount > 99)
            {
                AsyncLoad.allowSceneActivation = true;
                loadingUIPannel.SetActive(false);
            }
        }
    }

    //! 비동기 로딩 타이틀 텍스트 출력
    IEnumerator LoadTitleText()
    {
        int textOutputCount = 0;

        while(true)
        {
            if(textOutputCount == 4)
            {
                textOutputCount = 0;
            }

            yield return loadingTitleTextChangeTime;

            string _loadTitleText = textOutputCount switch
            {
                0 => "로딩 중",
                1 => "로딩 중 .",
                2 => "로딩 중 ..",
                3 => "로딩 중 ...",
            };

            loadingTitleText.text = _loadTitleText;

            textOutputCount++;
        }
    }

    /// <summary>
    /// 인게임 데이터를 저장하는 메서드.
    /// </summary>
    public void SaveIngameData()
    {
        // 플레이어의 마지막 위치 DB에 업데이트
        Vector3 _lastPlayerPos = GameObject.FindAnyObjectByType<CharacterController>().transform.position;
        PlayerPosUpdateToDB(_lastPlayerPos);
    }

    /// <summary>
    /// 로그아웃하는 메서드.
    /// </summary>
    /// <param name="isGoLoginScene_">true면 저장후 로그인씬으로 이동, false면 게임 종료</param>
    public void LogOut(bool isGoLoginScene_)
    {
        // 인게임 데이터 저장
        SaveIngameData();

        // 로그아웃이 자동으로 된다.
        auth.SignOut();

        // { 로그인 관련 UI 활성화
        loadingUIPannel.gameObject.SetActive(true);
        loginObjects.gameObject.SetActive(true);
        loadingObjects.gameObject.SetActive(false);
        // } 로그인 관련 UI 활성화

        // 파이어 베이스 객체 초기화
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        // 상태 메세지 초기화
        infoTextField.text = "상태 : 대기 중 ...";

        // 로그인씬으로 돌아가거나 게임 종료가 되는 로직
        if(isGoLoginScene_)
        {
            SceneManager.LoadScene(LOGINSCENE);
        }

        else
        {
            StartCoroutine(WaitAndGameEnd());
        }

        // { LEGACY : BSJ 240102
        //// 유저데이터 비우기 231229
        //// userData = new Userdata(null);

        //// 아이템 갯수 초기화
        //PlayerInventory.instance.ItemInit();

        //// 모든 아이템 딕셔너리 클리어 ( 6은 아이템 타입 )
        //for(int i = 0; i < 6; i++)
        //{
        //    PlayerInventory.instance.ItemDictionaryInit(i);
        //}
        // } LEGACY : BSJ 240102
    }

    /// 3초 뒤에 게임이 꺼지는 코루틴
    IEnumerator WaitAndGameEnd()
    {
        yield return new WaitForSecondsRealtime(3f);
        Application.Quit();
    }

    /// <summary>
    /// 퍼즐에 대한 처음 DB를 만들어주는 메서드.
    /// </summary>
    public void MakePuzzleDB()
    {
        // 경로 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // 경로 재지정
        reference.Child(User).Child(userID).Child(Puzzle).SetValueAsync(FirebasePuzzle.Instance.PuzzleClearDictionary);
    }


    /// <summary>
    /// 퍼즐 클리어의 여부를 업데이트 하는 메서드.
    /// </summary>
    /// <param name="_key">퍼즐의 번호</param>
    /// <param name="_value">클리어 여부</param>
    public void PuzzleClearUpdateToDB(int _key, bool _value)
    {
        // 레퍼런스 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // 파이어베이스 경로 재설정
        reference = reference.Child(User).Child(userID).Child(Puzzle);

        // 업데이트할 데이터 생성
        Dictionary<string, object> updateDate = new Dictionary<string, object>();
        updateDate[_key.ToString()] = _value;

        // 퍼즐 노드 업데이트
        reference.UpdateChildrenAsync(updateDate).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("업데이트 완료");
            }
        });

        // 레퍼런스 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// RDB에서 퍼즐 클리어의 여부를 인게임내로 가져오는 메서드.
    /// </summary>
    public void PuzzleClearUpdateFromDB()
    {
        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        //데이터 가져오기(유저 ID를 찾아서 스냅샷으로 가져옴)
        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userID).Child(Puzzle);

        reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // 정상적으로 데이터를 가져왔다면?
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                
                // 인 게임내의 퍼즐 매니저에 데이터 캐싱하기
                foreach(var data in snapshot.Children) 
                {
                    int _key = Convert.ToInt32(data.Key);
                    bool _value = Convert.ToBoolean(data.Value);

                    PuzzleManager.instance.CheckPuzzleClear(_key, _value);
                }

                // 각 퍼즐 마다 클리어 팻말 세우기
                PuzzleManager.instance.ActiveSign();

                // 획득한 별의 총 갯수 설정
                PuzzleManager.instance.InitAllStarCount();
            }
        });

        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// 플레이어 위치에 대한 처음 DB를 만들어주는 메서드.
    /// </summary>
    public void MakePlayerPosDB()
    {
        // 경로 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // 플레이어 포지션
        Dictionary<string, float> playerPosition = new Dictionary<string, float>();

        playerPosition.Add(X, playerXPos);
        playerPosition.Add(Y, playerYPos);
        playerPosition.Add(Z, playerZPos);

        // 경로 재지정 후 x, y, z 노드 생성하기.
        reference.Child(User).Child(userID).Child(POSITION).SetValueAsync(playerPosition);
    }

    /// <summary>
    /// 플레이어의 위치를 업데이트하는 메서드.
    /// </summary>
    public void PlayerPosUpdateToDB(Vector3 _PlayerPosition)
    {
        // 레퍼런스 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // 파이어베이스 경로 재설정
        reference = reference.Child(User).Child(userID).Child(POSITION);

        // 플레이어 마지막 위치 업데이트
        playerXPos = _PlayerPosition.x;
        playerYPos = _PlayerPosition.y;
        playerZPos = _PlayerPosition.z;

        // 업데이트할 데이터 생성
        Dictionary<string, object> updateDate = new Dictionary<string, object>();
        updateDate.Add(X, playerXPos);
        updateDate.Add(Y, playerYPos);
        updateDate.Add(Z, playerZPos);

        // 플레이어 위치 노드 업데이트
        reference.UpdateChildrenAsync(updateDate).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("플레이어 위치 업데이트 완료");
            }
        });

        // 레퍼런스 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// RDB에서 플레이어의 위치를 인게임내로 가져오는 메서드.
    /// </summary>
    public void PlayerPosUpdateFromDB(Transform playerTransform_)
    {
        float _xPos = 0;
        float _yPos = 0;
        float _zPos = 0;

        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        //데이터 가져오기(유저 ID를 찾아서 스냅샷으로 가져옴)
        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userID).Child(POSITION);

        reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // 정상적으로 데이터를 가져왔다면?
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                var DB_xPos = snapshot.Child(X).Value;
                var DB_yPos = snapshot.Child(Y).Value;
                var DB_zPos = snapshot.Child(Z).Value;

                string str_xPos = Convert.ToString(DB_xPos);
                string str_yPos = Convert.ToString(DB_yPos);
                string str_zPos = Convert.ToString(DB_zPos);

                _xPos = float.Parse(str_xPos);
                _yPos = float.Parse(str_yPos);
                _zPos = float.Parse(str_zPos);

                // DB에 있는 플레이어의 마지막 위치 캐싱
                playerTransform_.position = new Vector3(_xPos, _yPos, _zPos);
                Debug.Log(lastPlayerPos);
            }
        });

        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// 플레이어 MBTI에 대한 처음 DB를 만들어주는 메서드.
    /// </summary>
    public void MakeMbtiDB()
    {
        // 경로 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // MBTI
        Dictionary<string, float> playerMBTI = new Dictionary<string, float>();

        playerMBTI.Add(Define.MBTI_E, 0f);
        playerMBTI.Add(Define.MBTI_I, 0f);
        playerMBTI.Add(Define.MBTI_N, 0f);
        playerMBTI.Add(Define.MBTI_S, 0f);
        playerMBTI.Add(Define.MBTI_T, 0f);
        playerMBTI.Add(Define.MBTI_F, 0f);
        playerMBTI.Add(Define.MBTI_J, 0f);
        playerMBTI.Add(Define.MBTI_P, 0f);

        // 경로 재지정 후 x, y, z 노드 생성하기.
        reference.Child(User).Child(userID).Child(MBTI).SetValueAsync(playerMBTI);
    }

    /// <summary>
    /// 파이어베이스 MBTI의 값을 업데이트하는 메서드.
    /// </summary>
    public void PlayerMbtiUpdateToDB(string _key, float _value)
    {
        // 레퍼런스 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // 파이어베이스 경로 재설정
        reference = reference.Child(User).Child(userID).Child(MBTI);

        // 업데이트할 데이터 생성
        Dictionary<string, object> updateDate = new Dictionary<string, object>();
        updateDate[_key.ToString()] = _value;

        // 퍼즐 노드 업데이트
        reference.UpdateChildrenAsync(updateDate).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("MBTI 업데이트 완료");
            }
        });

        // 레퍼런스 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// RDB에서 MBTI의 값을 인게임 내로 가져오는 메서드.
    /// </summary>
    public void MbtiUpdateFromDB()
    {
        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        //데이터 가져오기(유저 ID를 찾아서 스냅샷으로 가져옴)
        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userID).Child(MBTI);

        reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // 정상적으로 데이터를 가져왔다면?
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                var DB_MBTI_E = snapshot.Child(Define.MBTI_E).Value;
                var DB_MBTI_I = snapshot.Child(Define.MBTI_I).Value;
                var DB_MBTI_N = snapshot.Child(Define.MBTI_N).Value;
                var DB_MBTI_S = snapshot.Child(Define.MBTI_S).Value;
                var DB_MBTI_T = snapshot.Child(Define.MBTI_T).Value;
                var DB_MBTI_F = snapshot.Child(Define.MBTI_F).Value;
                var DB_MBTI_J = snapshot.Child(Define.MBTI_J).Value;
                var DB_MBTI_P = snapshot.Child(Define.MBTI_P).Value;

                string str_MBTI_E = Convert.ToString(DB_MBTI_E);
                string str_MBTI_I = Convert.ToString(DB_MBTI_I);
                string str_MBTI_N = Convert.ToString(DB_MBTI_N);
                string str_MBTI_S = Convert.ToString(DB_MBTI_S);
                string str_MBTI_T = Convert.ToString(DB_MBTI_T);
                string str_MBTI_F = Convert.ToString(DB_MBTI_F);
                string str_MBTI_J = Convert.ToString(DB_MBTI_J);
                string str_MBTI_P = Convert.ToString(DB_MBTI_P);

                MBTI_E = float.Parse(str_MBTI_E);
                MBTI_I = float.Parse(str_MBTI_I);
                MBTI_N = float.Parse(str_MBTI_N);
                MBTI_S = float.Parse(str_MBTI_S);
                MBTI_T = float.Parse(str_MBTI_T);
                MBTI_F = float.Parse(str_MBTI_F);
                MBTI_J = float.Parse(str_MBTI_J);
                MBTI_P = float.Parse(str_MBTI_P);
            }
        });

        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }


    /// <summary>
    /// 퀘스트에 대한 처음 DB를 만들어주는 메서드.
    /// </summary>
    public void MakeQuestDB()
    {
        // 퀘스트 딕셔너리 초기화
        InitQuestDictionary();

        // 경로 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // 경로 재지정 후 퀘스트 노드 생성하기.
        reference.Child(User).Child(userID).Child(QUEST).SetValueAsync(QuestClearDictionary);
    }

    /// <summary>
    /// 파이어베이스 퀘스트의 값을 업데이트하는 메서드.
    /// </summary>
    public void QuestUpdateToDB(int _key, bool _value)
    {
        // 레퍼런스 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        // 파이어베이스 경로 재설정
        reference = reference.Child(User).Child(userID).Child(QUEST);

        // 업데이트할 데이터 생성
        Dictionary<string, object> updateDate = new Dictionary<string, object>();
        updateDate[_key.ToString()] = _value;

        // 퍼즐 노드 업데이트
        reference.UpdateChildrenAsync(updateDate).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("퀘스트 업데이트 완료");
            }
        });

        // 레퍼런스 초기화
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    /// <summary>
    /// RDB에서 MBTI의 값을 인게임 내로 가져오는 메서드.
    /// </summary>
    public void QuestUpdateFromDB()
    {
        // 퀘스트 딕셔너리 초기화
        InitQuestDictionary();

        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        //데이터 가져오기(유저 ID를 찾아서 스냅샷으로 가져옴)
        reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userID).Child(QUEST);

        reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            // 정상적으로 데이터를 가져왔다면?
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                // QuestClearDictionary 딕셔너리에 데이터 가져오기.
                for (int i = 0; i < quest_Table.dataArray.Length; i++) 
                {
                    var DB_QuestClear = snapshot.Child((quest_Table.dataArray[i].ID).ToString()).Value;
                    bool isQuestClear = Convert.ToBoolean(DB_QuestClear);

                    if (QuestClearDictionary.ContainsKey(quest_Table.dataArray[i].ID))
                    {
                        QuestClearDictionary[quest_Table.dataArray[i].ID] = isQuestClear;
                    }

                    else
                    {
                        QuestClearDictionary.Add(quest_Table.dataArray[i].ID, isQuestClear);
                    }
                }
            }
        });

        // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }


    /// <summary>
    /// 퀘스트 데이터 딕셔너리 초기화
    /// </summary>
    private void InitQuestDictionary()
    {
        QuestClearDictionary = new Dictionary<int, bool> { };

        for (int i = 0; i < quest_Table.dataArray.Length; i++)
        {
            QuestClearDictionary.Add(quest_Table.dataArray[i].ID, false);
        }
    }

    #endregion

    // { LEGACY : BSJ 231229
    /// <summary>
    /// Json 파일로 변환해서 새로운 DB 생성 후 저장
    /// </summary>
    //public void WriteDB()
    //{
    //    // 비 로그인시 리턴
    //    if (userData.UserID == null) { Debug.Log("로그인을 먼저 해주세요"); return; }

    //    // 이미 유저데이터가 있다면 반환
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID.ToString());
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            // Json 파일로 변환해서
    //            string jsondate1 = JsonUtility.ToJson(userData);

    //            // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
    //            reference = FirebaseDatabase.DefaultInstance.RootReference;

    //            // User 안에 유저데이터 안에 json 파일로 데이터를 DB에 저장
    //            reference.Child(User).Child(userData.UserID.ToString()).SetRawJsonValueAsync(jsondate1);
    //        }
    //    });
    //}

    // LEGACY : BSJ 231229
    /// <summary>
    /// 파이어 베이스에서 데이터 읽어오는 함수. TEST: v.01
    /// </summary>
    //public void ReadDB()
    //{
    //    // 비 로그인시 리턴
    //    if (userData.UserID == null) { Debug.Log("로그인을 먼저 해주세요"); return; }

    //    // 데이터 가져오기 (유저 ID를 찾아서 스냅샷으로 가져옴)
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID.ToString());
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        // 정상적으로 데이터를 가져왔다면?
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            IDictionary UserData = (IDictionary)snapshot.Value;
    //            Debug.Log($"아이디 : {UserData["UserID"]}" + $"정보 : {UserData["UserNickName"]}");
    //        }
    //    });

    //    // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
    //    reference = FirebaseDatabase.DefaultInstance.RootReference;
    //}
    // }

    /// <summary>
    /// 파이어 베이스에 저장된 DB 지우는 함수.
    /// </summary>
    //public void RemoveDB()
    //{
    //    FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).RemoveValueAsync();

    //    // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
    //    reference = FirebaseDatabase.DefaultInstance.RootReference;
    //}

    /// <summary>
    /// Json 파일로 생성하는 함수.
    /// </summary>
    //public void SaveJsonFile()
    //{
    //    File.WriteAllText(Application.dataPath + "/TestJson.json", JsonUtility.ToJson(userData));
    //}

    // { BSJ _ 231229 LEGACY 메서드 ( 인벤토리 관련 메서드 ) 주석 처리 했습니다.
    #region LEGACY : 닉네임 DB 변경 함수
    // LEGACY : 닉네임 사용 여부 확실치 않음.
    ///// <summary>
    ///// 닉네임 DB 변경 함수
    ///// </summary>
    ///// <param name="_nickName"></param>
    //public void SetDB_NickName(string _nickName)
    //{ 
    //    reference.Child(User).Child(userData.UserID.ToString()).Child(UserNickname).SetValueAsync(_nickName);

    //    // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
    //    reference = FirebaseDatabase.DefaultInstance.RootReference;
    //}
    #endregion

    #region LEGACY : 파이어베이스 DB에 플레이어의 인벤토리 여부 저장시키는 함수.
    // LEGACY : 인벤토리 사라짐.
    ///// <summary>
    ///// 파이어베이스 DB에 플레이어의 인벤토리 여부 저장시키는 함수.
    ///// </summary>
    ///// <param name="_itemType"></param>
    //public void UpdateFirebaseInventory(int _itemType)
    //{
    //    // 딕셔너리 초기화 -> 현재 인게임 내 아이템 동기화 한 후에 저장
    //    PlayerInventory.instance.ItemDictionaryInit(_itemType);
    //    PlayerInventory.instance.SetItemsDictionry(_itemType);

    //    switch (_itemType)
    //    {
    //        case 0:
    //            var dataToSave00 = PlayerInventory.instance.itemType0;
    //            reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE0).SetValueAsync(dataToSave00);
    //            break;

    //        case 1:
    //            var dataToSave01 = PlayerInventory.instance.itemType1;
    //            reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE1).SetValueAsync(dataToSave01);
    //            break;

    //        case 2:
    //            var dataToSave02 = PlayerInventory.instance.itemType2;
    //            reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE2).SetValueAsync(dataToSave02);
    //            break;

    //        case 3:
    //            var dataToSave03 = PlayerInventory.instance.itemType3;
    //            reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE3).SetValueAsync(dataToSave03);
    //            break;

    //        case 4:
    //            var dataToSave04 = PlayerInventory.instance.itemType4;
    //            reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE4).SetValueAsync(dataToSave04);
    //            break;

    //        case 5:
    //            var dataToSave05 = PlayerInventory.instance.itemType5;
    //            reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE5).SetValueAsync(dataToSave05);
    //            break;

    //        case 6:
    //            var dataToSave06 = PlayerInventory.instance.itemType6;
    //            reference.Child(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE6).SetValueAsync(dataToSave06);
    //            break;

    //        default: break;
    //    }
    //}
    #endregion

    #region LEGACY : 파이어베이스에서 DB 데이터 가져오는 함수. TEST: v.02
    // LEGACY : 인벤토리 사라짐.
    ///// <summary>
    ///// 파이어베이스에서 DB 데이터 가져오는 함수. TEST: v.02
    ///// </summary>
    //public void UpdatePlayerInventory()
    //{
    //    // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
    //    reference = FirebaseDatabase.DefaultInstance.RootReference;

    //    // 데이터 가져오기 (유저 ID의 인벤토리 항목 스냅샷으로 가져옴)
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE1);
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if(task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            IDictionary itemData = (IDictionary)snapshot.Value;

    //            // 테스트용
    //            Debug.Log($"{itemData["apple"]}");

    //            // TODO: 잘 Read 되었으면 다음 할 일
    //        }
    //    });

    //    // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
    //    reference = FirebaseDatabase.DefaultInstance.RootReference;
    //}
    #endregion

    #region LEGACY : 로그인 시 파이어 베이스 DB에서 모든 아이템 받아오는 함수.
    // LEGACY : 인벤토리 사라짐.
    ///// <summary>
    ///// 로그인 시 파이어 베이스 DB에서 모든 아이템 받아오는 함수.
    ///// </summary>
    //public void InitInventoryFromDB()
    //{
    //    // 데이터 가져오기 (유저 ID의 인벤토리 항목 스냅샷으로 가져옴)
    //    #region 아이템 타입 0
    //    // 아이템 타입 0
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID.ToString()).Child(INVENTORY).Child(ITEMTYPE0);
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            IDictionary ItemData = (IDictionary)snapshot.Value;

    //            // 아이템 타입 0
    //            PlayerInventory.instance.wood = Convert.ToInt32(ItemData["wood"]);    // ItemData["apple"]이 long 타입이라 Convert로 정수형으로 변환해야함.
    //            PlayerInventory.instance.stone = Convert.ToInt32(ItemData["stone"]);
    //            PlayerInventory.instance.iron = Convert.ToInt32(ItemData["iron"]);
    //        }
    //        else
    //        {   /*Do Nothing*/    }
    //    });
    //    #endregion

    //    #region 아이템 타입 1
    //    // 아이템 타입 1
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE1);
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            IDictionary itemData = (IDictionary)snapshot.Value;

    //            // 아이템 타입 1은 아직 추가되지 않음.
    //        }
    //        else
    //        {   /*Do Nothing*/    }
    //    });
    //    #endregion

    //    #region 아이템 타입 2
    //    // 아이템 타입 2
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE2);
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            IDictionary itemData = (IDictionary)snapshot.Value;

    //            // 아이템 타입 2
    //            PlayerInventory.instance.ice = Convert.ToInt32(itemData["ice"]);
    //        }
    //        else
    //        {   /*Do Nothing*/    }
    //    });
    //    #endregion

    //    #region 아이템 타입 3
    //    // 아이템 타입 3
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE3);
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            IDictionary itemData = (IDictionary)snapshot.Value;

    //            // 아이템 타입 3
    //            PlayerInventory.instance.oil = Convert.ToInt32(itemData["oil"]);
    //        }
    //        else
    //        {   /*Do Nothing*/    }
    //    });
    //    #endregion

    //    #region 아이템 타입 4
    //    // 아이템 타입 4
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE4);
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            IDictionary itemData = (IDictionary)snapshot.Value;

    //            // 아이템 타입 4
    //            PlayerInventory.instance.torch = Convert.ToInt32(itemData["torch"]);
    //            PlayerInventory.instance.flashlight = Convert.ToInt32(itemData["flashlight"]);
    //            PlayerInventory.instance.brush = Convert.ToInt32(itemData["brush"]);
    //        }
    //        else
    //        {   /*Do Nothing*/    }
    //    });
    //    #endregion

    //    #region 아이템 타입 5
    //    // 아이템 타입 5
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE5);
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            IDictionary itemData = (IDictionary)snapshot.Value;

    //            // 아이템 타입 5
    //            PlayerInventory.instance.toyParts = Convert.ToInt32(itemData["toyParts"]);
    //            PlayerInventory.instance.bond = Convert.ToInt32(itemData["bond"]);
    //            PlayerInventory.instance.repairedToy = Convert.ToInt32(itemData["repairedToy"]);
    //            PlayerInventory.instance.cookie = Convert.ToInt32(itemData["cookie"]);
    //            PlayerInventory.instance.wrappingPaper = Convert.ToInt32(itemData["wrappingPaper"]);
    //            PlayerInventory.instance.deliciousSnack = Convert.ToInt32(itemData["deliciousSnack"]);
    //            PlayerInventory.instance.thickThread = Convert.ToInt32(itemData["thickThread"]);
    //            PlayerInventory.instance.knittingNeedles = Convert.ToInt32(itemData["knittingNeedles"]);
    //            PlayerInventory.instance.warmShawl = Convert.ToInt32(itemData["warmShawl"]);
    //            PlayerInventory.instance.film = Convert.ToInt32(itemData["film"]);
    //            PlayerInventory.instance.cameraWithoutFilm = Convert.ToInt32(itemData["cameraWithoutFilm"]);
    //            PlayerInventory.instance.camera = Convert.ToInt32(itemData["camera"]);
    //        }
    //        else
    //        {   /*Do Nothing*/    }
    //    });
    //    #endregion

    //    #region 아이템 타입 6
    //    // 아이템 타입 6
    //    reference = FirebaseDatabase.DefaultInstance.GetReference(User).Child(userData.UserID).Child(INVENTORY).Child(ITEMTYPE6);
    //    reference.GetValueAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCompleted)
    //        {
    //            DataSnapshot snapshot = task.Result;
    //            IDictionary itemData = (IDictionary)snapshot.Value;

    //            // 아이템 타입 6
    //            PlayerInventory.instance.spiritAxe = Convert.ToInt32(itemData["spiritAxe"]);
    //            PlayerInventory.instance.magicMap = Convert.ToInt32(itemData["magicMap"]);
    //        }
    //        else
    //        {   /*Do Nothing*/    }
    //    });
    //    #endregion

    //    // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
    //    reference = FirebaseDatabase.DefaultInstance.RootReference;
    //}
    #endregion

    #region LEGACY : 예전 로그인 메서드.
    //LEGACY :
    //public void Login()
    //{
    //    //제공되는 함수 : 이메일과 비밀번호로 로그인 시켜 줌
    //    auth.SignInWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWith(
    //        task =>
    //        {
    //            if (task.IsCanceled)
    //            {
    //                Debug.Log("로그인 캔슬됨.\n");
    //                return;
    //            }

    //            if (task.IsFaulted)
    //            {
    //                Debug.Log("로그인 오류 발생\n" + task.Exception);
    //                return;
    //            }

    //            AuthResult result = task.Result;
    //            Debug.Log($"로그인 성공 : {emailField.text}, {result.User.UserId}");

    //            // 로그인 성공 시 유저 데이터 초기값 셋팅
    //            userData = new Userdata(result.User.UserId.ToString(), nickNameField.text);

    //            // 레퍼런스 초기화 (초기화 안하면 새로 덮어 씌워짐)
    //            reference = FirebaseDatabase.DefaultInstance.RootReference;

    //            // 로그인 시 파이어 베이스 DB에서 아이템 받아오기
    //            InitInventoryFromDB();

    //            // 인게임 내 인벤토리로 동기화 (첫 동기화 이기 떄문에 모든 아이템 동기화해주어야 한다.) 6은 아이템 타입
    //            for (int i = 0; i <= 6; i++)
    //            {
    //                PlayerInventory.instance.ItemDictionaryInit(i);
    //                PlayerInventory.instance.SetItemsDictionry(i);
    //            }
    //        }
    //    );

    //    // 아이템 박스 활성화
    //    itemBoxes.SetActive(true);


    //    // 캔버스 비활성화 전환
    //    loginCanvas.transform.gameObject.SetActive(false);
    //}
    #endregion
    // } BSJ _ 231229 LEGACY 메서드 ( 인벤토리 관련 메서드 ) 주석 처리 했습니다.
}