using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Generator
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length > 0 && Directory.Exists(args[0]))
            {
                new Program().Run(Path.GetFullPath(args[0].Trim('"')));
            }
            else
            {
                Console.WriteLine("Generator file.xsd outputFolder");
            }
        }

        
        public void Run(string dir)
        {
            DataBase db = new DataBase();

            string inputFolder = Path.Combine(dir, "Schema");
            Parser.Parse(db, inputFolder);

            string outputFolder = Path.Combine(dir, "Model");
            if (Directory.Exists(outputFolder))
            {
                Directory.Delete(outputFolder, true);
            }
            Directory.CreateDirectory(outputFolder);

            Stat.WriteStat(db, outputFolder);
            Creator.Create(db, outputFolder, APIType.JSON);
        }

        

        

        

        

        



        
    }
}




