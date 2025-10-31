using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Double number = 180;
            string num = number.ToString();
            // 研究看看 enum 對轉 (能處 enum 轉數字 / 數字轉 enum,再進階一點，字串轉enum)

            StudentDAO dao = new StudentDAO("1", "Leo", "0933xxxxxx", 180, 60, PositionType.班長);
            Mapper mapper = new Mapper();
            StudentDTO student = mapper.Map<StudentDTO>(dao);
            Console.ReadKey();
        }


    }
}
