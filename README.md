WinApp Data Fetcher
===================

Is a C# library used to get information about **Windows Phone** or **Windows Store Apps**. 

App information is obtained fetching it through internet visiting each App Store web page, 
his allows you get updated infomation for every app you want and uses this information in your any way.

You only need to know two things about an App

* App Guid 
* What kind of app it's (Windows Phone or Windows Store) 

Usage Example
-------------

`
var phoneGuid = "6908b8c1-c106-496a-a5e3-004322baecc7";
var parser = new StoreParser(new PhoneStoreBehavior());
           
IStoreApp appData = await parser.GetStoreAppDataAsync(phoneGuid);

Console.WriteLine(appData);
`

