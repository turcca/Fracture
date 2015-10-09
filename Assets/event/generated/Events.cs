// Events.cs compiled: 17:10:27 07/10/2015
#pragma warning disable 0162, 1717
using System;
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 1
//---------------------------------------------------------------------------------
public class Event_1 : EventBase {
public Event_1() : base("Fracked Crew -knocking") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.QuietEerie;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if ( getWarpMagnitude() >= 1.9 )
{
p *=  2;
}
if ( getWarpMagnitude() < 1 )
{
p *=  0;
}
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("mercantile") )
{
eventAdvice.text = "Well that's weird. Aren't we supposed to find out what this is about?";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("transhumanist") )
{
eventAdvice.text = "It's a contact. Someone is trying to contact us. Any ideas what this is about?";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("liberal") )
{
eventAdvice.text = "If there is a pattern to this knocking, it's a signal. By who? We should look into it.";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("technocrat") )
{
eventAdvice.text = "If there is a pattern to this knocking, it's a signal. Signals we can decode.";
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("cult") )
{
eventAdvice.text = "Fill the ears with prayer and call their minds.";
eventAdvice.recommend =  742;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
{
eventAdvice.text = "This can't be good. Are the sensors operating? What is our security status? Our Flood Shields are up, right?";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "The Flood Shields are more than energy projectors in the hull. \nIt's also the way we open or close the portal we carry in us.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("security") >= 2 )
{
eventAdvice.text = "When things get confused, it is best to have trusted people by key equipment.";
eventAdvice.recommend =  741;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("holiness") >= 1 )
{
eventAdvice.text = "I have a bad feeling about this. Better be safe than sorry while in a Fracture Fall.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "If it's a code, we should be able to crack it.";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getAdvisor(job).getStat("security") <= -1 )
{
eventAdvice.text = "Armed security will only escalate things, and there are lots of tension already. Let's find out if this is a threat before jumping the gun.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Always secure the Core, bridge, navigation equipment, life support and arsenal.";
eventAdvice.recommend =  741;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") < 0 )
{
eventAdvice.text = "The crew is already &shipStatHappiness&, we might be better off giving them something else to chew on.";
eventAdvice.recommend =  742;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("security") >= 1 )
{
eventAdvice.text = "Is everyone going to act strange again? Are they going to do something stupid again?";
eventAdvice.recommend =  741;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("holiness") >= 1 )
{
eventAdvice.text = "It's the Daemons outside. Testing our resolve, trying to claw in. We should prepare for the worst.";
eventAdvice.recommend =  741;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "I wonder what this is about.";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") <= 3 )
{
eventAdvice.text = "If we don't have a long way to go still, we may want to see what this is about.";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("psy") >= 1 && getAdvisor(job).getStat("hr") <= 3 )
{
eventAdvice.text = "I can hear the knocking too. I wonder what it means.";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") >= 2 && getAdvisor(job).getStat("corruption") >= 3 )
{
eventAdvice.text = "Something is out there. We should find more about it, this information can be extremely valuable.";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") >= 4 && getAdvisor(job).getStat("corruption") >= 4 )
{
eventAdvice.text = "Let's see who is knocking on our door this time.";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") < 2 && getAdvisor(job).getStat("corruption") >= 3 )
{
eventAdvice.text = "We must quickly interrogate people to find everything we can about what they have heard.";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "Careful examination may be in order. &No&, this doesn't seem to be an immediate threat.";
eventAdvice.recommend =  740;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Let us pray things won't turn for the worst. Resolve is our best ally.";
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 743)
{
return "Series of data was collected around the ship, and data analysis start running on the ship computer. Wild and anxious speculation about the messages start to revolve around in the ship.";
}
if (outcome== 0)
{
return "There are reports coming from all over the ship. Apparently the crew is hearing knocking coming outside of the hull. Series of knocks are repeated over and over again. Some people insists it's a code. Outside sensors are not showing anything.";
}
if (outcome== 744)
{
return "Security surprised a group of crew members trying to break into the Restricted Core parts. They were armed and fought back in craze, but were overpowered. Some of the crew protested against gunning down a team of engineers, while others shaved their heads and joined the ship's spiritual congregation.";
}
if (outcome== 745)
{
return "The crew enjoy their medication with blissful, calm faces. This is, after all, why some of these people serve on ships.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Try to record the knocks and resolve if there is a pattern to it.",  740);
}
if (outcome== 743)
{
choices.Add("Continue",  748);
}
if (outcome== 0)
{
choices.Add("Send out security to defend high priority targets. Broadcast a warning against listening to the knocking.",  741);
}
if (outcome== 744)
{
choices.Add("Continue",  755);
}
if (outcome== 0)
{
choices.Add("Deal out extra dozes of Angel Meld to the crew.",  742);
}
if (outcome== 745)
{
choices.Add("Continue",  760);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 740)
{
outcome= 743;
}
if (choice== 748)
{
outcome= 749 ;
available=false;
end();
}
if (choice== 741)
{
outcome= 744;
}
if (choice== 755)
{
outcome= 756 ;
available=false;
end();
}
if (choice== 742)
{
outcome= 745;
}
if (choice== 760)
{
outcome= 761 ;
available=false;
end();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 2
//---------------------------------------------------------------------------------
public class Event_2 : EventBase {
public Event_2() : base("Fracked Crew -raves") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if ( getWarpMagnitude() > 1.7 )
{
p *=  2;
}
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
if ( getAdvisor(job).getStat("leadership") >= 4 )
{
eventAdvice.text = "Let's take a clear stand on the issue. Any thoughts?";
return eventAdvice;
}
}
if (job == Character.Job.captain) {
{
eventAdvice.text = "Some of these records are downright creepy. Who listens to this stuff?";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
if ( getAdvisor(job).isIdeology("navigators") )
{
eventAdvice.text = "I've heard things you people wouldn't believe.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "I hear this a lot. You shouldn't be listening to it.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("holiness") >= 1 )
{
eventAdvice.text = "There is something creepy going on.";
eventAdvice.recommend =  767;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("psy") >= 1 )
{
eventAdvice.text = "There is something creepy going on.";
eventAdvice.recommend =  768;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("psy") >= 3 )
{
eventAdvice.text = "Something is going on.";
eventAdvice.recommend =  768;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("security") <= 0 )
{
eventAdvice.text = "It's okay, some of the people down there are my maintainance crews. They are good people and work hard for this ship.";
eventAdvice.recommend =  766;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("security") >= 1 )
{
eventAdvice.text = "Lower decks have no crucial components in them. But alot of rudimentary maintainance is done below, so I want to know my teams are doing ok.";
eventAdvice.recommend =  767;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "Lower decks have no crucial components in them.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getAdvisor(job).getStat("security") >= 0 )
{
eventAdvice.text = "They bathe their minds in chemicals as they whip themselves into some kind of a trance. This needs to stop.";
eventAdvice.recommend =  767;
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "As long as we don't get any complains, I'm okay with it.";
eventAdvice.recommend =  766;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("security") >= 2 )
{
eventAdvice.text = "There's always some fractured shit. Just make them stop before they do something stupid again.";
eventAdvice.recommend =  767;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("holiness") >= 1 )
{
eventAdvice.text = "Unholy music. No-one should be listening to this.";
eventAdvice.recommend =  767;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("psy") >= 1 )
{
eventAdvice.text = "This... music. There is something to it.";
eventAdvice.recommend =  768;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("corruption") >= 4 )
{
eventAdvice.text = "Let them enjoy themselves. Don't take away the rare piece of happiness they can find.";
eventAdvice.recommend =  766;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Holiness") <= -1 )
{
eventAdvice.text = "The crew... they are not doing so well. We may want to control this.";
eventAdvice.recommend =  766;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") <= 0 )
{
eventAdvice.text = "The crew needs this to take their mind of the Fall.";
eventAdvice.recommend =  766;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") <= 3 )
{
eventAdvice.text = "We can carefully monitor the situation.";
eventAdvice.recommend =  768;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "We can watch how things develop. No need to overreact.";
eventAdvice.recommend =  768;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy")== 2 && getAdvisor(job).getStat("corruption") >= 2 )
{
eventAdvice.text = "This is... beautiful. We must hear more.";
eventAdvice.recommend =  768;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") >= 3 && getAdvisor(job).getStat("corruption") >= 3 )
{
eventAdvice.text = "Beautifully orchestrated. For our benefit. We shouldn't miss it.";
eventAdvice.recommend =  768;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") < 2 && getAdvisor(job).getStat("corruption") >= 2 )
{
eventAdvice.text = "Interesting. We have to know more, how does it affect people.";
eventAdvice.recommend =  768;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "We can send a few people to observe. I have a few people in mind.";
eventAdvice.recommend =  768;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getAdvisor(job).getStat("holiness") >= 1 )
{
eventAdvice.text = "These sounds are troubling, and shouldn't be listened to.";
eventAdvice.recommend =  767;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getAdvisor(job).getStat("holiness") >= 2 )
{
eventAdvice.text = "Put an end to the sounds of madness, or there will be dire consequences.";
eventAdvice.recommend =  767;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "We must help those who can not help themselves.";
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 769)
{
return "There is an underground culture forming deep within the ship.";
}
if (outcome== 0)
{
return "Distorted, fractured broadcasts from real space start streaming in through ship comms. People are tapping onto the streams, listening and recording them around the ship. It's even said people are raving to them on the lower levels in illegal parties.";
}
if (outcome== 770)
{
return "The raid turns violent and it takes awhile before it is clear what happened. In the end, some of the crew was shot, others detained. Enforced calm settles over the ship.";
}
if (outcome== 771)
{
return "Data on abnormal behaviour starts to trickle in. The observers are interviewing people and closely listening to the records, really immersing themselves into this project.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Allow it.",  766);
}
if (outcome== 769)
{
choices.Add("Continue",  773);
}
if (outcome== 0)
{
choices.Add("Break the illegal parties up and restrict the records.",  767);
}
if (outcome== 770)
{
choices.Add("Continue",  775);
}
if (outcome== 0)
{
choices.Add("Put a team to study the phenomenom.",  768);
}
if (outcome== 771)
{
choices.Add("Continue",  777);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 766)
{
outcome= 769;
}
if (choice== 773)
{
outcome= 778 ;
available=false;
end();
}
if (choice== 767)
{
outcome= 770;
}
if (choice== 775)
{
outcome= 780 ;
available=false;
end();
}
if (choice== 768)
{
outcome= 771;
}
if (choice== 777)
{
outcome= 779 ;
available=false;
end();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 3
//---------------------------------------------------------------------------------
public class Event_3 : EventBase {
public Event_3() : base("Odd behaviour") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if ( getWarpMagnitude() < 1 )
{
p *=  0;
}
if ( getWarpMagnitude() >= 2 )
{
p *=  2;
}
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Most of the reports come from the engine crew.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
if ( getAdvisor(job).getStat("corruption") >= 40 )
{
eventAdvice.text = "To hear is to listen. Meanwhile, we must go on.";
eventAdvice.recommend =  808;
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
if ( getAdvisor(job).getStat("corruption") >= 70 )
{
eventAdvice.text = "A good crew is attuned to its function. I encourage this.";
eventAdvice.recommend =  807;
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "A distraction.";
eventAdvice.recommend =  808;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("corruption") >= 20 )
{
eventAdvice.text = "These are my people, and I have full control over the matter. In fact, I have some suggestions.";
eventAdvice.recommend =  807;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("holiness") >= 4 )
{
eventAdvice.text = "It's my crew, I know them. I'm not exactly sure what's going on, but the core is running fine and there's no reason to overreact.";
eventAdvice.recommend =  809;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "It's my crew, I know them. I'm not exactly sure what's going on, but the core is running fine and there's no reason to overreact.";
eventAdvice.recommend =  808;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getAdvisor(job).getStat("corruption") >= 80 )
{
eventAdvice.text = "We have established cooperation with the group in question. I believe we can further this understanding.";
eventAdvice.recommend =  807;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getAdvisor(job).getStat("holiness") >= 4 )
{
eventAdvice.text = "Heresy! We must end this now, before it gets out of hand. You can't just go and change the Approved Procedures!";
eventAdvice.recommend =  809;
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "The core is high security area, and there should be very little tolerance towards unregulated activities.";
eventAdvice.recommend =  809;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("corruption") >= 60 )
{
eventAdvice.text = "Such efficiency and dedication.";
eventAdvice.recommend =  807;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("hr") >= 6 )
{
eventAdvice.text = "Interfering with this can demoralize the crew. Any radical reaction could unbalance things.";
eventAdvice.recommend =  808;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "I'm not entirely sure how we should handle this. Perhaps we need to see how this develops before weigh in.";
eventAdvice.recommend =  808;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("corruption") >= 40 )
{
eventAdvice.text = "Interesting. We must participate in this to truly understand it.";
eventAdvice.recommend =  807;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") >= 6 )
{
eventAdvice.text = "There is a possibility of exposure here. I feel we're being influenced.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "I am curious.";
eventAdvice.recommend =  808;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getAdvisor(job).getStat("holiness") >= 2 )
{
eventAdvice.text = "We must stop the spread of these deviations from Approved Procedures. At once.";
eventAdvice.recommend =  809;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Perhaps a silent meditation will bring clarity to the issue.";
eventAdvice.recommend =  808;
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "There are increase in reports of odd behaviour among the crew. The crew are holding meetings on their own and changing the practices and procedures.";
}
if (outcome== 810)
{
return "A strange new hierarchy is emerging within core engineers. It seems like there are ringleaders emerging within the group called 'core singers'. The core stability seems to benefit from this, yet it remains unclear where these new practices emerge from.";
}
if (outcome== 812)
{
return "The security encounters an organized group of workers operating in the core. They had done something to the rigging, but the damage is easily reversed and the ringleaders were apprehended for interrogation.";
}
if (outcome== 811)
{
return "It seems like there is an organized group operating near the engine core. It is unclear what they are doing there or why, but the core efficiency doesn't seem to be affected.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Partake in new practices to understand them better.",  807);
}
if (outcome== 810)
{
choices.Add("Continue",  823);
}
if (outcome== 0)
{
choices.Add("Send in the security. Contain and question everyone involved.",  809);
}
if (outcome== 812)
{
choices.Add("Continue",  825);
}
if (outcome== 0)
{
choices.Add("Do not interfere, but keep an eye on any developments.",  808);
}
if (outcome== 811)
{
choices.Add("Continue",  824);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 807)
{
outcome= 810;
}
if (choice== 823)
{
outcome= 826 ;
available=false;
end();
}
if (choice== 809)
{
outcome= 812;
}
if (choice== 825)
{
outcome= 828 ;
available=false;
end();
}
if (choice== 808)
{
outcome= 811;
}
if (choice== 824)
{
outcome= 827 ;
available=false;
end();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 4
//---------------------------------------------------------------------------------
public class Event_4 : EventBase {
public Event_4() : base("contact_nobleHouse4") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
{
p *=  0;
}
base.lastProbability = p;
return base.lastProbability;
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
return "I am the representative of House Valeria.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("contact_factions");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 5
//---------------------------------------------------------------------------------
public class Event_5 : EventBase {
public Event_5() : base("core singers") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Elevated;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.EngineRoom;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if ( getEvent("Odd behaviour").outcome== 0 )
{
p *=  0;
}
if ( getEvent("Odd behaviour").outcome== 828 )
{
p *=  0;
}
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "The core is functioning well it seems. But is it possible to run a ship with independent departments?";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
if ( getAdvisor(job).getStat("corruption") >= 50 )
{
eventAdvice.text = "As long as the core is in tune.";
eventAdvice.recommend =  838;
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "The core is in tune.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("corruption") >= 30 )
{
eventAdvice.text = "I have things under control. We are actually on to something very interesting.";
eventAdvice.recommend =  838;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getAdvisor(job).getStat("corruption") >= 70 )
{
eventAdvice.text = "There must be a misunderstanding here. We do have access! I am a member after all.";
eventAdvice.recommend =  838;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "This is absurd. I'm going to need access to my core.";
eventAdvice.recommend =  839;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getAdvisor(job).getStat("corruption") >= 80 )
{
eventAdvice.text = "I have infiltrated the group. There is no reason to interfere yet, we can control the situation if need arises.";
eventAdvice.recommend =  838;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getAdvisor(job).getStat("corruption") >= 50 )
{
eventAdvice.text = "I am not convinced this is a threat to the ship security. It's a matter of preference, really.";
eventAdvice.recommend =  838;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") >= 1 && getAdvisor(job).getStat("leadership") >= 4 )
{
eventAdvice.text = "I trust my men. You can count on us.";
eventAdvice.recommend =  840;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") >= 1 )
{
eventAdvice.text = "It may be difficult to take the core, if the defenders are determined.";
eventAdvice.recommend =  839;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") >= 1 && getAdvisor(job).getStat("combat") >= 5 )
{
eventAdvice.text = "I can take them.";
eventAdvice.recommend =  840;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") >= 1 && getAdvisor(job).getStat("security") >= 3 )
{
eventAdvice.text = "The core can be easily defended, but our security can handle this.";
eventAdvice.recommend =  840;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") < 1 && getAdvisor(job).getStat("leadership") >= 4 )
{
eventAdvice.text = "If the resistance is determined at the core, our security forces may not be able to handle it. I need more men!";
eventAdvice.recommend =  839;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") < 1 && getAdvisor(job).getStat("security") >= 3 )
{
eventAdvice.text = "The core can be easily defended. Our security forces may have a hard time enforcing this. This is why we need better security on the ship!";
eventAdvice.recommend =  839;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") < 1 )
{
eventAdvice.text = "It may be difficult to take the core, if the defenders are determined.";
eventAdvice.recommend =  839;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") >= 1 && getAdvisor(job).getStat("hr") >= 4 )
{
eventAdvice.text = "The situation on the ship is well at hand. This is negotiable.";
eventAdvice.recommend =  839;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") >= 1 && getAdvisor(job).getStat("hr") < 4 && getAdvisor(job).getStat("hr") < 4 )
{
eventAdvice.text = "If this doesn't interfere with the ship's procedures, perhaps we should allow this. The crew is handing this well.";
eventAdvice.recommend =  838;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") >= 1 && getAdvisor(job).getStat("hr") < 4 && getAdvisor(job).isIdeology("cult") )
{
eventAdvice.text = "Who knows what is going on in there? This isn't right, we have to get in there.";
eventAdvice.recommend =  840;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("hr") >= 5 )
{
eventAdvice.text = "The chain of command is breaking up. These so called 'core singers' are not taking orders from our leadership anymore.";
eventAdvice.recommend =  839;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getAdvisor(job).getStat("corruption") >= 40 )
{
eventAdvice.text = "As long as the group keeps operating the core this well, there is no need to interfere.";
eventAdvice.recommend =  838;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") < 1 && getAdvisor(job).getStat("hr") >= 5 )
{
eventAdvice.text = "Things are tense in the core. We may not have the leverage to make demands, the group has grown in numbers.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") < 1 && getAdvisor(job).getStat("hr") < 5 )
{
eventAdvice.text = "Things are tense in the core, and more people are now joining the group.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") >= 7 )
{
eventAdvice.text = "The core has given birth to these so called 'core singers'. Giving birth to a mind, but not from nothingness. From the other side. Do you understand what I'm saying?";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("corruption") >= 40 )
{
eventAdvice.text = "I have been in contact to these 'core singers' and I am intrigued. They may have something to contribute.";
eventAdvice.recommend =  838;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("corruption") >= 80 )
{
eventAdvice.text = "I have met these 'core singers' myself. I believe they have a role to play in the future.";
eventAdvice.recommend =  838;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") >= 5 )
{
eventAdvice.text = "These 'core singers' are more than what they appear. We all wear masks, but not like these. Not like these.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") >= 3 )
{
eventAdvice.text = "There is something going on here. At the core. A song.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "Isn't this a matter for the security?";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getAdvisor(job).getStat("security") < -1 )
{
eventAdvice.text = "The core contains some of the great secrets of the past. We should not reveal them so easily.";
eventAdvice.recommend =  839;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getShipStat("Holiness") >= 3 )
{
eventAdvice.text = "No heretics can take our holy ship! I will lead our people to the path of righteousness.";
eventAdvice.recommend =  840;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "The core is a sacred place, and must be under our direct control!";
eventAdvice.recommend =  840;
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 843)
{
return "The 'core singers' are established as a group with some autonomy in the core. They continue to serve the ship well while having an impact on practices and beliefs of the crew.";
}
if (outcome== 0)
{
return "Newly formed group of engine crew living near the core continue to operate by their own practices. Most recently they have restricted the access to the core from nonmembers.";
}
if (outcome== 844)
{
return "After quick negations, it became clear that the boundaries set by the group are strict, and their leaders the 'core singers' assume a position of autonomy in the core.";
}
if (outcome== 859)
{
return "The security encountered resistance at the core that escalated into a fight in the narrow corridors leading to the core. The defenders were prepared, but the dedicated security forces routed the resistance. In a fierce final stand, the 'core singers' were killed. After that, the involved crew quickly submitted to disciplinary actions and the crew rotated to restore security to the core.";
}
if (outcome== 884)
{
return "The security encountered resistance at the core that escalated into fighting in the narrow corridors leading to the core. The defenders were prepared, and the ship's security forces were unable to reach the core. As the stalemate and high tensions aboard continued, the group declared themselves the 'core singers' and responsible for the core operations.";
}
if (outcome== 860)
{
return "The 'core singers' are established as a group with some autonomy in the core. They continue to serve the ship well while having an impact on practices and beliefs of the crew.";
}
if (outcome== 842)
{
return "After quick negotiations it is agreed that key security and engineering personnel are allowed access to the core. It will otherwise remain under the tutelage of the 'core singers', the leaders of the group.";
}
if (outcome== 841)
{
return "The security encountered resistance at the core. But soon after, a devout of the Cult started singing from the Holy Scriptures of the Church and it picked up until the entire engine section was singing it. 'Core singers' disappeared soon after and the core resumed its proper state.";
}
if (outcome== 867)
{
return "The security encountered resistance at the core that escalated into fighting in the narrow corridors leading to the core. The ship's security forces were unable to reach the core. As the stalemate and high tensions aboard continued, the group declared themselves the 'core singers' and responsible for the core operations.";
}
if (outcome== 845)
{
return "The security encountered resistance at the core that escalated into a fight in the narrow corridors leading to the core. The ship's security forces were motivated and routed the resistance. In a fierce final stand, the 'core singers' were killed. After that, the involved crew quickly submitted to disciplinary actions and the crew rotated to restore security to the core.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Do not interfere.",  838);
}
if (outcome== 843)
{
choices.Add("Continue",  857);
}
if (outcome== 0)
{
choices.Add("Demand access to the core for key personnel.",  839);
}
if (outcome== 844)
{
choices.Add("Send in the security to reclaim the core.",  858);
}
if (outcome== 859)
{
choices.Add("Continue",  864);
}
if (outcome== 884)
{
choices.Add("Continue",  888);
}
if (outcome== 844)
{
choices.Add("Appoint the 'core singers' an official position on the core.",  851);
}
if (outcome== 860)
{
choices.Add("Continue",  862);
}
if (outcome== 842)
{
choices.Add("Continue",  850);
}
if (outcome== 0)
{
choices.Add("Send in the security to reclaim the core.",  840);
}
if (outcome== 841)
{
choices.Add("Praise the Church!",  870);
}
if (outcome== 867)
{
choices.Add("Continue",  880);
}
if (outcome== 845)
{
choices.Add("Continue",  875);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 838)
{
outcome= 843;
}
if (choice== 857)
{
outcome= 893 ;
available=false;
end();
}
if (choice== 839 && getShipStat("Happiness") < 1)
{
outcome= 844;
}
if (choice== 858 && getShipStat("Morale") >= 2)
{
outcome= 859;
}
if (choice== 864)
{
outcome= 865 ;
available=false;
end();
}
if (choice== 858 && getShipStat("Morale") < 2)
{
outcome= 884;
}
if (choice== 888)
{
outcome= 890 ;
available=false;
end();
}
if (choice== 851)
{
outcome= 860;
}
if (choice== 862)
{
outcome= 866 ;
available=false;
end();
}
if (choice== 839 && getShipStat("Happiness") >= 1 || getShipStat("Holiness") >= 3)
{
outcome= 842;
}
if (choice== 850)
{
outcome= 852 ;
available=false;
end();
}
if (choice== 840)
{
outcome= 889;
}
if (choice== 840 && getShipStat("Holiness") >= 3)
{
outcome= 841;
}
if (choice== 870)
{
outcome= 871 ;
available=false;
end();
}
if (choice== 840 && getShipStat("Morale") < 1)
{
outcome= 867;
}
if (choice== 880)
{
outcome= 881 ;
available=false;
end();
}
if (choice== 840 && getShipStat("Morale") >= 1)
{
outcome= 845;
}
if (choice== 875)
{
outcome= 876 ;
available=false;
end();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 6
//---------------------------------------------------------------------------------
public class Event_6 : EventBase {
public Event_6() : base("default_advice") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if ( getElapsedDays() >= 0 )
{
p *=  0;
}
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Any suggestions?";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "This doesn't concern me.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "I can't fix this.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Nothing to report.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "We need to know more.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "Hmm.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "I sahould pray for guidance.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 7
//---------------------------------------------------------------------------------
public class Event_7 : EventBase {
public Event_7() : base("intro Valeria1t") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C07";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("aristocrat") )
{
eventAdvice.text = "This can get us some recognition. And it's a strong statement. Consul Regulus has come from the sector Capital to succeed Lord Calius Valeria. Planetary governor Evander is the other nominee for the position.";
eventAdvice.recommend =  708;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("liberal") )
{
eventAdvice.text = "Consul Regulus has come from the sector Capital to succeed Lord Calius Valeria. Planetary governor Evander is the other nominee for the position. If we join this chest-thumping, it will bring attention to us.";
eventAdvice.recommend =  709;
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "Many eyes are upon us. We should consider this carefully.";
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "Consul Regulus Valeria asks Valerian captains in orbit to publicly pledge their protection of Valerian colonies, and to keep the Dacei from interloping in Valerian interests.";
}
if (outcome== 710)
{
return "Consul Regulus and several Valerian captains appear in public with a strong statement. Dacei envoy responds by calling it an aggression. Among the captains, Regulus asks if anyone have the guts to poke around in Dacei shipping lines to see what they are doing. He'd be interested in the content of their transports.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Join the pledge.",  708);
}
if (outcome== 710)
{
choices.Add("Continue.",  712);
}
if (outcome== 0)
{
choices.Add("Stay out of it.",  709);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 708)
{
outcome= 710;
}
if (choice== 712)
{
outcome= 713 ;
available=false;
end();
}
if (choice== 709)
{
outcome= 716 ;
available=false;
end();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("intro_Valeria");
addFilter("intro");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 8
//---------------------------------------------------------------------------------
public class Event_8 : EventBase {
public Event_8() : base("intro Valeria2t") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C07";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if ( getEvent("intro Valeria1t").outcome== 0 || getEvent("intro Valeria1t").outcome== 713 )
{
p *=  0;
}
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("aristocrat") )
{
eventAdvice.text = "Exarch Evander is the local planetary governor, and a nominee for the position of Lord House Valeria. Official dinners are political manouvering.";
eventAdvice.recommend =  722;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("liberal") )
{
eventAdvice.text = "Exarch Evander is the local planetary governor, and a nominee for the position of Lord House Valeria. Exarch Evander is popular, and his attention would reflect well on us.";
eventAdvice.recommend =  721;
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "The symmetry of politics. We are being weighted.";
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "Planetary governor Evander contacts the ship and commends the captain for staying out of internal politics. He invites you to an official dinner to discuss local matters.";
}
if (outcome== 726)
{
return "Exarch Evander hosts several Valerian captains and members of the Guiding Council. The event is broadcasted planetwide repeatedly over the next few days. In private, Evander asks you to help the liberal movement here and in the Sovereign Void, to counter Dacei interests from spreading. But he discourages direct confrontation, as Dacei are well prepared for aggression.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Go to the official dinner.",  721);
}
if (outcome== 726)
{
choices.Add("Continue.",  728);
}
if (outcome== 0)
{
choices.Add("Excuse yourself.",  722);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 721)
{
outcome= 726;
}
if (choice== 728)
{
outcome= 729 ;
available=false;
end();
}
if (choice== 722)
{
outcome= 723 ;
available=false;
end();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("intro_Valeria");
addFilter("intro");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 9
//---------------------------------------------------------------------------------
public class Event_9 : EventBase {
public Event_9() : base("intro Valeria3t") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C07";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
if ( getEvent("intro Valeria2t").outcome== 0 || getEvent("intro Valeria2t").outcome== 729 )
{
p *=  0;
}
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Lord Calius is one of the most powerful persons in the sector, but it may change soon.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "In whispers, Calius is called a powerful fracture bender.";
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "Incoming private message:\n\n'Artifacts of the grey past are being transported in the Sovereign Void. You might be the one who brings one for me.'            \n\n-Lord Calius Valeria";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Continue.",  734);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 734)
{
outcome= 735 ;
available=false;
end();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("intro_Valeria");
addFilter("intro");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 10
//---------------------------------------------------------------------------------
public class Event_10 : EventBase {
public Event_10() : base("loc_advice") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "+location.name+ reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "+location.name+ reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "+location.name+ reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "+location.name+ reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "+location.name+ reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "+location.name+ reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "+location.name+ reverts to default comment";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 11
//---------------------------------------------------------------------------------
public class Event_11 : EventBase {
public Event_11() : base("loc_advice_1C01") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C01";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "New Dirae is the capital of this subsector. There are many embassies housed in the Old Furian palaces.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "A long time ago, this world seeded the whole sector. The Navigators Spire is the tallest building in the Old Capital. And below, there is something else...";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "It's said this world has a hollow core, but the ship instruments show sprawling underground structures of planetary-scale. A lot of old tech is dug up around here.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "There is a sizable fleet at the station. But even with large shipyards, there are nothing really fancy here.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "This is one of the big worlds, the Old Capital itself is 300 million people. We can probably find specialists here.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") >= 4 )
{
eventAdvice.text = "We're being watched by several groups down at the planet. Buildings on top of buildings, societies behind societies. All this going back a long, long time.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).getStat("psy") < 4 )
{
eventAdvice.text = "Someone down at the planet is watching us. Or someones.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Down there is a tomb of old technology. Tombs should not be disturbed.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 12
//---------------------------------------------------------------------------------
public class Event_12 : EventBase {
public Event_12() : base("loc_advice_1C02") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C02";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "The Arch. The greatest achievement of mankind. Built in the Age of Fracture back at Second Earth, it's been here for over 400 years. How many Archs were built?";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "Orion Bridge, Perseus Transit. Most Fracture Cores originally come from here. Even ours.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "Nobody understands the Arch anymore, but there are the Caretakers who maintain it, and are responsible for the Beacon. When it broke a 100 years ago, we lost contact to the Imperium and were left here to our own devices.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "You can find no better technology in the sector. If you have the money, you can fit anything to your ships in the Arch. The shipyards are huge and can even dock a Ship of the Line.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "There are 300.000 people living on The Arch, which is more than any other space station can handle. Some of the best minds of the sector are here. There is a pull to it, gravitas. Can you feel it?";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "You can't see the Caretakers on the station, they live in separated sections on the Arch. But I think I can feel them. I can feel someone, deep inside the Arch.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "The Blessed Arch. After the Caretakers failed at their task with the Beacon, the Church has been negotiating to share in the responsibility. It feels wrong to have these... these technicians handling this sacred task.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 13
//---------------------------------------------------------------------------------
public class Event_13 : EventBase {
public Event_13() : base("loc_advice_1C03") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C03";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "This world is doing pretty well. A good supply world.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "Great Navigators come from Magaera. Lots of trade ships and skilled crew.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "Magaera is a volcanic world pushing up minerals and energy for the factories. You can get pretty good trades in here, importing food and exporting industrial goods.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Small industrial world that doesn't build ships and arms. It needs to rely on the protection of a powerful faction. Maybe that was the intention?";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "Smart money is on Magaera. You can find all kinds of talents around here.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getAdvisor(job).isIdeology("transhumanist") )
{
eventAdvice.text = "Free minds at work. So industrious. So innovative. My brothers, so many of them.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "Living at the edges of High Fracture leaves its mark. It's a good thing there is a strong order of the Brotherhood here.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Corruption. The kind that spoils both body and mind. We should aid the Cult and the Imperialists here.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 14
//---------------------------------------------------------------------------------
public class Event_14 : EventBase {
public Event_14() : base("loc_advice_1C04") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Primitive worlds are abandoned to their own devices. In this case, it all started with an orbital bombardment. They can still produce food in the Southern continent and it is traded out through the orbiting station.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "Insignificant rubble. If not for the food, why are we here?";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "What they really need is education. However, many of these primitive worlds have descended from reason into superstition and it would take a real effort to make that happen.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Some of these primitive worlds have societies with true warrior culture. Utterly fearless, quick to learn. I could beef up my security.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "You can find fierce warriors in these feral worlds. Hard conditions breed hard men. Should we get some?";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "The scars run deep here, and not all have forgotten what was done to them. Few of them look up at the stars anymore. Not like they used to.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "The Church has monasteries in many of these planets, teaching people the right way to live. But it is hard sometimes and many of them live in the darkness.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 15
//---------------------------------------------------------------------------------
public class Event_15 : EventBase {
public Event_15() : base("loc_advice_1C05") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C05";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
{
p *=  0;
}
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "You can still see the scars from the orbit. And the carcasses of huge ships, now rusting skeletons half buried in the sand.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "The upper orbit still glitters like tears from the weeping eyes gone blind.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "A graveyard of ships. Let's have a look what they've dug up.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "War stories from generations to generations. The Fall of Tisiphone is well remembered.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "Some things you pick from the junkyard, others not so much.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "Death is near, beneath these freezing stars. Life clinging to its corpse like the drowning.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "A site of great victory, always a place of reflection. This is where the spine of Old Furia was crushed.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 16
//---------------------------------------------------------------------------------
public class Event_16 : EventBase {
public Event_16() : base("loc_advice_1C06") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Primitive worlds are abandoned to their own devices. They still produce food that is traded out through the orbiting station. In this case, mycoprotein.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "What are we doing here again?";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "They actually needed only atmo terraformers in this world, since it already had large biomass. Much of it is converted to imported funguses now. It looks pretty grim.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Myco fracking fusarium. It's those yellow foil-wrapped compressed meal-bars that taste like shrooms and old socks. And we're here to buy millions of them, aren't we?";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "There are always people trying to get out of Creon.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "I don't know. I'm not sure, I mean.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Everyone has their place, and this is an important part of the Cradle of the Furies. There is something odd about this place though.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 17
//---------------------------------------------------------------------------------
public class Event_17 : EventBase {
public Event_17() : base("loc_advice_1C07") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C07";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "Valerian capital world. You only ever get a glimpse of it.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "Valerian Navigators are trained here, at the Academy. They are very good.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "I wouldn't mind getting staffed here.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Not much in the sense of military. Academists have little use for weapons, it seems. Hope it won't backfire into their ivory towers.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "The Academy supplies skilled specialists sought throughout the sector.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "Beyond the Horizon has many great thinkers. And the smartest of them are thinking quietly.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "It shouldn't be said of one of the great Noble Houses of the Imperium, but Valerians have always made me question how did they pull it off.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 18
//---------------------------------------------------------------------------------
public class Event_18 : EventBase {
public Event_18() : base("loc_advice_1C08") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C08";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("transhumanist") )
{
eventAdvice.text = "They are hiding here from the world. And why not? They are doing well.";
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getAdvisor(job).isIdeology("nationalist") )
{
eventAdvice.text = "Tiny population, hiding from the rest. And they seem well off. It feels so right.";
return eventAdvice;
}
}
if (job == Character.Job.captain) {
{
eventAdvice.text = "Tiny population, hiding from the rest of humanity. It seems so strange.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "Here in the fold. I found them, even with their beacon turned off.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "They cut the fracture beacon on that station. And those docked ships probably fly dark in and out. I wonder how they find home.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "They don't want to be found. We must thread carefully here.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "They don't want anyone to know. They won't let anyone leave either. I wonder if that includes us now.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "There are people on that station, trying to hide an entire planet. Every day, working to mask these gardens from the seeking eyes.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Captain, I implore you to reveal this location to the Authority. These people are being kept from the light of truth.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 19
//---------------------------------------------------------------------------------
public class Event_19 : EventBase {
public Event_19() : base("loc_advice_1C09") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1C09";
}
//------------------------------------------------------- FREQUENCY AND AMBIENT
public override freq getFrequency() {
return freq.Default;
}
public override status getCrewStatus() {
return status.Default;
}
public override noise getAmbientNoise() {
return noise.DefaultBridge;
}
//------------------------------------------------------- PROBABILITY
public override float calculateProbability() {
float p = 1.0f;
base.lastProbability = p;
return base.lastProbability;
}
//------------------------------------------------------- ADVICE
public override EventAdvice getAdvice(Character.Job job) {
EventAdvice eventAdvice = new EventAdvice();
if (job == Character.Job.captain) {
{
eventAdvice.text = "A world robbed of function. And yet, old routines are maintained generations after generations.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "The Mortuary is a lone world at the edge of the sector. The path here is little travelled, a mere thread running in the Fracture. Sicent oblivion, a fitting fate.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Grave robbers can be resourceful and in possession of all kinds of old technology. We should keep an eye on who we pick up here.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "A solemn place.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "I wonder if the rumours are true. I can't feel anything below, it's all quiet.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "It is a holy place, even if they are far off and alone. Perhaps that is why they have avoided temptation.";
return eventAdvice;
}
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
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("LOC_advice");
}
}
