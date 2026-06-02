# Participants
- Jarkko Sikiö
- Tiitus Kivikangas
- Anssi Salo
- Joel Ollikainen
- Mikita Lisouski
- Christian Sixhøi

# Topics

## National tailoring of eForms notices
- Documentation in GitHub at https://github.com/Hankintailmoitukset/hilma-api/blob/master/Hilma_national_tailoring_API.xlsx and https://github.com/Hankintailmoitukset/hilma-api/blob/master/National%20eForms%20-%20specifications%20version%200.99.xlsx

## SDK version update
- The SDK version 1.13.2 in production environment
- SDK version 1.13.3 coming at some point
- Next major version change is 2.0.0, planned to be done during Q3/2027

## Upcoming national tailoring
- IPI and EED fields in production
	- EED fields still hidden behind a feature flag
- In test environment
	- New field "FI-70 Contract value" for award notices for contracts
	- Field BT-537 can not be solely mandatory when the value of field BT-538 "Toistaiseksi voimassa oleva sopimus tai sopimuksen kesto ei ole tiedossa" is "Määräaikainen".
	- Because of the above, we have to release HilmaSDKv3

## GitHub Issues
- A new issue with BT-50 when creating a DPS, Joel will create an issue for this
- Remember to use GitHub to report any issues with the API in either PROD or TEST environment
- Remember the documentation at https://hns-hilma-prod-apim.developer.azure-api.net
	- Needs Signing up before any documentation is visible, the approval process for sign up takes a few minutes and you will get an email to verify when it's done