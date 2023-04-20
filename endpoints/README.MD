INFORMATION BELOW IS WORK IN PROGRESS AND IS SUBJECT TO CHANGE

# Create Procedure
<a name="create-procedure"></a>

For API reference, see [Create Procedure API](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=ets-write-eforms-api&operation=post-external-write-v1-procedure).

Successful request to create a procedure results in http status 201 response with the response body described in API reference. Possible error responses with status codes, in addition to general errors, are:

- 400 Validation failure. Request structure was invalid. Problem details will include 'errors' as additional property. Errors include all possible errors caused by Hilma custom validations.

For general error handling details, see [Error Handling](#error-handling).

# Create Notice
<a name="create-notice"></a>

To create a notice, a procedure must be created first. To create a procedure, see [Create Procedure](#create-procedure).

For API reference, see [Create Notice API](https://hns-hilma-test-apim.developer.azure-api.net/api-details#api=ets-write-eforms-api&operation=post-external-write-v1-procedure-procedureid-notice-etsidentifier).

Note that not all notice types are supported in Hilma. Trying to create a non supported notice type will result in validation error. Currently supported notice types are:
- Prior information notices from 4 to 9 and 25 to 28
- Contract notices from 14 to 24
- Contract award notices from 29 to 37
- Ex-Ante notices from 25 to 28
- Contract modification notices from 38 to 40
- Social notices 20 and 21 and 33 to 25A

Notice types E1 to E5 are not yet supported.
  - Will be supported from 1.1.2024

## Statistical questions

- Statistical questions are asked only in procurement and contract-award notice types
- Statistical questions are:
  - Main statistical questions
    - Preparation of the procurement(social and contract-award  procurements)
  - Clean vehicles
  - Accessability
- Notices are visible in Hilma just like notices that are created in hilma web-application
- Statistical questions that can be sent to TED, are visible in TED and HILMA
  - If questions can not be sent to TED, then they are visible only in HILMA

### Statistical questions in detail( yes/no)
  - All questions are mandatory to answer

#### Innovation
  - In preparation, need and opportunities for innovation was considered.
  - The solution sought after, or part of it, is new to us as a buyer. 
  - The solution sought after, or part of it, is new to the market or industry. 

#### Environmental sustainability
  - Procurement takes into account energy efficiency
    - Energy-using equipment and solutions, and construction
  - This procurement promotes low carbon emissions. 
    - Emission reduction; reducing the production and use of fossil energy or increasing the production and use of renewable energy.
  - This procurement promotes circular economy.
  - This procurement promotes biodiversity
  - This procurement promotes a sustainable food system.
    - Promotion of good farming practices, promotion of food safety, promotion of animal welfare.
  - Are Motiva's, ecolabels' or EU GPP criteria used in the procurement? 

#### Social sustainability
  - This procurement promotes fair working conditions. 
  - This procurement includes a requirement to provide employment for people with partial work capacity
  - Code of Conduct is used in this procurement. 

#### Consideration of SMEs
  - Procurement allows for participation of small and medium-sized enterprises.

## Example json payload for hilma statistics

- Note: key for statistics payload is LOT id
  - LOT ID is defined in Eform's json:
  ```json
  "ProcurementProjectLot": [
    {
      ...
      "ID": { "schemeName": "Lot", "Value": "LOT-0000" }
      ...
    }
  }
  ```

```json
{
  "LOT-0000": {
    "EnergyEfficiencyConsidered": false,
    "LowCarbon": true,
    "CircularEconomy": true,
    "Biodiversity": false,
    "SustainableFoodProduction": false,
    "ListedGreenCriteriaUsed": true,
    "JustWorkingConditions": true,
    "EmploymentCondition": false,
    "HowManyOpportunitiesIsEstimated": false,
    "CodeOfConduct": true,
    "InnovationConsidered": true,
    "SolutionNewToBuyer": true,
    "SolutionNewToMarketOrIndustry": false,
    "SMEParticipationConsidered": false,
    "EndUserInvolved": true,
  }
}

```

## Hilma to TED mapping values. (hilma key -> TED value)
- TED field: **bt-776**
- codelist: **innovative-acquisition**
    "SolutionNewToBuyer": "org-nov",
    "SolutionNewToMarketOrIndustry": "mar-nov",

- codelist: **environmental-impact**
    "CircularEconomy": "circ-econ",
    "Biodiversity": "biodiv-eco",

- codelist: **social-objective**
  "CodeOfConduct": "hum-right",
  "EmploymentCondition": "opp",
  "JustWorkingConditions": "work-cond",

## Special mappings
  - Special mappings means that certain value in statistics object updates eform values
  - field: "SMEParticipationConsidered"
    - If set to True and procurement is not contract-award,
      -  will set eforms(lot instance) ProcurementPlot.ProcurementProject to True



Successful request to create a notice results in http status 201 response with the response body described in API reference. Possible error responses with status codes, in addition to general errors, are:

- 400 Validation failure. Notice or request structure was invalid. Problem details will include either 'errors' and/or 'tedValidationErrors' additional properties. Errors include all possible errors caused by Hilma custom validations. TedValidationErrors includes full TED validation report XML as a string.
- 404 Procedure not found. Specified procedure does not exist.
- 409 Conflict. ETS identifier or some other identifier already exists and cannot be reused.
- 502 Upstream error. Hilma API failed to validate the notice in TED

For general error handling details, see [Error Handling](#error-handling).

# Hilma API Error Handling
<a name="error-handling"></a>

All successful requests return a http success status (200, 201 or 204). Likewise all errors are returned with either a 4xx or 5xx http status code. For most errors, the response body will contain problem details described below. With the exception of 503 when only a string response is returned.

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

Title and detail contain human readable error messages and additional properties provide further details about the error. Additional properties returned are explained in more detail along with each endpoint.

Common error responses to all endpoints are
- 403 Authorization failure. API user was not authorized to perform requested operation.
- 500 Unknown error or operation cancelled. An unknown/unexpected error occured while processing the request or request was cancelled.
- 503 API unavailable.

