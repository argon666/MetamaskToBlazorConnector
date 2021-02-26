using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumProviders
{
    class EthereumProviderRequestInterceptor : RequestInterceptor
    {
        private readonly EthereumProviderJSInterop _ethereumProviderJSInterop;
        private readonly IEthereumProvider _ethereumProvider;
        public EthereumProviderRequestInterceptor(EthereumProviderJSInterop ethereumProviderJSInterop, IEthereumProvider ethereumProvider)
        {
            _ethereumProviderJSInterop = ethereumProviderJSInterop;
            _ethereumProvider = ethereumProvider;
        }
        public override async Task<object> InterceptSendRequestAsync<T>(
            Func<RpcRequest, string, Task<T>> interceptedSendRequestAsync, RpcRequest request,
            string route = null)
        {
            if (request.Method == "eth_sendTransaction")
            {
                var transaction = (TransactionInput)request.RawParameters[0];
                transaction.From = _ethereumProvider.Account;
                request.RawParameters[0] = transaction;
                var response = await _ethereumProviderJSInterop.SendAsync(new EthereumProviderRpcRequestMessage(request.Id, request.Method, _ethereumProvider.Account,
                    request.RawParameters));
                return ConvertResponse<T>(response);
            }
            else
            {
                var response = await _ethereumProviderJSInterop.SendAsync(new RpcRequestMessage(request.Id,
                    request.Method,
                    request.RawParameters));
                return ConvertResponse<T>(response);
            }

        }
        public override async Task<object> InterceptSendRequestAsync<T>(
            Func<string, string, object[], Task<T>> interceptedSendRequestAsync, string method,
            string route = null, params object[] paramList)
        {
            if (method == "eth_sendTransaction")
            {
                var transaction = (TransactionInput)paramList[0];
                transaction.From = _ethereumProvider.Account;
                paramList[0] = transaction;
                var response = await _ethereumProviderJSInterop.SendAsync(new EthereumProviderRpcRequestMessage(route, method, _ethereumProvider.Account,
                    paramList));
                return ConvertResponse<T>(response);
            }
            else
            {
                var response = await _ethereumProviderJSInterop.SendAsync(new RpcRequestMessage(route, _ethereumProvider.Account, method,
                    paramList));
                return ConvertResponse<T>(response);
            }

        }
        protected void HandleRpcError(RpcResponseMessage response)
        {
            if (response.HasError)
                throw new RpcResponseException(new Nethereum.JsonRpc.Client.RpcError(response.Error.Code, response.Error.Message,
                    response.Error.Data));
        }

        private T ConvertResponse<T>(RpcResponseMessage response,
            string route = null)
        {
            HandleRpcError(response);
            try
            {
                return response.GetResult<T>();
            }
            catch (FormatException formatException)
            {
                throw new RpcResponseFormatException("Invalid format found in RPC response", formatException);
            }
        }
    }
}
