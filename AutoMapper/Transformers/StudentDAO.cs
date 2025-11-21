using AutoMapper.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class StudentDAO
    {
        public string ID { get; set; }
        public string _Name { get; set; }
        public string _PhoneNumber { get; set; }
        public double _Height { get; set; }
        public double _Weight { get; set; }
        public PositionType _Position { get; set; }
        public List<string> Scores { get; set; }

        public string[] _Subjects { get; set; }
        public ObjectOfDAO _ObjectTest { get; set; }

        public List<ObjectOfDAO> objectList { get; set; }

        public StudentDAO(string ID, string Name, string PhoneNumber, double height, double weight, PositionType position, List<string> scores, string[] subjects, ObjectOfDAO objectTest, List<ObjectOfDAO> objectList)
        {
            this.ID = ID;
            _Name = Name;
            _PhoneNumber = PhoneNumber;
            _Height = height;
            _Weight = weight;
            _Position = position;
            Scores = scores;
            _Subjects = subjects;
            _ObjectTest = objectTest;
            this.objectList = objectList;
        }
        public StudentDAO() { }
    }
}
