# Bruk av HelseID og accesstoken

HelseID benyttes av SFM klient , SFM Datadeling API og SFM Basis API.

HelseID er basert på OAuth 2.0 og er en løsning for helsesektoren som tilbyr:
- Felles påloggingsløsning
- Tillitsanker for sektoren
- Sikring av API'er

HelseID-løsningen er basert på at en klient (et bestemt system installert i en bestemt organisasjon) er forhåndsregistrert hos HelseID og kan be om et bevis (token) fra HelseID som bekrefter at systemet er den det utgir seg for å være. Tokenet er et JSON Web Token (JWT) som inneholder koder som bekrefter utgiver og system. Token kan også berikes med informasjon om innlogget person, og støtter innlogging via ID-porten på høyt nivå (nivå 4). Tokenet er signert av HelseID og kan verifiseres.

Tokenet skal ha et claim som er spesifikt for SFM (SFM-id), og kun ha SFM som audience. 
Det er to scopes som er relevant i første omgang:

- SFM portaler og integrasjoner:   *Scope: e-helse:sfm.api/sfm.api*,  Beskrivelse: Benyttes for SFM Portaler, SFM Datadeling API og SFM Basis API.
- SFM migrering:  *Scope: e-helse:sfm.api/sfm-migrering.api* , Beskrivelse: Benyttes for første gangs opplasting av pasient- og virksomhetsdata fra EPJ system til SFM.


Ved å be om et accesstoken kan et system dermed benytte seg av et API, og dersom sikringsmekanismene ellers er gode kan API-tilbyder stole på innholdet. Fordi systemet gir fra seg tokenet (til den som tilbyr API) er det tokenet som definerer hvilket API/ressurs det er ment å gi tilgang til. Accesstoken har relativt kort levetid. 

Klienten kan også be om et refreshtoken som har lengre levetid, og kan benytte dette til å fornye aksesstoken så lenge refreshtokenet lever. Refresh-token er ikke ment å forlate klientapplikasjonen og kan derfor ha lengre levetid.

SFM er bygget på en slik måte at API kall som kommer inn, eller funksjoner som gjøres i SFM klient medfører innmelding av opplysninger eller oppslag i sentrale systemer som skal gjøres på vegne av klienten/brukeren som benytter SFM. SFM er derfor konfigurert i HelseID til å kunne utføre en "token exchange", altså å bytte ut et token med audience for SFM, til et token som gir tilgang til f.eks. Reseptformidleren. Tokenet vil da inneholde opplysninger om opprinnelig klient, om SFM som har byttet token og audience for å gi tilgang til Reseptformidlerens API.

Det er noen viktige ting å være klar over:

- På grunn av mulig klokke-forskyvning anbefaler HelseID at klientapplikasjonen benytter verdien ExpiresIn (seconds) som returneres sammen med tokenet fra HelseId for å støtte refresh-algoritme internt.
- SFM fullversjon med GUI klient må opprette en sesjon mot SFM med endepunktet: ```/api/Session/create``` 
- SFM fullversjon med GUI klient må oppfriskes med nytt token når EPJ har gjort refresh. Dette gjøres i API Datadeling med endepunktet: ```/api/Session/refresh```  
- På samme måte skal sesjonen lukkes dersom brukeren logger av EPJ systemet: ```/api/Session/end```.

> SFM har en sesjon for hver bruker i en virksomhet. Dette er slik for å unngå at det skal hope seg opp med sesjoner når bruker hopper mellom maskiner. Sesjon er her en knytting mellom accesstoken og cookie. En bruker kan ta create session fra mange maskiner og det fungerer for bruker i alle browsere fram til han på en av maskinene velger å logge ut. Da er det krav om å avslutte sesjon og EPJ sender avslutt sesjon. Når bruker kommer til en av de andre maskinene som bruker sesjonen, så finnes ikke cookie og kall fra SFM feiler. EPJ må da ta en ny create session og starte browser igjen for at bruker skal kunne fortsette å jobbe på pasienten. 

For mer informasjon om HelseID:  https://www.nhn.no/helseid/hvordan-ta-i-bruk-helseid/

For utviklere: [How do I, as a developer, get started with HelseID](https://e-resept.atlassian.net/wiki/spaces/HELSEID/pages/217382951/How+do+I+as+a+developer+get+started+with+HelseID)

For mer informasjon om JWT token: https://jwt.io/
