using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper.Transformers
{
    internal class ObjectOfDTO
    {
        public string _ID { get; set; }
        public string _Name { get; set; }
        public string _PhoneNumber { get; set; }
        public double _Height { get; set; }
        public double _Weight { get; set; }
        public PositionType _Position { get; set; }
        public List<string> _Scores { get; set; }
        public string[] _Subjects { get; set; }

        public ObjectOfDTO(string ID, string Name, string PhoneNumber, double height, double weight, PositionType position, List<string> scores, string[] subjects)
        {
            _ID = ID;
            _Name = Name;
            _PhoneNumber = PhoneNumber;
            _Height = height;
            _Weight = weight;
            _Position = position;
            _Scores = scores;
            _Subjects = subjects;
        }
        public ObjectOfDTO() { }
    }
}
