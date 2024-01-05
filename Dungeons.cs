using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textGame
{
    public class Dungeons
    {
        //던전넘버링 / 던전이름 / 던전 필요 방어력 / 던전 필요 공격력 / 획득골드 / 제한레벨 / 추가골드제한 / 경험치획득량
        public int Id { get; set; }
        public string Name { get; set; }
        public int Armor { get; set; }
        public int Damage { get; set; }
        public int Gold { get; set; }
        public int Level { get; set; }
        public int AddGold { get; set; }
        public int Exp { get; set; }

        static Dungeons dungeon1 = new Dungeons();
        static Dungeons dungeon2 = new Dungeons();
        static Dungeons dungeon3 = new Dungeons();

        public static List<Dungeons> dungeons = new List<Dungeons>();
        public static void makeDungeon()
        {
            dungeon1.Id = 0;
            dungeon1.Name = "쉬운 던전";
            dungeon1.Armor = 0;
            dungeon1.Damage = 15;
            dungeon1.Gold = 1000;
            dungeon1.Level = 1;
            dungeon1.AddGold = 20;
            dungeon1.Exp = 50;

            dungeon2.Id = 1;
            dungeon2.Name = "일반 던전";
            dungeon2.Armor = 12;
            dungeon2.Damage = 40;
            dungeon2.Gold = 2000;
            dungeon2.Level = 5;
            dungeon2.AddGold = 25;
            dungeon2.Exp = 100;

            dungeon3.Id = 2;
            dungeon3.Name = "어려운 던전";
            dungeon3.Armor = 25;
            dungeon3.Damage = 50;
            dungeon3.Gold = 3000;
            dungeon3.AddGold = 35;
            dungeon3.Level = 10;
            dungeon3.Exp = 200;

            dungeons.Add(dungeon1);
            dungeons.Add(dungeon2);
            dungeons.Add(dungeon3);
        }

    }
}
