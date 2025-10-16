Participants:
- Diego Rönkkömäki-Tapia

## eForms reform of national notices
Second phase of national eForms notices is postponed to 2026. The second phase notifications are Pienhankintailmoitus, Maatalouden hankintailmoitus, Ennakkoilmoitus suorahankinnasta (ex ante) ja Avoimuusilmoitus ulosmyyntiaikeesta
https://www.hankintailmoitukset.fi/fi/info/eforms-uudistus?utm_source=emaileri&utm_medium=email&utm_campaign=Tervetuloa%20API-kokoukseen%2016%2a10%2a2025%2aWelcome%20to%20API%20meeting&utm_term=Read%20more%20about%20the%20eForms%20reform%20%2ain%20Finnish%2a%2a%20%2aRead%20more%20about%20the%20eForms%20reform%20%2ain%20Finnish%2a%2a&utm_content=u-5597410-84760694-2589223-1

## National tailoring, i.e. nationally implemented exceptions to the TED data model (SDK)
The tailoring of eForms notices for procurements exceeding EU and national thresholds is documented on GitHub.

EU eForms: https://github.com/Hankintailmoitukset/hilma-api/blob/master/Hilma_national_tailoring_API.xlsx?utm_source=emaileri&utm_medium=email&utm_campaign=Tervetuloa%20API-kokoukseen%2016*10*2025*Welcome%20to%20API%20meeting&utm_term=Tailoring%20of%20EU%20eForms*%20*Tailoring%20of%20EU%20eForms*&utm_content=u-5597411-84760694-2589223-4

National eForms: https://github.com/Hankintailmoitukset/hilma-api/blob/master/National%20eForms%20-%20specifications%20version%200.98.xlsx?utm_source=emaileri&utm_medium=email&utm_campaign=Tervetuloa%20API-kokoukseen%2016*10*2025*Welcome%20to%20API%20meeting&utm_term=Tailoring%20of%20national%20eForms%20*v*%200*98**%20*Tailoring%20of%20national%20eForms%20*v*%200*98**&utm_content=u-5597412-84760694-2589223-7

#### Upcoming national tailoring:
FSR, IPI and EED fields
- FSR is work-in-progress and planned schedule for the implementation is October 2025
- IPI requires SDK 1.13.2 upgrade
- EED specifications for national tailorings are not yet done

IPI and EED are probably not ready in October 2025

## Planned schedule for the following changes is Q4/2025.
- Fields BT-735, BT-723, BT-715, BT-725, BT-716 are mandatory when field BT-717 is true. (Clean and energy-efficient vehicles)
- Field BT-725 can only be filled in when the value "Linja-auto (M3)" has been selected in field BT-723. (Clean and energy-efficient vehicles)
- Some of the values are removed from selection list in field BT-723: "M1", "M2", "N1", "N2" and "N3". Three values remain: "Henkilöautot ja kevyet hyötyajoneuvot (M1, M2, N1)", "Linja-autot (M3)" and "Kuorma-autot (N2, N3)". (Clean and energy-efficient vehicles)
- New field "FI-70 Contract value" coming for award notices for contracts.
- Field BT-536 "(Arvioitu) alkamispäivä" becomes mandatory when the value of field BT-538 "Toistaiseksi voimassa oleva sopimus tai sopimuksen kesto ei ole tiedossa" is "Toistaiseksi voimassa oleva".
 
## Other topics
A new field, FI-125, is being added to the national notice E3, which can be used to refer to the previous notice using the Hilma notice number.
