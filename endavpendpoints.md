# Search Notices
<a name="search"></a>

For API reference, see [Search Notices](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-api&operation=eform-search). (Sign in required)

Search for EForms and non-EForms according to Azure Search queries: https://docs.microsoft.com/en-us/rest/api/searchservice/search-documents. Both, EForms and non-EForms (pre-EForms) notices can searched from the same index, including all non-EForms notices published prior to EForms switch over.

For more information about fields available in the index, see [Index Definition](#index-definition).

Note that index does not include full notice information. Full notices can be fetched using [Read Endpoints](#read) using notice IDs available in the index. 

# Index definitions and documentation
<a name="index-definition"></a>

For API reference, see [Index Definitions](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-api&operation=eform-definition). (Sign in required)

Execute this query to get the latest EForms and non-EForms notices index fields, parameters and their respective attributes. Note that both, EForms and non-EForms notices share the same model.

# Read Notices
<a name="read"></a>

Reading full notices has been split into two, one for EForms and one for non-EForms notices. EForms can be read either one notice at a time or upto maximum of 50 at a time. Non-EForms notices are limited to one at a time.

Note that all read endpoints have rate limits in use and the endpoints should be used in moderation.

For API reference to read single EForms notice, see [Read EForms Notice](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-eforms-api&operation=get-external-read-v1-notice-noticeid). (Sign in required)

For API reference to read multiple EForms notices, see [Read EForms Notice](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-eforms-api&operation=get-external-read-v1-notices). (Sign in required)

For API reference to read single non-EForms notice, see [Read EForms Notice](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=avp-read-notice-api&operation=get-api-avp-notices-noticeid). (Sign in required)

EForms full notice is provided as Base64 encoded EForms XML document. For more information on the structure, see [EForms SDK](https://github.com/OP-TED/eForms-SDK)
