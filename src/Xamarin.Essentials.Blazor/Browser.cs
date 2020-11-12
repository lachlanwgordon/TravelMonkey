using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Xamarin.Essentials.Blazor
{
    public class Browser : Xamarin.Essentials.Interfaces.IBrowser
    {
        private readonly IJSRuntime JSRuntime;

        public Browser(IJSRuntime jsRuntime)
        {
            JSRuntime = jsRuntime;
        }

        public async Task OpenAsync(string uri)
        {
            await JSRuntime.InvokeAsync<object>("open", uri, "_blank");
        }

        public Task OpenAsync(string uri, BrowserLaunchMode launchMode)
        {
            return OpenAsync(uri);
        }

        public Task OpenAsync(string uri, BrowserLaunchOptions options)
        {
            return OpenAsync(uri);

        }

        public Task OpenAsync(Uri uri)
        {
            return OpenAsync(uri);

        }

        public Task OpenAsync(Uri uri, BrowserLaunchMode launchMode)
        {
            return OpenAsync(uri);
        }

        public async Task<bool> OpenAsync(Uri uri, BrowserLaunchOptions options)
        {
            await OpenAsync(uri.ToString());
            return true;
        }
    }
}
