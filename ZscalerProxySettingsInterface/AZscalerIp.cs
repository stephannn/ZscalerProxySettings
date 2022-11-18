using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hanisch.ZscalerProxySettings
{
    public abstract class AZscalerIp
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public abstract string ServerIP { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public abstract string ZscalerProxyVirtualIP { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public abstract string ZscalerHostname { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public abstract string ZscalerProxy { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public abstract string GatewayIPAddress { get; set; }

        [JsonIgnore]
        public abstract Hashtable ZSattribute { get; set; }

    }
}
