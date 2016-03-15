using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicAssessment
{
    public class UserModule : Nancy.NancyModule
    {
        public UserModule()
        {
            Post["/generatePassword"] = user => {
                    return Guid.NewGuid().ToString();
                };
        }
    }

}
