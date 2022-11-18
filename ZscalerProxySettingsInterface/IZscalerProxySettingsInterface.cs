using System;
using System.Threading.Tasks;

namespace hanisch.ZscalerProxySettings
{
    public interface IZscalerProxySettingsInterface
    {
        public string GetProxyJson(string uriEndpoint);

        public AZscalerIp GetProxyZscalerObj(string uriEndpoint);

        //protected Task<AZscalerIp> GetZscalerProxyAsync(string uriEndpoint);

    }
}
