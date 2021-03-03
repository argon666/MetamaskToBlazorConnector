using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthereumProviders
{
    public partial class WalletConnect
    {
        [Inject]
        public IJSRuntime _jsRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/_content/EthereumProviders/test.js");
            await module.InvokeVoidAsync("showAlert");
        }
    }
}
