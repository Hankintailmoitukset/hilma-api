# Participants
- Jarkko Sikiö
- Tiitus Kivikangas
- Timo Rantanen
- Jan Borgelin
- Max Kholoshnyi
- Christian Sixhøi

# Topics

## National tailoring of eForms notices
- Documentation in GitHub at https://github.com/Hankintailmoitukset/hilma-api/blob/master/Hilma_national_tailoring_API.xlsx and https://github.com/Hankintailmoitukset/hilma-api/blob/master/National%20eForms%20-%20specifications%20version%200.99.xlsx

## SDK version update
- The SDK version 1.13.2 in production environment
- SDK version 1.13.3 coming at some point
- Next major version change is 2.0.0, planned to be done during Q3/2027
	- We aim to have this in test environment by the end Q2/2027

## Upcoming national tailoring
- IPI and EED fields in production
	- EED fields still hidden behind a feature flag
- In test environment
	- New field "FI-70 Contract value" for award notices for contracts and tailoring for related field BT-161
	- Field BT-537 and BT-538 related mandatory rules
	- New national tailoring options for BT-67
	- Because of the above, we have to release HilmaSDKv3
	- Also some new tailoring for EEDs
	- Production deployment during 06/2026

## GitHub Issues
- Remember to use GitHub to report any issues with the API in either PROD or TEST environment
- Remember the documentation at https://hns-hilma-prod-apim.developer.azure-api.net
	- Needs Signing up before any documentation is visible, the approval process for sign up takes a few minutes and you will get an email to verify when it's done