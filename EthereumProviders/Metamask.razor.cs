using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Nethereum.JsonRpc.Client.RpcMessages;
using Nethereum.Web3;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumProviders
{
    public partial class Metamask : IEthereumProvider
    {
        [Inject]
        public IJSRuntime _jsRuntime { get; set; }

        private EthereumProviderJSInterop _ethereumProviderJSInterop;
        private EthereumProviderRequestInterceptor _ethereumProviderRequestInterceptor;
        protected override async Task OnInitializedAsync()
        {
            var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/_content/EthereumProviders/EthereumProvider.js");
            _ethereumProviderJSInterop = new EthereumProviderJSInterop(_jsRuntime, module);
            _ethereumProviderRequestInterceptor = new EthereumProviderRequestInterceptor(_ethereumProviderJSInterop, this);
            Available = await IsProviderInstalled();
        }
        public bool Available { get; private set; }
        public string Account { get; private set; }

        public string Network { get; private set; }

        public string NameOfProvider { get; private set; }

        public async Task<bool> IsProviderInstalled()
        {
            var result = await _ethereumProviderJSInterop.IsEthereumProviderInstaled();
            Available = result;
            return result;
        }

        public async Task<string> EnableProviderAndGetAddress()
        {
            var result = await _ethereumProviderJSInterop.EnableProviderAndGetAddress();
            Account = result;
            return result;
        }

        public  Web3 GetWeb3Async()
        {
            var web3 = new Web3 { Client = { OverridingRequestInterceptor = _ethereumProviderRequestInterceptor } };
            Network = web3.Net.Version.ToString();
            return web3;
        }

    }
}
