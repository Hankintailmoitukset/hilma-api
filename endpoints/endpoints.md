# Create Procedure
<a name="create-procedure"></a>

For API reference, see [Create Procedure API](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=ets-write-eforms-api&operation=post-external-write-v1-procedure). (Sign in required)

A successful request to create a procedure results in an HTTP status 201 response with the response body described in the API reference. Possible error responses with status codes, in addition to general errors, are:

- 400 Validation failure. The request structure was invalid. Problem details will include 'errors' as an additional property. Errors include all possible errors caused by Hilma's custom validations.

For general error handling details, see [Error Handling](#error-handling).

# Create Notice
<a name="create-notice"></a>

To create a notice, a procedure must be created first. To create a procedure, see [Create Procedure](#create-procedure).

For API reference, see [Create Notice API](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=ets-write-eforms-api&operation=post-external-write-v1-procedure-procedureid-notice-etsidentifier). (Sign in required)

Note that not all notice types are supported in Hilma. Trying to create a non-supported notice type will result in a validation error. Currently, supported notice types are:
- Prior information notices from 4 to 9 and 25 to 28
- Contract notices from 14 to 24
- Contract award notices from 29 to 37
- Ex-Ante notices from 25 to 28
- Contract modification notices from 38 to 40
- Social notices 20 and 21 and 33 to 25A

Notice types E1 to E5 are not yet supported.
- Will be supported from 1.1.2024


Successful request to create a notice results in HTTP status 201 response with the response body described in the API reference. Possible error responses with status codes, in addition to general errors, are:

- 400 Validation failure. Notice or request structure was invalid. Problem details will include either 'errors' and/or 'tedValidationErrors' additional properties. Errors include all possible errors caused by Hilma's custom validations. TedValidationErrors includes full TED validation report XML as a string.
- 404 Procedure not found. The specified procedure does not exist.
- 409 Conflict. ETS identifier or some other identifier already exists and cannot be reused.
- 502 Upstream error. Hilma API failed to validate the notice in TED

For general error handling details, see [Error Handling](#error-handling).

# Hilma API Error Handling
<a name="error-handling"></a>

All successful requests return an HTTP success status (200, 201, or 204). Likewise, all errors are returned with either a 4xx or 5xx HTTP status code. For most errors, the response body will contain the problem details described below. Except 503 when only a string response is returned.

```json
{
  "type": "string",
  "title": "string",
  "status": 0,
  "detail": "string",
  "instance": "string",
  "additionalProp1": "string",
  "additionalProp2": {},
  "additionalProp3": []
}
```

Title and detail contain human-readable error messages and additional properties provide further details about the error. Additional properties returned are explained in more detail along with each endpoint.

Common error responses to all endpoints are
- 403 Authorization failure. API user was not authorized to perform the requested operation.
- 500 Unknown error or operation canceled. An unknown/unexpected error occurred while processing the request or the request was canceled.
- 503 API unavailable.
