using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loxFinder
{

    

    class Program
    {
        public class lox
        {
            public string nick;
            public int times = 0;

            public lox(string nick)
            {
                this.nick = nick;
                this.times += 1;
            }

            public void ShowMeNick()
            {
                Console.WriteLine("\t\t{0, 10} tryed {1, 2} times.", nick, times);
            }

            public void HeTryed()
            {
                times++;
            }
        }

        public class PlayerIp
        {
            List<lox> loxes = new List<lox>();
            List<string> myBad = new List<string>();
            string ip;

            public PlayerIp() { }
            public PlayerIp(string ip)
            {
                this.ip = ip;
            }
            public PlayerIp(string ip, string name)
            {
                this.ip = ip;
                loxes.Add(new lox(name));
                myBad.Add(name);
            }

            public void AnotherNick(string nick)
            {
                if (myBad.Contains(nick))
                {
                    for (int i = 0; i < myBad.Count; i++)
                    {
                        if (myBad[i] == nick)
                            loxes[i].HeTryed();
                    }
                }else{
                    loxes.Add(new lox (nick));
                    myBad.Add(nick);
                }
            }

            public void ShowMeIp()
            {
                Console.WriteLine("On IP {0, 16}:", ip);
                foreach (lox i in loxes)
                {
                    i.ShowMeNick();
                }
            }

            
        }

        static void Main(string[] args)
        {
            string fileName = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(fileName);

            List<PlayerIp> idOfLoxs = new List<PlayerIp>();
            List<string> ids = new List<string>();

            string nick;
            //string nickId;
            string ip;
            
            foreach (string line in lines)
            {
                if (line.IndexOf("[Server thread/INFO]: com.mojang.authlib.GameProfile") > 0)
                {
                    nick =   line.Substring(line.IndexOf("name=") + 5,(line.IndexOf(",properties") - line.IndexOf("name=")) - 5);   // name=electronic,properties
                    //nickId = line.Substring(line.IndexOf("id=") + 4  ,(line.IndexOf(",name=") - line.IndexOf("id="))- 4);           // id=44e27a3f-96b1-3a27-9527-927ef7b04e6e,name=
                    ip =     line.Substring(line.IndexOf("(/") + 2   ,(line.IndexOf(")") - line.IndexOf("(/")) - 6);                // (/95.135.170.6:57388)
                    ip = ip.Substring(0, ip.IndexOf(":"));

                    if (ids.Contains(ip))
                    {
                        for (int i = 0; i < ids.Count; i++)
                        {
                            if (ids[i] == ip)
                            {
                                idOfLoxs[i].AnotherNick(nick);
                                break;
                            }
                        }
                    }
                    else
                    {
                        idOfLoxs.Add(new PlayerIp(ip, nick));
                        ids.Add(ip);
                    }
                    //Console.WriteLine("nick - {0} , id - {1} , ip - {2}", nick, nickId, ip);
                    //2019-04-17-2.log - nice
                    //2019-04-13-17.log - nice
                    //2019-04-16-13.log - nice, id is "null>"
                    //2019-04-16-14.log - nice, BUT WAIT
                }

            }

            foreach(PlayerIp i in idOfLoxs)
            {
                i.ShowMeIp();
            }

            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
            //[Server thread/INFO]: [AutoWhitelist] Kicking player:
            //com.mojang.authlib.GameProfile@1d1c8d22[id=44e27a3f-96b1-3a27-9527-927ef7b04e6e,name=electronic,properties={},legacy=false] (/95.135.170.6:57388)
        }
    }
}
