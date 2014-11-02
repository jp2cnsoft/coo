using System; using System.Data; using System.Configuration; using System.Web; using System.Web.Security; using System.Web.UI; using System.Web.UI.WebControls; using System.Web.UI.WebControls.WebParts; using System.Web.UI.HtmlControls; using log4net; using log4net.Config;  namespace Seika.Util {     /// <summary>     /// Summary description for Log     /// </summary>     ///      public class Log     {         private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);          public static void WriteDebug(String str)         {                        log.Debug(str);
            Console.WriteLine(str);         }         public static void WriteInfo(String str)         {                        log.Info(str);
            Console.WriteLine(str);
        }         public static void WriteWarn(String str)         {                        log.Warn(str);
            Console.WriteLine(str);         }          public static void WriteError(String str)         {                     log.Error(str);
            Console.WriteLine(str);
        }          public static void WriteFatal(String str)         {                       log.Fatal(str);
            Console.WriteLine(str);         }     } }