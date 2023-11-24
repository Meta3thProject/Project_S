using System;
using UnityEngine;

public static partial class GFunc
{
    // ! Enum을 Int로 파싱하는 함수 
    public static int EnumToInt<T>(this T _parameter) where T : Enum
    {        
        if( _parameter != null)
        {
            int tempParameter = default;
            tempParameter = Convert.ToInt32(_parameter);
            return tempParameter;
        }       // if : _parameter가 비어 있지 않다면 변환 된 값을 리턴함
        return -1;
        // 아닌 경우 NONE에 해당하는 -1을 반환 
    }       // ParseToInt()


}