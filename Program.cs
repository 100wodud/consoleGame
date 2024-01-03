using System;
using static System.Console;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;


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
    }

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
    }


    class Program
    {

        static Player player = new Player();
        static Item armor1 = new Item();
        static Item armor2 = new Item();
        static Item armor3 = new Item();
        static Item weapon1 = new Item();
        static Item weapon2 = new Item();
        static Item weapon3 = new Item();
        static List<Item> itemList = new List<Item>();
        static List<Item> myItem = new List<Item>();
        static void makeItem()
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

            itemList.Add(armor1);
            itemList.Add(armor2);
            itemList.Add(armor3);
            itemList.Add(weapon1);
            itemList.Add(weapon2);
        }
        static void makePlayer()
        {
            player.Level = 1;
            player.Name = "미정";
            player.Class = "전사";
            player.Power = 10;
            player.Hp = 100;
            player.Gold = 15000;
            player.Arm = null;
            player.AddAstat = 0;
            player.AddDstat = 0;
        }
        static void Start()
        {
            Console.Clear();
            int num;
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 세이브");
            Console.WriteLine("5. 로드\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            string choice = Console.ReadLine();

            bool res = int.TryParse(choice, out num);
            if (!res)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Start();
            }else if (num == 1)
            {
                Status();
            }
            else if (num == 2)
            {
                Inventory();
            }
            else if (num == 3)
            {
                Store();
            }
            else if (num == 4)
            {
                Save();
            }
            else if (num == 5)
            {
                Load();
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Start();
            }
        }

        static void Save()
        {
            List<Player> savePlayer = new List<Player>();
            savePlayer.Add(player);
            string itemLists = JsonConvert.SerializeObject(itemList);
            string myItems = JsonConvert.SerializeObject(myItem);
            string charInfo = JsonConvert.SerializeObject(savePlayer);

            //저장시 위치 꼭 확인
            File.WriteAllText(@"F:\itemList.json", itemLists);
            File.WriteAllText(@"F:\myItem.json", myItems);
            File.WriteAllText(@"F:\charInfo.json", charInfo);
            Start();
        }
        static void Load()
        {
            
            List<Player> LoadPlayer = new List<Player>();
            LoadPlayer.Add(player);

            //저장위치와 꼭 같은 위치에 저장
            string itemLists = File.ReadAllText(@"F:\itemList.json");
            string myItems =  File.ReadAllText(@"F:\myItem.json");
            string charInfo = File.ReadAllText(@"F:\charInfo.json");
            if (itemLists != null && myItems != null && charInfo != null )
            {
                itemList = JsonConvert.DeserializeObject<List<Item>>(itemLists);
                myItem = JsonConvert.DeserializeObject<List<Item>>(myItems);
                LoadPlayer = JsonConvert.DeserializeObject<List<Player>>(charInfo);
                player = LoadPlayer[0];
            }
            else
            {
                Console.WriteLine("저장된 항목이 없습니다.");
                Thread.Sleep(500);
            }
            Start();

        }
        static void Status()
        {
            Console.Clear();
            int num;
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"chad < {player.Class} >");
            Console.WriteLine($"공격력 :  {player.Power} (+{player.AddDstat})");
            Console.WriteLine($"방어력 : {player.Armor} (+{player.AddAstat})");
            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G\n");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n");
            Console.Write(">>");
            string choice = Console.ReadLine();

            bool res = int.TryParse(choice, out num);
            if (!res || num != 0)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Status();
            }
            else if (num == 0)
            {
                Start();
            }
        }

        static void Store()
        {
            Console.Clear();
            int num;
            Console.WriteLine("싱점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold}G\n");
            Console.WriteLine("[아이템 목록]");
            foreach (var item in itemList)
            {

                Console.Write($"- {item.Name}    |");
                if(item.Type == 1)
                {
                    Console.Write($" 방어력 +{item.stat}  |");
                } else if (item.Type == 2)
                {
                    Console.Write($" 공격력 +{item.stat}  |");
                }
                Console.Write($" {item.Explain}             |  ");
                if (item.Sold)
                {
                    Console.WriteLine($"{item.Gold} G");
                }
                else
                {
                    Console.WriteLine($"구매완료");
                }
            }

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            string choice = Console.ReadLine();
            bool res = int.TryParse(choice, out num);
            if (!res)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Store();
            }
            else if (num == 0)
            {
                Start();
            }
            else if (num == 1)
            {
                buyingItem();
            }
            else if (num == 2)
            {
                sellingItem();
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Store();
            }

        }

        static void buyingItem()
        {
            Console.Clear();
            int num;
            Console.WriteLine("싱점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold}G\n");
            Console.WriteLine("[아이템 목록]");

            int i = 1;
            foreach (var item in itemList)
            {
                Console.Write($"- {item.Id + 1} ");
                Console.Write($"{item.Name}    |");
                if (item.Type == 1)
                {
                    Console.Write($" 방어력 +{item.stat}  |");
                }
                else if (item.Type == 2)
                {
                    Console.Write($" 공격력 +{item.stat}  |");
                }
                Console.Write($" {item.Explain}             |  ");
                if (item.Sold)
                {
                    Console.WriteLine($"{item.Gold} G");
                }
                else
                {
                    Console.WriteLine($"구매완료");
                }
                i++;
            }

            Console.WriteLine("\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            string choice = Console.ReadLine();
            bool res = int.TryParse(choice, out num);
            if (!res ||  num >= i)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                buyingItem();
            } 
            else if(num == 0){
                Store();
            }
            else if (itemList[num -1] != null && itemList[num - 1].Sold && player.Gold - itemList[num - 1].Gold >= 0)
            {
                itemList[num - 1].Sold = false;
                player.Gold = player.Gold - itemList[num - 1].Gold;
                myItem.Add(itemList[num - 1]);
                buyingItem();
            }
            else if (player.Gold - itemList[num - 1].Gold < 0)
            {
                Console.WriteLine("\n골드가 부족합니다\n");
                Thread.Sleep(500);
                buyingItem();
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                buyingItem();
            }
        }
        static void sellingItem()
        {
            Console.Clear();
            int num;
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold}G\n");
            Console.WriteLine("[아이템 목록]\n");

            int i = 0;
            if (myItem.Count != 0)
            {
                foreach (var item in myItem)
                {

                    Console.Write($"- {i + 1} ");
                    Console.Write($"{item.Name}    |");
                    if (item.Type == 1)
                    {
                        Console.Write($" 방어력 +{item.stat}  |");
                    }
                    else if (item.Type == 2)
                    {
                        Console.Write($" 공격력 +{item.stat}  |");
                    }
                    Console.Write($" {item.Explain}     | ");
                    Console.Write($" {Math.Round(item.Gold * 0.85)}G\n");
                    i++;
                }
            }
            Console.WriteLine("\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            string choice = Console.ReadLine();
            bool res = int.TryParse(choice, out num);
            if (!res || num > i)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                sellingItem();
            }
            else if (num == 0)
            {
                Store();
            }
            else if (myItem[num - 1] != null)
            {
                if (myItem[num - 1].Equip)
                {
                    if (myItem[num - 1].Type == 1)
                    {
                        player.AddAstat = player.AddAstat - myItem[num - 1].stat;
                    }
                    else if (myItem[num - 1].Type == 2)
                    {
                        player.AddDstat = player.AddDstat - myItem[num - 1].stat;
                    }

                }
                itemList[myItem[num - 1].Id].Equip = false;
                itemList[myItem[num - 1].Id].Sold = true;
                player.Gold = Convert.ToInt32(player.Gold + myItem[num - 1].Gold * 0.85);
                myItem.RemoveAt(num - 1);
                sellingItem();
            } else
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                sellingItem();
            }
        }

        static void Inventory()
        {
            Console.Clear();
            myItem = myItem.OrderBy(i => i.Id).ToList();
            int num;
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold}G\n");
            Console.WriteLine("[아이템 목록]\n");

            int i = 1;
            foreach (var item in myItem)
            {

                Console.Write($"- ");
                if (item.Equip)
                {

                    Console.Write($"[E]");
                }
                Console.Write($"{item.Name}    |");
                if (item.Type == 1)
                {
                    Console.Write($" 방어력 +{item.stat}  |");
                }
                else if (item.Type == 2)
                {
                    Console.Write($" 공격력 +{item.stat}  |");
                }
                Console.Write($" {item.Explain}\n");
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            string choice = Console.ReadLine();
            bool res = int.TryParse(choice, out num);
            if (!res)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Inventory();
            }
            else if (num == 0)
            {
                Start();
            }
            else if (num == 1)
            {
                Equipment();
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Inventory();
            }
        }

        static void Equipment()
        {
            Console.Clear();
            int num;
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold}G\n");
            Console.WriteLine("[아이템 목록]\n");

            int i = 0;
            foreach (var item in myItem)
            {

                Console.Write($"- {i + 1} ");
                if (item.Equip)
                {
                    Console.Write($"[E]");
                }
                Console.Write($"{item.Name}    |");
                if (item.Type == 1)
                {
                    Console.Write($" 방어력 +{item.stat}  |");
                }
                else if (item.Type == 2)
                {
                    Console.Write($" 공격력 +{item.stat}  |");
                }
                Console.Write($" {item.Explain}\n");
                i++;
            }

            Console.WriteLine("\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            string choice = Console.ReadLine();
            bool res = int.TryParse(choice, out num);
            if (!res || num > i)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Equipment();
            }
            else if (num == 0)
            {
                Inventory();
            }
            else if (myItem[num - 1] != null)
            {
                myItem[num - 1].Equip = !myItem[num - 1].Equip;
                if(myItem[num - 1].Type == 1)
                {
                    if (player.Arm == null)
                    {
                        player.Arm = myItem[num - 1].Id;
                        myItem[num - 1].Equip = myItem[num - 1].Equip ? true : false;
                        player.AddAstat = myItem[num - 1].stat;
                    } else
                    {
                        if(player.Arm == myItem[num - 1].Id)
                        {
                            foreach(var item in myItem)
                            {
                                if(item.Type == 1 && item.Equip)
                                {
                                    item.Equip = false;
                                    player.AddAstat = player.AddAstat - item.stat;
                                }
                            }
                        } else
                        {
                            foreach (var item in myItem)
                            {
                                if (item.Type == 1 && item.Id == player.Arm)
                                {
                                    item.Equip = false;
                                    player.AddAstat = player.AddAstat - item.stat;
                                }
                            }
                            player.Arm = myItem[num - 1].Id;
                            player.AddAstat = myItem[num - 1].stat;
                            myItem[num - 1].Equip = myItem[num - 1].Equip ? true : false;
                        }
                    }

                } else if (myItem[num - 1].Type == 2)
                {
                    if (player.Wep == null)
                    {
                        player.Wep = myItem[num - 1].Id;
                        player.AddDstat = myItem[num - 1].stat;
                        myItem[num - 1].Equip = myItem[num - 1].Equip ? true : false;

                    }
                    else
                    {
                        if (player.Wep == myItem[num - 1].Id)
                        {
                            foreach (var item in myItem)
                            {
                                if (item.Type == 2 && item.Equip)
                                {
                                    item.Equip = false;
                                    player.AddDstat = player.AddDstat - item.stat;
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in myItem)
                            {
                                if (item.Type == 2 && item.Id == player.Wep)
                                {
                                    item.Equip = false;
                                    player.AddDstat = player.AddDstat - item.stat;
                                }
                            }
                            player.Wep = myItem[num - 1].Id;
                            player.AddDstat = myItem[num - 1].stat;
                            myItem[num - 1].Equip = myItem[num - 1].Equip ? true : false;
                        }
                    }
                }
                Equipment();
            }
            else
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Equipment();
            }
        }

        static void Main(string[] args)
        {
            makeItem();
            makePlayer();
            Start();
        }
    }
}

