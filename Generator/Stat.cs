using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public static class Stat
    {
        public static void WriteStat(DataBase db, string dir)
        {
            using (StreamWriter writer = File.CreateText(Path.Combine(dir, "stat.txt")))
            {
                writer.WriteLine("Enums");
                foreach (var st in db.Enums.OrderBy(e => e.Name))
                {
                    writer.WriteLine($"    {st.Name}");
                }

                writer.WriteLine();
                writer.WriteLine("Classes");

                foreach (var ct in db.Classes.OrderBy(e => e.Name))
                {
                    writer.WriteLine($"    {ct.Name}");
                }
            }
        }
    }
}
