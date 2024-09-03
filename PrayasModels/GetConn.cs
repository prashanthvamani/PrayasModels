using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrayasModels
{
    public class GetConn
    {
        public static string constring()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var config = builder.Build();
            string constring = config.GetConnectionString("Conn");
            return constring;
        }


    

    }
}
