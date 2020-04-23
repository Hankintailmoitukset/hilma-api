# Hilma API meeting 23.4.2020 14:00-14:30 (EET)

## Participants
- Henri Hietala
- Tuomas Tolvanen
- Esa Ikonen
- Jyri Turkia
- Antti Remes
- Marko Rossi

## Topics
- New features (pending deployment)
  - National small value procurement [noticeType=9912]
  - Small value procurement social and specific services [noticeType=9913]
  - ETS API
    - POST requests will return same error messages as PUT
    - Fixed organisation information not updating anymore when department information is used
  - AVP API
    - Rate-limits set to 6 requests per 60 seconds
- Defence notices F17 & F18: removed CommunicationInformation.ElectronicAccess field (TED URL_INFORMATION)

## Known issues
- DisagreeToBePublished for value fields **should not be set** if it is not mentioned in the API documentation. ETS API will return an error if DisagreeToBePublished is set (true) in any other case.
