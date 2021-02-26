using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumProviders
{
    public interface IEthereumProvider
    {
        public bool Available { get; }
        public string Account { get; }
        public string Network { get; }
        public string NameOfProvider { get; }
        public Task<bool> IsProviderInstalled();
        public Task<string> EnableProviderAndGetAddress();
        public Web3 GetWeb3Async();
    }
}
