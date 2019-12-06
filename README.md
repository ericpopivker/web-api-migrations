# Web Api Migrations

Handle breaking changes in RESTFul ASP.NET Web API.

Similar to how stripe does it:
https://stripe.com/blog/api-versioning



## Features

* Core Functionality: Proper handling of breaking changes without affecting clients
* Provide a set of common breaking changes class, but allow customers to easily write their own
* Log of breaking changes for whole API, Resource, Action, Property
* Versioning is standardized using date labels like Stripe API: https://stripe.com/docs/upgrades#api-changelog
* Supports webhook changes

### V2
* Swagger documentation auto generated for each version



##  Breaking changes in REST API

This changes stripe considers backwards incompatible
https://stripe.com/docs/upgrades#what-changes-does-stripe-consider-to-be-backwards-compatible

There are multiple of changes that may break the RESP Api contract:

* rename request/response field name
* a change in a request/response field type (string to int)
* a change in a request/response field size (string(500) to string(100))
* remove resource
* remove request/response property
* add/remove/rename enums


## Implementation Notes

Should we log all changes (breaking and not) to get a complete change log?

Stripe keeps Weebhooks and API documented in one place. Probably makes sense to always have Webhooks as part of API.



## Interesting links


https://byrondover.github.io/post/restful-api-guidelines/#breaking-change
- Versioning, Breaking Changes, Depreciation
- Very good article in general
