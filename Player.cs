using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textGame
{
    public class Player
    {
        //캐릭터 생성
        public string Name { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public int Power { get; set; }
        public int Armor { get; set; }
        public int Hp { get; set; }
        public int Gold { get; set; }
        public int? Arm { get; set; }
        public int? Wep { get; set; }
        public int AddAstat { get; set; }
        public int AddDstat { get; set; }
        public int Exp { get; set; }

        public static Player player = new Player();
        public static void makePlayer()
        {
            player.Level = 1;
            player.Name = "미정";
            player.Class = "전사";
            player.Power = 10;
            player.Hp = 100;
            player.Gold = 1500;
            player.Arm = null;
            player.Wep = null;
            player.AddAstat = 0;
            player.AddDstat = 0;
            player.Exp = 0;
        }
    }

}
