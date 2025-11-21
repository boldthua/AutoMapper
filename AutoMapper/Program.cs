using AutoMapper.Transformers;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //int[] numbers = new int[5];
            //List<int> list = new List<int>();


            //object datas = new string[] { "1", "2", "3", "4", "5" };
            //Type dest = typeof(int[]);


            ////我如何知道對方是 array?
            //// datas.getType().IsArray
            ////我要如何知道自己的長度是多少?
            //// 
            ////我要如何創建指定類型 array?

            //Array array = (Array)datas;
            //int length = array.Length;
            //Type destElementType = dest.GetElementType();
            //Type sourceElementType = datas.GetType().GetElementType();

            //var parseMethod = destElementType.GetMethod("Parse", new Type[] { sourceElementType });

            //Array sourceArray = (Array)datas;
            //Array destArray = Array.CreateInstance(destElementType, length);

            //for (int i = 0; i < length; i++)
            //{
            //    var sourceValue = sourceArray.GetValue(i);
            //    var result = parseMethod.Invoke(null, new object[] { sourceValue });
            //    destArray.SetValue(result, i);
            //}


            //List<string>
            //List<int>

            //我如何得知她是List?
            //我需要創建List物件
            //List裡面該放甚麼類型? 我知道是string-> int, 但真實情況是 xxx->yyyy 如何知道?

            //Type destType = typeof(List<int>);
            //List<string> datas = new List<string> { "1", "2", "3", "4", "5" };


            //bool isEnumerable = destType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            //if (isEnumerable)
            //{
            //    //拆開他，變成 List<T>
            //    Type listStringType = datas.GetType();
            //    Type sourceListTypeeDefinition = listStringType.GetGenericTypeDefinition(); // List<?>

            //    //取得泛型類型參數 <?>
            //    Type sourceListGenericArgument = listStringType.GetGenericArguments()[0]; // string

            //    //拆開destType,變成 List<T>
            //    Type destListTypeeDefinition = destType.GetGenericTypeDefinition();
            //    Type destListGenericArgument = destType.GetGenericArguments()[0]; // int


            //    //創建destList物件
            //    Type destListType = destListTypeeDefinition.MakeGenericType(destListGenericArgument);
            //    object destList = Activator.CreateInstance(destListType);
            //    var addMethod = destList.GetType().GetMethod("Add");
            //    var parseMethod = destListGenericArgument.GetMethod("Parse", new Type[] { typeof(string) });
            //    foreach (object source in datas)
            //    {

            //        object destValue = parseMethod.Invoke(null, new object[] { source });
            //        addMethod.Invoke(destList, new object[] { destValue });
            //    }
            //}
            //ObjectOfDAO smallDao = new ObjectOfDAO("1", "Leo", "0933xxxxxx", 180, 60, PositionType.班長, new List<string> { "100", "0" }, new string[] { "100", "0" });
            //StudentDAO dao = new StudentDAO("1", "Leo", "0933xxxxxx", 180, 60, PositionType.班長, new List<string> { "100", "0" }, new string[] { "100", "0" }, smallDao, new List<ObjectOfDAO>() { smallDao });
            //StudentDTO student = mapper.Map<StudentDTO>(dao);
            ////StudentDTO dto2 = new StudentDTO("1", "Leo", "0933xxxxxx", 180, 60, 2);
            ////StudentDAO student2 = mapper.Map<StudentDAO>(dto);
            ////StudentDAO student3 = mapper.Map<StudentDAO>(dto2);



            //string propertyName = student.GetType().GetProperty("ID").Name;
            //var propertyNames = student.GetType().GetProperties().Select(x => x.Name.ToString()).ToList();
            ////datas.Select(x=>x.Name)
            ///


            //List<StudentDAO> studentDAOs = new List<StudentDAO>()
            //{
            //    new StudentDAO() {_Height = 100,ID = "1"},
            //    new StudentDAO() {_Height = 200,ID = "2"},
            //    new StudentDAO() {_Height = 300,ID = "3"},
            //    new StudentDAO() {_Height = 400,ID = "4"},
            //    new StudentDAO() {_Height = 500,ID = "5"},
            //};

            //List<StudentDAO> studens = GetStudents(studentDAOs, x => x._Height * 10 > x._Height / 10);

            //Member => 直接傳入類別屬性
            //Binary => 當今天有多種條件
            //Conditional => 條件式 (三元運算式)
            //Constant => 常數使用
            //MethodCall => 函數呼叫完後的結果
            //Unary => !x.Enabled  => 一元運算等簡單運算
            //Parameter
            //Lambda
            //New



            Mapper mapper = new Mapper();
            ObjectOfDTO smallDto = new ObjectOfDTO("1", "Leo", "0933xxxxxx", 180, 60, PositionType.班長, new List<string> { "100", "0" }, new string[] { "100", "0" });
            StudentDTO dto = new StudentDTO("1", "Leo", "0933xxxxxx", 180, 60, 1, new ObservableCollection<int> { 100, 0 }, new int[] { 100, 0 }, smallDto, new List<ObjectOfDTO>() { smallDto });

            StudentDAO student2 = mapper.Map<StudentDAO, StudentDTO>(dto, exp =>
            {
                exp.ForMember(x => x.ID, y => y._ID)
                   .ForMember(x => x.Scores, y => y._Scores);
            });
            //  string sourcePropName = ((MemberExpression)getSourcePropName.Body).Name;





            //    var config = new MapperConfiguration(cfg =>
            //    cfg.CreateMap<Card, CardViewModel>()
            //    .ForMember(x => x.Id, y => y.MapFrom(o => o.CardId))
            //    .ForMember(x => x.Name, y => y.MapFrom(o => $"{o.Id}: {o.Name}"))
            //); // 註冊Model間的對映 建立設定檔
            // 取得來源跟目標的propertyName
            // ForMember(Expression dest, Expression source)
            // string destName = ((MemberExpression)dest.Body).Name;
            // string sourceName = ((MethodExpression)source.Body).Body;

            Console.ReadKey();
        }

        private static List<StudentDAO> GetStudents<T>(List<StudentDAO> students, Expression<Func<StudentDAO, T>> func)
        {
            var member = func.Body as BinaryExpression;
            List<StudentDAO> studentDAOs = new List<StudentDAO>();
            var xName = (member.Left as BinaryExpression).Left;


            //foreach (StudentDAO student in students)
            //{
            //    var target = func.GetMethodInfo().ReturnParameter.Name;

            //    //studentDAOs.Add(func.Invoke(student));

            //}
            return studentDAOs;

        }

        //List<string> source => List<int>





        //object data = new List<string>();
        //PropertyType  destProp = typeof(List<int>)

        // 先查source是不是泛型是不是IEnumerable
        // 是的話 取得source成員的Type

        // GetGenericTypeDefination  // GetGenericArgument 
        //        解析dest 的 泛型定義(前面的)和泛型型別(裡面的)
        // 定義新型別 = 泛型定義.MakeGenericType(泛型型別)
        // 使用 Activator.CreateInstance(新型別)建立新容器
        //        取得泛型定義的 Add方法
        //        取得泛型型別的 Parse方法
        //        用 Parse方法轉換data內的每一成員後，用Add塞進新容器裡
        ///       
    }
}
