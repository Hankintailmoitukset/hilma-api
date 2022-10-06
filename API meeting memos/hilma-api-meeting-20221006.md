# Participants

* Ari Nurkkala
* Veera Lavikkala


# Topics

* EForms ominaisuuksia valuu 'näkyviin' tuotantoon, mutta ei käytettävissä. Palauttaa 501.

* Hilman eForms toteutus alkanut
  - TEDin Migraatiotyökalu tukee vasta SDK 0.7, nyt mennään jo v 1.1
	- Voidaan exporttaa eForms Loco resursseja rajapintakäyttäjille käytettäväksi tarpeen mukaan. Ei vielä toteutettu. Tehdään, kunhan Hanselin asiantuntijat ovat käyneet komission konekäännökset läpi.
  - Hanselin tekemä eForms mäpperi vanhaan tietomalliin kiinnostaa. 
	- Työjärestys Hilmassa niin, että ensin ETS ja Hilma web api rajapinnat, sitten indeksit / haku, julkinen näkymä ja lopuksi Hilman käyttöliittymä.
	- https://hns-hilma-test-apim.developer.azure-api.net/ First POST endpoint are now available for the ETS product. Serialization, deserialization and validation endpoints.
	
	- Eforms SDK päivityksiä (1.3) tulossa vielä marraskuussa ja ehkä 2.0 ensi kesäkuussa.
		- 1.3 ei ole taaksepäinsopiva aikasempien kanssa
		- TED rajapinnat noin 3-6kk SDK:ta jäljessä. Tällä hetkellä lähetys toimii SDK 0.6 ja pitäisi päivittyä 1.0 lokakuun aikana
	- Maanantaina Eformsista päättävä palaveri, jossa on mukana suomesta yksi. Agendalla ei tiedettävästi ole aikatalua.
		- Asetukseen lisätietoa tämän jälkeen.
	
* Hilman alustava aikataulu heinäkuuhun mennessä.
	- Hilman tilastotiedot (Innovaatiot, Ekologinen kestävyys, Sosiaalinen kestävyys, PK-yritysten huomioiminen) halutaan nykyisellään Hilmaan. Toteutus vielä epäselvä, että käytetäänkö nykyisiä JSON kenttiä vai ujutetaanko EFormsiin. Tarkentuu, kunhan tiedetään miten EFormsia voi räätälöidä näiden osalta.
	- Käyttöliittymäsuunnittelut (alustava UI - tulee vielä muuttumaan)
	https://www.figma.com/proto/seb6tr2HFeYyEpovWNzOzk/Hilma?page-id=4254%3A15221&node-id=4363%3A6959&viewport=667%2C897%2C0.5&scaling=min-zoom&starting-point-node-id=4254%3A15222
