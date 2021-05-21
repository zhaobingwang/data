using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPump.Engine
{
    public class TextEngine
    {
        /// <summary>
        /// 逐行读取文本
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="start">读取起始行位置</param>
        /// <param name="lineCount">读取行数</param>
        /// <returns>当前分片数据</returns>
        public List<string> ReadLine(string fileName, int start = 1, int lineCount = 1000)
        {
            if (start < 1)
                throw new ArgumentOutOfRangeException(nameof(start));

            List<string> datas = new List<string>();
            var skip = (start - 1) * lineCount;
            var lines = File.ReadLines(fileName).Skip(skip).Take(lineCount);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                datas.Add(line);
            }
            return datas;
        }
    }
}
