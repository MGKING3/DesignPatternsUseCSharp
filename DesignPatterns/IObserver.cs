using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mg.Utils.DesignPatterns.ObserverPattern
{
    /// <summary>
    /// 观察者接口（拉方式）
    /// <para>
    /// 2016.12.22 By MG
    /// </para>
    /// </summary>
    /// <typeparam name="TIObservable">被观察者接口（拉方式）</typeparam>
    interface IObserver<TIObservable>
    {
        /// <summary>
        /// 被观察者更新数据时调用，观察者应对数据变化的函数应在这里定义（拉方式）
        /// </summary>
        /// <param name="observable">被观察者，观察者可以通过该被观察者开放的接口获得所需数据</param>
        void updateDataPull(TIObservable observable);
    }
    /// <summary>
    /// 观察者接口（推方式）
    /// <para>
    /// 2016.12.22 By MG
    /// </para>
    /// </summary>
    /// <typeparam name="TIObservable">被观察者接口（推方式）</typeparam>
    /// <typeparam name="TIDataPack">被观察者推的数据</typeparam>
    interface IObserver<TIObservable, TIDataPack>
    {
        /// <summary>
        /// 被观察者更新数据时调用，观察者应对数据变化的函数应在这里定义（推方式）
        /// </summary>
        /// <param name="observable">被观察者</param>
        /// <param name="dataPack">被观察者推的数据</param>
        void updateDataPush(TIObservable observable, TIDataPack dataPack);
    }
}
