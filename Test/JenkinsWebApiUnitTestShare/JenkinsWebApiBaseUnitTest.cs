using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsTest
{
    public class JenkinsWebApiBaseUnitTest
    {
        protected readonly Uri host = new Uri("http://tiny:8080");
        //protected readonly string login = "Admin";
        //protected readonly string password = "admin";
        protected readonly string login = "Tester";
        protected readonly string password = "tester";
        //token name = TesterAPIToken
        protected readonly string token = "11096e7fa3b687e849ee95908b869058bc";
        // legacy token d65e88e40bb2bf5f72029713dce48243
    }
}
