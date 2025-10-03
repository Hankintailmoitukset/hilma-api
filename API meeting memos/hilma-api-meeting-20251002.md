# Participants
- Jarkko Sikiö
- Laura Kivikangas

# Topics

## National eForms notices
- Second phase of national eForms notices is postponed to 2026. The second phase notifications are 
	- Pienhankintailmoitus
	- Maatalouden hankintailmoitus
	- Ennakkoilmoitus suorahankinnasta (ex ante)
	- Avoimuusilmoitus ulosmyyntiaikeesta.

## National tailoring of eForms notices
- Documentation in GitHub at https://github.com/Hankintailmoitukset/hilma-api/blob/master/Hilma_national_tailoring_API.xlsx and https://github.com/Hankintailmoitukset/hilma-api/blob/master/National%20eForms%20-%20specifications%20version%200.98.xlsx

## Upcoming national tailoring
- The planned schedule for the implementation of the EED, IPI, and FSR fields is October 2025
	- IPI fields need SDK 1.13.2 version, so a minor SDK update is also needed
	- EED fields national tailoring specifications pending for reasons unrelated to Hilma
	- Both of above combined may cause delays in the implementation
- Clean and energy-efficient vehicles in test environment
	- Fields BT-735, BT-723, BT-715, BT-725, BT-716 are mandatory when field BT-717 is true
	- Field BT-725 can only be filled in when the value "Linja-auto (M3)" has been selected in field BT-723
	- Some of the values are removed from selection list in field BT-723: "M1", "M2", "N1", "N2" and "N3". Three values remain: "Henkilöautot ja kevyet hyötyajoneuvot (M1, M2, N1)", "Linja-autot (M3)" and "Kuorma-autot (N2, N3)"
- Following changes are planned to be implemented during Q4 / 2025
	- New field "FI-70 Contract value" coming for award notices for contracts
	- Field BT-536 "(Arvioitu) alkamispäivä" becomes mandatory when the value of field BT-538 "Toistaiseksi voimassa oleva sopimus tai sopimuksen kesto ei ole tiedossa" is "Toistaiseksi voimassa oleva"

## BT-125 - Previous Planning Identifier
- A new field to refer to a previous notice
- Allows only TED notice identifiers, so if the notice is published only in Hilma (with FI-20) we will instead use field FI-125

## GitHub Issues
- Remember to use GitHub to report any issues with the API in either PROD or TEST environment
- Remember the documentation at https://hns-hilma-prod-apim.developer.azure-api.net
	- Needs Signing up before any documentation is visible, the approval process for sign up takes a few minutes and you will get an email to verify when it's done