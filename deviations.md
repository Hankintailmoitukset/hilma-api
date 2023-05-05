# EForms SDK deviations

The purpose of this document is to describe the changes in how Hilma eForms implementation deviates
from the standard eForms SDK

## Differences
1. Custom validations
2. Hilma statistics to eForm fields mapping


## 1. Custom validations

Hilma has added validation for some of the BT fields.
The document to describe all custom validations can be found at https://github.com/Hankintailmoitukset/hilma-api/tree/master/validation.

## 2. Hilma Statistics to eForm fields mapping

**Hilma statistics values override the following eForms BT fields (BT-726, BT-774, BT-775, BT-776).**

Hilma automatically maps the following eForm BT fields to the matching eForm notice procurement project lot fields
from the Hilma statistics values.

When Hilma statistics boolean field is false it removes the corresponding eForms code list value from the matching procurement project lot
and when the value for the statistics field is true it ensures the corresponding eForm notice field is set to the provided notice

| Statistics field              | Statistics value | BT-Field | eForm code list        | eForm value | XML xPath                                                                                                                                                                    |
|-------------------------------|------------------|----------|------------------------|-------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| solutionNewToBuyer            | boolean          | BT-776   | innovative-acquisition | `org-nov`     | `/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:ProcurementProject/cac:ProcurementAdditionalType/cbc:ProcurementTypeCode[@listName='innovative-acquisition']`  |
| solutionNewToMarketOrIndustry | boolean          | BT-776   | innovative-acquisition | `mar-nov`     | `/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:ProcurementProject/cac:ProcurementAdditionalType/cbc:ProcurementTypeCode[@listName='innovative-acquisition']`  |
| circularEconomy               | boolean          | BT-774   | environmental-impact   | `circ-econ`   | `/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:ProcurementProject/cac:ProcurementAdditionalType/cbc:ProcurementTypeCode[@listName='environmental-impact']`    |
| biodiversity                  | boolean          | BT-774   | environmental-impact   | `biodiv-eco`  | `/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:ProcurementProject/cac:ProcurementAdditionalType/cbc:ProcurementTypeCode[@listName='environmental-impact']`    |
| codeOfConduct                 | boolean          | BT-775   | environmental-impact   | `hum-right`   | `/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:ProcurementProject/cac:ProcurementAdditionalType/cbc:ProcurementTypeCode[@listName='social-objective']`        |
| employmentCondition           | boolean          | BT-775   | social-objective       | `opp`         | `/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:ProcurementProject/cac:ProcurementAdditionalType/cbc:ProcurementTypeCode[@listName='social-objective']`        |
| justWorkingConditions         | boolean          | BT-775   | social-objective       | `work-cond`   | `/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:ProcurementProject/cac:ProcurementAdditionalType/cbc:ProcurementTypeCode[@listName='social-objective']`        |
| smeParticipationConsidered    | boolean          | BT-726   |                        | `true` / `null` | `"/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:ProcurementProject/cbc:SMESuitableIndicator"`                                                               |

**NOTE:** When eForms SDK defines the field as **FORBIDDEN**, then the mapping is not done and the field is left empty

General documentation about Hilma Statistics can be found at https://github.com/Hankintailmoitukset/hilma-api/tree/master/endpoints/hilmastatistics.md
