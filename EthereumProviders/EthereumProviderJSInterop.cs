using Microsoft.JSInterop;
using Nethereum.JsonRpc.Client.RpcMessages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumProviders
{
    public class EthereumProviderJSInterop
    {
        public IJSRuntime _jsRuntime { get; set; }

        private IJSObjectReference _jsModule;
        public EthereumProviderJSInterop(IJSRuntime jsRuntime, IJSObjectReference jsModule)
        {
            _jsRuntime = jsRuntime;
            _jsModule = jsModule;
        }
        public async ValueTask<bool> IsEthereumProviderInstaled()
        {
            return await _jsRuntime.InvokeAsync<bool>("EthereumProvider.IsAvailable");
        }
        public async ValueTask<string> EnableProviderAndGetAddress()
        {
            return await _jsRuntime.InvokeAsync<string>("EthereumProvider.ReturnAccount");
        }
        public async ValueTask<RpcResponseMessage> SendAsync(RpcRequestMessage rpcRequestMessage)
        {
            var response = await _jsRuntime.InvokeAsync<string>("EthereumProvider.SendMessage", JsonConvert.SerializeObject(rpcRequestMessage));
            return JsonConvert.DeserializeObject<RpcResponseMessage>(response);
        }
    }
}
