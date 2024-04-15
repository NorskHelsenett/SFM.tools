# Bruk av HelseID og accesstoken

HelseID benyttes av SFM klient , SFM Datadeling API og SFM Basis API.

HelseID er basert på OAuth 2.0 og er en løsning for helsesektoren som tilbyr:
- Felles påloggingsløsning
- Tillitsanker for sektoren
- Sikring av API'er

HelseID-løsningen er basert på at en klient (et bestemt system installert i en bestemt organisasjon) er forhåndsregistrert hos HelseID og kan be om et bevis (token) fra HelseID som bekrefter at systemet er den det utgir seg for å være. Tokenet er et JSON Web Token (JWT) som inneholder koder som bekrefter utgiver og system. Token kan også berikes med informasjon om innlogget person, og støtter innlogging via ID-porten på høyt nivå (nivå 4). Tokenet er signert av HelseID og kan verifiseres.

Tokenet skal ha et claim som er spesifikt for SFM (SFM-id), og kun ha SFM som audience. 
Det er to scopes som er relevant i første omgang:

- SFM portaler og integrasjoner:   Scope: e-helse:sfm.api/sfm.api ,  Beskrivelse: Benyttes for SFM Portaler, SFM Datadeling API og SFM Basis API.
- SFM migrering:  Scope: e-helse:sfm.api/sfm-migrering.api , Beskrivelse: Benyttes for første gangs opplasting av pasient- og virksomhetsdata fra EPJ system til SFM.
