# Statistical questions

- Statistical questions are asked only in procurement and contract-award notice types
    - ('14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '29', '30', '31', '32', '33', '34', '35', '36', '37', 'E1', 'E3', 'E4')
- For contract award notices, they are only mandatory when BT-142-LotResult for the specific lot has value: 'selec-w', otherwise they are forbidden for that specific lot.
- Statistical questions are:
    - Main statistical questions
        - Preparation of the procurement(social and contract-award  procurements)
    - Clean vehicles
    - Accessibility
- Statistical questions that can be sent to TED, are visible in TED and HILMA
    - If questions can not be sent to TED, then they are visible only in HILMA

## Statistical questions in detail ( yes/no )
- All questions are mandatory to answer

### Innovation
- "In preparation, need and opportunities for innovation was considered." (`statistics.innovationConsidered`)
- "The solution sought after, or part of it, is new to us as a buyer." (`statistics.solutionNewToBuyer`)
- "The solution sought after, or part of it, is new to the market or industry." (`statistics.solutionNewToMarketOrIndustry`)

### Environmental sustainability
- "Procurement takes into account energy efficiency" (`statistics.energyEfficiencyConsidered`)
    - Energy-using equipment and solutions, and construction
- "This procurement promotes low carbon emissions." (`statistics.lowCarbon`)
    - Emission reduction; reducing the production and use of fossil energy or increasing the production and use of renewable energy.
- "This procurement promotes circular economy." (`statistics.circularEconomy`)
- "This procurement promotes biodiversity" (`statistics.biodiversity`)
- "This procurement promotes a sustainable food system." (`statistics.sustainableFoodProduction`)
    - Promotion of good farming practices, promotion of food safety, promotion of animal welfare.

If any of the ENVIRONMENTAL SUSTAINABILITY categories questions are answered "Yes" then a new follow-up question is shown and required to answer.

- "Are Motiva's, ecolabels' or EU GPP criteria used in the procurement?" (`statistics.listedGreenCriteriaUsed`)

### Social sustainability
- "This procurement promotes fair working conditions." (`statistics.justWorkingConditions`)
- "This procurement includes a requirement to provide employment for people with partial work capacity" (`statistics.employmentCondition`)
  - (When this question is answered as yes, then a new follow-up question is shown, which is also required to answer)
  - "How many jobs and apprenticeships are expected to be created as a result of the procurement?" (`statistics.howManyOpportunitiesIsEstimated`)
- "Code of Conduct is used in this procurement." (`statistics.codeOfConduct`)

### Consideration of SMEs
- "Procurement allows for participation of small and medium-sized enterprises." (`statistics.smeParticipationConsidered`)

### Involvement of service users in the preparation of the procurement

The following question is shown only for notice types (**'14', '20', '21', '33', '34', '35'**)
for all other notice types the value is left `null`. When the question is shown, it is required to answer.

- "The participation of service users or their representatives in the preparation of the procurement has been taken into account in this procurement." (`hilmaStatistics.endUserInvolved`)

## Example json payload for hilma statistics


- Key for statistics payload is LOT ID (**BT-137-LOT**)
    - LOT ID is defined in eForms XML payload: `"/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cbc:ID"`
    - The hilma statistics key must be specified once for each lot of the provided notice
    - Extra keys are not allowed in the object

```json
{
  "LOT-0000": {
    "energyEfficiencyConsidered": true,
    "lowCarbon": true,
    "circularEconomy": true,
    "biodiversity": true,
    "sustainableFoodProduction": true,
    "listedGreenCriteriaUsed": true,
    "justWorkingConditions": true,
    "employmentCondition": true,
    "howManyOpportunitiesIsEstimated": 12,
    "codeOfConduct": true,
    "innovationConsidered": true,
    "solutionNewToBuyer": true,
    "solutionNewToMarketOrIndustry": true,
    "smeParticipationConsidered": true,
    "endUserInvolved": null
  }
}
```
