//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Firebase.Auth;
//using UnityEngine.UI;
//using TMPro;
//using Unity.VisualScripting;

//public static class PlayerUser
//{
//    public static string userID;
//    public static string userNickName;

//    // LEGACY:
//    // public static string username;

//    public static void Initialize_User(string userID)
//    {
//        PlayerUser.userID = userID;

//        // LEGACY:
//        // User.username = username;
//    }
//}

//public class FirebaseAuthManager : MonoBehaviour
//{
//    public static FirebaseAuthManager instance;

//    private FirebaseAuth auth;  // 로그인 | 회원가입 등에 사용 => 인증을 관리할 객체
//    private FirebaseUser user;  // 인증이 완료된 유저 정보

//    [SerializeField] private TMP_InputField emailField;         // 이메일 입력받을 필드
//    [SerializeField] private TMP_InputField passField;          // 비밀번호 입력받을 필드
//    [SerializeField] private TMP_InputField nickNameField;      // 닉네임 입력받을 필드

//    void Awake()
//    {
//        instance = this;

//        // 객체 초기화
//        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

//    }

//    public void register()
//    {
//        // 제공되는 함수 : 이메일과 비밀번호로 회원가입 시켜 줌
//        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passField.text).ContinueWith(
//            task => {
//                if(task.IsCanceled) 
//                {
//                    Debug.Log("회원가입 캔슬됨.\n");
//                    return;
//                }

//                if(task.IsFaulted) 
//                {
//                    Debug.Log("오류 발생\n" + task.Exception);
//                    return;
//                }

//                AuthResult result = task.Result;
//                Debug.Log($"회원가입 성공 : {result.User.DisplayName}, {result.User.UserId}");
//            }
//            );
//    }

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
//                PlayerUser.Initialize_User(result.User.UserId.ToString());

//                // 닉네임 셋팅
//                PlayerUser.userNickName = nickNameField.text;

//            }
//        );
//    }

//    public void LogOut()
//    {
//        auth.SignOut(); // 로그아웃이 자동으로 된다.
//        Debug.Log("로그아웃");
//        PlayerUser.userID = null;
//    }
//}