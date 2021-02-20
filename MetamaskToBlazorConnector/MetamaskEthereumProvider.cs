using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetamaskToBlazorConnector
{
    public class MetamaskEthereumProvider : IEthereumProvider
    {
        private readonly EthereumProviderJSInterop _ethereumProviderJSInterop;
        private MetamaskRequestInterceptor _metamaskRequestInterceptor;
        public bool Available { get; private set; }
        public string Account { get; private set; }

        public string Network { get; private set; }

        public string NameOfProvider { get; private set; }

        public MetamaskEthereumProvider(EthereumProviderJSInterop ethereumProviderJSInterop)
        {
            _ethereumProviderJSInterop = ethereumProviderJSInterop;
            _metamaskRequestInterceptor = new MetamaskRequestInterceptor(_ethereumProviderJSInterop, this);
            NameOfProvider = "MetaMask";
        }
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
        public Task<Web3> GetWeb3Async()
        {
            var web3 = new Web3 { Client = { OverridingRequestInterceptor = _metamaskRequestInterceptor } };
            Network = web3.Net.Version.ToString();
            return Task.FromResult(web3);
        }
    }
}
