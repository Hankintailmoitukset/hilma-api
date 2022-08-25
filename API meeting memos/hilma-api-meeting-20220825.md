# Participants

* Ari Nurkkala
* Aleksandra Löytäinen
* Esa Ikonen
* Veera Lavikkala
* Antti Remes
* Juho Esselström


# Topics

* Sari ja Christina poistunut tiimin vahvuudesta. Hilma etsii uusia kehittäjiä paraikaa.

* CPV koodit ja lisäkoodit 'puurakenteisesti' haku AVP käyttäjille lisätty testiin (tuotantoon seuraavassa julkaisussa).

* Hilman eForms toteutus alkanut
	- Voidaan exporttaa eForms Loco resursseja rajapintakäyttäjille käytettäväksi tarpeen mukaan. Ei vielä toteutettu. Tehdään, kunhan Hanselin asiantuntijat ovat käyneet komission konekäännökset läpi.
  - Hanselin tekemä eForms mäpperi vanhaan tietomalliin kiinnostaa.
	- EForms tietomallia alettu rakentamaan Hilmaan. Tavoitteena saada serialisointi toimivaksi kaikilla SDKn esimerkeillä.
	- Työjärestys Hilmassa niin, että ensin ETS ja Hilma web api rajapinnat, sitten indeksit / haku, julkinen näkymä ja lopuksi Hilman käyttöliittymä.
	
	- https://hns-hilma-test-apim.developer.azure-api.net/ First POST endpoint are now available for the ETS product. Serialization, deserialization and validation endpoints.
	- These example EForms from SDK do not comply with schema. Defects have been reported to the publication office and will be fixed for next release.
		pin-buyer_24_published.xml
		pin-only_24.xml
		pin-only_fin-reg.xml
		veat_25.xml
