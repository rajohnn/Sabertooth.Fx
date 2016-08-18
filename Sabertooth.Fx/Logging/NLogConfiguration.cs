using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;
using System;

namespace Sabertooth.Fx.Logging {

    public class NLogConfiguration {

        /// <summary>
        /// Template used to create the default insert statement.  Depends on a valid database table name.
        /// </summary>
        public static string LOG_COMMAND_TEXT = @"INSERT INTO {0} ( [LogDate], [ThreadId], [EventLevel], [LoggerName], [Message], [Exception]) VALUES ( GETDATE(), @thread, @level, @logger, @message, @exception)";

        /// <summary>
        /// A fast way of adding NLog database logging to an application.  
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="databaseTableName">The database table name to write the log to.</param>
        /// <remarks>
        /// You will want to run the following script to create the log table.  Note that the unit
        /// test expects there to be a table named "TestLog".
        /// <code language="SQL" title="Log Table Creation Script">
        /// <![CDATA[  
        ///  SET ANSI_NULLS ON
        ///  GO
        ///  
        ///  SET QUOTED_IDENTIFIER ON
        ///  GO
        ///  
        ///  CREATE TABLE [MyDatabaseTableName](
	    ///     [LogId] [int] IDENTITY(1,1) NOT NULL,
        ///     [LogDate] [datetime] NOT NULL,
        ///     [ThreadId] int NULL,
        ///     [EventLevel] [nvarchar](50) NOT NULL,
        ///     [LoggerName] [nvarchar](500) NULL,
	    ///     [Message][nvarchar](max) NULL,
	    ///     [Exception][nvarchar](max) NULL,
	    ///      PRIMARY KEY CLUSTERED ([LogId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
        ///  ) 
        ///  GO
        /// ]]>
        /// </code> 
        /// </remarks>
        public static void DefaultDatabase_Initialization(string connectionString, string databaseTableName)  {
            DefaultDatabase_Initialization(connectionString, databaseTableName, false);
        }
        /// <summary>
        /// A fast way of adding NLog database logging to an application.  
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="databaseTableName">The database table name to write the log to.</param>
        /// <param name="enableTracing">True to enable NLog debugging and tracing.  Defaults to false if not specified.</param>
        /// <remarks>
        /// You will want to run the following script to create the log table.
        /// <code language="SQL" title="Log Table Creation Script">
        /// <![CDATA[  
        ///  SET ANSI_NULLS ON
        ///  GO
        ///  
        ///  SET QUOTED_IDENTIFIER ON
        ///  GO
        ///  
        ///  CREATE TABLE [MyDatabaseTableName](
	    ///     [LogId] [int] IDENTITY(1,1) NOT NULL,
        ///     [LogDate] [datetime] NOT NULL,
        ///     [ThreadId] int NULL,
        ///     [EventLevel] [nvarchar](50) NOT NULL,
        ///     [LoggerName] [nvarchar](500) NULL,
	    ///     [Message][nvarchar](max) NULL,
	    ///     [Exception][nvarchar](max) NULL,
	    ///      PRIMARY KEY CLUSTERED ([LogId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
        ///  ) 
        ///  GO
        /// ]]>
        /// </code> 
        /// </remarks>
        public static void DefaultDatabase_Initialization(string connectionString, string databaseTableName, bool enableTracing) {
           
            if (enableTracing) {
                InternalLogger.LogToConsole = true;
                InternalLogger.LogLevel = LogLevel.Trace;
                InternalLogger.LogFile = "c:\\Log\\log.txt"; // Change path to your preferred location
            }

            var config = new LoggingConfiguration();
            var target = new DatabaseTarget("dbtarget");

            target.ConnectionString = connectionString;
            target.CommandText = String.Format(LOG_COMMAND_TEXT, databaseTableName);
            target.Parameters.Add(new DatabaseParameterInfo("@thread", new SimpleLayout("${threadid}")));
            target.Parameters.Add(new DatabaseParameterInfo("@level", new SimpleLayout("${level}")));
            target.Parameters.Add(new DatabaseParameterInfo("@logger", new SimpleLayout("${logger}")));
            target.Parameters.Add(new DatabaseParameterInfo("@message", new SimpleLayout("${message}")));
            target.Parameters.Add(new DatabaseParameterInfo("@exception", new SimpleLayout("${exception}")));

            var rule = new LoggingRule("*", LogLevel.Trace, target);
            config.LoggingRules.Add(rule);

            config.AddTarget(target);

            LogManager.Configuration = config;

            if (enableTracing) {
                // Enable these commands when troubleshooting.
                LogManager.ThrowExceptions = true;
                LogManager.ThrowConfigExceptions = true;
            }

            var logger = LogManager.GetCurrentClassLogger();
            logger.Info("Logger initialized.");
        }
    }
}