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

    
    class Program
    {
        static void Start()
        {
            Console.Clear();
            int num;
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("6. 세이브");
            Console.WriteLine("7. 로드\n");
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
                Dungeon();
            }
            else if (num == 5)
            {
                rest();
            }
            else if (num == 6)
            {
                Save();
            }
            else if (num == 7)
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
            savePlayer.Add(Player.player);
            string itemLists = JsonConvert.SerializeObject(Item.itemList);
            string myItems = JsonConvert.SerializeObject(Item.myItem);
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
            LoadPlayer.Add(Player.player);

            //저장위치와 꼭 같은 위치에 저장
            string itemLists = File.ReadAllText(@"F:\itemList.json");
            string myItems =  File.ReadAllText(@"F:\myItem.json");
            string charInfo = File.ReadAllText(@"F:\charInfo.json");
            if (itemLists != null && myItems != null && charInfo != null )
            {
                Item.itemList = JsonConvert.DeserializeObject<List<Item>>(itemLists);
                Item.myItem = JsonConvert.DeserializeObject<List<Item>>(myItems);
                LoadPlayer = JsonConvert.DeserializeObject<List<Player>>(charInfo);
                Player.player = LoadPlayer[0];
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
            Console.WriteLine($"Lv. {Player.player.Level}");
            Console.WriteLine($"chad < {Player.player.Class} >");
            Console.WriteLine($"공격력 : {Player.player.Power} (+{Player.player.AddDstat})");
            Console.WriteLine($"방어력 : {Player.player.Armor} (+{Player.player.AddAstat})");
            Console.WriteLine($"체력 : {Player.player.Hp}");
            Console.WriteLine($"Gold : {Player.player.Gold} G\n");
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
            Console.WriteLine($"{Player.player.Gold}G\n");
            Console.WriteLine("[아이템 목록]");
            foreach (var item in Item.itemList)
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
            Console.WriteLine($"{Player.player.Gold}G\n");
            Console.WriteLine("[아이템 목록]");

            int i = 1;
            foreach (var item in Item.itemList)
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
            else if (Item.itemList[num -1] != null && Item.itemList[num - 1].Sold && Player.player.Gold - Item.itemList[num - 1].Gold >= 0)
            {
                Item.itemList[num - 1].Sold = false;
                Player.player.Gold = Player.player.Gold - Item.itemList[num - 1].Gold;
                Item.myItem.Add(Item.itemList[num - 1]);
                buyingItem();
            }
            else if (Player.player.Gold - Item.itemList[num - 1].Gold < 0)
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
            Console.WriteLine($"{Player.player.Gold}G\n");
            Console.WriteLine("[아이템 목록]\n");

            int i = 0;
            if (Item.myItem.Count != 0)
            {
                foreach (var item in Item.myItem)
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
            else if (Item.myItem[num - 1] != null)
            {
                if (Item.myItem[num - 1].Equip)
                {
                    if (Item.myItem[num - 1].Type == 1)
                    {
                        Player.player.AddAstat = Player.player.AddAstat - Item.myItem[num - 1].stat;
                    }
                    else if (Item.myItem[num - 1].Type == 2)
                    {
                        Player.player.AddDstat = Player.player.AddDstat - Item.myItem[num - 1].stat;
                    }

                }
                Item.itemList[Item.myItem[num - 1].Id].Equip = false;
                Item.itemList[Item.myItem[num - 1].Id].Sold = true;
                Player.player.Gold = Convert.ToInt32(Player.player.Gold + Item.myItem[num - 1].Gold * 0.85);
                Item.myItem.RemoveAt(num - 1);
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
            Item.myItem = Item.myItem.OrderBy(i => i.Id).ToList();
            int num;
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.player.Gold}G\n");
            Console.WriteLine("[아이템 목록]\n");

            int i = 1;
            foreach (var item in Item.myItem)
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
            Console.WriteLine($"{Player.player.Gold}G\n");
            Console.WriteLine("[아이템 목록]\n");

            int i = 0;
            foreach (var item in Item.myItem)
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
            else if (Item.myItem[num - 1] != null)
            {
                Item.myItem[num - 1].Equip = !Item.myItem[num - 1].Equip;
                if(Item.myItem[num - 1].Type == 1)
                {
                    if (Player.player.Arm == null)
                    {
                        Player.player.Arm = Item.myItem[num - 1].Id;
                        Item.myItem[num - 1].Equip = Item.myItem[num - 1].Equip ? true : false;
                        Player.player.AddAstat = Item.myItem[num - 1].stat;
                    } else
                    {
                        if(Player.player.Arm == Item.myItem[num - 1].Id)
                        {
                            foreach(var item in Item.myItem)
                            {
                                if(item.Type == 1 && item.Equip)
                                {
                                    item.Equip = false;
                                    Player.player.AddAstat = Player.player.AddAstat - item.stat;
                                }
                            }
                        } else
                        {
                            foreach (var item in Item.myItem)
                            {
                                if (item.Type == 1 && item.Id == Player.player.Arm)
                                {
                                    item.Equip = false;
                                    Player.player.AddAstat = Player.player.AddAstat - item.stat;
                                }
                            }
                            Player.player.Arm = Item.myItem[num - 1].Id;
                            Player.player.AddAstat = Item.myItem[num - 1].stat;
                            Item.myItem[num - 1].Equip = Item.myItem[num - 1].Equip ? true : false;
                        }
                    }

                } else if (Item.myItem[num - 1].Type == 2)
                {
                    if (Player.player.Wep == null)
                    {
                        Player.player.Wep = Item.myItem[num - 1].Id;
                        Player.player.AddDstat = Item.myItem[num - 1].stat;
                        Item.myItem[num - 1].Equip = Item.myItem[num - 1].Equip ? true : false;

                    }
                    else
                    {
                        if (Player.player.Wep == Item.myItem[num - 1].Id)
                        {
                            foreach (var item in Item.myItem)
                            {
                                if (item.Type == 2 && item.Equip)
                                {
                                    item.Equip = false;
                                    Player.player.AddDstat = Player.player.AddDstat - item.stat;
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in Item.myItem)
                            {
                                if (item.Type == 2 && item.Id == Player.player.Wep)
                                {
                                    item.Equip = false;
                                    Player.player.AddDstat = Player.player.AddDstat - item.stat;
                                }
                            }
                            Player.player.Wep = Item.myItem[num - 1].Id;
                            Player.player.AddDstat = Item.myItem[num - 1].stat;
                            Item.myItem[num - 1].Equip = Item.myItem[num - 1].Equip ? true : false;
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

        static void Dungeon()
        {
            Console.Clear();
            int num;
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");

            Console.Write("[현재 체력]");
            Console.Write($" {Player.player.Hp}\n");
            Console.Write("[현재 방어력]");
            Console.Write($"{Player.player.Armor} (+{Player.player.AddAstat})");
            Console.Write("[현재 공격력]");
            Console.Write($"{Player.player.Power} (+{Player.player.AddDstat})\n\n");

            int i = 0;
            foreach (var dungeon in Dungeons.dungeons)
            {
                Console.Write($"{dungeon.Id + 1}.");
                Console.Write($"{dungeon.Name}     | ");
                Console.Write($"방어력 {dungeon.Armor} 이상 권장    | ");
                Console.Write($"레벨 {dungeon.Level} 입장 가능\n");
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
                Dungeon();
            } 
            else if (num == 0)
            {
                Start();
            }
            else if (Dungeons.dungeons[num - 1] != null)
            {
                if (Dungeons.dungeons[num - 1].Level <= Player.player.Level)
                {
                    dungeonClear(num - 1);
                } else
                {
                    Console.WriteLine("입장 레벨이 아닙니다.");
                    Thread.Sleep(500);
                    Dungeon();
                }
            } else
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                Dungeon();
            }

            static void dungeonClear(int dun)
            {
                Console.Clear();
                int num; 
                Random randomObj = new Random();
                int random = randomObj.Next(0, 101);
                if (Dungeons.dungeons[dun].Armor > Player.player.Armor + Player.player.AddAstat && random <= 40)
                {
                    Fail(dun);
                } else
                {
                    Succece(dun);
                }


                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                string choice = Console.ReadLine();
                bool res = int.TryParse(choice, out num);
                if (!res)
                {
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                    Thread.Sleep(500);
                    Dungeon();
                }
                else if (num == 0)
                {
                    Dungeon();
                } else
                {
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                    Thread.Sleep(500);
                    Dungeon();
                }


                static void Succece(int dun)
                {
                    int ranHp = 0;
                    int ranDam = 0;
                    Random randomObj = new Random();
                    ranHp = randomObj.Next(19, 36) - Player.player.AddAstat - Player.player.Armor + Dungeons.dungeons[dun].Armor;
                    Random randomObj2 = new Random();
                    ranDam = randomObj2.Next(1, 36) + Player.player.AddDstat + Player.player.Power - Dungeons.dungeons[dun].Damage;

                    if (ranHp < 0)
                    {
                        ranHp = 1;
                    }

                    if (ranHp >= Player.player.Hp) 
                    {
                        Fail(dun);
                    } else
                    {
                        if (ranDam > Dungeons.dungeons[dun].AddGold)
                        {

                            ranDam = Dungeons.dungeons[dun].AddGold;
                        } else if (ranDam < 0)
                        {
                            ranDam = 0;
                        }

                        double ranGold = (double)Dungeons.dungeons[dun].Gold * (ranDam / 100.00 + 1.00);


                        Console.WriteLine("던전 클리어");
                        Console.WriteLine("축하합니다!!");
                        Console.WriteLine($"{Dungeons.dungeons[dun].Name}을 클리어 하였습니다.\n");

                        Console.WriteLine("[탐험 결과]");
                        Console.Write("체력");
                        Console.Write($" {Player.player.Hp} -> {Player.player.Hp - ranHp} \n");
                        Console.Write("Gold");
                        Console.Write($" {Player.player.Gold} -> {Player.player.Gold + Convert.ToInt32(Math.Round(ranGold))} \n\n");


                        Player.player.Hp = Player.player.Hp - ranHp;
                        Player.player.Gold = Player.player.Gold + Convert.ToInt32(Math.Round(ranGold));
                        Player.player.Exp = Player.player.Exp + Dungeons.dungeons[dun].Exp;
                        double expCount = Math.Pow(Player.player.Exp / 25, 0.4) * 49 / 50 + 1;

                        int level = Convert.ToInt32(Math.Floor(expCount));
                        if(level > Player.player.Level)
                        {
                            Console.WriteLine("\n★레벨업\n");
                            Console.Write("레벨");
                            Console.Write($" {Player.player.Level} -> {level} \n");
                            Console.Write("공격력");
                            Console.Write($" {Player.player.Power} -> {level + Player.player.Power} \n");
                            Console.Write("방어력");
                            Console.Write($" {Player.player.Armor} -> {level + Player.player.Armor} \n\n");
                            Console.WriteLine("체력이 회복되었습니다\n");
                            Player.player.Level = level;
                            Player.player.Power = level + Player.player.Power;
                            Player.player.Armor = level + Player.player.Armor;
                            Player. player.Hp = 100;
                        }

                    }

                    
                }
                static void Fail(int dun)
                {
                    Random randomObj = new Random();
                    int ran = randomObj.Next(0, 51);

                    double ranGold = (double)   Player.player.Gold * (ran / 1000.00);

                    Console.WriteLine("던전 실패");
                    Console.WriteLine($"{Dungeons.dungeons[dun].Name} 클리어에 실패하였습니다.\n");

                    Console.WriteLine("[탐험 결과]");
                    Console.Write("체력");
                    Console.Write($" {Player.player.Hp} -> 0 \n");
                    Console.Write("Gold");
                    Console.Write($" {Player.player.Gold} -> {Player.player.Gold - Convert.ToInt32(Math.Round(ranGold))} \n");

                    Player.player.Hp = 0;
                    Player.player.Gold = Player.player.Gold - Convert.ToInt32(Math.Round(ranGold));
                }

            }
        }

        static void rest()
        {
            Console.Clear();
            int num;
            Console.WriteLine("휴식하기");
            Console.WriteLine("500 G 를 내면 체력을 회복할 수 있습니다.\n");

            Console.Write("[현재 골드]");
            Console.Write($" {Player.player.Gold}G\n");

            Console.Write("[현재 체력]");
            Console.Write($" {Player.player.Hp}\n");


            Console.WriteLine("\n1. 휴식하기");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.\n");
            Console.Write(">>");
            string choice = Console.ReadLine();

            bool res = int.TryParse(choice, out num);
            if (!res)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                rest();
            }
            else if (num == 0)
            {
                Start();
            }
            else if (num == 1)
            {
                if(Player.player.Gold < 500)
                {
                    Console.WriteLine("\nGold가 부족합니다.\n");
                    Thread.Sleep(500);
                    rest();
                } 
                else if (Player.player.Hp >= 100)
                {
                    Console.WriteLine("\n이미 체력이 가득차 있습니다.\n");
                    Thread.Sleep(500);
                    rest();
                }
                else
                {
                    Console.WriteLine("\n휴식을 완료했습니다\n");
                    Player.player.Hp = 100;
                    Player.player.Gold = Player.player.Gold - 500;
                    Thread.Sleep(500);
                    rest();
                }
            } else
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                Thread.Sleep(500);
                rest();
            }

        }

        static void Main(string[] args)
        {
            Item.makeItem();
            Player.makePlayer();
            Dungeons.makeDungeon();
            Start();
        }
    }
}

