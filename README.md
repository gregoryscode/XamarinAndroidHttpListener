# XamarinAndroidHttpListener
Simple HTTP listener for Xamarin Android

**Usage**

Just add the prefixes you want to listen with the port number to your `HttpListener`:

```
_listener.Prefixes.Add($"http://+:1616/MyHttpListener/");
```

And then in the `HandleRequest`, check if your url has been called:

```
string url = context.Request.Url.ToString();

if(url.Contains(TurnOnCameraController))
{
    response = "<html><h1>The camera has been turned on.</h1></html>";
}
```

**HttpListener in Mono**

As told by [J Millis](https://stackoverflow.com/a/3071515/2466497) in StackOverflow:

> The `HttpListener` class in Mono works without much of a problem. I think that the most significant difference between its usage in a MS environment and a Linux environment is that port 80 cannot be bound to without a `root/su/sudo security`. Other ports do not have this restriction. For instance if you specify the prefix: http://localhost:1234/ the `HttpListener` works as expected. However if you add the prefix http://localhost/, which you would expect to listen on port 80, it fails silently. If you explicitly attempt to bind to port 80 (http://localhost:80/) then you throw an exception. If you invoke your application as a super user or root, you can explicitly bind to port 80 (http://localhost:80/).


