Prayas — a Self-Service Reporting Portal I built at BFIL.

The business problem was that over 20,000 field staff across India needed access to their own operational data — loan reports, collections, targets — but they were completely dependent on the central team to pull reports manually. This caused delays, bottlenecks, and errors. I was given the responsibility to design and deliver this portal end-to-end.

Technologies: ASP.NET Core 8, MVC, REST APIs, ADO.NET , SQL Server, jQuery, Ajax, and Bootstrap, deployed on Azure App Services.

Featues : since 20,000+ users from different branches, regions, and roles were accessing the same portal, I implemented row-level security so each user could only see data within their own operational boundary. Not a single user could access another branch's data.

I set up a full CI/CD pipeline using GitHub Actions with separate DEV and PROD stages and a manual approval gate before every production release

The outcome : 20,000+ staff could now access their own reports instantly, without raising a single request to the central team. It saved significant manual effort every week and improved field productivity across the organization."
