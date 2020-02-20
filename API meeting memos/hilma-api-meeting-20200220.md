# Hilma API meeting 20.2.2020 14:00-14:30 (EET)

## Participants

- Antti Remes
- Ari Nurkkala
- Janne Forsell
- Esa Ikonen
- Marko Rossi

## Topics

### Upcoming API changes/features

* Hilma will send information about possible attachments or external links to TED. The information will be appended to ComplementaryInformation_INFO_ADD field. Due to this, ComplementaryInformation.AdditionalInformation (VI.3) max characters is reduced to 9800 (was 10000) in ETS API. Next production release will be pushed live today.
* Hilma testing environment will be moved to another environment which is in par with production environment. 
  * API Developer portal: https://hns-hilma-staging-apim.developer.azure-api.net/. 
  * UI: https://hilma-staging.azurefd.net/
  * The "48h rule" will be deactivated in test/staging environment
  * Please register to the new environment as soon as possible.
* Near future
  * Notice cancellation through ETS API
  * Documentation enhancements

## Known bugs

* EU ulkopuoliset nuts codet eivät vieläkään ole kunnossa. Janne tekee tiketin.
* F17 ilmoitukset eivät palauta XML dokumenttia. Janne tekee tiketin.
