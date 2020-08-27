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

## Upcoming changes

### CONTENT OF THE NEXT TED XML PACKAGE, RELEASE 2020
We would like to inform you about the content of the next TED XML package, Release 2020. Please see below the summary of the changes:
•	NUTS upgrade
NUTS-2021 classification will replace NUTS-2016.
See https://ec.europa.eu/eurostat/web/nuts/history

####	NUTS extended with Country codes
For all regions which are not covered by the official NUTS, the Country code is used as NUTS code. Specific value ‘00’ will be removed.
####	Currencies update
Alignment with EU-Vocalularies code list
Add: MRU, STN, SVC, VES. Remove: CYP, EEK, LTL, LVL, MRO, MTL, SKK, STD, VEF.
####	eMail in XML R2.0.8
Rule R123 will have severity ERROR
R123 = Section I.1 Contracting authority/entity: the E-mail is required.
#### Legal Basis and Legal Basis Other
No change for Member States; the new schema release is less restrictive; hence, a rule will control the specific use.
####	eMail and URL
No change for Member States; the new R2.0.9 schema release is less restrictive; hence, a rule will control the specific use.
####	Deprecated schema
Removal of the DEVCO schema (forms D01, D02, ...)
European External Aid will use standard forms F01, F02, …
The details on the implementation and on the planning will be communicated during the first half of September.
Best regards
OJS eSenders Team


### Statics
New property will be added to EtsNoticeContract: HilmaStatics. The questions should be answered by the contracting authority. The object corresponds to "Statistics section" of Hilma UI.

``` csharp
public HilmaStatistics HilmaStatistics { get; set; }
```

``` csharp
    [Contract]
    public class HilmaStatistics
    {
        /// <summary>
        /// The procurement takes energy efficiency into consideration
        /// </summary>
        public bool EnergyEfficiencyConsidered { get; set; }

        /// <summary>
        /// The procurement takes innovations into consideration
        /// </summary>
        public bool InnovationConsidered { get; set; }

        /// <summary>
        /// The procurement is inclusive to SMEs.
        /// </summary>
        public bool SMEParticipationConsidered { get; set; }

        [NoValidation]
        public ValidationState ValidationState { get; set; }
    }
```

### National notice validation

National notice validation is rolled out in near future. At first, the validation is optional and only available for two notice types. NationalContract (9902) and NationalAgricultureContract (9902). The feature will be up for testing soon and will be opt-in at first. Both, POST and PUT endpoints of the EtsApi will be accepting a new query parameter experimentalValidation, which, when set to true, will enable the validation. The validation report, in case of errors (there are no warnings, the validation is strict), a bad request is returned with error details in the response body.

``` csharp
[SwaggerResponse(202, "Notice update was successful, summary of the updated notice.", typeof(EtsNoticeSummary))]
[SwaggerResponse(201, "Notice creation was successful, summary of the created notice.",
    typeof(EtsNoticeSummary))]
[SwaggerResponse(400, "Notice creation failed. returns errors describing the failure.", typeof(string))]
[SwaggerResponse(409, "Specified EtsIdentifier already exists for this Ets API subscription", typeof(string))]
[HttpPut("{etsIdentifier}")]
public async Task<ActionResult<EtsNoticeSummary>> Put([FromBody] EtsNoticeContract dto,
    [FromRoute] string etsIdentifier, CancellationToken token,
    [FromQuery] string parentId = "",
    [FromQuery] int projectId = 0,
    [FromQuery] bool experimentalValidation = false)
{
 // Implementation
}
```


## Release notes
<a name="release-notes"></a>

Here we will publish information about api hotfixes, upcoming changes and documentation updates


| Release date | Changes |
|--------------|---------|
| 9.7.2020 hotfix | Resending rejected corrigendums to defence family of notices should now work |
| 1.7.2020 hotix | ExAnte notices can now correctly hide the final total value on utility procurements
| 7.2.2020 hotfix    | Fixed null reference issues in defence corrigendum notice factory. Fixed corrigendum notices to generate new notice numbers on create |
| 10.2.2020 hotfix | Fixed issue with notice validation incorrectly using depricated TED schema version. Now the validation uses the latest release |
| 20.2.2020 release | 1.4.0 Release. notice.additionalInformation max size is reduced to 9800 characters due to adding link and attachment information to notice |
| 24.2.2020 hotfix | The update fixed the schema validation of defence notices. From now on the validation errors as well as produced xml is returned for defence notices in production as well as in staging and testing environments. Fixed issue #16, F17 xml issue. |
| 11.3.2020 release | Added a new ted publish state ApprovedForPublish. See details below. New api for simulation notice publish and cancellation of notice publish for production TED |
| 26.3.2020 hotfix | National defence contract didn't have UI support for procedure type other. Support is now added. |
| 15.4.2020 TED Change in effect | ContractType must match Main CPV code values also in contract and prior information notices. This validation is changing from WARNING to ERROR on 15th of April. Further information: https://op.europa.eu/en/web/eu-vocabularies/e-procurement/tedschemas |
| 4.5.2020 | New validation for disagreeToBePublished. The validation allow the field disagreeToBePublished to be set true only for F06 totalValue and finalTotalValue field and national contract notice estimatedValue fields. Other use will result in validation errror in the ets api. The fix can be tested before production in staging environment | 


### Notice status changes

New property for notice to signal if notice should not be published to search index.
Note: this only affects national small value procurements

ETS users can add or update departments with a department identifier. The identifier is found in organisation.departmentIdentifier


We have added extra ted status to handle better publish process details in TED api.


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
