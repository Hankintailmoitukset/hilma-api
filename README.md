# TOC
* [hilma-api](#hilma-api)
* [Release notes](#release-notes)
* [Basic usage](#instructions)
  * [Summary](#api-summary)
  * [Usage](#api-usage)


# hilma-api
<a name="hilma-api"></a>

Production notice documentation can be found at http://www.hankintailmoitukset.fi/en/documentation

Production api portal is hosted at: https://hns-hilma-prod-apim.portal.azure-api.net/

## Release notes
<a name="release-notes"></a>

Here we will publish information about api hotfixes, upcoming changes and documentation updates


| Release date | Changes |
|--------------|---------|
| 7.2.2020 hotfix    | Fixed null reference issues in defence corrigendum notice factory. Fixed corrigendum notices to generate new notice numbers on create |
| 10.2.2020 hotfix | Fixed issue with notice validation incorrectly using depricated TED schema version. Now the validation uses the latest release |
| 20.2.2020 release | 1.4.0 Release. notice.additionalInformation max size is reduced to 9800 characters due to adding link and attachment information to notice |
| 24.2.2020 hotfix | The update fixed the schema validation of defence notices. From now on the validation errors as well as produced xml is returned for defence notices in production as well as in staging and testing environments. Fixed issue #16, F17 xml issue. |
| 11.3.2020 release | Added a new ted publish state ApprovedForPublish. See details below. |

### Planned changes

We are planning to add extra ted status to handle better publish process details in TED api.


| Ted status | Hilma TedPublishState |
|------------|-----------------------|
| IN_PROGRESS | “AwaitingTedPublish” |
| PUBLISHED (scheduled in future) | "*ApprovedForPublish*”  (NEW)|
| PUBLISHED (scheduled publish time reached in TED) | “PublishedInTed” |

The new values:

 * SendingToTed = 1,
 * SentToTed = 2,
 * AwaitingTedPublish = 3,
 * PublishedInTed = 4,
 * RejectedByTed = 5,
 * WaitingForInformation = 6,
 * NotPublished = 7,
 * **ApprovedForPublish = 8**
 
## Basic API usage
<a name="instructions"></a>

### What is the API?
<a name="api-summary"></a>
Hankintailmoitukset.fi API consists of two products: AVP-Read (Additional Value Provider) and ETS-Write (Electronic Tender Sender). Both API's support self-registration in the portal. To self-register, create subscribtion to corresponding product in the API [portal](https://hns-hilma-prod-apim.developer.azure-api.net/signup).

AVP API is available for use immediately after creating the subscription. ETS API subscriptions are created pending approval. Please contact Hansel Oy at yllapito@hankintailmoitukset.fi to start the process / for additional information. The API keys and such are managed by the user in the self service portal.

#### AVP
AVP APi is for fetching open data from hankintailmoitukset.fi. The API is free to use. Commercial usage is allowed. Request rates however are limited per API key and the API does not serve CORS headers.

#### ETS
ETS is an API to send notices to Hilma and TED. Limitations and requirements on the API usage apply.

### Using the API
<a name="api-usage"></a>
Send requests, get responses.

#### ETS

##### POST /notice/{etsIdentifier}
Endpoint for running validation on a notice. The notice is not commited nor sent upstream. Validation report and generated TED XML is returned.

##### PUT /notice/{etsIdentifier}
Endpoint for sending notices. Notices sent to this endpoint are commited and publication process for them is immediately started. Track the notice publication process using ***GET /notice/{etsIdentifier}***. If there is a problem in the process, the notice will be returned to a "draft" steate, and should be updated using the same EtsIdentifier. The update again starts the publication process right away.

##### GET /notice/{etsIdentifier}
Fetch most recent notice meta-data for the given notice. Optionally includes the full notice dto.

##### GET /notice
Search notices sent by current API user. Supplie query parameters as search predicates. Following filters are available. Before (datetime), After (datetime), HilmaStatus, TedStatus and Type (noticeType). Returns notice meta-data, and optionally the full dto.

##### POST /notice/{etsIdentifier}/status[?noticeStatus][&reasonCode]
Publish or reject a notice in TED. Only available against simulation endpoint.

##### POST /notice/{etsIdentifier}/stop-publication[?simulateError]
Stops publication of notice in TED. Only available in simulation or production TED environments. SimulateError parameter is available only in simulation endpoint.

#### AVP
Due technical limitaions, the API is split into separate API's under the AVP product.

##### POST /notices/docs/search
Search API. This endpoint exposes Azure Cognitive Search search endpoint for the api users. For full documentation and instructions, refer to [Azure documentation](https://docs.microsoft.com/en-us/rest/api/searchservice/search-documents). The exposed endpoint is the more versatile POST endpoint.

For upto date information about the **notices** search index schema, refer to ***GET /notices*** endpoint.

##### GET /notices
Returns current definition document for the notices search index. Refer here to see a list of searchable / filterable / facetable fields etc.
