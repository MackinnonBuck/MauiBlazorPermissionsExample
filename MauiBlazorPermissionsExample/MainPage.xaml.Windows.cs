using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Web.WebView2.Core;
using System;

namespace MauiBlazorPermissionsExample;

public partial class MainPage
{
    // It might be perfectly acceptable to not override the WebView2 permission requesting behavior
    // at all, relying on the default popup to decide whether to grant specific permissions. However,
    // one reason to override this behavior is to prevent the user from getting "stuck" if they accidentally
    // deny a permission requested by the WebView. In an actual web browser, the user could change their
    // decision using the browser's UI, but no such UI is built in to the WebView2 control. This
    // leaves it up to us to decide what to do in these cases.
    //
    // The implementation below takes a simple approach, allowing all permission requests originating from
    // the base URI while denying all requests coming from an unknown source. No action from the user is
    // required. This results in a user experience that matches what one might expect from a typical
    // native Windows app.
    //
    // However, there are certainly more sophisticated viable approaches here - you could display a custom
    // content dialog that justifies why the app needs a certain permission. This behavior would match that
    // exhibited by iOS and Android apps. Keep in mind that this would require you to implement the mechanism
    // that persists the user's previous app permission decisions and decides when the popup should be displayed.

    private static readonly Uri BaseUri = new("https://0.0.0.0");

    private partial void BlazorWebViewInitializing(object? sender, BlazorWebViewInitializingEventArgs e)
    {
    }

    private partial void BlazorWebViewInitialized(object? sender, BlazorWebViewInitializedEventArgs e)
    {
        e.WebView.CoreWebView2.PermissionRequested += PermissionRequested;
    }

    private void PermissionRequested(CoreWebView2 sender, CoreWebView2PermissionRequestedEventArgs args)
    {
        args.State = Uri.TryCreate(args.Uri, UriKind.RelativeOrAbsolute, out var uri) && BaseUri.IsBaseOf(uri) ?
            CoreWebView2PermissionState.Allow :
            CoreWebView2PermissionState.Deny;
        args.Handled = true;
    }
}
