using System;
using Sabertooth.Fx.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace Sabertooth.Fx.Tests {
    [TestClass]
    public class NLogConfigurationTests {
        /// <summary>
        /// Test NLog database configuration with full tracing turned on.
        /// </summary>
        [TestMethod]
        public void NLog_DatabaseConfigurationTraceTest() {
            string databaseTableName = "TestLog";
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
            NLogConfiguration.DefaultDatabase_Initialization(connectionString, databaseTableName, true);

        }

        /// <summary>
        /// Test NLog database configuration with full tracing turned off.
        /// </summary>
        /// <remarks>You will not see a complete dump in the unit test output when tracing is turned off.</remarks>
        [TestMethod]
        public void NLog_DatabaseConfigurationTest() {
            string databaseTableName = "TestLog";
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
            NLogConfiguration.DefaultDatabase_Initialization(connectionString, databaseTableName, false);
            Console.WriteLine("Complete");
        }
    }
}
