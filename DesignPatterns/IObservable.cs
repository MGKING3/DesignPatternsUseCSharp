using com.mg.Utils.DesignPatterns.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mg.Utils.DesignPatterns.ObserverPattern
{
    /// <summary>
    /// 被观察者基础接口，提供添加、删除观察者的函数接口，
    /// 但是该接口不能直接使用，需要使用IObservable接口
    /// <para>
    /// 2016.12.22 By MG
    /// </para>
    /// </summary>
    /// <typeparam name="TIObserver">观察者接口</typeparam>
    interface IObservableBase<TIObserver>
    {
        /// <summary>
        /// 添加观察者
        /// </summary>
        /// <param name="observer">观察者</param>
        /// <returns>添加成功与否</returns>
        bool addObserver(TIObserver observer);
        /// <summary>
        /// 删除观察者
        /// </summary>
        /// <param name="observer">观察者</param>
        /// <returns>删除成功与否</returns>
        bool removeObserver(TIObserver observer);
    }

    /// <summary>
    /// 被观察者接口（拉方式）
    /// <para>
    /// 2016.12.22 By MG
    /// </para>
    /// </summary>
    /// <typeparam name="TIObserver">观察者接口（拉方式）</typeparam>
    interface IObservable<TIObserver> : IObservableBase<TIObserver>
    {
        /// <summary>
        /// 被观察者数据变化时调用，通知观察者（拉方式）
        /// </summary>
        void notifyObserver();
    }

    /// <summary>
    /// 被观察者接口（推方式）
    /// <para>
    /// 2016.12.22 By MG
    /// </para>
    /// </summary>
    /// <typeparam name="TIObserver">观察者接口（推方式）</typeparam>
    /// <typeparam name="TIDataPack">被观察者推的数据</typeparam>
    interface IObservable<TIObserver, TIDataPack> : IObservableBase<TIObserver>
    {
        /// <summary>
        /// 被观察者数据变化时调用，通知观察者（推方式）
        /// </summary>
        /// <param name="dataPack">包含被观察者推给观察者的数据</param>
        void notifyObserverPush(TIDataPack dataPack);
    }
}
