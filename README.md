# Telegenic.WebAPI

This WebAPI implements Dependency Injection using Castle.Windsor.\
It is also using nHibernate as the Object Relational Mapper.

# Dependencies
- Castle.Windsor v5.0.1
- FluentNHibernate v2.1.2

# Deployment Notes
This WebAPI should be depoyed as a stand-alone service. I recommend using the following hostname:\
https://webapi.telegenic.com

Service calls should be made using the following URI pattern:\
https://webapi.telegenic.com/default/get/5

