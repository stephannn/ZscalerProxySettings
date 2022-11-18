using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace hanisch.ZscalerProxySettings
{
    public class ZscalerIp : AZscalerIp
    {

        //[RegularExpression(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")]
        public override string ServerIP { get; set; } = string.Empty;
        //[RegularExpression(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public override string ZscalerProxyVirtualIP { get; set; } = null;
        //FQDN
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public override string ZscalerHostname { get; set; } = null;
        //[RegularExpression(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public override string ZscalerProxy { get; set; } = null;
        //[RegularExpression(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public override string GatewayIPAddress { get; set; } = null;

        [JsonIgnore]
        public override Hashtable ZSattribute { get; set; }

        public ZscalerIp(Hashtable? zs = null)
        {

            if (zs == null)
            {
                ZSattribute = new Hashtable();
                ZSattribute.Add("ServerIP", "Your request is arriving at this server from the IP address");
                ZSattribute.Add("ZscalerProxyVirtualIP", "The Zscaler proxy virtual IP is");
                ZSattribute.Add("ZscalerHostname", "The Zscaler hostname for this proxy appears to be");
                ZSattribute.Add("ZscalerProxy", "The request is being received by the Zscaler Proxy from the IP address");
                //ZSattribute.Add("GatewayIPAddress", "Your Gateway IP Address is");
                ZSattribute.Add("GatewayIPAddress", @"^(Your Gateway IP Address is \s*(?:most likely)?)");
            }
            else
            {
                ZSattribute = zs;
            }

        }


    }

}