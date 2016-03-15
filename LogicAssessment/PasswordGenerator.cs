using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAssessment
{
    class PasswordGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
