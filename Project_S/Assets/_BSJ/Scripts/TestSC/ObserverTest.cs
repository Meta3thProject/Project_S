using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ObserverPattern
{
    public interface ISubject
    {
        //옵저버 등록
        void ResisterObserver(IObserver obsever);
        // 옵저버 제거
        void RemoveObserver(IObserver observer);
        // 옵저버들에게 내용 전달
        void NotifyObservers();
    }

    public interface IObserver
    {
        // 주행거리, 연료량 정보 업데이트
        void UpdateData(float mileage, float amountFuel);
    }

    public class ObserverTest : MonoBehaviour, ISubject
    {
        private List<IObserver> list_Observers = new List<IObserver>();

        // 마일리지 데이터
        private float mileage;

        // 연료량 데이터
        private float fuelAmount;

        /// <summary>
        /// 옵저버 등록 함수.
        /// </summary>
        /// <param name="observer"></param>
        public void ResisterObserver(IObserver observer)
        {
            list_Observers.Add(observer);
        }

        /// <summary>
        /// 옵저버 해지 함수.
        /// </summary>
        /// <param name="observer"></param>
        public void RemoveObserver(IObserver observer)
        {
            list_Observers.Remove(observer);
        }

        /// <summary>
        /// 옵저버들에게 정보 전달 함수
        /// </summary>
        public void NotifyObservers()
        {
            foreach(IObserver observer in list_Observers)
            {
                observer.UpdateData(mileage, fuelAmount);
            }
        }

        /// <summary>
        /// 자동차 소프트웨어로부터 업데이트된 정보를 받는 함수
        /// </summary>
        /// <param name="newMileage">갱신된 마일리지 정보.</param>
        /// <param name="newFuelAmount">갱신된 연료량 정보.</param>
        public void UpdataData(float newMileage, float newFuelAmount)
        {
            mileage = newMileage;
            fuelAmount = newFuelAmount;
            NotifyObservers();
        }
    }
}

