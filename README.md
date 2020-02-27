# TOC
1. [hilma-api](#hilma-api)
2. [Release notes](#release-notes)
3. [Basic usage](#instructions)


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

## Basic API usage
<a name="instructions"></a>

Send requests, get responses.
