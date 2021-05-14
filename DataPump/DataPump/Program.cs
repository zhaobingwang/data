using DataPump.Data;
using DataPump.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataPump
{
    class Program
    {
        static void Main(string[] args)
        {
            string resourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../../Data/resource");
            string cnNameTxtPath = Path.Combine(resourcePath, "Chinese_Names_Corpus_Gender（120W）.txt");

            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);


            int idx = 1;
            int readLinesOnce = 10000;
            while (true)
            {
                Console.WriteLine($"第{idx}次读取");
                var lines = PagedChineseNameByLine(cnNameTxtPath, idx, readLinesOnce);

                if (lines.Count() < 1)
                {
                    Console.WriteLine("已全部读取");
                    break;
                }
                Console.WriteLine($"当次读取{lines.Count}个");

                using (var db = new PumpDbContext())
                {
                    db.ChineseNames.AddRange(lines);
                    db.SaveChanges();
                }

                idx++;
            }
            Console.WriteLine("任务结束");
        }

        public static IEnumerable<string> PagedReadFromTxtByLine(string fileName, int start, int lineCount)
        {
            if (start < 1)
                throw new ArgumentOutOfRangeException(nameof(start));

            var skip = (start - 1) * lineCount;
            var lines = File.ReadLines(fileName).Skip(skip).Take(lineCount);
            return lines;
        }

        public static List<ChineseName> PagedChineseNameByLine(string fileName, int start, int lineCount)
        {
            if (start < 1)
                throw new ArgumentOutOfRangeException(nameof(start));

            List<ChineseName> cnNames = new List<ChineseName>();
            var skip = (start - 1) * lineCount;
            var lines = File.ReadLines(fileName).Skip(skip).Take(lineCount);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                var currentLine = line.Split(',');
                var name = currentLine[0];
                var gender = currentLine[1];
                short genderCode = 9;
                if (gender == "男")
                {
                    genderCode = 1;
                }
                else if (gender == "女")
                {
                    genderCode = 2;
                }
                else
                {
                    genderCode = 9;
                }
                cnNames.Add(new ChineseName { Name = currentLine[0], Gender = genderCode });
            }
            return cnNames;
        }
    }
}
