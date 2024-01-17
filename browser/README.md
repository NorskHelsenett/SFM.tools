The SFM browser-integration is expemplified in the *microEPJ* HMTL file with SFM-iframe and javascript setup for communication between iframe (SFM client) and host page.

Norwegian:
MicroEpj har ytre (EPJ) ramme og kan vise portalene i iframe, og viser meldingene som går mellom EPJ og SFM.

Token henter jeg via DrWho og legger inn på toppnivå i collection. Husk "lagre".

Postman collection har kall for session/create og patient ticket.
Det er "hardkodede" verdier her for nonce, og i microEpj kan du dobbeltklikke på nonce for å få den fram. 
"code" må hentes fra session hver gang, men nonce kan gjenbrukes (i test selvfølgelig)

Eksemplene fra https://e-resept.atlassian.net/wiki/spaces/SFMDOK/pages/2160493151/Sequence+diagrams+and+examples+-+Integrating+EHR+with+the+SFM+client
er benyttet her.
