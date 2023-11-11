using Microsoft.JSInterop;

namespace Sales.WEB.Helpers
{
    public static class IJSRuntimeExtensionMethods { 
     public static ValueTask<object> SetLocalStorage(this IJSRuntime js, string key, string content)
    {
        return js.InvokeAsync<object>("localStorage.setItem", key, content);
    }//recordación del usuario

    public static ValueTask<object> GetLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<object>("localStorage.getItem", key);
    }

    public static ValueTask<object> RemoveLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<object>("localStorage.removeItem", key);
    }//se remueve el token cuando se desloguea
}
}
