# API-Migrations

Versioning for ASP.NET Web API.

Similar to how stripe does it:
https://stripe.com/blog/api-versioning



## Features

* Versioning is standardized using date labels like Stripe API: https://stripe.com/docs/upgrades#api-changelog
* Proper handling of breaking changes without affecting clients
* Log of changes for whole API, Resource, Action, Property
* Swagger documentation auto generated for each version
* Supports webhook changes



##  Breaking changes in REST API

This changes stripe considers backwards incompatible
https://stripe.com/docs/upgrades#what-changes-does-stripe-consider-to-be-backwards-compatible

There are 3 types of changes that may break the RESP Api contract:

* a change in a request/response property type (string to int)
* a change in a request/response property size (string(500) to string(100))
* removing any part of the API (resource, request property, response property, webhook events)


## Implementation Notes

Should we log all changes (breaking and not) to get a complete change log?

Stripe keeps Weebhooks and API documented in one place. Probably makes sense to always have Webhooks as part of API.


## Names for this product

CleanBreak

BulletproofApi

UnofficialStripeVersioning

