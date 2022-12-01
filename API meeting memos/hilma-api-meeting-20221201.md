# Participants

* Ari Nurkkala
* Tuomas Saari
* Veera Lavikkala
* Joel Ollikainen


# Topics

* Hilman eForms toteutus alkanut
	- eForms SDK 1.3.2. Hilmaa päivitetään sen mukaiseksi. Näköjään 1.4.1 ilmestynyt 22 tuntia sitten.
	- Hilma statistiikat tullaan toteuttamaan nykysellä HilmaStatistics -luokalla. Tästä poimitaan pällekkäisyydet eForms tietomalliin. Tämä muuttaa mallia, jolla kirjoitusrajapintaa käytetään.
		- Hilman eForms kirjoitusrajapinta tulee vastaanottamaan tietomallia, jossa on hankintamenettely, eForm ja Hilman tilastotiedot.
	- Voidaan exporttaa eForms Loco resursseja rajapintakäyttäjille käytettäväksi tarpeen mukaan. Ei vielä toteutettu. Tehdään, kunhan Hanselin asiantuntijat ovat käyneet komission konekäännökset läpi. 
		- SC haluaa lopulliset käännökset
	- Työjärestys Hilmassa niin, että ensin ETS ja Hilma web api rajapinnat, Hilman käyttöliittymä, julkinen näkymä ja lopuksi indeksit / haku.
		- https://hns-hilma-test-apim.developer.azure-api.net/ First POST endpoint are now available for the ETS product. Serialization, deserialization and validation endpoints.
	- Eu / komissio palavereita pidetty, jossa keksusteltu viimeisestä ajasta jolloin voi lähettää vanhoja ilmoituksia. Aikataulu pysyy ennallaan.
		- Hilman sisäinen tavoite on olla eFormsin osalta valmis kesäkuussa 2023.
		- Nykyinen TED vastaanottaa 24.10.2023. Sen jälkeen pakko lähettää eFormseja.
		- Hilman eForms golive on 1.9.2023. Tämän jälkeen Hilma ei vastaanota nykyisiä ilmoituksia.
	- Esimerkki julkaisutusta ilmoitukseta TEDissä: https://ted.europa.eu/udl?uri=TED:NOTICE:634805-2022:TEXT:EN:HTML&tabId=0
	- eForms poissulkukriteerit ja ESPD vaatimuksissa päällekkäisyyksiä. Miten nämä hoidetaan? Kysymys Hanselille.
	- Tehdään Hilmaan vastaava ilmoitustyyppeihin perustuva tietomalli dokumentaatio
	- SDK:n koodilistat: https://github.com/OP-TED/eForms-SDK/tree/develop/codelists
