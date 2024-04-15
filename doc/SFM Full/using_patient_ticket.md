# Bruk av SFM Pasientbillett

Pasientbillett benyttes for SFM klient og SFM Datadeling API.  Pasientbillett benyttes ikke for SFM Basis API.

For å ivareta personvern ved overføring av informasjon mellom EPJ og SFM, baseres kommunikasjonen på en to-trinns modell hvor EPJ etterspør en pasient billett med begrenset varighet for å kunne få tilgang til pasientinformasjon fra SFM og skrive pasientinformasjon til SFM. Pasientbilletten må benyttes ved åpning av SFM klient og ved bruk av SFM Datadelings API.

Pasientbilletten fungerer som en nøkkel mellom pasient ID og legemiddeldataene, slik at det ikke skal være mulig å finne fram til hvilke legemiddeldata som er relatert til hvilken pasient. 

Merk at EPJ systemet må be om pasientbillett for hver kombinasjon av bruker og pasient.

Pasientbilletten skal benyttes i hele brukersesjonen. Det vil si at det ikke skal forespørres en ny pasientbillett for hvert kall til SFM Datadeling API.

Pasientbilletten er unik for SFM tjenesten og spesifisert slik at det ikke skal være mulig å gjette seg fram til en gyldig billett. Dette gjør også at det ikke er mulig å skrive eller lese på tvers av virksomheter. Løsningen er på den måten feiltollerant. Spørringen for tilgang til en pasientbillett skal inneholde en pasient-id (eg. fnr, dnr eller xxx-id). Siden billetten har begrenset levetid er det mulig å fornye billetten på lik linje med at systemet også fornyer accesstoken. Pasientbilletten er en tekststreng på maks 200 tegn, f.eks. en UUID: “44300220-455f-4333-b6e8-1464a7f44608”.
