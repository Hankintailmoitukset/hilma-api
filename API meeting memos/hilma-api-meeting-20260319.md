# Participants
- Jarkko Sikiö
- Anssi Salo
- Joel Ollikainen
- Mikita Lisouski

# Topics

## National tailoring of eForms notices
- Documentation in GitHub at https://github.com/Hankintailmoitukset/hilma-api/blob/master/Hilma_national_tailoring_API.xlsx and https://github.com/Hankintailmoitukset/hilma-api/blob/master/National%20eForms%20-%20specifications%20version%200.99.xlsx
	- The Hilma_national_tailoring_API.xlsx has been updated and the major change is the removal of 60 000 € limit from statistical questions.

## SDK version update
- The SDK version will be updated to 1.13.2.
	- Should be a backwards compatible minor update
	- If breaking changes are identified, there will be a 3 month transition period
	- Now in test environment

## Upcoming national tailoring
- The planned schedule for the implementation of the EED and IPI fields has changed.
	- IPI and EED fields need SDK 1.13.2 version, so a minor SDK update is also needed
	- Now in test environment
- Next in line for test environment, includes new mandatory fields
	- New field "FI-70 Contract value" coming for award notices for contracts
		- Mandatory for all Contract reward notices (e.g. eF29)
			- Except when there is a frame-agreement in place or when informing about termination of DPS
		- Technical specifications and rules can be found from main-branch at https://github.com/Hankintailmoitukset/hilma-sdk
	- Field FI-70 works in tandem with notice-level field BT-161 (Contract Value)
		- BT-161 should be the sum of all FI-70 fields
	- Field BT-536 "(Arvioitu) alkamispäivä" becomes mandatory when the value of field BT-538 "Toistaiseksi voimassa oleva sopimus tai sopimuksen kesto ei ole tiedossa" is "Toistaiseksi voimassa oleva"
	- There will be a transition period in Hilma SDK for these	

## API Updates
- ReadAPI(eForm): Addition of Modified and Created timestamps added to PublicNoticeContract JSON
- The award and contract modification notices refer to the contract notice in field BT-04
- End of life for Hilma-SDK v1 has passed, official deprecation not done yet but it won't be supported anymore

## GitHub Issues
- Remember to use GitHub to report any issues with the API in either PROD or TEST environment
- Remember the documentation at https://hns-hilma-prod-apim.developer.azure-api.net
	- Needs Signing up before any documentation is visible, the approval process for sign up takes a few minutes and you will get an email to verify when it's done

