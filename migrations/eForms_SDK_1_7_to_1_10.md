# Hilma EForms 1.7 to 1.10.1 migration guide

This document describes the changes in the eForms SDK from version 1.7 to 1.10.1 in context of Hilma and how to migrate from the old version to the new one.

Hilma APIs will continue to support eForms SDK 1.7 as-is for the time being.
However, when viewing notices in Hilma, the Hilma internal API will convert the notice to eForms SDK 1.10.1 format for display purposes for the UI.

When sending notice through draft API to Hilma in 1.7 format the notice will be converted to 1.10.1 once user edits the notice for the first time.

## Data contract changes 

Call for tender document languages structure has changed.

In SDK 1.7 the languages are located in the following path:

BT-737 non-official languages `"/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/cac:CallForTendersDocumentReference/cbc:LanguageID[../cbc:DocumentStatusCode/text()='non-official']",`
BT-708 official languages: `/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Part']/cac:TenderingTerms/cac:CallForTendersDocumentReference/cbc:LanguageID[not(../cbc:DocumentStatusCode/text()='non-official')]`

OPT-050 DocumentStatusCode = `non-official` or `official`

In SDK 1.10 the languages are located in following path:

BT-737 non-official languages `"/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/cac:CallForTendersDocumentReference/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:NonOfficialLanguages/cac:Language/cbc:ID"`
BT-708 official languages: `"/*/cac:ProcurementProjectLot[cbc:ID/@schemeName='Lot']/cac:TenderingTerms/cac:CallForTendersDocumentReference/ext:UBLExtensions/ext:UBLExtension/ext:ExtensionContent/efext:EformsExtension/efac:OfficialLanguages/cac:Language/cbc:ID"`

OPT-050 DocumentStatusCode value has been removed, it is now clearly separated by the path

For more details see:

sdk 1.7 https://github.com/OP-TED/eForms-SDK/blob/1.7.0/fields/fields.json

sdk 1.10 https://github.com/OP-TED/eForms-SDK/blob/1.10.1/fields/fields.json


Here is an example of how to migrate the languages from 1.7 to 1.10.1:


In SDK 1.7 notice declared Finnish as official language and Swedish and Spanish as non-official language:
```json
{
    "eForm": {
        "procurementProjectLot": [{
                "tenderingTerms": {
                    "callForTendersDocumentReference": [{
                            "id": {
                                "value": "CFTDR_1"
                            },
                            "attachment": {
                                "externalReference": {
                                    "uri": {
                                        "value": "https://testi.hankintailmoitukset.fi//fi/public/procedure/15292/enotice/17301"
                                    }
                                }
                            },
                            "documentType": [{
                                    "value": "non-restricted-document"
                                }
                            ],
                            "documentTypeCode": {
                                "listName": "communication-justification"
                            },
                            "languageID": {
                                "value": "FIN"
                            },
                            "documentStatusCode": {
                                "value": "official"
                            }
                        }, {
                            "id": {
                                "value": "CFTDR_2"
                            },
                            "attachment": {
                                "externalReference": {
                                    "uri": {
                                        "value": "https://testi.hankintailmoitukset.fi//fi/public/procedure/15292/enotice/17301"
                                    }
                                }
                            },
                            "documentType": [{
                                    "value": "non-restricted-document"
                                }
                            ],
                            "documentTypeCode": {
                                "listName": "communication-justification"
                            },
                            "languageID": {
                                "value": "SWE"
                            },
                            "documentStatusCode": {
                                "value": "non-official"
                            }
                        }, {
                            "id": {
                                "value": "CFTDR_3"
                            },
                            "attachment": {
                                "externalReference": {
                                    "uri": {
                                        "value": "https://testi.hankintailmoitukset.fi//fi/public/procedure/15292/enotice/17301"
                                    }
                                }
                            },
                            "documentType": [{
                                    "value": "non-restricted-document"
                                }
                            ],
                            "documentTypeCode": {
                                "listName": "communication-justification"
                            },
                            "languageID": {
                                "value": "SPA"
                            },
                            "documentStatusCode": {
                                "value": "non-official"
                            }
                        }
                    ]
                }
            }
        ]
    }
}
```

In SDK 1.10 the same call for tender document can contain all that information, if the actual document URI is the same:

```json
{
  "eForm": {
    "procurementProjectLot": [
      {
        "tenderingTerms": {
          "callForTendersDocumentReference": [
            {
              "id": {
                "value": "CFTDR_1"
              },
              "attachment": {
                "externalReference": {
                  "uri": {
                    "value": "https://dev.hankintailmoitukset.fi//fi/public/procedure/51348/enotice/51421"
                  }
                }
              },
              "documentType": [
                {
                  "value": "non-restricted-document"
                }
              ],
              "documentTypeCode": {
                "listName": "communication-justification"
              },
              "ublExtensions": [
                {
                  "extensionContent": {
                    "eformsExtension": {
                      "officialLanguages": [
                        {
                          "id": {
                            "value": "FIN"
                          }
                        }
                      ],
                      "nonOfficialLanguages": [
                        {
                          "id": {
                            "value": "SWE"
                          }
                        },
                        {
                          "id": {
                            "value": "SPA"
                          }
                        }
                      ]
                    }
                  }
                }
              ]
            }
          ]
        }
      }
    ]
  }
}

```

And same in XML format:

eForms 1.7
```xml
<cac:ProcurementProjectLot>
    <cac:CallForTendersDocumentReference>
        <cbc:ID>CFTDR_1</cbc:ID>
        <cbc:DocumentType>non-restricted-document</cbc:DocumentType>
        <cbc:LanguageID>FIN</cbc:LanguageID>
        <cbc:DocumentStatusCode>official</cbc:DocumentStatusCode>
        <cac:Attachment>
            <cac:ExternalReference>
                <cbc:URI>https://testi.hankintailmoitukset.fi//fi/public/procedure/15292/enotice/17301</cbc:URI>
            </cac:ExternalReference>
        </cac:Attachment>
    </cac:CallForTendersDocumentReference>
    <cac:CallForTendersDocumentReference>
    <cbc:ID>CFTDR_2</cbc:ID>
    <cbc:DocumentType>non-restricted-document</cbc:DocumentType>
    <cbc:LanguageID>SWE</cbc:LanguageID>
    <cbc:DocumentStatusCode>non-official</cbc:DocumentStatusCode>
    <cac:Attachment>
        <cac:ExternalReference>
            <cbc:URI>https://testi.hankintailmoitukset.fi//fi/public/procedure/15292/enotice/17301</cbc:URI>
        </cac:ExternalReference>
    </cac:Attachment>
    </cac:CallForTendersDocumentReference>
    <cac:CallForTendersDocumentReference>
    <cbc:ID>CFTDR_3</cbc:ID>
    <cbc:DocumentType>non-restricted-document</cbc:DocumentType>
    <cbc:LanguageID>SPA</cbc:LanguageID>
    <cbc:DocumentStatusCode>non-official</cbc:DocumentStatusCode>
    <cac:Attachment>
        <cac:ExternalReference>
            <cbc:URI>https://testi.hankintailmoitukset.fi//fi/public/procedure/15292/enotice/17301</cbc:URI>
        </cac:ExternalReference>
    </cac:Attachment>
    </cac:CallForTendersDocumentReference>
</cac:ProcurementProjectLot>
```

eForms 1.10
```xml
<cac:ProcurementProjectLot>
    <cac:CallForTendersDocumentReference>
        <ext:UBLExtensions>
            <ext:UBLExtension>
                <ext:ExtensionContent>
                    <efext:EformsExtension>
                        <efac:NonOfficialLanguages>
                            <cac:Language>
                                <cbc:ID>SWE</cbc:ID>
                            </cac:Language>
                            <cac:Language>
                                <cbc:ID>SPA</cbc:ID>
                            </cac:Language>
                        </efac:NonOfficialLanguages>
                        <efac:OfficialLanguages>
                            <cac:Language>
                                <cbc:ID>FIN</cbc:ID>
                            </cac:Language>
                        </efac:OfficialLanguages>
                    </efext:EformsExtension>
                </ext:ExtensionContent>
            </ext:UBLExtension>
        </ext:UBLExtensions>
        <cbc:ID>CFTDR_1</cbc:ID>
        <cbc:DocumentType>non-restricted-document</cbc:DocumentType>
        <cac:Attachment>
            <cac:ExternalReference>
                <cbc:URI>https://dev.hankintailmoitukset.fi//fi/public/procedure/51348/enotice/51421</cbc:URI>
            </cac:ExternalReference>
        </cac:Attachment>
    </cac:CallForTendersDocumentReference>
</cac:ProcurementProjectLot>
```


## Hilma UI changes

The following document describes the summary of changes done to Hilma UI during the SDK upgrade process. These changes affect only the notice editing views

#### Organisation
- buyer organization groupLeadIndicator should **not** be defined when there is a single buyer organization in the notice. `ublExtensions.extensionContent.eformsExtension.organizations.organization.groupLeadIndicator`.
- The BT-10 Activity authority field is shown only when the buyer's legal type (BT-11) is a public authority. See SDK for technical details: 
https://github.com/OP-TED/eForms-SDK/blob/1.10.1/fields/fields.json#L2607
- Only selected organization roles are being defined in the eForms, not selecting the role now does not define the role at all. Previously the roles were defined as 'false' when not set


#### Contact point
- When "Add contact point address" is checked, all address fields are required 

#### Place of performance
- In Hilma UI BT-727 "place performance services other" -field empty selection has a help text "Specific place of performance" to guide the user on what it means to leave that field empty
- Selecting BT-727 "anywhere in the given country" - only country selection and additional information is available
- Selecting BT-727 "anywhere" or "Anywhere in the European Economic Area" only additional information is available


#### Deadlines
- in Hilma UI deadline fields (BT-631, BT-130, BT-127, BT-13, BT-630, BT-1311, BT-537) are required to be earliest tomorrow to prevent issues with the notice publication issue date. This is not a validation rule but a UI requirement.
- when BT-631: dispatch invitation interest is set BT-630: deadline receipt expressions has to be later than BT-631
- in EF21 BT-630 deadline receipt expressions field is shown only when the procedure type is "Negotiated with prior publication of a call for competition / competitive with negotiation"

#### Decision-making criteria
- in Hilma UI when notice is a type of result, contract modification, or direct award preannouncement then award criterion number weight type (BT-5421) selection does not allow "middle of a range" options
- When any of the award criterion type (BT-5421, BT-5422, BT-5423) is selected then award criterion number (BT-541) is required
- BT-543 award criteria complicated is shown only when awarding criteria (BT-539 and BT-541) cannot be used
- awarding criteria fields BT-539 award criterion type and BT-540 award criterion description are always required

#### Contract modification notice
- When creating a contract modification notice from scratch it contains one empty contract by default

#### Submission of tenders, participation requests, or answers
- BT-18: Submission URL is not allowed when electronic submission is not allowed
- When an electronic submission is not "Required" then the description of non-electronic submission (BT-745) is required

#### Contract duration fields
- In many notice types the contract duration needs to be defined one way or another. One of the following BT fields or combinations are required: BT-36 duration period, BT-536 duration start date, and BT-537 duration end date or BT-538 duration known or unknown.
- Contract duration end date (BT-537) needs to be after the contract start date (BT-536)

#### Framework agreement fields
- A new "framework agreement" checkbox was added to the notice procurement details page, selecting the checkbox controls if any of the framework agreement fields are shown or not
  - This field is not available through API's and will be selected in the Hilma UI when any of the notice lots have a framework agreement
- Notice result amount fields (BT-161, BT-118 and BT-1118) are disabled when there are no winners selected

#### Awards and selection committee
- BT-47 participant name cannot be repeated


#### Corrigendum notices
- The changes section in the corrigendum notices was renewed. The view now contains change reason (BT-140) and change reason description (BT-762) to be given once and the possibility to define the change itself multiple times using (BT-13716, BT-141, and BT-718)

#### Other small improvements
- The eForms SDK version was added to the notice PDF.
- translations and help texts have been improved
- TED validation errors are shown in the UI 
language
