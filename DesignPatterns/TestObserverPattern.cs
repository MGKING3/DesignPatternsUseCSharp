//定义NORMAL，表示使用普通方法
//#define NORMAL
//如果没有定义NORMAL，则定义EVENT，代表使用事件方法
#if !NORMAL
#define EVENT
#endif
//定义PUSH，表示使用推方式
//#define PUSH
//如果没有定义PUSH，则定义PULL，表示使用拉方式
#if !PUSH
#define PULL
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.mg.Utils.DesignPatterns.ObserverPattern;

namespace com.mg.Test.DesignPatterns
{
    /// <summary>
    ///  【观察者模式】：
    ///  <para>
    ///  定义一个信息中心（被观察者），多个观察者通过注册注册到该被观察者。
    ///  那么观察者可以在被观察者数据变化时得到提示消息，然后观察者再根据所需，
    ///  可以有多种处理该类变化后的数据的方法。
    ///  </para>
    ///  <para>
    ///  观察者模式有两种发送消息和接收消息的方式：推（push）和拉（pull）
    ///  1.推：当被观察者的数据有更新的时候，被观察者会通知观察者，并将想传的数据传给观察者。
    ///  因为是被观察者主导的，所以被观察者有决定传哪些数据的权力，这样有利于被观察者的封装性，
    ///  但是有一个问题就是，观察者可以有很多，所需的数据也不尽相同，众口难调，所以被观察者就
    ///  需要传所有观察者可能需要的数据，这样，传的数据，相对来说，量就变大了。
    ///  2.拉：当被观察者的数据有更新的时候，被观察者会通知观察者，但是没有传数据给观察者，而
    ///  是让观察者按需来获取。这个时候，被观察者就需要保证自己的封装性，又要保证提供给观察者
    ///  足够多的接口，以便观察者能获取所需的数据。
    ///  </para>
    ///  <para>
    ///  举个例子：有一家报社newspaper office，拥有许多的订阅者subscriber，
    ///  该newspaper office固定每天向各个subscriber投送报纸，让各个subscriber
    ///  知道每天的新闻、时事，也可以通知各个subscriber报纸的售价改变等等等等。
    ///  
    ///  而这里，双方都有着各自的权限了：
    ///  newspaper office方面，可以取消各个subscriber的订阅（即注销任一的注册者）
    ///  而subscriber方面，可以自行选择订阅和取消订阅（即可以注册和注销），
    ///  而且也可以同时订阅多家newspaper office的报纸（即一个观察者可以同时观察多个被观察者）
    ///  </para>
    ///  <para>
    ///  观察者模式的一个典型用法就是MVC（Model-View-Controller）模式，即（模型-视图-控制器）模式
    ///  </para>
    ///  <para>
    ///  2016.12.22
    ///  By MG
    ///  </para>
    /// </summary>
    class TestObserverPattern
    {
        /*
         以下以上述的报社和订阅者的例子（只针对“售价改变”这一个情况）对【观察者模式】进行说明：
         一种是用普通的方法进行实现，再一种是用C#的event来实现。
         但是，经过测试，普通方法不可取，因为普通方法违反了封装性，导致任何人、事、物可以“冒充”
         报社来通知订阅者，而event则可以保证封装性。但是普通方法可以限定观察者是什么类型（实现接
         口时），而event可以被任何类所观察，灵活。详细代码如下：
         */
        public static void test()
        {
#if NORMAL

            #region 测试普通方法（不推荐使用）

            //定义报社和订阅者A、B
            NewspaperOffice no = new NewspaperOffice("天天报社");
            Subscriber subA = new Subscriber("订阅者A");
            Subscriber subB = new Subscriber("订阅者B");
            //注册订阅者A、B
            no.addObserver(subA);
            no.addObserver(subB);
#if PUSH
            //推方式
            //A和B都有订阅的情况，修改《天天下午茶》的售价
            Console.WriteLine("第一次修改：将《天天下午茶》的售价改为8");
            no["天天下午茶"] = 8;
            Console.WriteLine();
            //只有A订阅的情况，修改《天天晚报》的售价
            //注销订阅者B
            no.removeObserver(subB);
            Console.WriteLine("注销订阅者B");
            Console.WriteLine("第二次修改：将《天天晚报》的售价改为6");
            no["天天晚报"] = 6;

            /*
            测试普通方法的触发环境问题
            以下代码能通过编译且能成功运行，但是这样不符合封装性，
            本来notifyObserverPush函数只能由NewspaperOffice调用，
            即只有报社本身才能在数据改变的时候通知所有的订阅者（推方式），
            而不是任由其他人或事物在其它时间段、其它位置调用！！
            因此普通方法不可取，而改进方法是不对外公开notifyObserverPush函数接口，
            但是，这样的话，势必需要将notifyObserverPush函数封装在被观察者里，
            那样的话，就如《Head First Design Patterns》里提及的Java中使用
            java.uitl.Observer和java.util.Observable一般，被观察者需要继承Observable，
            丧失了松耦合。如果在C#中想要用这种普通方法的话，需要建立一个基础的被观察者类，
            让需要实现被观察者功能的类继承该类，这样能保证安全调用notifyObserverPush函数，
            但是却丧失了松耦合。综上，该方法不可取
            */
            Console.WriteLine();
            no.notifyObserverPush(new NewspaperOffice("某某报社"),"不安全触发！！！");
#elif PULL
            //拉方式
            //A和B都有订阅的情况，修改《天天下午茶》的售价
            Console.WriteLine("第一次修改：将《天天下午茶》的售价改为18");
            no["天天下午茶"] = 18;
            Console.WriteLine();
            //只有A订阅的情况，修改《天天晚报》的售价
            //注销订阅者B
            no.removeObserver(subB);
            Console.WriteLine("注销订阅者B");
            Console.WriteLine("第二次修改：将《天天晚报》的售价改为16");
            no["天天晚报"] = 16;

            /*
            测试普通方法的触发环境问题
            以下代码能通过编译且能成功运行，但是这样不符合封装性，
            本来notifyObserver函数只能由NewspaperOffice调用，
            即只有报社本身才能在数据改变的时候通知所有的订阅者（拉方式），
            而不是任由其他人或事物在其它时间段、其它位置调用！！
            因此普通方法不可取，而改进方法是不对外公开notifyObserver函数接口，
            但是，这样的话，势必需要将notifyObserver函数封装在被观察者里，
            那样的话，就如《Head First Design Patterns》里提及的Java中使用
            java.uitl.Observer和java.util.Observable一般，被观察者需要继承Observable，
            丧失了松耦合。如果在C#中想要用这种普通方法的话，需要建立一个基础的被观察者类，
            让需要实现被观察者功能的类继承该类，这样能保证安全调用notifyObserver函数，
            但是却丧失了松耦合。综上，该方法不可取
            */
            Console.WriteLine("\n不安全触发！！！");
            no.notifyObserverPull(new NewspaperOffice("某某报社"));
#endif
            #endregion

#elif EVENT

            #region 测试事件方法（推荐使用）

            //定义报社和订阅者A、B
            NewspaperOffice no = new NewspaperOffice("天天报社");
            Subscriber subA = new Subscriber("订阅者A");
            Subscriber subB = new Subscriber("订阅者B");
#if PUSH
            //推方式
            //注册订阅者A、B
            no.pushHolder += subA.updateDataPush;
            no.pushHolder += subB.updateDataPush;
            //A和B都有订阅的情况，修改《天天下午茶》的售价
            Console.WriteLine("第一次修改：将《天天下午茶》的售价改为8");
            no["天天下午茶"] = 8;
            Console.WriteLine();
            //只有A订阅的情况，修改《天天晚报》的售价
            //注销订阅者B
            no.pushHolder -= subB.updateDataPush;
            Console.WriteLine("注销订阅者B");
            Console.WriteLine("第二次修改：将《天天晚报》的售价改为6");
            no["天天晚报"] = 6;

            /*
            测试事件方法的触发环境问题
            以下代码无法通过编译，因为event仅能在定义它的类中触发
            （即在NewspaperOffice中才能触发），在其它位置调用
            只能使用+=或-=
            */
            //no.pushHolder(no,"不安全触发！！！");
#elif PULL
            //拉方式
            //注册订阅者A、B
            no.pullHolder += subA.updateDataPull;
            no.pullHolder += subB.updateDataPull;
            //A和B都有订阅的情况，修改《天天下午茶》的售价
            Console.WriteLine("第一次修改：将《天天下午茶》的售价改为18");
            no["天天下午茶"] = 18;
            Console.WriteLine();
            //只有A订阅的情况，修改《天天晚报》的售价
            //注销订阅者B
            no.pullHolder -= subB.updateDataPull;
            Console.WriteLine("注销订阅者B");
            Console.WriteLine("第二次修改：将《天天晚报》的售价改为16");
            no["天天晚报"] = 16;

            /*
            测试事件方法的触发环境问题
            以下代码无法通过编译，因为event仅能在定义它的类中触发
            （即在NewspaperOffice中才能触发），在其它位置调用
            只能使用+=或-=
            */
            //no.pullHolder(no);
#endif
            #endregion

#endif
        }
    }

    #region 普通方法相关定义

#if NORMAL
    /// <summary>
    /// 报社，被观察者，具有推方式和拉方式
    /// </summary>
    class NewspaperOffice : IObservable<NewspaperOffice,Subscriber, string>, Utils.DesignPatterns.ObserverPattern.IObservable<NewspaperOffice,Subscriber>
    {
        private string name;
        /// <summary>
        /// 报社名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        /// <summary>
        /// 用来保存订阅者
        /// </summary>
        private List<Subscriber> list = null;
        private Dictionary<string, int> newspapers = null;
        /// <summary>
        /// 索引器，根据刊物名获取或设置售价
        /// </summary>
        /// <param name="key">刊物名</param>
        /// <returns>售价</returns>
        public int this[string key]
        {
            get
            {
                return newspapers[key];
            }
            set
            {
                if (newspapers.ContainsKey(key))
                {
                    newspapers[key] = value;
#if PUSH
                    //推方式
                    StringBuilder builder = new StringBuilder();
                    foreach(string tempKey in newspapers.Keys)
                    {
                        int tempValue = newspapers[tempKey];
                        builder.Append("《" + tempKey + "》的售价为" + tempValue + ";");
                    }
                    notifyObserverPush(this,builder.ToString());

#elif PULL
                    //拉方式
                    notifyObserverPull(this);
#endif
                }
            }
        }
        /// <summary>
        /// 构造函数，自定义报社名称
        /// </summary>
        /// <param name="name">报社名称</param>
        public NewspaperOffice(string name)
        {
            this.name = name;
            list = new List<Subscriber>();
            newspapers = new Dictionary<string, int>();
            //以下代码仅做测试用：
            newspapers.Add("天天下午茶", 10);
            newspapers.Add("天天晚报", 15);
            newspapers.Add("天天经济报", 12);
        }
        /// <summary>
        /// 添加订阅者
        /// </summary>
        /// <param name="observer">订阅者</param>
        /// <returns>添加成功与否</returns>
        public bool addObserver(Subscriber observer)
        {
            list.Add(observer);
            return true;
        }
        /// <summary>
        /// 数据变化通知观察者时调用（拉方式）
        /// </summary>
        public void notifyObserverPull(NewspaperOffice observable)
        {
            foreach(Subscriber sub in list)
            {
                sub.updateDataPull(observable);
            }
        }
        /// <summary>
        /// 数据变化通知观察者时调用（推方式）
        /// </summary>
        /// <param name="dataPack">被观察者推的数据包</param>
        public void notifyObserverPush(NewspaperOffice observable,string dataPack)
        {
            foreach(Subscriber sub in list){
                sub.updateDataPush(observable, dataPack);
            }
        }
        /// <summary>
        /// 删除订阅者
        /// </summary>
        /// <param name="observer">订阅者</param>
        /// <returns>删除成功与否</returns>
        public bool removeObserver(Subscriber observer)
        {
            return list.Remove(observer);
        }
    }

    class Subscriber : IObserver<NewspaperOffice, string>, Utils.DesignPatterns.ObserverPattern.IObserver<NewspaperOffice>
    {
        private string name;
        /// <summary>
        /// 构造函数，自定义订阅者名字
        /// </summary>
        /// <param name="name">订阅者名字</param>
        public Subscriber(string name)
        {
            this.name = name;
        }
        /// <summary>
        /// 被观察者数据改变时调用（推方式）
        /// </summary>
        /// <param name="observable">被观察者</param>
        /// <param name="dataPack">数据包</param>
        public void updateDataPush(NewspaperOffice observable, string dataPack)
        {
            Console.WriteLine("**********" + name + "(推方式)**********");
            Console.WriteLine("《" + observable.Name + "》消息： " + dataPack);
            Console.WriteLine("******************************");
        }
        /// <summary>
        /// 被观察者数据改变时调用（拉方式）
        /// </summary>
        /// <param name="observable">被观察者</param>
        public void updateDataPull(NewspaperOffice observable)
        {
            Console.WriteLine("**********" + name + "(拉方式)**********");
            Console.WriteLine("我所关心的是报社的名称： " + observable.Name);
            Console.WriteLine("我所关心的是《" + observable.Name + "》的售价： " + observable["天天晚报"]);
            Console.WriteLine("******************************");
        }
    }

#endif

    #endregion

    #region 事件方法相关定义

#if EVENT

    class NewspaperOffice : IObservableEvent<NewspaperOffice>,IObservableEvent<NewspaperOffice,string>
    {
        private string name;
        /// <summary>
        /// 报社名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        /// <summary>
        /// 作为“推方式”的注册注销事件，外界注册注销均需通过该事件
        /// </summary>
        public event Action<NewspaperOffice, string> pushHolder;
        /// <summary>
        /// 作为“拉方式”的注册注销事件，外界注册注销均需通过该事件
        /// </summary>
        public event Action<NewspaperOffice> pullHolder;

        private Dictionary<string, int> newspapers = null;
        /// <summary>
        /// 索引器，根据刊物名获得或设置售价
        /// </summary>
        /// <param name="key">刊物名</param>
        /// <returns>售价</returns>
        public int this[string key]
        {
            get
            {
                return newspapers[key];
            }
            set
            {
                if (newspapers.ContainsKey(key))
                {
                    newspapers[key] = value;
#if PUSH
                    //推方式
                    StringBuilder builder = new StringBuilder();
                    foreach (string tempKey in newspapers.Keys)
                    {
                        int tempValue = newspapers[tempKey];
                        builder.Append("《" + tempKey + "》的售价为" + tempValue + ";");
                    }
                    pushHolder(this, builder.ToString());
#elif PULL
                    //拉方式
                    pullHolder(this);
#endif
                }
            }
        }
        /// <summary>
        /// 构造函数，自定义报社名称
        /// </summary>
        /// <param name="name">报社名称</param>
        public NewspaperOffice(string name)
        {
            this.name = name;
            newspapers = new Dictionary<string, int>();
            //以下代码仅做测试用：
            newspapers.Add("天天下午茶", 10);
            newspapers.Add("天天晚报", 15);
            newspapers.Add("天天经济报", 12);
        }
    }

    class Subscriber : Utils.DesignPatterns.ObserverPattern.IObserver<NewspaperOffice>, IObserver<NewspaperOffice,string>
    {
        private string name;
        /// <summary>
        /// 构造函数，自定义订阅者名字
        /// </summary>
        /// <param name="name">订阅者名字</param>
        public Subscriber(string name)
        {
            this.name = name;
        }
        /// <summary>
        /// 被观察者数据改变时调用（推方式）
        /// </summary>
        /// <param name="observable">被观察者</param>
        /// <param name="dataPack">数据包</param>
        public void updateDataPush(NewspaperOffice observable, string dataPack)
        {
            Console.WriteLine("**********" + name + "(推方式)**********");
            Console.WriteLine("《" + observable.Name + "》消息： " + dataPack);
            Console.WriteLine("******************************");
        }
        /// <summary>
        /// 被观察者数据改变时调用（拉方式）
        /// </summary>
        /// <param name="observable">数据包</param>
        public void updateDataPull(NewspaperOffice observable)
        {
            Console.WriteLine("**********" + name + "(拉方式)**********");
            Console.WriteLine("我所关心的是报社的名称： " + observable.Name);
            Console.WriteLine("我所关心的是《" + observable.Name + "》的售价： " + observable["天天晚报"]);
            Console.WriteLine("******************************");
        }
    }

#endif

    #endregion
}
