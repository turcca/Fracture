/*
#pragma strict


private var scriptHolder : GameObject;
//private var gameTime : GameTime;
//private var collector : locationsDataCollector;
//private var ship : PlayerShipData;
//private var shipPop : ShipPopulation;
//private var economy : Economy;
//private var event : Events;
//private var eventManager : EventManager;
private var characters : Characters;


function initialize() {
	scriptHolder = GameObject.Find("ScriptHolder");
		//gameTime 		= scriptHolder.gameObject.GetComponent(GameTime);
		//collector 		= scriptHolder.gameObject.GetComponent(locationsDataCollector);
		//ship 			= scriptHolder.gameObject.GetComponent(PlayerShipData);
		//shipPop 		= scriptHolder.gameObject.GetComponent(ShipPopulation);
		//economy 		= scriptHolder.gameObject.GetComponent(Economy);
		//event 			= scriptHolder.gameObject.GetComponent(Events);
		//eventManager	= scriptHolder.gameObject.GetComponent(EventManager);
		characters		= scriptHolder.gameObject.GetComponent(Characters);
}



public function getLocalName(loc : GameObject) : String {
	
	var roll : float = Random.value;
	
	var location : locationDatabase;
	location = loc.gameObject.GetComponent(locationDatabase);
	
	//var totalNobles : float = location.nobleHouse1ctrl + location.nobleHouse2ctrl + location.nobleHouse3ctrl + location.nobleHouse4ctrl;
	//var totalGuilds : float = location.guild1ctrl + location.guild2ctrl + location.guild3ctrl;
	//var total = totalNobles + totalGuilds + location.churchCtrl + location.hereticCtrl;
	//if (total < 1) { total = 1; }

	var adder : float = location.nobleHouse1ctrl;
	
		if (roll < adder) {
			// faction affiliation
			characters.affiliation = "House Furia";
			return getName("House Furia");
		} else adder += location.nobleHouse2ctrl;
		
		if (roll < adder) {
			// faction affiliation
			characters.affiliation = "House Rathmund";
			return getName("House Rathmund");
		} else adder += location.nobleHouse3ctrl;
		
		if (roll < adder) {
			// faction affiliation
			characters.affiliation = "House Tarquinia";
			return getName("House Tarquinia");
		} else adder += location.nobleHouse4ctrl;
		
		if (roll < adder) {
			// faction affiliation
			characters.affiliation = "House Valeria";
			return getName("House Valeria");
		} else adder += location.guild1ctrl;
		
		if (roll < adder) {
			// faction affiliation
			characters.affiliation = "Everlasting Union";
			return getName("Everlasting Union");
		} else adder += location.guild2ctrl;
		
		if (roll < adder) {
			// faction affiliation
			characters.affiliation = "Dacei Family";
			return getName("Dacei Family");
		} else adder += location.guild3ctrl;
		
		if (roll < adder) {
			// faction affiliation
			characters.affiliation = "Coruna Cartel";
			return getName("Coruna Cartel");
		} else adder += location.churchCtrl;
		
		if (roll < adder) {
			// faction affiliation
			characters.affiliation = "Church";
			return getName("Church");
		} else adder += location.hereticCtrl;
		
		if (roll < adder) {
			// faction affiliation
			characters.affiliation = "Heretics";
			return getName("Heretics");
		}
		
		else return getName("local");
}

static public function getName(who : String) : String {
	
	var nobleHouse1: String = "House Furia";
	var nobleHouse2: String = "House Rathmund";
	var nobleHouse3: String = "House Tarquinia";
	var nobleHouse4: String = "House Valeria";
	var guild1: String = "Trade Union";
	var guild2: String = "Dacei Family";
	var guild3: String = "Coruna Cartel";
	var theChurch: String = "Church";
	var heretic: String = "Heretics";
	
	var latin : String[];
	var greek : String[];
	var italian : String[];
	var spanish : String[];
	var oldenglish : String[];
	
	var listCheck : boolean = false;
	
	var list : System.String[] = latin;
	
	var i : float = Random.value*100;
	
	
	latin = ["Acacius", "Achaicus", "Achilles", "Adelphus", "Adeodatus", "Adolphus", "Aegidius", "Aelia", "Aelianus", "Aemilianus", "Aemilius", "Aeneas", "Aeolos", "Aeolus", "Aeschylus", "Aeson", "Aetius", "Agapetus", "Agapitus", "Agrippa", "Aiolus", "Ajax", "Alair", "Alcaeus", "Alcander", "Alcinder", "Alerio", "Alfonsus", "Alphonsus", "Alphonzus", "Alfonzus", "Almericus", "Aloisius", "Aloysius", "Aloys", "Alphaeus", "Alpheaus", "Alpheus", "Alphoeus", "Alphonsus", "Alured", "Alva", "Alvah", "Amandus", "Amantius", "Amatus", "Ambrosius", "Americus", "Amery", "Amory", "Ampelius", "Anacletus", "Anastasius", "Anastatius", "Anastius", "Anatolius", "Androcles", "Andronicus", "Anencletus", "Angelus", "Anicetus", "Antigonus", "Antipater", "Antoninus", "Antonius", "Apollo", "Appius", "Aquila", "Archelaus", "Argus", "Aries", "Aristaeus", "Aristarchus", "Aristides", "Aristocles", "Aristotle", "Arminius", "Athanasius", "Atilius", "Atticus", "Attius", "Augustinus", "Augustus", "Aulus", "Aurelianus", "Aurelius", "Avitus", "Bartholomaeus", "Bacchus", "Benedictus", "Benignus", "Blasius", "Bonifatius", "Brendanus", "Brennius", "Bricius", "Brictius", "Brutus", "Caecilius", "Caecus", "Caelestinus", "Caelestis", "Caelinus", "Caelius", "Caietanus", "Caius", "Caligula", "Calix", "Callias", "Callistus", "Callixtus", "Calogerus", "Camillus", "Caratacus", "Carolus", "Carpus", "Cassian", "Cassius", "Castor", "Cato", "Celsus", "Cephalus", "Cepheus", "Cerberus", "Christianus", "Christos", "Cicero", "Claudius", "Cleisthenes", "Clemens", "Cleopas", "Cleopatros", "Cletes", "Cletis", "Cletus", "Climacus", "Clitus", "Columba", "Columbanus", "Constans", "Constant", "Constantine", "Constantinus", "Constantius", "Consus", "Cornelius", "Cornell", "Creon", "Crescentius", "Crispinus", "Crispus", "Crius", "Cronos", "Cronus", "Cornus", "Cupid", "Cyprianus", "Cyriacus", "Daedalus", "Damianus", "Damocles", "Darius", "Decebalus", "Decimus", "Delicius", "Demetrius", "Democritus", "Deodatus", "Desiderius", "Deusdedit", "Didacus", "Didagus", "Diodorus", "Diodotus", "Dionysius", "Dionysus", "Dominicus", "Domitian", "Domitianus", "Domitius", "Donatus", "Dorianus", "Dorotheus", "Draco", "Duilius", "Eleutherius", "Egnatius", "Elianus", "Eligius", "Elpidius", "Emericus", "Emidius", "Emygdius", "Ennius", "Eolus", "Ephesius", "Erasmus", "Erebus", "Eugenius", "Euphemius", "Eusebius", "Euthymius", "Eutropius", "Eutychius", "Evaristus", "Evander", "Fabianus", "Fabius", "Fabricius", "Faunus", "Faustinus", "Faustus", "Felicianus", "Felix", "Ferox", "Festus", "Fidelis", "Fido", "Filbertus", "Firminus", "Flavian", "Flavius", "Florentius", "Florian", "Florianus", "Fortunatus", "Franciscus", "Fredericus", "Frigidian", "Fulgentius", "Fulvius", "Gabinus", "Gaius", "Galenus", "Gallus", "Gavius", "Gennadius", "Gerontius", "Gervasius", "Gieronymus", "Gilebertus", "Gilibertus", "Gillebertus", "Glaucia", "Gordian", "Gordianus", "Gracilis", "Gratian", "Gratianus", "Gregorius", "Griffinus", "Gundisalvus", "Gustavus", "Hadrian", "Hadrianus", "Hector", "Heironymus", "Helier", "Heliodorus", "Helladius", "Hemigidius", "Henricus", "Hephaestus", "Heracleitus", "Heraclitus", "Hercules", "Herk", "Hermanus", "Herodotus", "Hesperus", "Herminius", "Valerianus", "Hilarius", "Hippocrates", "Homerus", "Honoratus", "Honorius", "Honorus", "Horatius", "Hortensius", "Hrabanus", "Hugo", "Hyginus", "Iacchus", "Iacobus", "Iacomus", "Icarus", "Ignatius", "Innocentius", "Iohannes", "Iovannis", "Ireneus", "Isaias", "Isocrates", "Italus", "Ivo", "Jacobus", "Januarius", "Janus", "Jason", "Joannes", "Jodocus", "Johannes", "Josephus", "Jove", "Jovita", "Julianus", "Julius", "Junius", "Justinus", "Justus", "Juvenal", "Juvenalis", "Ladislas", "Ladislaus", "Laelianus", "Laelius", "Latinus", "Laurentinus", "Laurentius", "Laurus", "Leander", "Leo", "Leonius", "Leontinus", "Leontius", "Liber", "Liberius", "Linus", "Livianus", "Livius", "Longinus", "Lucas", "Lucianus", "Lucius", "Lucretius", "Ludovicus", "Lupus", "Lycurgus", "Lysimachus", "Macarius", "Magnus", "Manilius", "Manius", "Manlius", "Marcellinus", "Marcellus", "Marcius", "Marcus", "Marianus", "Marinus", "Marius", "Martialis", "Martinus", "Mauricius", "Maurus", "Maxentius", "Maximian", "Maximilianus", "Maximinus", "Maximus", "Menelaus", "Mercury", "Methodius", "Milo", "Modestus", "Naevius", "Narcissus", "Nazarius", "Nemo", "Neoptolemus", "Nero", "Nerva", "Nicator", "Nicodemus", "Nigellus", "Oceanus", "Octavian", "Octavianus", "Octavius", "Olympus", "Onesimus", "Onesiphorus", "Ovid", "Ovidius", "Paramonus", "Paschalis", "Patricius", "Patroclus", "Paulinus", "Paulus", "Pelagius", "Peregrine", "Peregrinus", "Pericles", "Petronius", "Phaedrus", "Philippus", "Phocas", "Phoebus", "Phrixus", "Pius", "Placidus", "Plinius", "Pliny", "Pollux", "Pompeius", "Pompilius", "Pontius", "Porcius", "Primitivus", "Primus", "Priscus", "Prochorus", "Prosperus", "Prudentius", "Publius", "Pyrrhus", "Quintinus", "Quintus", "Quique", "Quirinus", "Rabanus", "Ramirus", "Rastus", "Reginaldus", "Regulus", "Remigius", "Remus", "Renatus", "Riothamus", "Rogatus", "Rogelius", "Rogellus", "Rollo", "Rolo", "Rominus", "Romulus", "Rudolphus", "Rufinus", "Rufus", "Sabinus", "Salvator", "Salvatore", "Sanctius", "Sandalius", "Saternius", "Scaevola", "Sebastianus", "Secundinus", "Secundus", "Seleucus", "Seneca", "Septimus", "Sergius", "Servius", "Severianus", "Severinus", "Severus", "Sextilius", "Sextus", "Sidonius", "Silvanus", "Silvius", "Spurius", "Stanislas", "Stasius", "Stephanas", "Summanus", "Tacitus", "Tatianus", "Tatius", "Terentius", "Terminus", "Tertius", "Thelonius", "Themistocles", "Theocritus", "Theodosius", "Theodotus", "Theodulus", "Theophilus", "Thracius", "Thucydides", "Tiberius", "Tiburtius", "Timaeus", "Timeus", "Timoteus", "Titanius", "Titus", "Tycho", "Ulixes", "Ulysses", "Urbanus", "Urbgenius", "Ursinus", "Ursus", "Valentinus", "Valerian", "Valerius", "Varinius", "Varius", "Velius", "Vergilius", "Verissimus", "Viator", "Vick", "Victor", "Victorinus", "Victorius", "Vincens", "Vincentius", "Vinicius", "Virginius", "Vitalis", "Vitus", "Vivianus", "Vulcan", "Zeno", "Zephyrinus", "Zosimus", "Zoticus"];
	greek = ["Abderus", "Absyrtus", "Abydos", "Acastus", "Achates", "Achelous", "Acheron", "Achilles", "Achlys", "Acrisius", "Actaeon", "Acteon", "Adelphos", "Admes", "Admetus", "Adonis", "Adras", "Adrastos", "Adrastus", "Adrian", "Aeacus", "Aeetes", "Aegeus", "Aegis", "Aegisthus", "Aegyptus", "Aeneas", "Aeolus", "Aesculapius", "Aeson", "Aetos", "Agamedes", "Agamemnon", "Agape", "Agatone", "Aindreas", "Aindriu", "Ajax", "Akil", "Alasd", "Alcander", "Alcinoos", "Alcinous", "Alcnaeon", "Alcyoneus", "Alddes", "Alessandro", "Alex", "Alexander", "Alexander", "Alexandras", "Alexandros", "Alexandrukas", "Alexei", "Alexio", "Alix", "Aloeus", "Alpheus", "Altair", "Amaoebus", "Ambrocio", "Ambrose", "Ambrose", "Ambrus", "Ampyx", "Amycus", "Anastagio", "Anastasio", "Anastasios", "Anastasius", "Anasztaz", "Anatol", "Anatole", "Anatoli", "Anatolijus", "Anatolio", "Ancaeus", "Anchises", "Andor", "Andreas", "Andres", "Andreus", "Andrew", "Andries", "Androgeus", "Androu", "Anstice", "Anstiss", "Antaeus", "Antares", "Anteros", "Antilochus", "Antinous", "Antiphates", "Anton", "Antony", "Aonghas", "Aphrodite", "Apollo", "Apoloniusz", "Apostolos", "Arcadicus", "Arcas", "Arcenio", "Archemorus", "Ares", "Argo", "Argos", "Argus", "Arion", "Aristaeus", "Aristo", "Aristotle", "Arsen", "Arsenio", "Artemas", "Artemesio", "Artemus", "Ascalaphus", "Asklepios", "Asopus", "Athamas", "Athan", "Athanasios", "Athanasius", "Atlas", "Atreus", "Attis", "Autolycus", "Avernus", "Baccaus", "Baccus", "Balasi", "Baltsaros", "Baptiste", "Basil", "Basile", "Basilio", "Baste", "Bastiaan", "Bastien", "Baucis", "Bazyli", "Bellerophon", "Bemus", "Biton", "Boreas", "Brasidas", "Briareus", "Brygus", "Butades", "Cadmus", "Caesare", "Calchas", "Calisto", "Capaneus", "Caseareo", "Castor", "Cecrops", "Celeus", "Cenon", "Cephalus", "Cepheus", "CerbeIus", "Cesare", "Cetus", "Ceyx", "Charybdis", "ChIyses", "Chris", "Chruse", "Chrysostom", "Cimon", "Cirio", "Ciro", "Claas", "Claus", "Cleobis", "Cleon", "Cleon", "Cletus", "Cleytus", "Clio", "Cocytus", "Coeus", "Coireall", "Cole", "Colin", "Colum", "Corban", "Coridan", "Corybantes", "Corydon", "Cos", "Cosmas", "Cosmo", "Cottus", "Creon", "Cretien", "Cronus", "Ctesippus", "Cy", "Cycnus", "Cyr", "Cyrano", "Cyrano", "Cyrek", "Cyril", "Cyril", "Cyrus", "Cyryl", "Daedalus", "Daemon", "Damae", "Damalis", "Damaris", "Damaskenos", "Damaskinos", "Damen", "Dameon", "Damian", "Damion", "Damocles", "Damon", "Danaus", "Dardanus", "Darian", "Darien", "Darion", "Darrian", "Darrien", "Darrion", "Daymon", "Deiphobus", "Deke", "Dekel", "Dekle", "Delbin", "Demetre", "Demetri", "Demetrios", "Demetrius", "Demetrius", "Demitri", "Demitrius", "Demodocus", "Demogorgon", "Demophon", "Demoritus", "Denes", "Dennis", "Deo", "Deucalion", "Diokles", "Diomedes", "Dion", "Dionysios", "Dionysius", "Dionysus", "Doran", "Dorian", "Dorion", "Drakon", "Drew", "Dunixi", "Dymas", "Eachann", "Efterpi", "Egidio", "Eleftherios", "Elek", "Eleutherios", "Eli", "Enceladus", "Endre", "Endymion", "Eneas", "Eoghan", "Epeius", "Erasmus", "Erebus", "Erechtheus", "Erichthonius", "Erymanthus", "Erysichthon", "Eryx", "Estevao", "Eteocles", "Etor", "Eubuleus", "Eugen", "Eugene", "Eugenio", "Eugenios", "Eumaeus", "Eupeithes", "Euphrosyne", "Eurus", "Euryalus", "Eurylochus", "Eurymachus", "Eurypylus", "Eurystheus", "Euryton", "Eusebius", "Eustace", "Eustachy", "Eustis", "Evan", "Evzen", "Fedor", "Feodor", "Feodras", "Filippo", "Filips", "Flip", "Fulop", "Gaelan", "Galan", "Galen", "Galinthias", "Galyn", "Ganymede", "Gelasius", "Georg", "George", "George", "Georges", "Giles", "Glaucus", "Goran", "Graeae", "Gregoire", "Gregoly", "Gregor", "Gregorie", "Gregorior", "Gregory", "Gregos", "Grigor", "Grigorov", "Gruev", "Guilio", "Gyes", "Gyoergy", "Gyorgy", "Gyuri", "Haemon", "Hali", "Halirrhothius", "Halithersis", "Hector", "Helios", "Hephaestus", "Heraklesr", "Hercules", "Hermes", "Herodotus", "Heron", "Hesperos", "Homar", "Homer", "Homeros", "Homerus", "Hypnos", "Iapetus", "Iasion", "Iason", "Ibycus", "Icarius", "Icarus", "Icelos", "Icos", "Idas", "Idomeneus", "Ignatius", "Igorr", "Inachus", "Iorgas", "Iphicrates", "Iphitus", "Isocrates", "Istvan", "Ivan", "Ivankor", "Ixion", "Jaison", "Jase", "Jayce", "Jayr", "Jaysen", "Jayson", "Jencir", "Jeno", "Jeroenr", "Jerome", "Jerzyr", "Jirkar", "Jorenr", "Julius", "Jurgisr", "Kadmus", "Kairos", "Khristos", "Khrystiyanr", "Kit", "Klaasr", "Klaus", "Korudon", "Kosmosr", "Kratos", "Krikor", "Krischnan", "Kristian", "Kristofr", "Krystupasr", "Krzysztofr", "Kuirilr", "Kyrillos", "Kyrillosr", "Kyros", "Ladon", "Laertes", "Laestrygones", "Laius", "Lander", "Laocoon", "Laomedon", "Leander", "Leandro", "Leonidas", "Lichas", "Linus", "Lippio", "Lotus", "Loxias", "Luke", "Lycaon", "Lycomedes", "Lycurgus", "Lynceus", "Lysander", "Marcario", "Maurice", "Medus", "Melampus", "Melancton", "Melanthius", "Menelaus", "Menoeceus", "Mentor", "Midas", "Mikolas", "Miles", "Milo", "Minos", "Momus", "Mopsus", "Morpheus", "Moses", "Myles", "Myron", "Napoleon", "Narkis", "Nauplius", "Nectarios", "Neleus", "Nemo", "Nemos", "Neo", "Nicholas", "Nicholaus", "Nicias", "Nick", "Nicodemus", "Nicolas", "Nicolaus", "Nik", "Niklaus", "Nikodem", "Nikolai", "Nikolajis", "Nikolos", "Niles", "Nilo", "Nilos", "Nisus", "Notus", "Obiareus", "Oceanus", "Ocnus", "Odysseus", "Oedipus", "Oeneus", "Oenomaus", "Oighrig", "Oles", "Orion", "Orpheus", "Orrin", "Orthros", "Otis", "Otos", "Otus", "Palaemon", "Palamedes", "Pan", "Panagiotis", "Pancratius", "Panos", "Panteleimon", "Papandrou", "Paris", "Parthenios", "Patroclus", "Pavlos", "Peadair", "Peder", "Pegeen", "Peleus", "Pelias", "Pello", "Pelops", "Pentheus", "Pero", "Perrin", "Perseus", "Persius", "Peterke", "Petrelis", "Petros", "Petter", "Phaethon", "Phantasos", "Phaon", "Phemius", "Philemon", "Philip", "Philo", "Philoctetes", "Phineas", "Phineus", "Phlegethon", "Phrixus", "Phylo", "Piero", "Pierro", "Pieter", "Pietr", "Pietro", "Pilib", "Piotr", "Pippo", "Pirithous", "Pirro", "Pittheus", "Plexippus", "Plutus", "PoIlux", "Poseidon", "Preben", "Priam", "Priapus", "Procrustes", "Prokopios", "Prometheus", "Prophyrios", "Protesilaus", "Proteus", "Ptolemy", "Pygmalion", "Pyramus", "Rasmus", "Rhadamanthus", "Rhodes", "Risto", "Sabastian", "Salmoneus", "Scopas", "Sebasten", "Sebastian", "Sebastiano", "Simon", "Sinon", "Socrates", "Sofronio", "Soterios", "Spyridon", "Stamitos", "Stavros", "Steafan", "Stefan", "Stefano", "Stefanos", "Stefford", "Stefon", "Stephano", "Stephanos", "Stephen", "Stephon", "Steven", "Steverino", "Takis", "Talos", "Talus", "Tantalus", "Tarasios", "Teague", "Telamon", "Telegonus", "Telemachus", "Telephus", "Tellus", "Teodors", "Teofile", "Teuthras", "Thad", "Thaddeus", "Thaddius", "Thadeus", "Thais", "Thanatos", "Thanos", "Thanos", "Thaumas", "Theo", "TheocIymenus", "Theodon", "Theodore", "Theodore", "Theodosios", "Theodrekr", "Theodric", "Theon", "Theophile", "Theophilus", "Theron", "Therron", "Thersites", "Theseus", "Thomas", "Thyestes", "Tiege", "Tim", "Timeus", "Timmy", "Timocrates", "Timoleon", "Timon", "Timotheos", "Timothy", "Timun", "Tiomoid", "Tito", "Titos", "Titus", "Tityus", "Tivadar", "Todor", "Toxeus", "Triptolemus", "Triton", "Trophonius", "Turannos", "Tydeus", "Tylissus", "Tymek", "Tymon", "Tyndareus", "Typhoeus", "Typhon", "Tyrone", "Tyrone", "Ulysses", "Uranus", "Urian", "Vasileios", "Vasilios", "Vasilis", "Vasos", "VasyItso", "Vasyklo", "Vasyl", "Xanthus", "Xanthus", "Xerxes", "Xuthus", "Xylon", "Yanni", "Yehor", "Yuri", "Zale", "Zarek", "Zelotes", "Zeno", "Zenobio", "Zenon", "Zenos", "Zephyr", "Zetes", "Zoltan", "Zoltar", "Zorba", "Zotico"];
	italian = ["Aberto", "Abramo", "Adolfo", "Adriano", "Agostino", "Alanzo", "Alba", "Aldo", "Alessandro", "Alfonso", "Alfredo", "Alonso", "Alonzo", "Alrigo", "Amadeo", "Ambrosi", "Amo", "Anastagio", "Andino", "Andrea", "Angelino", "Angelo", "Antonino", "Antonio", "Antony", "Arlo", "Armanno", "Armond", "Armondo", "Arnaldo", "Aroghetto", "Arrigo", "Arturo", "Baldasarre", "Baldassario", "Benedetto", "Beniamino", "Benito", "Bernardo", "Bertrando", "Biaiardo", "Brando", "Bruno", "Calvino", "Carlino", "Carlo", "Carmine", "Caseareo", "Cecilio", "Cesare", "Cesario", "Cirocco", "Constantin", "Corradeo", "Corrado", "Cristoforo", "Damiano", "Daniele", "Dantae", "Dante", "Dantel", "Daunte", "Davide", "Donato", "Donzel", "Draco", "Drago", "Edmondo", "Edoardo", "Eduardo", "Egidio", "Egiodeo", "Elmo", "Emesto", "Emiliano", "Emilio", "Enea", "Enrico", "Enzo", "Ermanno", "Este", "Ettore", "Eugenio", "Fabian", "Fabiano", "Fabrizio", "Fahroni", "Faust", "Fausto", "Federico", "Feleti", "Feliciano", "Felicio", "Ferdinando", "Fico", "Fidelio", "Filippo", "Fiorello", "Flavio", "Fontana", "Francesco", "Franco", "Frederico", "Gabriele", "Gaetan", "Gaetano", "Galtem", "Galterio", "Gavino", "Geomar", "Georgio", "Geovani", "Geovanni", "Geovanny", "Geovany", "Gerardo", "Geremia", "Gerodi", "Geronimo", "Giacomo", "Gian", "Giancarlo", "Gilberto", "Gino", "Giomar", "Giorgio", "Giovani", "Giovanni", "Giovanny", "Giovany", "Giovonni", "Gitano", "Giuliano", "Giulio", "Gregorio", "Gualtier", "Gualtiero", "Guglielmo", "Guido", "Gustavo", "Ignacio", "Ignazio", "Ilario", "Innocenzio", "Kajetan", "Lanzo", "Lauro", "Lazzaro", "Leo", "Leonardo", "Leone", "Leopoldo", "Lorenz", "Lorenzo", "Lucan", "Lucca", "Luciano", "Lucio", "Ludano", "Ludo", "Luigi", "Marcelino", "Marcell", "Marcello", "Marcelo", "Marciano", "Marcio", "Marco", "Mario", "Marquise", "Martino", "Martinus", "Massimo", "Matteo", "Maurilio", "Maurio", "Maurizio", "Mauro", "Maximiliano", "Maximino", "Maximo", "Michel", "Michelangelo", "Michele", "Montae", "Montay", "Monte", "Montel", "Montes", "Montez", "Montrel", "Montrell", "Montrelle", "Napoleon", "Nari", "Nario", "Niccolo", "Nicoli", "Nicolo", "Nico", "Orlando", "Orsino", "Paolo", "Pascal", "Pascual", "Pasquale", "Patrizio", "Peppino", "Piero", "Pietro", "Pio", "Pippino", "Placido", "Primo", "Rafaele", "Rafaello", "Raffaello", "Raimondo", "Raphael", "Renzo", "Ric", "Ricardo", "Riccardo", "Ricco", "Rinaldo", "Roberto", "Rocco", "Romano", "Romeo", "Ruggero", "Ruggiero", "Sal", "Salvatore", "Salvatorio", "Sandro", "Santino", "Santo", "Savino", "Sebastiano", "Sergio", "Silvio", "Stefano", "Stephano", "Teodoro", "Tiberio", "Tino", "Tito", "Tommaso", "Torre", "Ugo", "Umberto", "Uso", "Valente", "Valerio", "Vincenzio", "Vitale", "Vito", "Vittorio", "Xiomar"];
	spanish = ["Abran", "Adan", "Adelio", "Adriano", "Agustin", "Aimon", "Alano", "Alanzo", "Alarico", "Alba", "Alberto", "Alberto", "Alejandro", "Alejandro", "Alfonso", "Alfredo", "Alonso", "Alonzo", "Aluino", "Alvar", "Alvaro", "Alvaro", "Alvino", "Amadeo", "Amado", "Ambrosio", "Amoldo", "Anastasio", "Anbessa", "Andreo", "Andres", "Anibal", "Anselmo", "Anton", "Antonio", "Antonio", "Aquila", "Aquilino", "Archibaldo", "Arlo", "Armando", "Arnaldo", "Arnoldo", "Arturo", "Aureliano", "Aurelio", "Aurelius", "Barto", "Bartoli", "Bartolo", "Bartolome", "Basilio", "Beinvenido", "Beltran", "Bemabe", "Bembe", "Benedicto", "Bernardo", "Berto", "Blanco", "Blas", "Bonifacio", "Bonifaco", "Buinton", "Calvino", "Carlomagno", "Carlos", "Carlos", "Casimiro", "Casta", "Cedro", "Cesar", "Cesario", "Cesaro", "Chan", "Chano", "Charro", "Chavez", "Chayo", "Che", "Ciceron", "Cid", "Cidro", "Cipriano", "Cirilo", "Ciro", "Cisco", "Claudio", "Clodoveo", "Conrado", "Constantino", "Cornelio", "Cortez", "Cris", "Cristian", "Cristiano", "Cristobal", "Cristofer", "Cristofor", "Criston", "Cristos", "Cristoval", "Cruz", "Cruz", "Cuartio", "Cuarto", "Curcio", "Currito", "Curro", "Dacio", "Damario", "Damian", "Danilo", "Dantae", "Dante", "Dantel", "Dario", "Dario", "Daunte", "Delmar", "Demario", "Desiderio", "Desiderio", "Diego", "Diego", "Dino", "Domenico", "Domingo", "Donatello", "Donato", "Donzel", "Duardo", "Duarte", "Edgardo", "Edmundo", "Eduardo", "Efrain", "Elia", "Elias", "Eliazar", "Elija", "Eloy", "Elvio", "Emanuel", "Emesto", "Emilio", "Eneas", "Enrique", "Enzo", "Erasmo", "Ernesto", "Eron", "Esequiel", "Estevan", "Estevon", "Eugenio", "Evarado", "Everardo", "Ezequiel", "Fabio", "Fanuco", "Faro", "Faron", "Fausto", "Fausto", "Federico", "Feliciano", "Felipe", "Felippe", "Felix", "Feo", "Fermin", "Fernando", "Fernando", "Fidel", "Fidele", "Flavio", "Florentino", "Florinio", "Fraco", "Francisco", "Franco", "Frascuelo", "Frederico", "Fresco", "Frisco", "Gabino", "Gabriel", "Gabrio", "Galeno", "Galtero", "Garcia", "Gaspar", "Gaspard", "Generosb", "Geraldo", "Geraldo", "Gerardo", "Geronimo", "Gervasio", "Gervaso", "Gezane", "Gil", "Gilberto", "Gillermo", "Ginebra", "Ginessa", "Gitana", "Godalupe", "Godfredo", "Godofredo", "Gorane", "Gotzone", "Gracia", "Graciana", "Gregoria", "Gregorio", "Guadalupe", "Gualterio", "Guido", "Guillelmina", "Guillermo", "Guillermo", "Gustava", "Gustavo", "Hector", "Henriqua", "Heriberto", "Heriberto", "Hermosa", "Hernan", "Hernandez", "Hernando", "Hernando", "Hidalgo", "Hilario", "Honor", "Honoratas", "Honorato", "Honoria", "Horado", "Hortencia", "Hugo", "Humberto", "Iago", "Idoia", "Idurre", "Ignacia", "Ignado", "Ignazio", "Igone", "Ikerne", "Ileanna", "Iliana", "Incendio", "Inocencio", "Inocente", "Isadoro", "Isaias", "Ishmael", "Isidoro", "Isidro", "Ismael", "Ivan", "Jacinto", "Jacobo", "Jago", "Jaime", "Jairo", "Javier", "Javiero", "Jax", "Jeraldo", "Jeremias", "Jerico", "Jerold", "Jerone", "Jerrald", "Jerrold", "Joaquin", "Jonas", "Jorge", "Jorge", "Jose", "Jose", "Joselito", "Josias", "Josue", "Josue", "Juan", "Juan", "Juanito", "Julian", "Juliano", "Julio", "Julio", "Justino", "Katia", "Kemen", "Lalla", "Lalo", "Lazaro", "Leandro", "Leon", "Leonardo", "Leonel", "Leonides", "Leopoldo", "Lia", "Lisandro", "Lobo", "Lonzo", "Lorenzo", "Lucero", "Luciano", "Lucila", "Lucio", "Luis", "Macario", "Mano", "Manolito", "Manolo", "Manuel", "Manuelo", "Marco", "Marcos", "Mariano", "Mario", "Marquez", "Martin", "Martinez", "Martino", "Mateo", "Matias", "Matro", "Maureo", "Mauricio", "Mauro", "Miguel", "Milagro", "Mio", "Montae", "Montay", "Monte", "Montego", "Montel", "Montenegro", "Montes", "Montez", "Montrel", "Montrell", "Naldo", "Natal", "Natalio", "Natanael", "Nataniel", "Navarro", "Nemesio", "Neron", "Nesto", "Nestor", "Neto", "Nevada", "Nicanor", "Nicolas", "Niguel", "Noe", "Norberto", "Normando", "Oliverio", "Oliverios", "Onofre", "Orlan", "Orland", "Orlando", "Orlin", "Orlondo", "Oro", "Ovidio", "Pablo", "Pacho", "Paco", "Pacorro", "Palban", "Palben", "Pascual", "Pasqual", "Patricio", "Patrido", "Paz", "Pirro", "Placido", "Ponce", "Porfirio", "Porfiro", "Primeiro", "Prospero", "Pueblo", "Quin", "Quinto", "Quito", "Rafael", "Rafe", "Rai", "Raimundo", "Ramirez", "Ramiro", "Ramon", "Ramone", "Raul", "Raulo", "Rayman", "Raymon", "Renaldo", "Renato", "Reno", "Rey", "Reyes", "Reynaldo", "Reynardo", "Ricardo", "Richie", "Rico", "Ritchie", "Roano", "Roberto", "Rodas", "Roderigo", "Rodolfo", "Rodrigo", "Rogelio", "Roldan", "Ronaldo", "Roque", "Rosario", "Ruben", "Rufio", "Rufo", "Sabino", "Sal", "Salbatore", "Salomon", "Salvador", "Salvadore", "Salvatore", "Salvino", "Sancho", "Santiago", "Santo", "Santos", "Saturnin", "Saul", "Sebastiano", "Segundo", "Sein", "Senon", "Serafin", "Severo", "Silverio", "Silvino", "Socorro", "Sol", "Stefano", "Suelita", "Tabor", "Tadeo", "Tajo", "Taurino", "Tauro", "Tavio", "Tejano", "Teo", "Teodor", "Teodoro", "Terciero", "Teyo", "Timo", "Timoteo", "Tito", "Tobias", "Tohias", "Toli", "Tomas", "Tonio", "Toro", "Tulio", "Turi", "Turi", "Ulises", "Urbano", "Valentin", "Veto", "Vicente", "Victor", "Victoriano", "Victorino", "Victorio", "Victoro", "Vidal", "Vincente", "Virgilio", "Vito", "Xabat", "Xalbador", "Xalvador", "Xavier", "Xever", "Yago", "Zacarias"];
	oldenglish = ["Aballach", "Accalon", "Ackerley", "Ackley", "Acton", "Aglaral", "Aglarale", "Aglaval", "Agravain", "Alain", "Alburt", "Alden", "Aldin", "Aleyn", "Alfred", "Alfrid", "Alis", "Alton", "Amr", "Andret", "Anguysh", "Anir", "Antfortas", "Antor", "Arleigh", "Arthgallo", "Ashton", "Auctor", "Augwys", "Avalloc", "Awarnach", "Bagdemagus", "Baldulf", "Balen", "Balin", "Bax", "Baxter", "Bayley", "Beaumains", "Bedivere", "Bedver", "Bedwyr", "Bedwyr", "Bellangere", "Benoyce", "Bercilak", "Bernlak", "Bersules", "Berton", "Bertram", "Bicoir", "Bliant", "Blyth", "Blythe", "Boarte", "Bodwyn", "Bohort", "Borre", "Bors", "Bradan", "Braddon", "Bran", "Branor", "Branson", "Brant", "Brantley", "Braxton", "Brayden", "Braydon", "Bredbeddle", "Brehus", "Brenius", "Brennus", "Breri", "Bretton", "Breuse", "Byron", "Cabal", "Cador", "Cadwallon", "Caerleon", "Cafall", "Cai", "Calder", "Calibom", "Calibome", "Calibor", "Calibum", "Calibumus", "Calogrenant", "Camelon", "Camlann", "Carleton", "Carlson", "Carlton", "Carrado", "Cath", "Catterick", "Catterik", "Cavalon", "Caw", "Cedric", "Chanler", "Chapalu", "Clamedeus", "Clarion", "Clarke", "Cleve", "Clevon", "Clive", "Clyve", "Cnidel", "Codell", "Codey", "Coletun", "Colten", "Colton", "Corbenic", "Crom", "Crompton", "Cromwell", "Culhwch", "Cus", "Dagonet", "Daguenet", "Dalan", "Dale", "Dalen", "Dallan", "Dallen", "Dallin", "Dallon", "Dalon", "Dalton", "Dalyn", "Dane", "Daylan", "Dayle", "Daylen", "Daylon", "Dayton", "Denton", "Dinadan", "Dnias", "Dristan", "Drudwyn", "Drystan", "Dudley", "Dynadin", "Ector", "Edgar", "Edgard", "Edmon", "Edmond", "Edmund", "Edward", "Edwin", "Edwyn", "Ektor", "Eldan", "Elden", "Eldon", "Elmer", "Elvern", "Elvey", "Elvin", "Elvy", "Elvyn", "Engres", "Escalibor", "Escanor", "Evadeam", "Evalac", "Evelake", "Evrain", "Farlow", "Farnall", "Farnell", "Farnly", "Farold", "Farr", "Farran", "Farren", "Farrin", "Feirefiz", "Fenton", "Feran", "Firth", "Fleming", "Forde", "Frewen", "Frewin", "Frewyn", "Frey", "Frick", "Froille", "Frollo", "Frost", "Fuller", "Gahariet", "Gaheris", "Gahmuret", "Gais", "Galantyne", "Galatyn", "Galeron", "Galvarium", "Garey", "Garrick", "Garrson", "Garson", "Garsone", "Garvan", "Garvin", "Garvyn", "Garwig", "Garwin", "Garwyn", "Gauvain", "Geary", "Gerard", "Gerry", "Giflet", "Gilmar", "Gilmer", "Girflet", "Glais", "Godwin", "Godwine", "Goldwine", "Goldwyn", "Gordan", "Gorlois", "Gorvenal", "Gouveniail", "Goveniayle", "Graysen", "Grayson", "Griflet", "Gringalet", "Gringolet", "Gryfflet", "Guerehes", "Guivret", "Gurgalan", "Harris", "Harrison", "Havyn", "Hellekin", "Hoel", "Holcomb", "Houdain", "Houdenc", "Howel", "Hutton", "Hyatt", "Hyde", "Ingram", "Isdemus", "Ither", "Ivey", "Jeffery", "Jeffrey", "Jeffry", "Johfrit", "Kade", "Kaherdin", "Kanelinqes", "Kardeiz", "Ke", "Kei", "Kelvan", "Kelven", "Kelvin", "Kelvyn", "Kelwin", "Kelwyn", "Kendal", "Kendale", "Kendall", "Kendel", "Kendell", "Kenward", "Kenway", "Lailoken", "Laine", "Lamorak", "Lamorat", "Landon", "Landry", "Lane", "Laudegrance", "Launfal", "Layne", "Leigh", "Leodegan", "Leodegraunce", "Lind", "Llacheu", "Lludd", "Loe", "Lohengrin", "Lucan", "Lueius", "Lyndon", "Mabonaqain", "Mabuz", "Mador", "Maheloas", "Maldue", "Mariadok", "Marrok", "Meleagant", "Melechan", "Meliadus", "Meliodas", "Melvin", "Melwas", "Melyon", "Merlyn", "Milton", "Mitch", "Mitchel", "Mitchell", "Modred", "Monte", "Montie", "Mordrain", "Mordred", "Morgan", "Morholt", "Morold", "Morton", "Nantres", "Nentres", "North", "Norval", "Nudd", "Octha", "Ocvran", "Osmond", "Oswald", "Oswell", "Oswin", "Owain", "Palamedes", "Palomydes", "Palsmedes", "Pant", "Parsifal", "Parzifal", "Paton", "Pellam", "Pellean", "Pelleas", "Pelles", "Percyvelle", "Peredwus", "Petrus", "Peyton", "Presley", "Prestin", "Pslomydes", "Ramsay", "Ramsden", "Ramsey", "Ramzey", "Ramzi", "Ranald", "Rand", "Randal", "Randall", "Randel", "Randell", "Redd", "Redd", "Riley", "Rion", "Rivalen", "Royns", "Sagramour", "Sagremor", "Sewald", "Sewall", "Seward", "Sewell", "Shandon", "Shandy", "Shelny", "Stewert", "Sutton", "Taliesin", "Tedmond", "Tedmund", "Teller", "Tentagil", "Thane", "Thayne", "Theyn", "Thurl", "Thurle", "Tintagel", "Tolan", "Toland", "Tor", "Torey", "Torr", "Torrey", "Torrian", "Torrie", "Torry", "Tory", "Trenton", "Trevrizent", "Trey", "Urien", "Uriens", "Uther", "Vance", "Vortigem", "Vortimer", "Wardell", "Wilbur", "Wincel"];
	

	// Pick Namelist
	if (who == "latin") 	{ list = latin; listCheck = true; }
	if (who == "greek") 	{ list = greek; listCheck = true; }
	if (who == "italian") 	{ list = italian; listCheck = true; }
	if (who == "spanish") 	{ list = spanish; listCheck = true; }
	if (who == "oldenglish") { list = oldenglish; listCheck = true; }
	
		// Noble Houses
		if (who == "nobleHouse1" || nobleHouse1) { 
			if (i < 70) 	{ list = greek; listCheck = true; }
			if (i >= 70) 	{ list = italian; listCheck = true; }
		}
		if (who == "nobleHouse2" || nobleHouse2) { 
			if (i < 80) 	{ list = oldenglish; listCheck = true; }
			if (i >= 80) 	{ list = latin; listCheck = true; }
		}
		if (who == "nobleHouse3" || nobleHouse3) { 
			if (i < 80) 	{ list = latin; listCheck = true; }
			if (i >= 80) 	{ list = greek; listCheck = true; }
		}
		if (who == "nobleHouse4" || nobleHouse4) { 
			if (i < 90) 	{ list = latin; listCheck = true; }
			if (i >= 90) 	{ list = italian; listCheck = true; }
		}
		// Guilds
		if (who == "guild1" || guild1) { 
			if (i < 50) 			{ list = spanish; listCheck = true; }
			if (i >= 50 && i < 80) 	{ list = italian; listCheck = true; }
			if (i >= 80 && i < 90) 	{ list = oldenglish; listCheck = true; }
			if (i >= 90 && i <= 97) { list = greek; listCheck = true; }
			if (i > 97) 			{ list = latin; listCheck = true; }
		}
		if (who == "guild2" || guild2) { 
			if (i < 40) 			{ list = spanish; listCheck = true; }
			if (i >= 40 && i < 80) 	{ list = italian; listCheck = true; }
			if (i >= 80 && i <= 95) { list = greek; listCheck = true; }
			if (i > 95) 			{ list = latin; listCheck = true; }
		}
		if (who == "guild3" || guild3) { 
			if (i < 60) 			{ list = italian; listCheck = true; }
			if (i >= 60 && i <= 90) { list = greek; listCheck = true; }
			if (i > 90) 			{ list = latin; listCheck = true; }
		}
		// Church
		if (who == "church" || theChurch) { 
			if (i < 80) 			{ list = latin; listCheck = true; }
			if (i >= 80 && i < 90)	{ list = greek; listCheck = true; }
			if (i >= 90 && i <= 98) { list = italian; listCheck = true; }
			if (i > 98) 			{ list = spanish; listCheck = true; }
		}
		// Heretic
		if (who == "heretic" || heretic) { 
			if (i < 10) 			{ list = spanish; listCheck = true; }
			if (i >= 10 && i < 20) 	{ list = italian; listCheck = true; }
			if (i >= 20 && i < 30) 	{ list = oldenglish; listCheck = true; }
			if (i >= 30 && i <= 50) { list = latin; listCheck = true; }
			if (i > 50) 			{ list = greek; listCheck = true; }
		}
		// Any
		if (who == "any") { 
			if (i < 20) 			{ list = spanish; listCheck = true; }
			if (i >= 20 && i < 40) 	{ list = italian; listCheck = true; }
			if (i >= 40 && i < 60) 	{ list = oldenglish; listCheck = true; }
			if (i >= 60 && i <= 80) { list = greek; listCheck = true; }
			if (i > 80) 			{ list = latin; listCheck = true; }	
		}
		// Local
		if (who == "local") { 
			if (i < 60) 			{ list = spanish; listCheck = true; }
			if (i >= 60 && i < 90) { list = italian; listCheck = true; }
			if (i >= 90 && i <= 98) { list = oldenglish; listCheck = true; }
			if (i > 98) 			{ list = greek; listCheck = true; }
		}
		
	if (!listCheck)	{ Debug.LogError("getName input error: " +who); }
	

	// Pick Name

	i = Mathf.Floor(Random.Range(0, list.Length));

	return list[i];
}
*/