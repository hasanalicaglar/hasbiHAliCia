This web application has been created using ASP.NET, EntityFramework, SQLDependency, AJAX, SignalR environments and tools as a training output.

front&back-end: Hasan Ali ÇAĞLAR
Copyleft - July 2023

Adjust the connection strings according to you. (in ~Veri/VeritabaniBaglami.cs & ~Veri/IletiAbonesi.cs)
Set your SQL server's BROKER value to 1 for the SQLDependency plugin to work correctly. (
  SELECT IS_BROKER_ENABLED FROM SYS.DATABASES WHERE NAME='hasbiHAliCia'
  ALTER DATABASE hasbiHAliCia SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE
)