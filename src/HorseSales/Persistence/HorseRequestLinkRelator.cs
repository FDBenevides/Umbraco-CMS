using System.Collections.Generic;


namespace HorseSales.Persistence
{
    internal class HorseRequestLinkRelator
    {
        internal HorseRequestDto Current;

        internal HorseRequestDto Map(HorseRequestDto a, HorseRequestLinkDto p)
        {
            // Terminating call.  Since we can return null from this function
            // we need to be ready for PetaPoco to callback later with null
            // parameters
            if (a == null)
                return Current;

            // Is this the same HorseRequestDto as the current one we're processing
            if (Current != null && Current.Id == a.Id)
            {
                if (p.Id > 0)
                {
                    // Yes, just add this HorseRequestLinkDto to the current item's collection

                    //TODO check condition to decide if it will be added to the suggestions or final list
                    Current.HorseLinks.Add(p);
                }

                // Return null to indicate we're not done with this User yet
                return null;
            }

            // This is a different HorseRequestDto to the current one, or this is the 
            // first time through and we don't have one yet

            // Save the current HorseRequestDto
            var prev = Current;

            // Setup the new current HorseRequestDto
            Current = a;
            Current.HorseLinks = new List<HorseRequestLinkDto>();
            //this can be null since we are doing a left join
            if (p.Id > 0)
            {
                //TODO check condition to decide if it will be added to the suggestions or final list
                Current.HorseLinks.Add(p);
            }

            // Return the now populated previous User (or null if first time through)
            return prev;
        }
    }
}