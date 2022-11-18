using System;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using hanisch.ZscalerProxySettings;


namespace hanisch.ZscalerProxySettings
{
    class Program
    {
        static void Main(string[] args)
        {
            IZscalerProxySettingsInterface wps = new ZscalerProxySettings();

            Console.WriteLine("Json:");
            Console.WriteLine(" Proxy New: " + wps.GetProxyJson("ip.zscaler.com"));
            
            Console.WriteLine("---------------------------");

            Console.WriteLine("Zscaler Object:");
            var zo = wps.GetProxyZscalerObj("ip.zscaler.com");
            foreach (PropertyInfo prop in zo.GetType().GetProperties())
            {
                Console.WriteLine(prop.Name + ": " + prop.GetValue(zo, null));

            }

            Console.WriteLine("done");

            Console.ReadLine();

        }
    }
}
