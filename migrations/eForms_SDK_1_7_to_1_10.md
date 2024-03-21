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
