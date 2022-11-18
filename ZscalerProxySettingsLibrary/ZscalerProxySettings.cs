using System;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace hanisch.ZscalerProxySettings
{
    public class ZscalerProxySettings : IZscalerProxySettingsInterface
    {
        private static HttpClient client = new HttpClient();
        public string GetProxyJson(string uriEndpoint)
        {
            if (!String.IsNullOrEmpty(uriEndpoint))
            {
                if (!uriEndpoint.Contains("https://"))
                {
                    uriEndpoint = ("https://") + uriEndpoint;
                }

                var task = Task.Run(async () => await GetZscalerProxyAsync(uriEndpoint));
                //return task.Result.ZscalerProxy;

                JsonSerializerOptions options = new()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var json = JsonSerializer.Serialize<AZscalerIp>(task.Result, options);
                return json.ToString();
            }
            return "";
        }

        public AZscalerIp GetProxyZscalerObj(string uriEndpoint)
        {
            if (!String.IsNullOrEmpty(uriEndpoint))
            {
                if (!uriEndpoint.Contains("https://"))
                {
                    uriEndpoint = ("https://") + uriEndpoint;
                }

                var task = Task.Run(async () => await GetZscalerProxyAsync(uriEndpoint));
                
                return (ZscalerIp)task.Result;
            }
            return null;
        }

        private async Task<AZscalerIp> GetZscalerProxyAsync(string uriEndpoint)
        {
            string proxyReturn = "";
            AZscalerIp zsip = new ZscalerIp();

            try
            {
                //using (var client = new HttpClient())
                //{

                    HttpResponseMessage response = await client.GetAsync(uriEndpoint);

                    proxyReturn = response.Content.ReadAsStringAsync().Result.ToString();

                //}
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.WriteLine("GetProxyAsync WebException: " + e.ToString());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetProxyAsync Exception: " + e.ToString());
            }

            if (!(String.IsNullOrEmpty(proxyReturn)) && proxyReturn.ToLower().Contains("proxy"))
            {
                
                //Console.WriteLine(proxyReturn);
                string[] lines = StripHtml(proxyReturn).Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                foreach(DictionaryEntry de in zsip.ZSattribute){
                    //Console.WriteLine("Key: {0}, Value: {1}", de.Key, de.Value);
                    if(de.Key != null && de.Value != null){

                        foreach (string line in lines)
                        {
                            //Console.WriteLine(line);
                            //if (line.ToString().Trim().ToLower().StartsWith(de.Value.ToString()!.ToLower()) )
                            if (Regex.Match(line.ToString().Trim(), (de.Value.ToString()! + ".*$")).Success)
                            {
                                //typeof(ZscalerIp).GetProperty(de.Key.ToString()!)!.SetValue(zsip, line.Replace(de.Value.ToString()!, "").Trim());
                                // Regex only valid IP or Name without a dot at the end
                                typeof(ZscalerIp).GetProperty(de.Key.ToString()!)!.SetValue(
                                   zsip, (Regex.Match(Regex.Replace(line, de.Value.ToString()!, "").Trim(), @"((?:[0-9]{1,3}\.){3}[0-9]{1,3})|([-a-zA-Z0-9@:%._\+~#=]{1,256})[^\.]")).Groups[0]!.Value
                                //zsip, (Regex.Match(line.Replace(de.Value.ToString()!, "").Trim(), @"((?:[0-9]{1,3}\.){3}[0-9]{1,3})|([-a-zA-Z0-9@:%._\+~#=]{1,256})[^\.]")).Groups[0]!.Value
                                );
                                
                            }
                            //if (line.ToString().StartsWith("Your Gateway IP Address is most likely") )
                            //{
                            //    //Console.WriteLine(de.Value.ToString()!);
                            //    if(de.Value.ToString()! == @"^(Your Gateway IP Address is \s*(?:most likely)?)")
                            //    {
                            //        Console.WriteLine((de.Value.ToString()! + ".*$") + ";" + line);
                            //        if (Regex.Match(line.ToString().Trim(), (de.Value.ToString()! + ".*$")).Success)
                            //        {
                            //            Console.WriteLine("1");
                            //        }
                            //        Console.WriteLine(line);
                            //        //Console.WriteLine(de.Value.ToString());
                            //        Console.WriteLine("RegEx");
                            //        Console.WriteLine(de.Value.ToString()!);
                            //        Console.WriteLine(Regex.Replace(line, de.Value.ToString()!, "").Trim());
                            //        Console.WriteLine("RegEx Done");
                            //    }

                            //}
                        }

                    }
                }
            }

            // " " = Proxy Line will be shown even if no proxy has been found
            return zsip;
        }

        private static string StripHtml(string source)
        {
            string output;

            //get rid of HTML tags
            output = Regex.Replace(source, "<[^>]*>", string.Empty);

            //get rid of multiple blank lines
            output = Regex.Replace(output, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

            return output;
        }


    }
}
