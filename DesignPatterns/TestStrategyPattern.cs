using System;

namespace com.mg.Utils.DesignPatterns
{
    /// <summary>
    /// 【策略模式】：
    /// <para>
    /// 将算法封装起来，形成算法族，算法族内的算法可以互相替换，
    /// 使得算法的具体实现独立于使用该算法族的对象
    /// </para>
    /// <para>
    /// 举个例子：一辆汽车car，可以使用各个厂家生产的引擎engine，
    /// 但是car只要告诉engine：“嘿~我想开启引擎~”，engine就会开启，
    /// 而不需要知道engine具体的内部实现，这时不管car使用engineA或者
    /// 使用engineB或者使用其它engine，只需要调用engine的start即可
    /// （此处假设厂家都拥有统一的开启engine的方法start）
    /// </para>
    /// <para>
    /// 2016.12.20
    /// By MG
    /// </para>
    /// </summary>
    class TestStrategyPattern
    {
        /*
         以下使用《Head First Design Patterns》（中文名《深入浅出设计模式》）
         中的练习题，对【策略模式】进行简单的实现、说明，该练习题是这样的：
         角色可以使用武器战斗，但是武器是独立于角色存在的，不依附于任一种
	     角色，即角色和武器并没有所谓的对应关系
        */
        static void Main(string[] args)
        {
            //定义四个角色
            Character king = new King();
            Character queen = new Queen();
            Character knight = new Knight();
            Character troll = new Troll();
            //分别fight
            king.fight();
            queen.fight();
            knight.fight();
            troll.fight();
            //为巨魔更改武器成匕首，fight
            troll.weapon = new Knife();
            troll.fight();
            Console.ReadKey();
        }
    }
    /// <summary>
    /// 角色类，抽象 2016.12.20 By MG
    /// </summary>
    abstract class Character
    {
        /// <summary>
        /// 武器的引用，该文件中的会改变的部分，所以将其提取出来，实现一个算法族（武器库）
        /// </summary>
        private IWeapon _weapon;
        public IWeapon weapon
        {
            get
            {
                return _weapon;
            }
            set
            {
                _weapon = value;
            }
        }
        /// <summary>
        /// 战斗函数，具体的fight留待具体的类实现
        /// </summary>
        public abstract void fight();
    }
    /// <summary>
    /// 国王类，继承于角色类 2016.12.20 By MG
    /// </summary>
    class King : Character
    {
        /// <summary>
        /// 默认佩戴宝剑
        /// </summary>
        public King()
        {
            weapon = new Sword();
        }
        /// <summary>
        /// 国王战斗的方法，调用weapon.useWeapon()
        /// </summary>
        public override void fight()
        {
            Console.WriteLine("国王" + weapon.useWeapon() + "战斗");
        }
    }
    /// <summary>
    /// 皇后类，继承于角色类 2016.12.20 By MG
    /// </summary>
    class Queen : Character
    {
        /// <summary>
        /// 默认佩戴弓箭
        /// </summary>
        public Queen()
        {
            weapon = new BowAndArrow();
        }
        /// <summary>
        /// 皇后战斗的方法，调用weapon.useWeapon()
        /// </summary>
        public override void fight()
        {
            Console.WriteLine("皇后" + weapon.useWeapon() + "战斗");
        }
    }
    /// <summary>
    /// 骑士类，继承于角色类 2016.12.20 By MG
    /// </summary>
    class Knight : Character
    {
        /// <summary>
        /// 默认佩戴宝剑
        /// </summary>
        public Knight()
        {
            weapon = new Sword();
        }
        /// <summary>
        /// 骑士战斗的方法，调用weapon.useWeapon()
        /// </summary>
        public override void fight()
        {
            Console.WriteLine("骑士" + weapon.useWeapon() + "战斗");
        }
    }
    /// <summary>
    /// 巨魔类，继承于角色类 2016.12.20 By MG
    /// </summary>
    class Troll : Character
    {
        /// <summary>
        /// 默认佩戴斧头
        /// </summary>
        public Troll()
        {
            weapon = new Axe();
        }
        /// <summary>
        /// 巨魔战斗的方法，调用weapon.useWeapon()
        /// </summary>
        public override void fight()
        {
            Console.WriteLine("巨魔" + weapon.useWeapon() + "战斗");
        }
    }
    /// <summary>
    /// 武器接口，上述抽取出来的“武器库” 2016.12.20 By MG
    /// </summary>
    interface IWeapon
    {
        /// <summary>
        /// 具体武器的使用留待具体类实现
        /// </summary>
        /// <returns>使用武器的简要说明</returns>
        String useWeapon();
    }
    /// <summary>
    /// 宝剑类，实现武器接口 2016.12.20 By MG
    /// </summary>
    class Sword : IWeapon
    {
        /// <summary>
        /// 使用宝剑
        /// </summary>
        /// <returns>使用宝剑的简要说明</returns>
        public String useWeapon()
        {
            return "使用宝剑";
        }
    }
    /// <summary>
    /// 弓箭类，实现武器接口 2016.12.20 By MG
    /// </summary>
    class BowAndArrow : IWeapon
    {
        /// <summary>
        /// 使用弓箭
        /// </summary>
        /// <returns>使用弓箭的简要说明</returns>
        public String useWeapon()
        {
            return "使用弓箭";
        }
    }
    /// <summary>
    /// 匕首类，实现武器接口 2016.12.20 By MG
    /// </summary>
    class Knife : IWeapon
    {
        /// <summary>
        /// 使用匕首
        /// </summary>
        /// <returns>使用匕首的简要说明</returns>
        public String useWeapon()
        {
            return "使用匕首";
        }
    }
    /// <summary>
    /// 斧头类，实现武器接口 2016.12.20 By MG
    /// </summary>
    class Axe : IWeapon
    {
        /// <summary>
        /// 使用斧头
        /// </summary>
        /// <returns>使用斧头的简要说明</returns>
        public String useWeapon()
        {
            return "使用斧头";
        }
    }
}
