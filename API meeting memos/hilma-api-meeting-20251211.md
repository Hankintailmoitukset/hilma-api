# Participants
- Jarkko Sikiö
- Laura Kivikangas
- Joel Ollikainen
- Mauri Korhonen
- Mikita Lisouski

# Topics

## National tailoring of eForms notices
- Documentation in GitHub at https://github.com/Hankintailmoitukset/hilma-api/blob/master/Hilma_national_tailoring_API.xlsx and https://github.com/Hankintailmoitukset/hilma-api/blob/master/National%20eForms%20-%20specifications%20version%200.99.xlsx
	- The Hilma_national_tailoring_API.xlsx has been updated and the major change is the removal of 60 000 € limit from statistical questions.

## SDK version update
- The SDK version will be updated to 1.13.2. Preliminary schedule: Q4/2025-Q1/2026
	- Should be a backwards compatible minor update
	- If breaking changes are identified, there will be a 3 month transition period

## Upcoming national tailoring
- The planned schedule for the implementation of the EED and IPI fields has changed. The goal is Q1/2026 together with SDK 1.13.2.
	- IPI fields need SDK 1.13.2 version, so a minor SDK update is also needed
	- EED fields national tailoring specifications pending for reasons unrelated to Hilma
	- IPI will not have any national tailoring
- Clean and energy-efficient vehicles in production environment
	- Fields BT-735, BT-723, BT-715, BT-725, BT-716 are mandatory when field BT-717 is true
	- Field BT-725 can only be filled in when the value "Linja-auto (M3)" has been selected in field BT-723
	- Some of the values are removed from selection list in field BT-723: "M1", "M2", "N1", "N2" and "N3". Three values remain: "Henkilöautot ja kevyet hyötyajoneuvot (M1, M2, N1)", "Linja-autot (M3)" and "Kuorma-autot (N2, N3)"
- The timetable for the following changes is unclear but will be confirmed as soon as possible
	- New field "FI-70 Contract value" coming for award notices for contracts
	- Field BT-536 "(Arvioitu) alkamispäivä" becomes mandatory when the value of field BT-538 "Toistaiseksi voimassa oleva sopimus tai sopimuksen kesto ei ole tiedossa" is "Toistaiseksi voimassa oleva"

## FSR fields in production
- Concerns fields BT-681 and BT-682
- FSR does not have any national tailoring

## BT-125 - Previous Planning Identifier
- A new field to refer to a previous notice
- Allows only TED notice identifiers, so if the notice is published only in Hilma (with FI-20) we will instead use field FI-125
- The new field FI-123 is now also added to national notice E3 and is used to refer to the previous notice using the Hilma notice number

## API Updates
- ReadAPI(eForm): Addition of Modified and Created timestamps coming to PublicNoticeContract JSON
- The award and contract modification notices refer to the contract notice in field BT-04

## Hilma starts sending reminders to buyers to make award notices
- No direct effect on the API, more FYI for your end-users
- More info about the reminders in Hilma at https://www.hankintailmoitukset.fi/fi/announcement/uutta-hilmassa-hilma-alkaa-muistuttaa-jalki-ilmoitusten-tekemisesta?utm_source=emaileri&utm_medium=email&utm_campaign=Tervetuloa%20API-kokoukseen%2011*12*2025%20klo%2014*Welcome%20to%20API%20meeting&utm_term=Read%20more%20about%20the%20reminders%20in%20Hilma&utm_content=u-5647772-84760694-2596017-5

## GitHub Issues
- Remember to use GitHub to report any issues with the API in either PROD or TEST environment
- Remember the documentation at https://hns-hilma-prod-apim.developer.azure-api.net
	- Needs Signing up before any documentation is visible, the approval process for sign up takes a few minutes and you will get an email to verify when it's done

