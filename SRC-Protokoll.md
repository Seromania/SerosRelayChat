##Protokoll

Ankommende Pakete sind im folgenden Format:

	<Username[:Raum]> <Kommando> <Argument> \CR\LF

* Kommandos haben unterschiedlich viele Argumente. Ein Paket muss mit CR-LF beendet werden.
  Die Lücken zwischen den Username, Kommando und Argumenten sind mit einem Space (0x20) zu trennen.
* Daten werden im UTF-8 Format als Byte Übertragen. 
* Usernames dürfen folgende Zeichen NICHT im Namen tragen:
	* Space (0x20)
	* CR oder LF
* Der Username System ist dem Server vorbehalten und darf NICHT vergeben werden!
* Usernames d�rfen nicht zweimal vorkommen!

##Kommandos
LOGIN: Der erste Befehl der kommen muss!

	Bsp:
		vonUser LOGIN
		
	Server->Client:
		System LOGIN Begrüßungstext
		
	Ist Username schon vorhanden:
		System REFUSE Grund
		
SEND:   Sendet eine Nachricht in den Raum. 

	Bsp: 
        vonUser:InChannel SEND Dies ist ein Text
		
	Server->Client:
		vonUser:InChannel SEND Dies ist ein Text
		
	übergeben wird ein Argument. Der Text wird als ein Argument angesehen
	
WHISPER: Sendet eine Nachricht an die Person im ersten Argument

	Bsp:
		vonUser WHISPER zuUser Dies ist ein Text
		
	Server->Client:
		vonUser WHISPER Dies ist ein Text

JOIN:    Tritt den Raum im ersten Argument bei

	Bsp:
		vonUser JOIN #Channel
		
	Server->Client:
		vonUser:InChannel JOIN
		#Damit werden die Clients informiert, dass ein neuer User kam
		
LIST: Listet alle Benutzer im Channel auf

	Bsp:
		vonUser:InChannel LIST
		
	Server->Client:
		InChannel LIST User(1),User(2),...User(n)

KICK:	Kickt den User im ersten Argument

	Bsp:
		vonUser:InChannel KICK zuUser REASON
		
	Server->Client:
		zuUser:InChannel LEAVE REASON
		
		!!Dieser Befehl benötigt MOD-Rechte!!
		
BAN:	Bant den User im ersten Argument

	Bsp:
		vonUser:InChannel BAN zuUser REASON
	
	Server->Client:
		zuUser:InChannel LEAVE REASON
		
		!!Dieser Befehl benötigt OP-Rechte!!
		
MOD:	User im ersten Argument wird Mod

	Bsp:
		vonUser:InChannel MOD zuUser
		
	Server->Client:
		zuUser:InChannel NAMECHNG #zuUser
		Argument ist der neue Name
		
		!!Dieser Befehl benötigt OP-Rechte!!

VOICE:	User im ersten Argument bekommt Voice-Rechte

	Bsp:
		vonUser:InChannel VOICE zuUser
		
	Server->Client:
		zuUser:InChannel NAMECHNG +zuUser
		!!Dieser Befehl benötigt MOD-Rechte!!
		
OP:		User im ersten Argument wird OP

	Bsp:
		zuUser:InChannel OP zuUser
		
	Server->Client:
		zuUser:InChannel NAMECHNG @zuUser
		
		!!Dieser Befehl benötigt OP-Rechte!!
		
UNMOD:	User im ersten Argument wird Mod-Rechten entzogen

	Bsp:
	    vonUser:InChannel UNMOD zuUser
		
	Server-Client:
		zuUser:InChannel NAMECHNG zuUser
		
		!!Dieser Befehl benötigt OP-Rechte!!

DEOP:	User im ersten Argument wird OP-Rechte entzogen

	Bsp:
		vonUser:InChannel DEOP zuUser
		
	Server->Client:
		zuUser:InChannel NAMECHNG zuUser
		
		!!Dieser Befehl benötigt OP-Rechte!!

CHNMOD: Channel-Modus wird geändert. Im ersten Argument steht welcher Modus

	Bsp:
		vonUser:InChannel CHNMOD <Flag>
		Flag:
			n: Normal Modus (Jeder kann schreiben)
			v: Voice Modus (Jeder mit Voice kann schreiben)
			m: Moderator Modus (Jeder mit Moderator kann schreiben)
			
		!!Dieser Befehl benötigt OP-Rechte!!
		
TOPIC: Zeigt den Topic des Channels an oder setzt es

	Bsp:
		vonUser:InChannel TOPIC
			Zeigt den TOPIC an
		vonUser:InChannel TOPIC Neues Topic
			Setzt den Topic
			
		!!Dieser Befehl benötigt MOD-Rechte wenn Topic geändert werden soll!!
	
	Server->Client:
		InChannel TOPIC TopicNachricht
		
PING:  Sendet eine Ping abfrage mit random Nummer (Server->Client)

	Bsp:
		SERVER PING 1234 

PONG:  Antwort auf Ping mit gegebener Nummer (Client->Server)

	Bsp:
		vonUser PONG 1234
		
LEAVE: User verlässt den Channel

	Bsp:
		vonUser:InChannel LEAVE Leave Grund
		
	Server->Client:
		vonUser:InChannel LEAVE Leave Grund
		
NAMECHNG: User ändert sein Username

	Bsp:
		vonUser:InChannel NAMECHNG vonUserNewName
		
	Server->Client:
		vonUser:InChannel NAMECHNG vonUserNewName

