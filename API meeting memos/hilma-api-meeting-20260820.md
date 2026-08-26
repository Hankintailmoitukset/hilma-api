# Participants
- Jarkko Sikiö
- Tiitus Kivikangas
- Mikita Lisouski
- Jan Borgelin
- Joel Ollikainen

# Topics

## National tailoring of eForms notices
- Documentation in GitHub at https://github.com/Hankintailmoitukset/hilma-api/blob/master/Hilma_national_tailoring_API.xlsx and https://github.com/Hankintailmoitukset/hilma-api/blob/master/National%20eForms%20-%20specifications%20version%200.99.xlsx
- Technical documentation for Hilma-SDK can be found at https://github.com/Hankintailmoitukset/hilma-sdk

## SDK version update
- The SDK version 1.13.2 in production environment
- SDK version 1.13.3 coming during 2026, or 13.3.4 if that is actually happening by EU
	- There should be no breaking changes to API partners
- Next major version change is 2.0.0, planned to be done during Q3/2027
	- We aim to have this in test environment by the end Q2/2027

## Upcoming national tailoring
- IPI and EED fields in production
	- NOTE: In API meeting we told that EED is still behind feature flag. This was incorrect and the fields are in use in production.
- Released in June
	- New field "FI-70 Contract value" for award notices for contracts and tailoring for related field BT-161
	- Field BT-537 and BT-538 related mandatory rules
	- New national tailoring options for BT-67
	- Because of the above, we have to release HilmaSDKv3
		- Transition period through 09/2026. If more time is needed, please contact us
	- Also some tailoring for EEDs
- In test environment
	- Hilma React update, is mainly limited to UI changes so no changes for API partners
	- Some URLs might change, so if you have direct links to Hilma /procedure/<procedureId>/qna, /procedure/<procedureId>/tenders, or /procedure/<procedureId>/decisions, please contact us
- Remember to check national tailorings for -Review -fields, we have received some notices with e.g. field BT-783
	- Currently the API has no hard forbidden for these, but these are not used in Finland

## GitHub Issues
- Remember to use GitHub to report any issues with the API in either PROD or TEST environment
- Remember the documentation at https://hns-hilma-prod-apim.developer.azure-api.net
	- Needs Signing up before any documentation is visible, the approval process for sign up takes a few minutes and you will get an email to verify when it's done