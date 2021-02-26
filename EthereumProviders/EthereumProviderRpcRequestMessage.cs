using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumProviders
{
    public class EthereumProviderRpcRequestMessage : RpcRequestMessage
    {
        public EthereumProviderRpcRequestMessage(object id, string method, string from, params object[] parameterList) : base(id, method,
            parameterList)
        {
            From = from;
        }
        [JsonProperty("from")]
        public string From { get; private set; }
    }
}
