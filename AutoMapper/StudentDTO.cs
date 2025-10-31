﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    internal class StudentDTO
    {
        public string _ID { get; set; }
        public string _Name { get; set; }
        public string _PhoneNumber { get; set; }
        public int _Height { get; set; }
        public int _Weight { get; set; }
        public int _Position { get; set; }
        public StudentDTO(string ID, string Name, string PhoneNumber, int height, int weight, int position)
        {
            _ID = ID;
            _Name = Name;
            _PhoneNumber = PhoneNumber;
            _Height = height;
            _Weight = weight;
            _Position = position;
        }
        public StudentDTO() { }
    }
}
