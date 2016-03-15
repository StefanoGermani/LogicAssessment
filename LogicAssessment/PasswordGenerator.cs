using System;

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
