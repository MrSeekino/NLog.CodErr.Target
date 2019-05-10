NLog.CodErr.Target
=====================
NLog.CodErr.Target is a target that allows your app to send errors to an instance of CodErr (https://coderr.io/)

Latest Version
--------------
The best and quickest way to get the latest release of NLog.CodErr.Target is to add it to your project using 
NuGet (<https://www.nuget.org/packages/NLog.CodErr.Target>).

Alternatively binaries can be builded by opening "NLog.CodErr.Target.sln" available in current repository under [Sources](https://github.com/MrSeekino/NLog.CodErr.Target/tree/master/Sources) folder

Bug Reports
-----------
If you find any bugs, please report them using the [GitHub issue tracker](https://github.com/MrSeekino/NLog.CodErr.Target/issues) with as much details as you can.

Licenses
--------
This software is distributed under the terms of the Free Software Foundation [Lesser GNU Public License (LGPL), version 3.0](http://www.gnu.org/licenses/lgpl-3.0-standalone.html) (see [LICENSE](LICENSE)).

Examples
--------
An example project that uses EntityFramework.FluentHelper can be found under the [Examples](https://github.com/MrSeekino/NLog.CodErr.Target/tree/master/Examples) folder in current repository.

Usage
-----
Add NLog.CodErr.Target to your project, than edit your NLog configuration has shown below:

Add assembly to your extensions
```
<extensions>
  <add assembly="NLog.CodErr.Target"/>
</extensions>
```

Add a target with type **CodErr**
```
<targets>
  <target name="codErr" xsi:type="CodErr" appUrl="http://mycoderr.uri" appKey="myKey" appSecret="mySecret"  />
</target>
```

Add a rules for the target
```
<rules>
  <logger name="*" minlevel="Info" writeTo="codErr" />
</rules>
```

Target Options
--------------
The following are the available options for the target that can be specified as target attributes

```
- appUrl: the url of your coderr instance [Required]
- appKey: the app key to use when sending errors [Required]
- appSecret: the app secret to use when sending errors [Required]
- maxAttempts: maximum number of tries to send an error [Default: 3]
- maxQueueSize: maximum size of errors in internal queue to be sent [Default: 10]
- retryIntervalMinutes: minutes to wait until the next try when an error is failed to be sent [Default: 5]
- async: whether the process that send errors is async or not [Default: True]
- onlyExceptions: whether to send only errors containing an exception or also those with just text messages [Default: True]
```
