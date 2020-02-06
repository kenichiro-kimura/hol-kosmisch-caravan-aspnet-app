using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace MyWebApp.Models
{
    public class Helpers
    {
        public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["RDS_DB_NAME"];

            foreach (string s in appConfig.AllKeys)
            {
                dbname += ":(" + s + ")";
            }
            dbname = dbname + "hoge";
            //dbname = Environment.GetEnvironmentVariable("RDS_DB_NAME");
            dbname = "aa1bpi4ywjhu4qk";
            if (string.IsNullOrEmpty(dbname)) return null;

            string username = "test";// appConfig["RDS_USERNAME"];
            string password = "testtest";// appConfig["RDS_PASSWORD"];
            string hostname = "aa1bpi4ywjhu4qk.cugybtja2cyw.us-west-2.rds.amazonaws.com";// appConfig["RDS_HOSTNAME"];
            string port = "1433";// appConfig["RDS_PORT"];
            //concat('Data Source=tcp:', reference(concat('Microsoft.Sql/servers/', variables('sqlserverName'))).fullyQualifiedDomainName, ',1433;Initial Catalog=', variables('databaseName'), ';User Id=', parameters('sqlAdministratorLogin'), '@', reference(concat('Microsoft.Sql/servers/', variables('sqlserverName'))).fullyQualifiedDomainName, ';Password=', parameters('sqlAdministratorLoginPassword'),
            return "Server=tcp:" + hostname + ",1433;Database=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
            return "Data Source=" + hostname + ";Database=" + dbname + ";User Id=" + username + ";Password = " + password + "; MultipleActiveResultSets = True";
            //return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}
