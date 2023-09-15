# Hilma API Error Handling
<a name="error-handling"></a>

All successful requests return an HTTP success status (200, 201, or 204). Likewise, all errors are returned with either a 4xx or 5xx HTTP status code. For most errors, the response body will contain the problem details described below. A couple of exceptions to this exist which are described in [Exceptions](#exceptions)

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

# Exceptions
<a name="exceptions"></a>

The following error situations do not contain problem details in the response body
- 404 when endpoint attempted to reach does not exist a following json is returned
 ```json
{
  "statusCode": 404,
  "message": "Resource not found"
}
```
- 406 when request is blocked by security rules body contains error as string only
- 429 when request is dropped by rate limits imposed on the endpoint body contains error as string only
- 503 when API is unavailable body contains error as string only

