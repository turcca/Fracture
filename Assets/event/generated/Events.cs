#pragma warning disable 0162, 1717
using System;
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 1
//---------------------------------------------------------------------------------
public class Event_1 : EventBase {
public Event_1() : base("Test Event") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
locationEvent=true;
character = getBestCharacter( Character.Stat.leadership);
location= "4S03";
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if (getElapsedDays() > 1)
{
p = p * 10;
}
if ( ! (getPlayerLocationID() == location) )
{
p = p *  0;
}
if ( getCharacterStat(Character.Stat.leadership) > 3 )
{
p = p *  2;
}
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
if ( getCharacterStat(Character.Job.captain, Character.Stat.leadership) <= 3 )
{
eventAdvice.text = "I guess we have to settle.";
eventAdvice.recommend =  1;
}
}
if (job == Character.Job.captain) {
if ( getCharacterStat(Character.Job.captain, Character.Stat.leadership) > 3 )
{
eventAdvice.text = "Let me lead you!";
eventAdvice.recommend =  2;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "Hmm";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if ( outcome== 0 )
{
return "We must attack at +location.locName+.";
}
if ( outcome== 0 && getCharacterStat(Character.Stat.leadership) > 3 )
{
return " It won't work unless +character.name+ leads us!";
}
if ( outcome > 0 )
{
return "It has happened before!";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("There is but one choise.",  1);
}
if ( getCharacterStat(Character.Stat.leadership) > 3)
{
choices.Add("Unless +character.name+'s leadership is >3: +character.leadership+",  2);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice > 0)
{
outcome=choice;
factionChange("nobleHouse4",-1);
filterWeight("mutants",0);
available=false;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 2
//---------------------------------------------------------------------------------
public class Event_2 : EventBase {
public Event_2() : base("Test Event inLocation") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
character = getBestCharacter( Character.Stat.leadership);
location= "4S03";
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if (getElapsedDays() > 1)
{
p = p * 10;
}
if ( ! (getPlayerLocationID() == location) )
{
p = p *  0;
}
if ( getCharacterStat(Character.Stat.leadership) > 3 )
{
p = p *  2;
}
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if ( outcome== 0 )
{
return "What happens will happen in +location.locName+.";
}
if ( outcome== 0 )
{
return " It won't happen unless +character.name+ leads us!";
}
if ( outcome > 0 )
{
return "It has happened before!";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("There is but one choise.",  1);
}
if ( getCharacterStat(Character.Stat.leadership) > 3)
{
choices.Add("Unless +character.name+'s leadership is >3: +character.leadership+",  2);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice > 0)
{
outcome=choice;
available=false;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 3
//---------------------------------------------------------------------------------
public class Event_3 : EventBase {
public Event_3() : base("The root of all evil") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
character = getBestCharacter( Character.Stat.psy);
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if (getWarpMagnitude() < 1.7)
{
p = p *  0 ;
}
if (getElapsedDays() < 60)
{
p = p *  10 ;
}
if (getEvent("Test Event").outcome== 0 )
{
p = p * 10;
}
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "Traveling through deep Fracture, +character.name+";
}
if (outcome== 0 && getCharacterStat(Character.Stat.corruption) >= 70)
{
return " finally contacts an intelligence in the warp.";
}
if (outcome== 0 && getCharacterStat(Character.Stat.corruption) < 70)
{
return " has a terrible visitation from a daemon of the warp.";
}
if ( outcome== 1 )
{
return "+character.name+ had long talks with it, but didn't remember much afterwards.";
}
if ( outcome== 2 )
{
return "After a quick encounter, there is a lingering feeling it'll be back...";
}
if ( outcome== 3 )
{
return "+character.name+ learns its name, and starts preparing rituals.";
}
if ( outcome== 4 )
{
return "Escape! But what do they say about running from your problems?";
}
if ( outcome== 5 )
{
return "Reciting the holy scriptures banishes the daemon! Praised be!";
}
if ( outcome== 6 )
{
return "+character.name+ succeeds, and now can call upon great powers.";
}
if ( outcome== 7 )
{
return "+character.name+ leans its name, something will come out of this.";
}
if ( outcome== 8 )
{
return "+character.name+ succeeds, and now can call upon great powers.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if ( outcome== 0 )
{
choices.Add("Try to communicate with it, and learn its name.",  1);
}
if ( outcome== 0 )
{
choices.Add("Oh no, run away!",  2);
}
if ( outcome== 0 && getCharacterStat(Character.Stat.holiness) > 2)
{
choices.Add("Recite holy scriptures.",  3);
}
if ( outcome== 0 && getCharacterStat(Character.Stat.psy) > 3)
{
choices.Add("Try to capture the daemon.",  4);
}
if ( outcome > 0 && outcome != 5 )
{
choices.Add("Continue",  8);
}
if ( outcome== 5 )
{
choices.Add("Continue, you holy crusader!",  9);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice==1 && statRoll("psy") < 3)
{
outcome= 1;
addCharacterStat(Character.Stat.possessed,0);
}
if ( choice== 1 && statRoll("psy") > 2 && statRoll("psy") < 4 )
{
outcome= 2;
}
if ( choice== 2 )
{
outcome= 4;
}
if ( choice== 3 )
{
outcome= 5;
}
if (choice== 4 && statRoll("psy") <= 3)
{
outcome= 6;
setCharacterStat(Character.Stat.possessed,100);
}
if (choice== 4 && statRoll("psy") > 3 && getCharacterStat(Character.Stat.corruption) <= 100)
{
outcome= 7;
}
if (choice== 4 && statRoll("psy") > 3 && getCharacterStat(Character.Stat.corruption) > 100)
{
outcome= 8;
addCharacterStat(Character.Stat.corruption,0);
}
if ( choice == 8 )
{
outcome=outcome;
available=false;
end();
}
if ( choice== 9 )
{
outcome=outcome;
addCharacterStat(Character.Stat.holiness,5);
available=false;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 4
//---------------------------------------------------------------------------------
public class Event_4 : EventBase {
public Event_4() : base("Odd behavior") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Weeed to figure out what's going on.";
eventAdvice.recommend =  2;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "Odd behavior you say?";
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "I've got my men under control.";
eventAdvice.recommend =  1;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "We should set up a curfew and arrest the troublemakers at once!";
eventAdvice.recommend =  2;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "The crew could use some downtime. We've spend too much time in the Fracture lately.";
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "Well well, what do we have here?";
eventAdvice.recommend = 2;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Heretics! They are all damned heretics!";
eventAdvice.recommend = 2;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You hear reports of odd behavior among the crew.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("It's probably nothing, carry on!",  1);
}
{
choices.Add("Odd behavior? It might be wise to investigate this one.", 2);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
{
outcome=choice;
available=false;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 5
//---------------------------------------------------------------------------------
public class Event_5 : EventBase {
public Event_5() : base("locationAdvice") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
{
p = p *  0;
}
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
if ( getCharacterStat(Character.Job.captain, Character.Stat.diplomat) <= 2 )
{
eventAdvice.text = "+location.ruler+ is in charge here.";
}
}
if (job == Character.Job.captain) {
if ( getCharacterStat(Character.Job.captain, Character.Stat.diplomat) > 2 )
{
eventAdvice.text = "+location.ruler+ is in charge here.";
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "These people once counted their lucky stars. Now they worry about crops.";
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "We could use more engineers.";
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "We should keep a low profile and not recruit here.";
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "The crew could use some downtime. We've spend too much time in the Fracture lately.";
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "This is an ancient planet. Something forgotten is lying beneath the surface.";
}
}
if (job == Character.Job.priest) {
//if ( getLocation().ideology.effects.holy < -50 )
//{
//eventAdvice.text = "Heretics! They are all damned heretics!";
//}
}
if (job == Character.Job.priest) {
//if ( getLocation().ideology.effects.holy >= -50 && getLocation().ideology.effects.holy < 0)
//{
//eventAdvice.text = "What a misguided place this is.";
//}
}
if (job == Character.Job.priest) {
//if ( getLocation().ideology.effects.holy >= 0 && getLocation().ideology.effects.holy < 40)
//{
//eventAdvice.text = "This place seem decent enough";
//}
}
if (job == Character.Job.priest) {
//if ( getLocation().ideology.effects.holy > 40 )
//{
//eventAdvice.text = "Ah, +location.name+ is known for its diligent and productive citizens. Truly inspiring.";
//}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
{
outcome= 0;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 6
//---------------------------------------------------------------------------------
public class Event_6 : EventBase {
public Event_6() : base("appointment_Locals") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "We get to meet with +location.ruler+'s representative.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the Local Government.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 7
//---------------------------------------------------------------------------------
public class Event_7 : EventBase {
public Event_7() : base("appointment_nobleHouse1") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Yes, this is House Furia.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the House Furia.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 8
//---------------------------------------------------------------------------------
public class Event_8 : EventBase {
public Event_8() : base("appointment_nobleHouse2") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Yes, this is House Rathmund.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the House Rathmund.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 9
//---------------------------------------------------------------------------------
public class Event_9 : EventBase {
public Event_9() : base("appointment_nobleHouse3") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Yes, this is House Tarquinia.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the House Tarquinia.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 10
//---------------------------------------------------------------------------------
public class Event_10 : EventBase {
public Event_10() : base("appointment_nobleHouse4") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Yes, this is House Valeria.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the House Valeria.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 11
//---------------------------------------------------------------------------------
public class Event_11 : EventBase {
public Event_11() : base("appointment_guild1") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Yes, this is the Union.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the Everlasting Union.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 12
//---------------------------------------------------------------------------------
public class Event_12 : EventBase {
public Event_12() : base("appointment_guild2") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Yes, this is Dacei Family.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the Dacei Family.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 13
//---------------------------------------------------------------------------------
public class Event_13 : EventBase {
public Event_13() : base("appointment_guild3") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Yes, this is Coruna Cartel.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the Coruna Cartel.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 14
//---------------------------------------------------------------------------------
public class Event_14 : EventBase {
public Event_14() : base("appointment_church") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Yes, this is the Church.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the Church.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 15
//---------------------------------------------------------------------------------
public class Event_15 : EventBase {
public Event_15() : base("appointment_heretic") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= getPlayerLocationID();
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
return p;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Yes, this is Radical Movement.";
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
{
return "You meet with the representative of the Radical Movement.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
{
choices.Add("Done",  1);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if ( choice== 1)
{
outcome= 1;
end();
}
}
}
