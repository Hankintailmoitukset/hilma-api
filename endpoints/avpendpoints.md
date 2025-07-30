To use the read api you need to create a subscription key that you need to send in the `Ocp-Apim-Subscription-Key` header along with your request.
You can obtain a subscription key by signing up here for [prod](https://hns-hilma-prod-apim.developer.azure-api.net) or [test](https://hns-hilma-test-apim.developer.azure-api.net) and creating a new subscription at Products > avp-read > Subscribe.

# Search Notices
<a name="search"></a>

For API reference, see [Search Notices](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-api&operation=eform-search). (Sign in required)

Search for EForms and non-EForms according to Azure Search queries: https://docs.microsoft.com/en-us/rest/api/searchservice/search-documents. Both, EForms and non-EForms (pre-EForms) notices can searched from the same index, including all non-EForms notices published prior to EForms switch over.
Procurement plans are also available through the same index. They can be distinguished by the flag: isPlan.

For more information about fields available in the index, see [Index Definition](#index-definition).

Note that index does not include full notice information. Full notices can be fetched using [Read Endpoints](#read) using notice IDs available in the index. 

# Index definitions and documentation
<a name="index-definition"></a>

For API reference, see [Index Definitions](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-api&operation=eform-definition). (Sign in required)

Execute this query to get the latest EForms and non-EForms notices index fields, parameters and their respective attributes. Note that both, EForms and non-EForms notices share the same model.

# Read Notices
<a name="read-notices"></a>

Reading full notices has been split into two, one for EForms and one for non-EForms notices. EForms can be read either one notice at a time or upto maximum of 50 at a time. Non-EForms notices are limited to one at a time.

Note that all read endpoints have rate limits in use and the endpoints should be used in moderation.

For error handling for read endpoints, see [Error Handling](https://github.com/Hankintailmoitukset/hilma-api/blob/master/endpoints/errorhandling.md)

For API reference to read single EForms notice, see [Read EForms Notice](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-eforms-api&operation=get-external-read-v1-notice-noticeid). (Sign in required)

For API reference to read multiple EForms notices, see [Read EForms Notices](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-eforms-api&operation=get-external-read-v1-notices). (Sign in required)

For API reference to read single non-EForms notice, see [Read Notice](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-notice-api&operation=get-api-avp-notices-noticeid). (Sign in required)

EForms full notice is provided as Base64 encoded EForms XML document. For more information on the structure, see [EForms SDK](https://github.com/OP-TED/eForms-SDK)

# Read Plans
<a name="read-plans"></a>

Reading plans has been implemented in a similar fashion to eForms. You can get them one at a time or up to 50 at a time. Also rate limits are used in these endpoints, so it should be used in moderation.

For API reference to read single procurement plan, see [Read Procurement Plan](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-eforms-api&operation=get-external-read-v1-plan-planid). (Sign in required)

For API reference to read multiple procurement plans, see [Read Procurement Plans](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-eforms-api&operation=get-external-read-v1-plans). (Sign in required)
