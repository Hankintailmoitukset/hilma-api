# Participants
- Jarkko Sikiö

# Topics

## SDK Update
- Sending EU eForms notices to TED using SDK 1.13 via the API is now possible in Hilma Test and Prod environment.
	- Please note that to use SDK 1.13 the notice XML / draft JSON has to have the CustomizationID element with value "eforms-sdk-1.13". If the element is missing, the API will default to SDK 1.10.
- The SDK 1.13 version was released on Hilma on 24.6. The EED, IPI and FSR fields have not yet been implemented in Hilma. The timetable for the implementation of these fields will be specified at a later date. Please note that publishing SDK 1.10 drafts is not anymore possible in test or production environments.
- TED and Hilma will not accept notices for version 1.10 after 31.7.2025.

## National eForms notices
- Sending E1, E3 and E4 notice types to TED by using FI-20 is now possible.
- The following five national old-style notices will be phased out on 3 September 2025
	- National prior information notice / market consultation
	- National contract notice
	- National design contest
	- National prior information notice / market consultation - defence and security
	- National contract notice - defence and security
- Second phase of national eForms notices is postponed to 2026. The second phase notifications are 
	- Pienhankintailmoitus
	- Maatalouden hankintailmoitus
	- Ennakkoilmoitus suorahankinnasta (ex ante)
	- Avoimuusilmoitus ulosmyyntiaikeesta.

## GitHub Issues
- Remember to use GitHub to report any issues with the API in either PROD or TEST environment
- Remember the documentation at https://hns-hilma-prod-apim.developer.azure-api.net
	- Needs Signing up before any documentation is visible, the approval process for sign up takes a few minutes and you will get an email to verify when it's done