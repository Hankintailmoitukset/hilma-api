# Participants

* Ari Nurkkala
* Veera Lavikkala
* Sami Repo
* Joel Ollikainen
* Sampo Kivistö


# Topics

* Hilman eForms toteutus alkanut
	- Hilma käyttää tällä hetkellä eForms SDK 1.1, päivitellään kohta 1.3
	- Hilma statistiikat tullaan toteuttamaan nykysellä HilmaStatistics -luokalla. Tästä poimitaan pällekkäisyydet eForms tietomalliin. Tämä muuttaa mallia, jolla kirjoitusrajapintaa käytetään. Palataan asiaan, kun teidetään miten.
	- Voidaan exporttaa eForms Loco resursseja rajapintakäyttäjille käytettäväksi tarpeen mukaan. Ei vielä toteutettu. Tehdään, kunhan Hanselin asiantuntijat ovat käyneet komission konekäännökset läpi. 
		- SC haluaa lopulliset käännökset
  - Hanselin tekemä eForms 'mäpperi' vanhaan tietomalliin kiinnostaa. Exceliin siis merkattu missä ne on ollut Hilman käyttöliittymässä nykyään.
	- Työjärestys Hilmassa niin, että ensin ETS ja Hilma web api rajapinnat, sitten indeksit / haku, julkinen näkymä ja lopuksi Hilman käyttöliittymä.
	- https://hns-hilma-test-apim.developer.azure-api.net/ First POST endpoint are now available for the ETS product. Serialization, deserialization and validation endpoints.
	
	- Eforms SDK päivityksiä (1.3) tulossa vielä marraskuussa ja ehkä 2.0 ensi kesäkuussa.
		- 1.3 ei ole taaksepäinsopiva aikasempien kanssa
		- TED rajapinnat noin 3-6kk SDK:ta jäljessä. Tällä hetkellä lähetys toimii SDK 0.6 ja pitäisi päivittyä 1.0 lokakuun aikana
	- Eu / komissio palavereita pidetty, jossa keksusteltu viimeisestä ajasta jolloin voi lähettää vanhoja ilmoituksia. Aikataulu pysyy ennallaan.
		- Hilman sisäinen tavoite on olla eFormsin osalta valmis kesäkuussa 2023.
		- Nykyinen TED vastaanottaa marraskuun loppuun 2023. Sen jälkeen pakko lähettää eFormseja.
	- Käyttöliittymäsuunnittelut (alustava UI - tulee vielä muuttumaan)
	https://www.figma.com/proto/seb6tr2HFeYyEpovWNzOzk/Hilma?page-id=4254%3A15221&node-id=4363%3A6959&viewport=667%2C897%2C0.5&scaling=min-zoom&starting-point-node-id=4254%3A15222
	- Tehdään Hilmaan vastaava ilmoitustyyppeihin perustuva tietomalli dokumentaatio
