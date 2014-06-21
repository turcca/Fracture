public class Event_4 : EventBase {
 public Event_4() : base("Odd behavior") {}
 public override float calculateProbability() {
  float p = 1.0f;
  return p;
 }
 public override string getAdvice(string who) {
  if (who == "captain") {
   {
    return "Weeed to figure out what's going on.";
   }
  }
  if (who == "navigator") {
   {
    return "Odd behavior you say?";
   }
  }
  if (who == "engineer") {
   {
    return "I've got my men under control.";
   }
  }
  if (who == "security") {
   {
    return "We should set up a curfew and arrest the troublemakers at once!";
   }
  }
  if (who == "quartermaster") {
   {
    return "The crew could use some downtime. We've spend too much time in the Fracture lately.";
   }
  }
  if (who == "psycher") {
   {
    return "Well well, what do we have here?";
   }
  }
  if (who == "priest") {
   {
    return "Heretics! They are all damned heretics!";
   }
  }
 return "INSERT GENERAL ADVICE HERE";
 }
 public override string getText() {
  {
   return "You hear reports of odd behavior among the crew.";
  }
 return "";
 }
 public override void initChoices() {
  {
   choices.Add("It's probably nothing, carry on!",  1);
  }
  {
   choices.Add("Odd behavior? It might be wise to investigate this one.", 2);
  }
 }
 public override void doOutcome() {
  {
   outcome = choice ;
   available = false ;
   end();
  }
 }
}
