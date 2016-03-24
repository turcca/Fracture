// Events.cs compiled: 15:08:25 23/03/2016
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
if ( getCharacter(job).isIdeology("mercantile") )
{
eventAdvice.text = "Well that's weird. Aren't we supposed to find out what this is about?";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getCharacter(job).isIdeology("transhumanist") )
{
eventAdvice.text = "It's a contact. Someone is trying to contact us. Any ideas what this is about?";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getCharacter(job).isIdeology("liberal") )
{
eventAdvice.text = "If there is a pattern to this knocking, it's a signal. By who? We should look into it.";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getCharacter(job).isIdeology("technocrat") )
{
eventAdvice.text = "If there is a pattern to this knocking, it's a signal. Signals we can decode.";
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getCharacter(job).isIdeology("cult") )
{
eventAdvice.text = "Fill the ears with prayer and call their minds.";
eventAdvice.recommend =  742
;
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
if ( getCharacter(job).getStat("security") >=  175  )
{
eventAdvice.text = "When things get confused, it is best to have trusted people by key equipment.";
eventAdvice.recommend =  741
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("holiness") >=  150  )
{
eventAdvice.text = "I have a bad feeling about this. Better be safe than sorry while in a Fracture Fall.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "If it's a code, we should be able to crack it.";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).getStat("security") <=  -100  )
{
eventAdvice.text = "Armed security will only escalate things, and there are lots of tension already. Let's find out if this is a threat before jumping the gun.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Always secure the Core, bridge, navigation equipment, life support and arsenal.";
eventAdvice.recommend =  741
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") < 0 )
{
eventAdvice.text = "The crew is already &shipStatHappiness&, we might be better off giving them something else to chew on.";
eventAdvice.recommend =  742
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("security") >=  175  )
{
eventAdvice.text = "Is everyone going to act strange again? Are they going to do something stupid again?";
eventAdvice.recommend =  741
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("holiness") >=  150  )
{
eventAdvice.text = "It's the Daemons outside. Testing our resolve, trying to claw in. We should prepare for the worst.";
eventAdvice.recommend =  741
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "I wonder what this is about.";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") <= 3 )
{
eventAdvice.text = "If we don't have a long way to go still, we may want to see what this is about.";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("psy") >=  150  && getCharacter(job).getStat("hr") <=  150  )
{
eventAdvice.text = "I can hear the knocking too. I wonder what it means.";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  200  && getCharacter(job).getStat("corruption") >=  200  )
{
eventAdvice.text = "Something is out there. We should find more about it, this information can be extremely valuable.";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  300  && getCharacter(job).getStat("corruption") >=  300  )
{
eventAdvice.text = "Let's see who is knocking on our door this time.";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") <  200  && getCharacter(job).getStat("corruption") >=  200  )
{
eventAdvice.text = "We must quickly interrogate people to find everything we can about what they have heard.";
eventAdvice.recommend =  740
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "Careful examination may be in order. &No&, this doesn't seem to be an immediate threat.";
eventAdvice.recommend =  740
;
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
choices.Add("Try to record the knocks and resolve if there is a pattern to it.",  740
);
}
if (outcome== 743)
{
choices.Add("Continue",  748
);
}
if (outcome== 0)
{
choices.Add("Send out security to defend high priority targets. Broadcast a warning against listening to the knocking.",  741
);
}
if (outcome== 744)
{
choices.Add("Continue",  755
);
}
if (outcome== 0)
{
choices.Add("Deal out extra dozes of Angel Meld to the crew.",  742
);
}
if (outcome== 745)
{
choices.Add("Continue",  760
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 740)
{
outcome= 743
;
}
if (choice== 748)
{
outcome= 749 ;
available=false;
end();
}
if (choice== 741)
{
outcome= 744
;
}
if (choice== 755)
{
outcome= 756 ;
available=false;
end();
}
if (choice== 742)
{
outcome= 745
;
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
if ( getCharacter(job).getStat("leadership") >=  250  )
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
if ( getCharacter(job).isIdeology("navigators") )
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
if ( getCharacter(job).getStat("holiness") >=  150  )
{
eventAdvice.text = "There is something creepy going on.";
eventAdvice.recommend =  767
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("psy") >=  150  )
{
eventAdvice.text = "There is something creepy going on.";
eventAdvice.recommend =  768
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("psy") >=  250  )
{
eventAdvice.text = "Something is going on.";
eventAdvice.recommend =  768
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("security") <=  0  )
{
eventAdvice.text = "It's okay, some of the people down there are my maintainance crews. They are good people and work hard for this ship.";
eventAdvice.recommend =  766
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("security") >=  175  )
{
eventAdvice.text = "Lower decks have no crucial components in them. But alot of rudimentary maintainance is done below, so I want to know my teams are doing ok.";
eventAdvice.recommend =  767
;
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
if ( getCharacter(job).getStat("security") >=  0  )
{
eventAdvice.text = "They bathe their minds in chemicals as they whip themselves into some kind of a trance. This needs to stop.";
eventAdvice.recommend =  767
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "As long as we don't get any complains, I'm okay with it.";
eventAdvice.recommend =  766
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("security") >=  300  )
{
eventAdvice.text = "There's always some fractured shit. Just make them stop before they do something stupid again.";
eventAdvice.recommend =  767
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("holiness") >=  150  )
{
eventAdvice.text = "Unholy music. No-one should be listening to this.";
eventAdvice.recommend =  767
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("psy") >=  150  )
{
eventAdvice.text = "This... music. There is something to it.";
eventAdvice.recommend =  768
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("corruption") >=  50  )
{
eventAdvice.text = "Let them enjoy themselves. Don't take away the rare piece of happiness they can find.";
eventAdvice.recommend =  766
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Holiness") <= -1 )
{
eventAdvice.text = "The crew... they are not doing so well. We may want to control this.";
eventAdvice.recommend =  766
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") <= 0 )
{
eventAdvice.text = "The crew needs this to take their mind of the Fall.";
eventAdvice.recommend =  766
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") <= 3 )
{
eventAdvice.text = "We can carefully monitor the situation.";
eventAdvice.recommend =  768
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "We can watch how things develop. No need to overreact.";
eventAdvice.recommend =  768
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy")==  200  && getCharacter(job).getStat("corruption") >=  200  )
{
eventAdvice.text = "This is... beautiful. We must hear more.";
eventAdvice.recommend =  768
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  250  && getCharacter(job).getStat("corruption") >=  250  )
{
eventAdvice.text = "Beautifully orchestrated. For our benefit. We shouldn't miss it.";
eventAdvice.recommend =  768
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") <  200  && getCharacter(job).getStat("corruption") >=  200  )
{
eventAdvice.text = "Interesting. We have to know more, how does it affect people.";
eventAdvice.recommend =  768
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "I have a few people in mind.";
eventAdvice.recommend =  768
;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getCharacter(job).getStat("holiness") >=  150  )
{
eventAdvice.text = "These sounds are troubling, and shouldn't be listened to.";
eventAdvice.recommend =  767
;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getCharacter(job).getStat("holiness") >=  300  )
{
eventAdvice.text = "Put an end to the sounds of madness, or there will be dire consequences.";
eventAdvice.recommend =  767
;
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
choices.Add("Allow it.",  766
);
}
if (outcome== 769)
{
choices.Add("Continue",  773
);
}
if (outcome== 0)
{
choices.Add("Break the illegal parties up and restrict the records.",  767
);
}
if (outcome== 770)
{
choices.Add("Continue",  775
);
}
if (outcome== 0)
{
choices.Add("Put a team to study the phenomenom.",  768
);
}
if (outcome== 771)
{
choices.Add("Continue",  777
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 766)
{
outcome= 769
;
}
if (choice== 773)
{
outcome= 778 ;
available=false;
end();
}
if (choice== 767)
{
outcome= 770
;
}
if (choice== 775)
{
outcome= 780 ;
available=false;
end();
}
if (choice== 768)
{
outcome= 771
;
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
if ( getWarpMagnitude() >= 2 )
{
p *=  2;
}
if ( getShipStat("Fracture") >= 5 )
{
p *=  0;
}
if ( getShipStat("Holiness") <= -1 )
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
if ( getCharacter(job).getStat("corruption") >=  40  )
{
eventAdvice.text = "To hear is to listen. Meanwhile, we must go on.";
eventAdvice.recommend =  808
;
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
if ( getCharacter(job).getStat("corruption") >=  70  )
{
eventAdvice.text = "A good crew is attuned to its function. I encourage this.";
eventAdvice.recommend =  807
;
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "A distraction.";
eventAdvice.recommend =  808
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("corruption") >=  20  )
{
eventAdvice.text = "These are my people, and I have full control over the matter. In fact, I have some suggestions.";
eventAdvice.recommend =  807
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("holiness") >=  300  )
{
eventAdvice.text = "It's my crew, I know them. I'm not exactly sure what's going on, but the core is running fine and there's no reason to overreact.";
eventAdvice.recommend =  809
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "It's my crew, I know them. I'm not exactly sure what's going on, but the core is running fine and there's no reason to overreact.";
eventAdvice.recommend =  808
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).getStat("corruption") >=  80  )
{
eventAdvice.text = "We have established cooperation with the group in question. I believe we can further this understanding.";
eventAdvice.recommend =  807
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).getStat("holiness") >=  300  )
{
eventAdvice.text = "Heresy! We must end this now, before it gets out of hand. You can't just go and change the Approved Procedures!";
eventAdvice.recommend =  809
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "The core is high security area, and there should be very little tolerance towards unregulated activities.";
eventAdvice.recommend =  809
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("corruption") >=  60  )
{
eventAdvice.text = "Such efficiency and dedication.";
eventAdvice.recommend =  807
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("hr") >=  150  )
{
eventAdvice.text = "Interfering with this can demoralize the crew. Any radical reaction could unbalance things.";
eventAdvice.recommend =  808
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "I'm not entirely sure how we should handle this. Perhaps we need to see how this develops before weigh in.";
eventAdvice.recommend =  808
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("corruption") >=  40  )
{
eventAdvice.text = "Interesting. We must participate in this to truly understand it.";
eventAdvice.recommend =  807
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  300  )
{
eventAdvice.text = "There is a possibility of exposure here. I feel we're being influenced.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "I am curious.";
eventAdvice.recommend =  808
;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getCharacter(job).getStat("holiness") >=  300  )
{
eventAdvice.text = "We must stop the spread of these deviations from Approved Procedures. At once.";
eventAdvice.recommend =  809
;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Perhaps a silent meditation will bring clarity to the issue.";
eventAdvice.recommend =  808
;
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
choices.Add("Partake in new practices to understand them better.",  807
);
}
if (outcome== 810)
{
choices.Add("Continue",  823
);
}
if (outcome== 0)
{
choices.Add("Send in the security. Contain and question everyone involved.",  809
);
}
if (outcome== 812)
{
choices.Add("Continue",  825
);
}
if (outcome== 0)
{
choices.Add("Do not interfere, but keep an eye on any developments.",  808
);
}
if (outcome== 811)
{
choices.Add("Continue",  824
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 807)
{
outcome= 810
;
}
if (choice== 823)
{
outcome= 826 ;
available=false;
end();
}
if (choice== 809)
{
outcome= 812
;
}
if (choice== 825)
{
outcome= 828 ;
available=false;
end();
}
if (choice== 808)
{
outcome= 811
;
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
public Event_4() : base("[at] 1c02 arch beacon") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c02"
;
triggerEvent=trigger.atLocation;
locationRequired=true;
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
if ( getEvent("loc_advice_1C02").outcome== 0 )
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
if ( getCharacter(job).getStat("navigation") >=  150  )
{
eventAdvice.text = "The Arch.";
return eventAdvice;
}
}
if (job == Character.Job.captain) {
{
eventAdvice.text = "We are by the Arch now, aren't we?";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
if ( getCharacter(job).getStat("navigation") >=  250  )
{
eventAdvice.text = "The Arch beacon, it is our lifeline on this sector. It used to connect this sector to Orion sectors, but no longer.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "The Arch beacon can be located most of the time everywhere in the sector.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("engineering") >=  100  )
{
eventAdvice.text = "It is a wonder, how we used to build these technological marvels that can penetrate even the heart of the fracture.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "I can't imagine what goes into building a fracture beacon like this.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "Are we stopping here? I'll have to draw crew recreation rotation.";
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "There is an extremely strong beacon here, at the other side of the fracture.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Continue.",  981
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 981)
{
outcome= 982 ;
available=false;
end();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 5
//---------------------------------------------------------------------------------
public class Event_5 : EventBase {
public Event_5() : base("[at] 1c08 first found") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c08"
;
triggerEvent=trigger.atLocation;
locationRequired=true;
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
if ( getEvent("loc_advice_1C08").outcome== 0 )
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
eventAdvice.text = "If they have turned their beacon off by purpose, they do not want to be found. Will they let us leave, once we have entered?";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
if ( getCharacter(job).getStat("navigation") >=  250  )
{
eventAdvice.text = "This colony is not on any Imperial navigation charts, but I have acquired records of the Old Furia. I believe there are matching references to a small resort called Gardens of Augustus.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "This colony is not on any Imperial navigation charts. I had no idea there was something so far out here.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "Fracture beacons are lighthouses and landmarks for ships in the fracture. Some worlds have been unable to maintain them, and so they fall off the grid.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "We are taking an awful risk by even getting this close to it.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "There is always quite a lot of tension when encountering worlds without active beacons. It is not unheard of for ships to be seized by places like this.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  300  && getCharacter(job).getStat("precognition") >=  300  )
{
eventAdvice.text = "It's a small world, but they have developed strong affinity with the fracture. I believe they are hiding.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  250  )
{
eventAdvice.text = "It's a small world, but they have developed strong affinity with the fracture.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "So, uh. What we have here is a colony that is not shown on the map.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  150  )
{
eventAdvice.text = "It's a small colony.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Lost colonies of man. They need to be brought back into the fold. Enlightenment awaits!";
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "There is a colony here, but there is no active fracture beacon.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Continue",  919
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 919)
{
outcome= 920 ;
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
public Event_6() : base("[in] 1c08 first enter") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c08"
;
triggerEvent=trigger.inLocation;
locationRequired=true;
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
if ( getEvent("[at] 1c08 first found").outcome== 0 )
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
if ( getCharacter(job).getStat("holiness") >=  300  )
{
eventAdvice.text = "It is our duty to find out about this place.";
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getCharacter(job).getStat("diplomat") >=  250  )
{
eventAdvice.text = "Let's try to negotiate a safe passage. It would be good to have extra trading partner and a haven out here.";
eventAdvice.recommend =  925
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getCharacter(job).getStat("spaceBattle") >=  120  )
{
eventAdvice.text = "It's a precarious situation at best. We should play it safe and get out while we can.";
eventAdvice.recommend =  926
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
{
eventAdvice.text = "Is it an automated message? Maybe we should try to contact them.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
if ( getCharacter(job).getStat("navigation") >=  200  )
{
eventAdvice.text = "This must be the Gardens of Augustus.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "No beacon, but here they are.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("engineering") >=  50  )
{
eventAdvice.text = "If we want to leave, we need to start the prep ASAP.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "It will take a moment for our sensors to adjust from the shift to normal space. You see, our sensors are shielded against fracture energies and can operate there, but the spike coming from the transition overloads many of our systems and they need to be booted up...";
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).getStat("violent") >= 2 )
{
eventAdvice.text = "Prep for combat!";
eventAdvice.recommend =  929
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).getStat("spaceBattle") >=  150  )
{
eventAdvice.text = "Before we know what's in here, it is a very risky situation. Every moment lost in deliberations reduces our chances to get away, if there is more than we can handle.";
eventAdvice.recommend =  926
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).getStat("security") >=  300  )
{
eventAdvice.text = "If there is a colony down there, it is likely there is a standard space station as well. In that case, we need a big ship to deal with it. And I mean 'big'.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Before we know what's in here, it is a very risky situation.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("violent") < 2 )
{
eventAdvice.text = "Okay, they'll want assurances. But we should be able to talk our way in.";
eventAdvice.recommend =  925
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "We're not welcome, time to leave!";
eventAdvice.recommend =  926
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  200  )
{
eventAdvice.text = "There is a lot of raw potential here. No more than 20 million people.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  200  && getCharacter(job).isIdeology("brotherhood") )
{
eventAdvice.text = "Worlds like there often have no control over their brothers. I sense a lot of untamed minds here. No more than 20 million people.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("precognition") >=  250  )
{
eventAdvice.text = "There is danger here, and no half measures. Either convince them, or we leave. Now.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  150  )
{
eventAdvice.text = "It is hard for me to get a sense of how many there are. It feels different here.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "I should meditate to find answers.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getCharacter(job).getStat("holiness") >=  300  )
{
eventAdvice.text = "There is only one reason to turn out your fracture beacon, and it's because they are up to no good.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "To live in isolation is to invite a catastrophe one world can't solve. It would be in their best interests to cooperate with others.";
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "Immediately after transit, ship comms crackles on by a radio transmission from a nearby source. It will take a moment for the ship sensors to come back online and give more information.\n\n<INCOMING TRANSMISSION>\n\nWARNING TO ENTERING "+getShip().getShipTypeAndName().ToUpper()+"!\nYOU ARE AN UNLICENSED SHIP.\nPREPARE TO BE BOARDED.";
}
if (outcome== 954)
{
return "With a ship-shuddering groan, the core charge up for an emergency fracture fall.\n\nThe frigate boosts its engines to close in. There is a gleam in its prow as the ship alarm howls, reporting a hit from a laser weapon.\n\nAnd the ship falls back into the fracture.";
}
if (outcome== 946)
{
return "You receive a message that you have been granted a license for the station, but it is restricted from official Noble House business.\n\nThey also warn against betrayal, as they have intelligence assets outside the station.";
}
if (outcome== 927)
{
return "It seems there is a small standard space station nearby, orbiting a lush planet below. Not much more is seen, as the powerful station sensors are flooding the ship sensors. A frigate can be seen detaching from the station.\n\nSomeone from the station connects with you, demanding for you to declare your affiliations, as well as your promise never to reveal this location to outsiders.";
}
if (outcome== 951)
{
return "You receive a message that you have been granted a license for the station, and they welcome your business, as well as your discretion.\n\nThey also warn against betrayal, as they have intelligence assets outside the station.";
}
if (outcome== 928)
{
return "With a ship-shuddering groan, the core charge up for an emergency fracture fall. The combat screen is lit up as the station's scanners light the ship up. A frigate detaches from the station and starts to swings around. \n\nScanners are mostly blinded by station's active scanners, but a small standard space station is seen orbiting a lush planet. Sensor readings on life forms are inconclusive. \n\nAnd the ship falls back into the fracture.";
}
if (outcome== 930)
{
return "A frigate detaches from the station and swings around to point at your ship like an accusing finger on the intruder. Combat screen lights up by strong sensors from the station painting the ship white against the empty space.\n\n[TODO]";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Try to establish communications and try to negotiate a passage.",  925
);
}
if (outcome== 927)
{
choices.Add("End the call, and prep the engines for a fall back into the fracture.",  953
);
}
if (outcome== 954)
{
choices.Add("Leave.",  969
);
}
if (outcome== 927)
{
choices.Add("Declare yourself as an envoy from the Noble House Valeria. Promise not to share the location of this colony.",  945
);
}
if (outcome== 946)
{
choices.Add("Dock with the station.",  957
);
}
if (outcome== 927)
{
choices.Add("End the call, and prepare for combat.",  955
);
}
if (outcome== 927)
{
choices.Add("Declare yourself as an independent operator, seeking private contracts. Promise not to share the location of this colony.",  949
);
}
if (outcome== 951)
{
choices.Add("Dock with the station.",  964
);
}
if (outcome== 0)
{
choices.Add("Transmit peaceful message on all frequencies while prepping the engines for a fall back into the fracture.",  926
);
}
if (outcome== 928)
{
choices.Add("Leave.",  935
);
}
if (outcome== 0)
{
choices.Add("Prepare for combat.",  929
);
}
if (outcome== 930)
{
choices.Add("Start combat.",  975
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 925)
{
outcome= 927
;
}
if (choice== 953)
{
outcome= 954
;
}
if (choice== 969)
{
outcome= 970 ;
outcome=0;
end();
leaveLocation();
}
if (choice== 945)
{
outcome= 946
;
}
if (choice== 957)
{
outcome= 960 ;
available=false;
end();
}
if (choice== 955)
{
outcome= 990 ;
outcome=0;
end();
startCombat();
}
if (choice== 949)
{
outcome= 951
;
}
if (choice== 964)
{
outcome= 965 ;
available=false;
end();
}
if (choice== 926)
{
outcome= 928
;
}
if (choice== 935)
{
outcome= 936 ;
outcome=0;
end();
leaveLocation();
}
if (choice== 929)
{
outcome= 930
;
}
if (choice== 975)
{
outcome= 976 ;
outcome=0;
end();
startCombat();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 7
//---------------------------------------------------------------------------------
public class Event_7 : EventBase {
public Event_7() : base("contact_noble4") {}
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
if (job == Character.Job.captain) {
if ( getFactionReputation("noble4") >= 20 )
{
eventAdvice.text = "trusted";
eventAdvice.recommend =  1055
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getFactionReputation("noble4") <= -70 && getCharacter(job).getStat("violent") < 0 )
{
eventAdvice.text = "Having a vendetta with one of the great noble houses may not be beneficial. We should give peace talks a chance.";
eventAdvice.recommend =  1053
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getFactionReputation("noble4") <= -70 )
{
eventAdvice.text = "We'll burn their house down!";
eventAdvice.recommend =  1056
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
{
eventAdvice.text = "House Valeria is one of the great noble houses. They have quite a lot of power across the sector, and their favour can be a great asset.";
eventAdvice.recommend =  908
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getFactionReputation("noble2") <= -20 )
{
eventAdvice.text = "These noble houses are thing of the past. Why should we pamper them like they owned the place?";
eventAdvice.recommend =  1055
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getFactionReputation("noble2") <= -20 && getCharacter(job).getStat("aristocrat") > 0 )
{
eventAdvice.text = "One of the great noble houses are pretty pissed off at us.";
eventAdvice.recommend =  1053
;
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0 && getFactionReputation("noble4") >= 30)
{
return "Salutations!\n\nI am "+getCurrentLocation().getLocalfactionHeadTitleAndName(Faction.FactionID.noble4)+" at "+getCurrentLocation().name+". How may I be of assistance to you?";
}
if (outcome== 1083)
{
return "Of course, hope this helps.";
}
if (outcome== 1083 && getFactionReputation("noble4") <= 70)
{
return "You should know we can't offer you more support right now.";
}
if (outcome== 902)
{
return "Of course, what did you have in mind?";
}
if (outcome== 1077)
{
return "You have certainly earned a reward for all you have done, well done.";
}
if (outcome== 1077 && getFactionReputation("noble4") <= 40)
{
return "But now it is your time to help your house again, yes?";
}
if (outcome== 1057)
{
return "The captain and the delegation manage to fight their way out back to the ship.";
}
if (outcome== 909)
{
return "Here is what you can do:\nblah blah blah";
}
if (outcome== 0 && getFactionReputation("noble4") > -5 && getFactionReputation("noble4") <= 30)
{
return "Greetings,\n\nI am "+getCurrentLocation().getLocalfactionHeadTitleAndName(Faction.FactionID.noble4)+" at "+getCurrentLocation().name+". I will always make time to hear you out.";
}
if (outcome== 1058)
{
return "It is possible to talk about reparations, yes.";
}
if (outcome== 1111 && getFactionReputation("noble4") <= -50)
{
return "All right. You still have a long way to go, but I am glad you are taking the steps to make amends.";
}
if (outcome== 1111 && getFactionReputation("noble4") > -50 && getFactionReputation("noble4") <= -30)
{
return "This will go a long way to normalize our relations. I hope you keep the course.";
}
if (outcome== 1111 && getFactionReputation("noble4") > -30)
{
return "Thank you. You may consider our grievances settled.";
}
if (outcome== 1058 && getCredits() < 1000)
{
return "But it would take at least 1000 credits to start the process.\n[Current account balance: "+getCredits()+"].";
}
if (outcome== 1108)
{
return "I appreciate you taking the steps to right the wrongs you have done. There may be hope for you yet.";
}
if (outcome== 1099)
{
return "I do not see a way for you to lift the amount of contempt we have for you. Now, get out!";
}
if (outcome== 0 && getFactionReputation("noble4") <= -70)
{
return "Guards, seize that criminal!";
}
if (outcome== 0 && getFactionReputation("noble4") > -70 && getFactionReputation("noble4") <= -20)
{
return "You have a lot of nerve to show up in here. What do you want?";
}
if (outcome== 0 && getFactionReputation("noble4") > -20 && getFactionReputation("noble4") <= -5)
{
return "I am "+getCurrentLocation().getLocalfactionHeadTitleAndName(Faction.FactionID.noble4)+" at "+getCurrentLocation().name+". I am wondering what are you doing here. Perhaps you will offer to make reparations to get in our good graces, no? House Valeria can open many doors.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0 && getFactionReputation("noble4") > 30)
{
choices.Add("Ask for a favour.",  901
);
}
if (outcome== 902)
{
choices.Add("Never mind.",  1072
);
}
if (outcome== 902 && getFactionReputation("noble4") >= 50)
{
choices.Add("Ask for support [-20 faction reputation, 1000 credits]",  1044
);
}
if (outcome== 1083)
{
choices.Add("Continue.",  1085
);
}
if (outcome== 902)
{
choices.Add("Ask for a reward. [-10 faction reputation, 200 credits]",  1071
);
}
if (outcome== 1077)
{
choices.Add("Continue.",  1078
);
}
if (outcome== 0 && getFactionReputation("noble4") <= -70)
{
choices.Add("Attempt to escape!",  1056
);
}
if (outcome== 1057)
{
choices.Add("Continue.",  1067
);
}
if (outcome== 0 && getFactionReputation("noble4") > -20)
{
choices.Add("Ask how you can help.",  908
);
}
if (outcome== 909)
{
choices.Add("Continue.",  1147
);
}
if (outcome== 0 && getFactionReputation("noble4") > -70 && getFactionReputation("noble4") <= -20)
{
choices.Add("Leave quickly.",  1055
);
}
if (outcome== 0 && getFactionReputation("noble4") > -20)
{
choices.Add("Leave, begging their pardon.",  1054
);
}
if (outcome== 0 && getFactionReputation("noble4") <= -20)
{
choices.Add("Ask for a chance to make amends.",  1053
);
}
if (outcome== 1058 && getCredits() >= 1000 && getFactionReputation("noble4") > -70)
{
choices.Add("Offer 1.000 credits in hopes to amend the relations. [10 relations]",  1110
);
}
if (outcome== 1111)
{
choices.Add("Continue.",  1126
);
}
if (outcome== 1058)
{
choices.Add("Uhh... never mind.",  1121
);
}
if (outcome== 1058 && getCredits() >= 3000 && getFactionReputation("noble4") <= -70)
{
choices.Add("Offer 3.000 credits in reparations. [20 relations]",  1107
);
}
if (outcome== 1108)
{
choices.Add("Continue.",  1141
);
}
if (outcome== 1099)
{
choices.Add("Leave quickly.",  1102
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 901)
{
outcome= 902
;
}
if (choice== 1072)
{
outcome= 1073 ;
restartEvent();
}
if (choice== 1044)
{
outcome= 1083 ;
addFactionReputation("noble4",-20);
addCredits(1000);
}
if (choice== 1085)
{
outcome= 1086 ;
restartEvent();
}
if (choice== 1071)
{
outcome= 1077 ;
addFactionReputation("123",-10);
addCredits(200);
}
if (choice== 1078)
{
outcome= 1080 ;
restartEvent();
}
if (choice== 1056 && getAdvisorStat("captain", "combat") >= 3)
{
outcome= 1057
;
}
if (choice== 1067)
{
outcome= 1068 ;
outcome=0;
end();
}
if (choice== 1056)
{
outcome= 1061 ;
outcome=0;
end();
}
if (choice== 908)
{
outcome= 909
;
}
if (choice== 1147)
{
outcome= 1148 ;
restartEvent();
}
if (choice== 1055)
{
outcome= 1059 ;
outcome=0;
end();
}
if (choice== 1054)
{
outcome= 1060 ;
outcome=0;
end();
}
if (choice== 1053 && getFactionReputation("noble4") > -70 || getShipStat("Diplomacy") > 3)
{
outcome= 1058
;
}
if (choice== 1110)
{
outcome= 1111 ;
addCredits(-1000);
addFactionReputation("noble4",10);
}
if (choice== 1126)
{
outcome= 1127 ;
restartEvent();
}
if (choice== 1121)
{
outcome= 1122 ;
restartEvent();
}
if (choice== 1107)
{
outcome= 1108 ;
addCredits(-3000);
addFactionReputation("noble4",20);
}
if (choice== 1141)
{
outcome= 1142 ;
restartEvent();
}
if (choice== 1053 && getFactionReputation("noble4") <= -70)
{
outcome= 1099
;
}
if (choice== 1102)
{
outcome= 1103 ;
outcome=0;
end();
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("contact_factions");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 8
//---------------------------------------------------------------------------------
public class Event_8 : EventBase {
public Event_8() : base("core singers") {}
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
if ( getShipStat("Morale") >= 4 )
{
p *=  0;
}
if ( getShipStat("Morale") <= -3 )
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
eventAdvice.text = "The core is functioning well it seems. But is it possible to run a ship with independent departments?";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
if ( getCharacter(job).getStat("corruption") >=  50  )
{
eventAdvice.text = "As long as the core is in tune.";
eventAdvice.recommend =  838
;
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
if ( getCharacter(job).getStat("corruption") >=  30  )
{
eventAdvice.text = "I have things under control. We are actually on to something very interesting.";
eventAdvice.recommend =  838
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("corruption") >=  70  )
{
eventAdvice.text = "There must be a misunderstanding here. We do have access! I am a member after all.";
eventAdvice.recommend =  838
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "This is absurd. I'm going to need access to my core.";
eventAdvice.recommend =  839
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).getStat("corruption") >=  80  )
{
eventAdvice.text = "I have infiltrated the group. There is no reason to interfere yet, we can control the situation if need arises.";
eventAdvice.recommend =  838
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).getStat("corruption") >=  50  )
{
eventAdvice.text = "I am not convinced this is a threat to the ship security. It's a matter of preference, really.";
eventAdvice.recommend =  838
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") >= 1 && getCharacter(job).getStat("leadership") >=  250  )
{
eventAdvice.text = "I trust my men. You can count on us.";
eventAdvice.recommend =  840
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") >= 1 )
{
eventAdvice.text = "It may be difficult to take the core, if the defenders are determined.";
eventAdvice.recommend =  839
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") >= 1 && getCharacter(job).getStat("combat") >=  250  )
{
eventAdvice.text = "I can take them.";
eventAdvice.recommend =  840
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") >= 1 && getCharacter(job).getStat("security") >=  175  )
{
eventAdvice.text = "The core can be easily defended, but our security can handle this.";
eventAdvice.recommend =  840
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") < 1 && getCharacter(job).getStat("leadership") >=  150  )
{
eventAdvice.text = "If the resistance is determined at the core, our security forces may not be able to handle it. I need more men!";
eventAdvice.recommend =  839
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") < 1 && getCharacter(job).getStat("security") >=  300  )
{
eventAdvice.text = "The core can be easily defended. Our security forces may have a hard time enforcing this. This is why we need better security on the ship!";
eventAdvice.recommend =  839
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getShipStat("Morale") < 1 )
{
eventAdvice.text = "It may be difficult to take the core, if the defenders are determined.";
eventAdvice.recommend =  839
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") >= 1 && getCharacter(job).getStat("hr") >=  150  )
{
eventAdvice.text = "The situation on the ship is well at hand. This is negotiable.";
eventAdvice.recommend =  839
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") >= 1 && getCharacter(job).getStat("hr") <  150  && getCharacter(job).getStat("hr") <  150  )
{
eventAdvice.text = "If this doesn't interfere with the ship's procedures, perhaps we should allow this. The crew is handing this well.";
eventAdvice.recommend =  838
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") >= 1 && getCharacter(job).getStat("hr") <  150  && getCharacter(job).isIdeology("cult") )
{
eventAdvice.text = "Who knows what is going on in there? This isn't right, we have to get in there.";
eventAdvice.recommend =  840
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("hr") >=  250  )
{
eventAdvice.text = "The chain of command is breaking up. These so called 'core singers' are not taking orders from our leadership anymore.";
eventAdvice.recommend =  839
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).getStat("corruption") >=  40  )
{
eventAdvice.text = "As long as the group keeps operating the core this well, there is no need to interfere.";
eventAdvice.recommend =  838
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") < 1 && getCharacter(job).getStat("hr") >=  250  )
{
eventAdvice.text = "Things are tense in the core. We may not have the leverage to make demands, the group has grown in numbers.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getShipStat("Happiness") < 1 && getCharacter(job).getStat("hr") <  250  )
{
eventAdvice.text = "Things are tense in the core, and more people are now joining the group.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  300  )
{
eventAdvice.text = "The core has given birth to these so called 'core singers'. Giving birth to a mind, but not from nothingness. From the other side. Do you understand what I'm saying?";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("corruption") >=  40  )
{
eventAdvice.text = "I have been in contact to these 'core singers' and I am intrigued. They may have something to contribute.";
eventAdvice.recommend =  838
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("corruption") >=  80  )
{
eventAdvice.text = "I have met these 'core singers' myself. I believe they have a role to play in the future.";
eventAdvice.recommend =  838
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  250  )
{
eventAdvice.text = "These 'core singers' are more than what they appear. We all wear masks, but not like these. Not like these.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  200  )
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
if ( getCharacter(job).getStat("security") <  -100  )
{
eventAdvice.text = "The core contains some of the great secrets of the past. We should not reveal them so easily.";
eventAdvice.recommend =  839
;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
if ( getShipStat("Holiness") >= 1 )
{
eventAdvice.text = "No heretics can take our holy ship! I will lead our people to the path of righteousness.";
eventAdvice.recommend =  840
;
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "The core is a sacred place, and must be under our direct control!";
eventAdvice.recommend =  840
;
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
choices.Add("Do not interfere.",  838
);
}
if (outcome== 843)
{
choices.Add("Continue",  857
);
}
if (outcome== 0)
{
choices.Add("Demand access to the core for key personnel.",  839
);
}
if (outcome== 844)
{
choices.Add("Send in the security to reclaim the core.",  858
);
}
if (outcome== 859)
{
choices.Add("Continue",  864
);
}
if (outcome== 884)
{
choices.Add("Continue",  888
);
}
if (outcome== 844)
{
choices.Add("Appoint the 'core singers' an official position on the core.",  851
);
}
if (outcome== 860)
{
choices.Add("Continue",  862
);
}
if (outcome== 842)
{
choices.Add("Continue",  850
);
}
if (outcome== 0)
{
choices.Add("Send in the security to reclaim the core.",  840
);
}
if (outcome== 841)
{
choices.Add("Praise the Church!",  870
);
}
if (outcome== 867)
{
choices.Add("Continue",  880
);
}
if (outcome== 845)
{
choices.Add("Continue",  875
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 838)
{
outcome= 843
;
}
if (choice== 857)
{
outcome= 893 ;
available=false;
end();
}
if (choice== 839 && getShipStat("Happiness") < 1)
{
outcome= 844
;
}
if (choice== 858 && getShipStat("Morale") >= 2)
{
outcome= 859
;
}
if (choice== 864)
{
outcome= 865 ;
available=false;
end();
}
if (choice== 858 && getShipStat("Morale") < 2)
{
outcome= 884
;
}
if (choice== 888)
{
outcome= 890 ;
available=false;
end();
}
if (choice== 851)
{
outcome= 860
;
}
if (choice== 862)
{
outcome= 866 ;
available=false;
end();
}
if (choice== 839 && getShipStat("Happiness") >= 1 || getShipStat("Holiness") >= 3)
{
outcome= 842
;
}
if (choice== 850)
{
outcome= 852 ;
available=false;
end();
}
if (choice== 840)
{
outcome= 889
;
}
if (choice== 840 && getShipStat("Holiness") >= 3)
{
outcome= 841
;
}
if (choice== 870)
{
outcome= 871 ;
available=false;
end();
}
if (choice== 840 && getShipStat("Morale") < 1)
{
outcome= 867
;
}
if (choice== 880)
{
outcome= 881 ;
available=false;
end();
}
if (choice== 840 && getShipStat("Morale") >= 1)
{
outcome= 845
;
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
//------------------------------------------------------- EVENT 9
//---------------------------------------------------------------------------------
public class Event_9 : EventBase {
public Event_9() : base("default_advice") {}
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
//------------------------------------------------------- EVENT 10
//---------------------------------------------------------------------------------
public class Event_10 : EventBase {
public Event_10() : base("intro Valeria1t") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c07"
;
triggerEvent=trigger.inLocation;
locationRequired=true;
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
if ( getCharacter(job).isIdeology("liberal") )
{
eventAdvice.text = "Consul Regulus has come from the sector Capital to succeed the retiring Lord Calius Valeria. Planetary governor Evander is the other nominee for the position, smart and sensible.";
eventAdvice.recommend =  709
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getCharacter(job).isIdeology("aristocrat") )
{
eventAdvice.text = "This can get us some recognition. And it's a strong statement. Consul Regulus has come from the sector Capital to succeed Lord Calius Valeria. Planetary governor Evander is the other nominee for the position.";
eventAdvice.recommend =  708
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
{
eventAdvice.text = "Consul Regulus has come from the sector Capital to succeed Lord Calius Valeria. Planetary governor Evander is the other nominee for the position.";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "Many eyes are upon us. We should consider this carefully.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).isIdeology("liberal") )
{
eventAdvice.text = "&No&, Consuls are high-level politicians and we're about to be used.";
eventAdvice.recommend =  709
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).isIdeology("aristocrat") )
{
eventAdvice.text = "The Consul is one of the Great Council leaders from New Mercury. He is an insider and has considerable influence across the sector.";
eventAdvice.recommend =  708
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "I'm not very political. Anyone else?";
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).isIdeology("liberal") )
{
eventAdvice.text = "I'm not sure we'll be better off participating in this sabre rattling. Regulus is doing this to score points against his political opponent.";
eventAdvice.recommend =  709
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).isIdeology("aristocrat") )
{
eventAdvice.text = "Regulus is our man. Trust me, you won't regret it.";
eventAdvice.recommend =  708
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Political theater. We can be played, or benefit from it. Regulus is a strong leader, and I am up for giving it a try.";
eventAdvice.recommend =  708
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).isIdeology("liberal") )
{
eventAdvice.text = "Consul Regulus is not very popular among the locals. We might have harder time recruiting from here, if we support Regulus.";
eventAdvice.recommend =  709
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).isIdeology("aristocrat") )
{
eventAdvice.text = "We should do the pledge. It's a show of force, and we want to tie those";
eventAdvice.recommend =  708
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
if ( getCharacter(job).isIdeology("transhumanist") )
{
eventAdvice.text = "I don't think so, &no&. Consul is one of the top aristocrats. They care for nothing but their own political agendas. I want nothing to do with it.";
eventAdvice.recommend =  709
;
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "Consul Regulus is not very popular among the locals in "+getLocation().name+", but he has some fleet captains supporting him. We might be able to get some ties to Valerian military.";
eventAdvice.recommend =  708
;
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "The Consul is a powerful individual, and he carries influence among the imperialists.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "The Consul is asking us, and it is our duty to answer his call.";
eventAdvice.recommend =  708
;
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
return "Consul Regulus and several Valerian captains appear in public with a strong statement. Dacei envoy responds by calling it an aggression. Among the captains, Regulus asks if anyone have the guts to poke around in Dacei shipping lines to see what they are doing. He'd be interested in the contents of their transports.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Join the pledge.",  708
);
}
if (outcome== 710)
{
choices.Add("Continue.",  712
);
}
if (outcome== 0)
{
choices.Add("Stay out of it.",  709
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 708)
{
outcome= 710
;
}
if (choice== 712)
{
outcome= 713 ;
available=false;
end();
newLocFactionLeader("noble4","Evander");
}
if (choice== 709)
{
outcome= 716 ;
available=false;
end();
newLocFactionLeader("noble4","Evander");
}
}
//------------------------------------------------------- FILTERS
public override void initFilters() {
addFilter("intro_Valeria");
addFilter("intro");
}
}
//---------------------------------------------------------------------------------
//------------------------------------------------------- EVENT 11
//---------------------------------------------------------------------------------
public class Event_11 : EventBase {
public Event_11() : base("intro Valeria2t") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c07"
;
triggerEvent=trigger.inLocation;
locationRequired=true;
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
if ( getCharacter(job).isIdeology("aristocrat") )
{
eventAdvice.text = "Governor Evander is the local planetary governor, and a nominee for the position of Lord House Valeria. Official dinners are political maneuvering.";
eventAdvice.recommend =  722
;
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getCharacter(job).isIdeology("liberal") )
{
eventAdvice.text = "Governor Evander is the local planetary governor, and a nominee for the position of Lord House Valeria. He is popular, and his attention would reflect well on us.";
eventAdvice.recommend =  721
;
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = "The symmetry of politics. We are being weighted.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).isIdeology("liberal") )
{
eventAdvice.text = "Of course. We must not refuse this man.";
eventAdvice.recommend =  721
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
if ( getCharacter(job).isIdeology("aristocrat") )
{
eventAdvice.text = "No, I don't think so. I mean, there is so much to do and we're on a tight schedule.";
eventAdvice.recommend =  722
;
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "I'm not very political. But I told you we were being used and I will tell you again.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
if ( getCharacter(job).isIdeology("aristocrat") )
{
eventAdvice.text = "After refusing the pledge, Consul Regulus will not look at us favorably, if we now start supporting his political opponents.";
eventAdvice.recommend =  722
;
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "The navy is courted by the political rivals. So far it's just political and I hope there won't be an escalation in the internal politics of House Valeria.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "I wonder what kind of a man Consul Regulus is. If he takes this as a snub from us, it's not good. If we refuse, it's a snub against the Governor.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "I am a little bit preoccupied at the moment.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "I'm not sure we can accept now, when we just turned down a Consul.";
eventAdvice.recommend =  722
;
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "Planetary governor Evander Valeria contacts the ship and commends the captain for staying out of internal politics. He invites the top brass to an official dinner to discuss local matters.";
}
if (outcome== 726)
{
return "Governor Evander hosts several Valerian captains and members of the Guiding Council. The event is broadcasted planet wide repeatedly over the next few days. In private, Lord Evander asks you to help the liberal movement here and in the Sovereign Void, to counter Dacei interests from spreading. But he discourages direct confrontation, as Dacei are gearing up for aggression.";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Attend the official dinner.",  721
);
}
if (outcome== 726)
{
choices.Add("Continue.",  728
);
}
if (outcome== 0)
{
choices.Add("Excuse yourself.",  722
);
}
 }
//------------------------------------------------------- OUTCOMES
public override void doOutcome() {
if (choice== 721)
{
outcome= 726
;
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
//------------------------------------------------------- EVENT 12
//---------------------------------------------------------------------------------
public class Event_12 : EventBase {
public Event_12() : base("intro Valeria3t") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c07"
;
triggerEvent=trigger.inLocation;
locationRequired=true;
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
if (job == Character.Job.engineer) {
if ( getCharacter(job).getStat("scientist") >=  50  )
{
eventAdvice.text = "I have heard of these dig sites that have found some old buried things, but I haven't heard them being found in the Sovereign Void. Nor that top officials are acquiring them. They are considered highly illegal by the Church.";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = "These are highly illegal artifacts. I've heard the Church is hunting them down.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "I'm not sure that is meant for our eyes. But if we get involved, we have to be very discreet.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "Lord Calius does not appear in public, and publicly very little is known about him.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = "Calius Valeria. Most interesting. I will be very curious about what these 'artifacts' are.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "Church position on any 'artifacts' is absolute. The Inquisition will be after us, if we start doing this.";
return eventAdvice;
}
}
return eventAdvice;
}
//------------------------------------------------------- TEXT
public override string getText() {
if (outcome== 0)
{
return "<Incoming private message>\n\nCaptain "+getCharacter(Character.Job.captain).name+",\n\nI have noted your discretion in the recent political ramp up. And I have use for someone with a measure of discretion. Artifacts of the grey past are being transported in the Sovereign Void on board Dacei transports. I would be very interested in acquiring one.'            \n\n\n-Lord Calius Valeria";
}
return "INSERT TEXT HERE";
}
//------------------------------------------------------- CHOICES
public override void initChoices() {
if (outcome== 0)
{
choices.Add("Continue.",  734
);
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
//------------------------------------------------------- EVENT 13
//---------------------------------------------------------------------------------
public class Event_13 : EventBase {
public Event_13() : base("loc_advice") {}
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
eventAdvice.text = ""+getLocation().name+" reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.navigator) {
{
eventAdvice.text = ""+getLocation().name+" reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.engineer) {
{
eventAdvice.text = ""+getLocation().name+" reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = ""+getLocation().name+" reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = ""+getLocation().name+" reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
{
eventAdvice.text = ""+getLocation().name+" reverts to default comment";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = ""+getLocation().name+" reverts to default comment";
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
public Event_14() : base("loc_advice_1C01") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c01"
;
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
eventAdvice.text = "There is a sizable fleet at the station. And large shipyards. The import much of their military equipment.";
return eventAdvice;
}
}
if (job == Character.Job.quartermaster) {
{
eventAdvice.text = "This is one of the core worlds, the Old Capital itself is 300 million people. We can probably find specialists here.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") >=  300  )
{
eventAdvice.text = "We're being watched by several groups down at the planet. Buildings on top of buildings, societies behind societies. All this going back a long, long time.";
return eventAdvice;
}
}
if (job == Character.Job.psycher) {
if ( getCharacter(job).getStat("psy") <  300  )
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
//------------------------------------------------------- EVENT 15
//---------------------------------------------------------------------------------
public class Event_15 : EventBase {
public Event_15() : base("loc_advice_1C02") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c02"
;
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
eventAdvice.text = "There are 300,000 people living on The Arch, which is more than any other space station can handle. Some of the best minds of the sector are here. There is a pull to it, gravitas. Can you feel it?";
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
//------------------------------------------------------- EVENT 16
//---------------------------------------------------------------------------------
public class Event_16 : EventBase {
public Event_16() : base("loc_advice_1C03") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c03"
;
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
eventAdvice.text = "A good little supply world.";
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
if ( getCharacter(job).isIdeology("transhumanist") )
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
//------------------------------------------------------- EVENT 17
//---------------------------------------------------------------------------------
public class Event_17 : EventBase {
public Event_17() : base("loc_advice_1C04") {}
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
eventAdvice.text = "Some of these primitive worlds have societies with true warrior culture. Utterly fearless, quick to learn and nothing to lose. I could beef up my security with some recruits.";
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
eventAdvice.text = "The scars run deep here, not all have forgotten what was done to them. Few of them look up at the stars anymore. Not like they used to.";
return eventAdvice;
}
}
if (job == Character.Job.priest) {
{
eventAdvice.text = "The Church has monasteries in many of these planets, teaching people the right way to live. But it is hard sometimes, these are difficult circumstances. So many of them live in the darkness.";
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
public Event_18() : base("loc_advice_1C05") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c05"
;
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
eventAdvice.text = "Some things you pick from the junkyard, but most of it is basically lumps of molten minerals.";
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
//------------------------------------------------------- EVENT 19
//---------------------------------------------------------------------------------
public class Event_19 : EventBase {
public Event_19() : base("loc_advice_1C06") {}
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
eventAdvice.text = "Primitive worlds are abandoned to their own devices. They still produce food that is traded out through the orbiting station. In this case, it is mycoprotein.";
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
eventAdvice.text = "Myco-fracking-fusarium. It's those yellow foil-wrapped compressed meal-bars that taste like shrooms and old socks. And we're here to buy millions of them, aren't we?";
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
//------------------------------------------------------- EVENT 20
//---------------------------------------------------------------------------------
public class Event_20 : EventBase {
public Event_20() : base("loc_advice_1C07") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c07"
;
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
eventAdvice.text = "Valerian capital world.";
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
if ( getCharacter(job).getStat("security") <  0  )
{
eventAdvice.text = "The strength of Valeria is in knowledge, and in their ships.";
return eventAdvice;
}
}
if (job == Character.Job.security) {
{
eventAdvice.text = "Not much in the sense of military on the ground, besides the House Guard. Academics have little use for weapons, it seems. Hope it won't backfire all the way into their ivory towers.";
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
eventAdvice.text = "It shouldn't be said of one of the great Noble Houses of the Imperium, but Valerians have always made me ask how did they pull it off.";
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
//------------------------------------------------------- EVENT 21
//---------------------------------------------------------------------------------
public class Event_21 : EventBase {
public Event_21() : base("loc_advice_1C08") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c08"
;
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
if ( getCharacter(job).isIdeology("transhumanist") )
{
eventAdvice.text = "They are hiding here from the world. And why not? They are doing well.";
return eventAdvice;
}
}
if (job == Character.Job.captain) {
if ( getCharacter(job).isIdeology("nationalist") )
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
eventAdvice.text = "Here in the fold I found them. Even with their beacon turned off.";
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
eventAdvice.text = "There are people on that station, trying to hide an entire planet. Every day, working to mask these gardens from the seekers.";
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
//------------------------------------------------------- EVENT 22
//---------------------------------------------------------------------------------
public class Event_22 : EventBase {
public Event_22() : base("loc_advice_1C09") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "1c09"
;
triggerEvent=trigger.atLocation;
locationRequired=true;
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
eventAdvice.text = "A grim world robbed of function. And yet, old rituals are maintained generations after generations.";
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
eventAdvice.text = "It is a holy place, secluded and alone. That is why they have avoided temptation and stayed pure.";
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
//------------------------------------------------------- EVENT 23
//---------------------------------------------------------------------------------
public class Event_23 : EventBase {
public Event_23() : base("loc_advice_2V01") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "2v01"
;
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
//------------------------------------------------------- EVENT 24
//---------------------------------------------------------------------------------
public class Event_24 : EventBase {
public Event_24() : base("loc_advice_2V02") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "2v01"
;
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
//------------------------------------------------------- EVENT 25
//---------------------------------------------------------------------------------
public class Event_25 : EventBase {
public Event_25() : base("loc_advice_2V03") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "2v03"
;
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
//------------------------------------------------------- EVENT 26
//---------------------------------------------------------------------------------
public class Event_26 : EventBase {
public Event_26() : base("loc_advice_2V04") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "2v04"
;
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
//------------------------------------------------------- EVENT 27
//---------------------------------------------------------------------------------
public class Event_27 : EventBase {
public Event_27() : base("loc_advice_2V05") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "2v05"
;
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
//------------------------------------------------------- EVENT 28
//---------------------------------------------------------------------------------
public class Event_28 : EventBase {
public Event_28() : base("loc_advice_2V06") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "2v06"
;
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
//------------------------------------------------------- EVENT 29
//---------------------------------------------------------------------------------
public class Event_29 : EventBase {
public Event_29() : base("loc_advice_2V07") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "2v07"
;
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
//------------------------------------------------------- EVENT 30
//---------------------------------------------------------------------------------
public class Event_30 : EventBase {
public Event_30() : base("loc_advice_2V08") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "2v08"
;
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
//------------------------------------------------------- EVENT 31
//---------------------------------------------------------------------------------
public class Event_31 : EventBase {
public Event_31() : base("loc_advice_2V09") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "2v09"
;
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
//------------------------------------------------------- EVENT 32
//---------------------------------------------------------------------------------
public class Event_32 : EventBase {
public Event_32() : base("loc_advice_3F01") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "3f01"
;
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
//------------------------------------------------------- EVENT 33
//---------------------------------------------------------------------------------
public class Event_33 : EventBase {
public Event_33() : base("loc_advice_3F02") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "3f02"
;
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
//------------------------------------------------------- EVENT 34
//---------------------------------------------------------------------------------
public class Event_34 : EventBase {
public Event_34() : base("loc_advice_3F03") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "3f03"
;
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
//------------------------------------------------------- EVENT 35
//---------------------------------------------------------------------------------
public class Event_35 : EventBase {
public Event_35() : base("loc_advice_4S01") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s01"
;
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
//------------------------------------------------------- EVENT 36
//---------------------------------------------------------------------------------
public class Event_36 : EventBase {
public Event_36() : base("loc_advice_4S02") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s02"
;
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
//------------------------------------------------------- EVENT 37
//---------------------------------------------------------------------------------
public class Event_37 : EventBase {
public Event_37() : base("loc_advice_4S03") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s03"
;
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
//------------------------------------------------------- EVENT 38
//---------------------------------------------------------------------------------
public class Event_38 : EventBase {
public Event_38() : base("loc_advice_4S04") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s04"
;
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
//------------------------------------------------------- EVENT 39
//---------------------------------------------------------------------------------
public class Event_39 : EventBase {
public Event_39() : base("loc_advice_4S05") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s05"
;
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
//------------------------------------------------------- EVENT 40
//---------------------------------------------------------------------------------
public class Event_40 : EventBase {
public Event_40() : base("loc_advice_4S06") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s06"
;
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
//------------------------------------------------------- EVENT 41
//---------------------------------------------------------------------------------
public class Event_41 : EventBase {
public Event_41() : base("loc_advice_4S07") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s07"
;
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
//------------------------------------------------------- EVENT 42
//---------------------------------------------------------------------------------
public class Event_42 : EventBase {
public Event_42() : base("loc_advice_4S08") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s08"
;
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
//------------------------------------------------------- EVENT 43
//---------------------------------------------------------------------------------
public class Event_43 : EventBase {
public Event_43() : base("loc_advice_4S09") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s09"
;
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
//------------------------------------------------------- EVENT 44
//---------------------------------------------------------------------------------
public class Event_44 : EventBase {
public Event_44() : base("loc_advice_4S10") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s10"
;
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
//------------------------------------------------------- EVENT 45
//---------------------------------------------------------------------------------
public class Event_45 : EventBase {
public Event_45() : base("loc_advice_4S11") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "4s11"
;
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
//------------------------------------------------------- EVENT 46
//---------------------------------------------------------------------------------
public class Event_46 : EventBase {
public Event_46() : base("loc_advice_5E01") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "5e01"
;
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
//------------------------------------------------------- EVENT 47
//---------------------------------------------------------------------------------
public class Event_47 : EventBase {
public Event_47() : base("loc_advice_5E02") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "5e02"
;
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
//------------------------------------------------------- EVENT 48
//---------------------------------------------------------------------------------
public class Event_48 : EventBase {
public Event_48() : base("loc_advice_5E03") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "5e03"
;
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
//------------------------------------------------------- EVENT 49
//---------------------------------------------------------------------------------
public class Event_49 : EventBase {
public Event_49() : base("loc_advice_5E04") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "5e04"
;
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
//------------------------------------------------------- EVENT 50
//---------------------------------------------------------------------------------
public class Event_50 : EventBase {
public Event_50() : base("loc_advice_5E05") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "5e05"
;
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
//------------------------------------------------------- EVENT 51
//---------------------------------------------------------------------------------
public class Event_51 : EventBase {
public Event_51() : base("loc_advice_5E06") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "5e06"
;
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
//------------------------------------------------------- EVENT 52
//---------------------------------------------------------------------------------
public class Event_52 : EventBase {
public Event_52() : base("loc_advice_5E07") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "5e07"
;
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
//------------------------------------------------------- EVENT 53
//---------------------------------------------------------------------------------
public class Event_53 : EventBase {
public Event_53() : base("loc_advice_5E08") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "5e08"
;
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
//------------------------------------------------------- EVENT 54
//---------------------------------------------------------------------------------
public class Event_54 : EventBase {
public Event_54() : base("loc_advice_5E09") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "5e09"
;
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
//------------------------------------------------------- EVENT 55
//---------------------------------------------------------------------------------
public class Event_55 : EventBase {
public Event_55() : base("loc_advice_6T01") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "6t01"
;
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
//------------------------------------------------------- EVENT 56
//---------------------------------------------------------------------------------
public class Event_56 : EventBase {
public Event_56() : base("loc_advice_6T02") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "6t02"
;
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
//------------------------------------------------------- EVENT 57
//---------------------------------------------------------------------------------
public class Event_57 : EventBase {
public Event_57() : base("loc_advice_6T03") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "6t03"
;
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
//------------------------------------------------------- EVENT 58
//---------------------------------------------------------------------------------
public class Event_58 : EventBase {
public Event_58() : base("loc_advice_6T04") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "6t04"
;
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
//------------------------------------------------------- EVENT 59
//---------------------------------------------------------------------------------
public class Event_59 : EventBase {
public Event_59() : base("loc_advice_6T05") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "6t05"
;
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
//------------------------------------------------------- EVENT 60
//---------------------------------------------------------------------------------
public class Event_60 : EventBase {
public Event_60() : base("loc_advice_7I01") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "7i01"
;
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
//------------------------------------------------------- EVENT 61
//---------------------------------------------------------------------------------
public class Event_61 : EventBase {
public Event_61() : base("loc_advice_7I02") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "7i02"
;
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
//------------------------------------------------------- EVENT 62
//---------------------------------------------------------------------------------
public class Event_62 : EventBase {
public Event_62() : base("loc_advice_7I03") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "7i03"
;
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
//------------------------------------------------------- EVENT 63
//---------------------------------------------------------------------------------
public class Event_63 : EventBase {
public Event_63() : base("loc_advice_7I04") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "7i04"
;
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
//------------------------------------------------------- EVENT 64
//---------------------------------------------------------------------------------
public class Event_64 : EventBase {
public Event_64() : base("loc_advice_7I05") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "7i05"
;
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
//------------------------------------------------------- EVENT 65
//---------------------------------------------------------------------------------
public class Event_65 : EventBase {
public Event_65() : base("loc_advice_7I06") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "7i06"
;
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
//------------------------------------------------------- EVENT 66
//---------------------------------------------------------------------------------
public class Event_66 : EventBase {
public Event_66() : base("loc_advice_7I07") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "7i07"
;
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
//------------------------------------------------------- EVENT 67
//---------------------------------------------------------------------------------
public class Event_67 : EventBase {
public Event_67() : base("loc_advice_7I08") {}
//------------------------------------------------------- PREINIT
public override void initPre() {
location= "7i08"
;
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
