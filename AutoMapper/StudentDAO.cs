﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class StudentDAO
    {
        public string _ID { get; set; }
        public string _Name { get; set; }
        public string _PhoneNumber { get; set; }
        public double _Height { get; set; }
        public double _Weight { get; set; }
        public PositionType _Position { get; set; }

        public StudentDAO(string ID, string Name, string PhoneNumber, double height, double weight, PositionType position)
        {
            _ID = ID;
            _Name = Name;
            _PhoneNumber = PhoneNumber;
            _Height = height;
            _Weight = weight;
            _Position = position;
        }
        public StudentDAO() { }
    }
}
