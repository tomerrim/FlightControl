using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    [Flags]
    public enum LegNumber
    {
        One = 0b000000001,   //1
        Two = 0b000000010,   //2
        Thr = 0b000000100,   //4
        Fou = 0b000001000,   //8
        Fiv = 0b000010000,   //16
        Six = 0b000100000,   //32
        Sev = 0b001000000,   //64
        Eig = 0b010000000,   //128
        Dep = 0b100000000,   //256
    }
}
