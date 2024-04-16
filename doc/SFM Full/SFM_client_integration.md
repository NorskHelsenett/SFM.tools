# Integrasjon med SFM klient

 
SFM klientene er implementert i  Angular (JavaScript) for bruk i nettleser. SFM støtter Chrome, Safari og Edge.

Eksemplene som er tilgjengeliggjort er basert på bruk av Chrome. 

SFM klient kan benyttes både i en sky/nettleserbasert løsning og som del av en windows klient med integrert nettleser. 

## Oppsett av ny “instans”
Instans betyr her journalsystem hos enkelt kunde. Det er muligens litt upresist, men beskriver alle de variantene man kan se for seg, fra et enkelt tannlegekontor med en tannlege, til stor kommune eller interkommunalt samarbeid. 

Som utvikler må man også starte her. Du får ikke tilgang til noe i SFM før en instans er på plass. Den kan settes opp med assistanse fra NHN, eller du kan gjøre det selv.

SFM er “selvbetjening” i den betydningen at du fra å ha en godkjent HelseID klient (opprettet via HelseID selvbetjening) kan opprette instans mot ønsket test-system.

Det er noen forutsetninger: 

- HelseID klient må være satt opp med korrekt audience/scope. Sluttbrukersystemet må også være definert som Klientsystem i HelseID, og ha angitt “SFM” som konsumert API. 
- Organisasjonen må være registrert i Adresseregisteret med en hensiktsmessig HER-id (se egen artikkel)
- Organisasjonen må være registrert i Reseptformidler
- Organisasjonen må være registrert som lovlig bruker at Kjernejournal (i noen tilfeller skjer dette automatisk)

Kort fortalt er det denne fremgangsmåten:

- Opprett organisasjon ( Create Organization ) med HelseID token som korresponderer - det stilles noen krav til innholdet i organization for å kunne fullføre oppsett, inklusive innmelding til e-resept. 
SFM-id og Identifier ENH må samsvare med HelseID token.
SFM vil ved denne operasjonen opprette dedikert databaseskjema for din instans, og der vil den nye organisasjonen og superbruker (du) være registrert. Organisasjonen skal ha active=false.
Dette er forøvrig den eneste tillatte skriveoperasjonen man kan gjøre mot API uten at SFM-id er registrert.

- Gjør gjerne en search/read på Person/superbruker og oppdater med navn
- Opprett minst en helseperson (Create Practitioner) - du får tildelt helsepersoner med ønskede profesjoner og rettigheter fra NHN/SFM dersom du trenger.
- Opprett minst en pasient. Samme som over, NHN/SFM kan tildele testpasienter om du ikke allerede har.
- Oppdater organization med active=true. Da meldes organisasjon inn til Reseptformidler som aktiv. 

**Merk at dette må på plass før man kan teste integrasjonen. Det er ikke mulig å opprette brukersesjon eller hente data fra API uten at dette er på plass.**

## Integrasjon med nettleserbasert EPJ

For en nettleserbasert EPJ vil SFM klient integreres i EPJ’ens brukergrensesnittet ved hjelp av IFrame.  På samme måte som for Windows klient integrasjon benyttes pasientbillett og informasjon om hvilken pasient som behandles vises i EPJ sitt nettleservindu på “utsiden” av SFM nettleservindu.

Registrering av klienter i HelseID må gjøres per virksomhet fra serversiden av den skybaserte løsningen. Dette for å sikre sterk knytting mellom virksomhet og klient-id som igjen kan stoles på av SFM. For å hindre at hemmeligheter eksponeres i nettleser, skal forespørsel etter accesstoken gjøres fra serversiden. 

Eksempelkode for å integrere et skybasert EPJ system finnes i WebEPJSampleApp som er tilgjengelig på Github. 

## Windows-klient integrasjon

Tradisjonelt har EPJ-systemene vært windows applikasjoner installert på brukerens PC eller terminalserver.

SFM kan integreres med windows klienter ved at EPJ benytter en nettleserkomponent i sitt system hvor SFM startes via en definert URL. SFM Pasientrettet portal vil alltid startes i kontekst av et journalsystem, en bruker og en pasient.  Før det kan startes en slik sesjon er det nødvendig at system og bruker er identifisert gjennom en sekvens med HelseID. For brukerinnlogging i ID-porten bør samme nettleserkomponent benyttes. Dette muliggjør innlogging på tvers via mekanismer i web/http. Det er også nødvendig å initiere sesjon med enkeltpasient via SFM API for å hente en pasientbillett. Dette gjøres for å minimere datatrafikk som kombinerer sensitive helsedata og personinformasjon om pasienten det gjelder. Pasientens navn eller ID vil altså ikke kommuniseres i påfølgende nettlesersesjon og ikke være synlig i SFM sitt nettleservindu.  Det forutsettes at EPJ alltid viser hvilken pasient som er åpnet i SFM utenfor nettleservinduet.

 
