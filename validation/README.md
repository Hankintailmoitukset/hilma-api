# EForms Hilma specific validation rules
* [Validation](#custom-validation)
* [Procedure](#procedure)

# Validation
<a name="custom-validation"></a>
Hilma API validation for eForms is based on TED validation with a few exceptions. These exceptions are listed in json files at: https://github.com/Hankintailmoitukset/hilma-api/blob/master/validation/custom_validation_rules.json
The custom rules use the following structure:
```json
"Field Path": {
  "btId": "none",
  "required": true,
  "max": 111,
  "regex": "^[0-9]{8}$",
  "ted_email": true,
  "numeric": true,
  "between": { "min": 1, "max": 100 },
  "nationalidentifierorvatnumber": true,
  "nationalorprivateroadidentifier": true,
  "codeLists": [
    {
      "codeList": "codelist-a",
      "required": true,
      "max": 222,
      "regex": "^[0-9]{8}$",
      "ted_email": true,
      "numeric": true,
      "between": { "min": 2, "max": 200 },
      "nationalidentifierorvatnumber": true,
      "nationalorprivateroadidentifier": true
    }
  ]
}
```
"Field Path" represent the path of the field in eNotice (e.g. procurementProjectLot.procurementProject.plannedPeriod.durationMeasure), "btId" is the BT number of the field and the remaining fields are possible custom validation rules. 
Each of the custom validation rules can be present once or not at all. Additionally, if the field path can contain one or more code list values, each of them can have their own custom validation rules. 
Note that only a root or code list based custom validations are in use per field, not both.

"nationalidentifierorvatnumber" and "nationalorprivateroadidentifier" validators validate the field to be either a valid Finnish Business ID (Y-Tunnus), any valid European VAT Number or a valid "Käyttöoikeusyksikkötunnus" issued for a private road.

**NOTE:** When eForms SDK [fields.json](https://github.com/OP-TED/eForms-SDK/blob/develop/fields/fields.json) defines the field **FORBIDDEN** then the Hilma specific validation rules for that field does not apply and the field is not used in the given notice 

# Procedure
<a name="Procedure"></a>
Hilma API introduces a new concept of procedure on top of TED eNotice. 
Procedure is a container for related notices and all notices created over the API must be part of a procedure.
Procedures impose restrictions on notices created in them. Currently, these restrictions are:
  * Only a single notice in state "Draft" or "WaitingToBePublished" is allowed.
    * This means after sending a notice for publication a new notice cannot be added to the same procedure until the first
  * Procedure may only contain a single, published contract notice, excluding corrigendum to a contract notice inside the procedure.
    * Contract notices: 14, 15, 16, 17, 18,19, 20, 21, 22, 23, 24
notice has been published or rejected
