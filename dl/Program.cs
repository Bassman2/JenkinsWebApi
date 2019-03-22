using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dl
{
    class Program
    {
        static void Main(string[] args)
        {
            

            string path = null;
            string url = null;
            string username = null;
            string password = null;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].Substring(0, 2))
                {
                case "-o":
                    path = args[++i];
                    break;
                case "-u":
                    username = args[++i];
                    break;
                case "-p":
                    password = args[++i];
                    break;
                case "-h":
                case "/?":
                    Help();
                    return;
                default:
                    url = args[i];
                    break;
                }
            }

            if (string.IsNullOrEmpty(path))
            {
                Help();
            }
            else
            {
                WebRequestHandler handler = new WebRequestHandler();
                handler.CookieContainer = new CookieContainer();
                handler.UseCookies = true;
                HttpClient client = new HttpClient(handler);

                var list = new Dictionary<string, string>();
                list.Add("j_username", username);
                list.Add("j_password", password);
                list.Add("remember_me", "on");
                list.Add("Submit", "Anmelden");
                var content = new FormUrlEncodedContent(list);
                //var content = new StringContent("j_username=bs&j_password=ente51");

                using (HttpResponseMessage response = client.PostAsync("http://localhost:8080/j_acegi_security_check", content).Result)
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new WebException(response.ReasonPhrase);
                    }
                }
                                
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new WebException(response.ReasonPhrase);
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        using (Stream standardOutput = Console.OpenStandardOutput())
                        {
                            response.Content.ReadAsStreamAsync().Result.CopyTo(standardOutput);
                        }
                    }
                    else
                    {
                        using (FileStream file = File.Create(path))
                        {
                            response.Content.ReadAsStreamAsync().Result.CopyTo(file);
                        }
                    }
                }
                
            }
        }

        public static void Help()
        {
            Console.WriteLine("Download a file.");
            Console.WriteLine();
            Console.WriteLine("dl [-o path] [-u username] [-p password] [-h] url");
            Console.WriteLine();
            Console.WriteLine("-o path     Specifies the output file.");
            Console.WriteLine("-u username Specifies the username.");
            Console.WriteLine("-p password Specifies the password.");
            Console.WriteLine("-h          Show this help test.");
            Console.WriteLine("url         Specifies the URL of the download file.");
            Console.WriteLine();
        }
    }
}
