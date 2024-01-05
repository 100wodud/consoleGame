using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textGame
{
    public class Item
    {
        //아이템 생성
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int stat { get; set; }
        public int Gold { get; set; }
        public bool Sold { get; set; }
        public bool Equip { get; set; }
        public string Explain { get; set; }


        static Item armor1 = new Item();
        static Item armor2 = new Item();
        static Item armor3 = new Item();
        static Item weapon1 = new Item();
        static Item weapon2 = new Item();
        static Item weapon3 = new Item();

        public static List<Item> itemList = new List<Item>();
        public static List<Item> myItem = new List<Item>();
        public static void makeItem()
        {

            armor1.Id = 0;
            armor1.Name = "수련자 갑옷";
            armor1.Type = 1;
            armor1.stat = 5;
            armor1.Sold = true;
            armor1.Gold = 1000;
            armor1.Explain = "수련에 도움을 주는 갑옷";
            armor1.Equip = false;

            armor2.Id = 1;
            armor2.Name = "무쇠갑옷";
            armor2.Type = 1;
            armor2.stat = 9;
            armor2.Sold = true;
            armor2.Gold = 2000;
            armor2.Explain = "무쇠로 만들어져 튼튼한 갑옷";
            armor2.Equip = false;

            armor3.Id = 2;
            armor3.Name = "스파르타의 갑옷";
            armor3.Type = 1;
            armor3.stat = 15;
            armor3.Sold = true;
            armor3.Gold = 3500;
            armor3.Explain = "스파르타의 전사들이 사용했다는 전설의 갑옷";
            armor3.Equip = false;

            weapon1.Id = 3;
            weapon1.Name = "낡은 검";
            weapon1.Type = 2;
            weapon1.stat = 2;
            weapon1.Sold = true;
            weapon1.Gold = 600;
            weapon1.Explain = "쉽게 볼 수 있는 낡은 검";
            weapon1.Equip = false;


            weapon2.Id = 4;
            weapon2.Name = "청동 도끼";
            weapon2.Type = 2;
            weapon2.stat = 5;
            weapon2.Sold = true;
            weapon2.Gold = 1500;
            weapon2.Explain = "어디선가 사용됐던거 같은 도끼";
            weapon2.Equip = false;


            weapon3.Id = 5;
            weapon3.Name = "스파르타의 창";
            weapon3.Type = 2;
            weapon3.stat = 7;
            weapon3.Sold = true;
            weapon3.Gold = 4000;
            weapon3.Explain = "스파르타의 전사들이 사용했다는 전설의 창입니다.";
            weapon3.Equip = false;

            itemList.Add(armor1);
            itemList.Add(armor2);
            itemList.Add(armor3);
            itemList.Add(weapon1);
            itemList.Add(weapon2);
            itemList.Add(weapon3);
        }
    }
}
