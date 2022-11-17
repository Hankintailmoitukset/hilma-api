# Participants

* Ari Nurkkala
* Antti Remes
* Sami Pöysä
* Veera Lavikkala
* Joel Ollikainen
* Joose Huopio


# Topics

* Hilman eForms toteutus alkanut
	- eForms SDK 1.3.2. Hilmaa päivitetään sen mukaiseksi.
	- Hilma statistiikat tullaan toteuttamaan nykysellä HilmaStatistics -luokalla. Tästä poimitaan pällekkäisyydet eForms tietomalliin. Tämä muuttaa mallia, jolla kirjoitusrajapintaa käytetään. Palataan asiaan, kun teidetään miten.
	- Voidaan exporttaa eForms Loco resursseja rajapintakäyttäjille käytettäväksi tarpeen mukaan. Ei vielä toteutettu. Tehdään, kunhan Hanselin asiantuntijat ovat käyneet komission konekäännökset läpi. 
		- SC haluaa lopulliset käännökset
  - Hanselin tekemä eForms 'mäpperi' vanhaan tietomalliin kiinnostaa. Exceliin siis merkattu missä ne on ollut Hilman käyttöliittymässä nykyään.
	- Työjärestys Hilmassa niin, että ensin ETS ja Hilma web api rajapinnat, Hilman käyttöliittymä, julkinen näkymä ja lopuksi indeksit / haku.
	- https://hns-hilma-test-apim.developer.azure-api.net/ First POST endpoint are now available for the ETS product. Serialization, deserialization and validation endpoints.

	- TED rajapinnat noin 3-6kk SDK:ta jäljessä. Tällä hetkellä lähetys toimii SDK 0.6 ja pitäisi päivittyä 1.0 lokakuun aikana. 
	- Eu / komissio palavereita pidetty, jossa keksusteltu viimeisestä ajasta jolloin voi lähettää vanhoja ilmoituksia. Aikataulu pysyy ennallaan.
		- Hilman sisäinen tavoite on olla eFormsin osalta valmis kesäkuussa 2023.
		- Nykyinen TED vastaanottaa marraskuun loppuun 2023. Sen jälkeen pakko lähettää eFormseja.
	- Käyttöliittymäsuunnittelut (alustava UI - tulee vielä muuttumaan)
	- eForms poissulkukriteerit ja ESPD vaatimuksissa päällekkäisyyksiä. Miten nämä hoidetaan? Kysymys Hanselille.
	- Tehdään Hilmaan vastaava ilmoitustyyppeihin perustuva tietomalli dokumentaatio
	- SDK:n koodilistat: https://github.com/OP-TED/eForms-SDK/tree/develop/codelists
