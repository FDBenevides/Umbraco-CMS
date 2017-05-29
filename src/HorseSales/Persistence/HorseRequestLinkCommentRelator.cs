using System.Collections.Generic;


namespace HorseSales.Persistence
{
    internal class HorseRequestLinkCommentRelator
    {
        internal HorseRequestDto CurrentReq;
        internal HorseRequestLinkDto CurrentLink;

        internal HorseRequestDto Map(HorseRequestDto req, HorseRequestLinkDto reqLink, HorseRequestLinkCommentDto reqLinkComment)
        {
            // Terminating call.  Since we can return null from this function
            // we need to be ready for PetaPoco to callback later with null
            // parameters
            if (req == null)
                return CurrentReq;

            // Is this the same HorseRequestDto as the current one we're processing
            if (CurrentReq != null && CurrentReq.Id == req.Id)
            {
                // Is this the same HorseRequestLinkDto as the current one we're processing
                if (CurrentLink != null && CurrentLink.LinkId == reqLink.LinkId)
                {
                    //FLOW PHASE EXPLANATION: the database query's record change but the HorseRequestDto and 
                    // HorseRequestLinkDto is the same
                    if (reqLinkComment.Id > 0)
                    {
                        // Yes, just add this HorseRequestLinkCommentDto to the current item's collection
                        CurrentLink.Comments.Add(reqLinkComment);
                    }

                    // Return null to indicate we're not done with this HorseRequestDto yet
                    return null;
                }else
                {
                    //FLOW PHASE EXPLANATION: the database query's record change but only the HorseRequestDto
                    // is the same
                    if (reqLink.LinkId > 0)
                    {
                        // Yes, just add this HorseRequestLinkDto to the current item's collection
                        CurrentReq.HorseLinks.Add(reqLink);
                        CurrentLink = reqLink;

                        if (reqLinkComment.Id > 0)
                        {
                            // Yes, just add this HorseRequestLinkCommentDto to the current item's collection
                            CurrentLink.Comments.Add(reqLinkComment);
                        }
                    }

                    // Return null to indicate we're not done with this HorseRequestDto yet
                    return null;
                }
            }

            //FLOW PHASE EXPLANATION: This is a different HorseRequestDto to the current one, or this is the 
            // first time through and we don't have one yet

            // Save the current HorseRequestDto
            var prev = CurrentReq;

            // Setup the new current HorseRequestDto
            CurrentReq = req;
            //this can be null since we are doing a left join
            if (reqLink.LinkId > 0)
            {
                CurrentLink = reqLink;
                CurrentReq.HorseLinks.Add(reqLink);

                if (reqLinkComment.Id > 0)
                {
                    // Yes, just add this HorseRequestLinkCommentDto to the current item's collection
                    CurrentLink.Comments.Add(reqLinkComment);
                }
            }

            // Return the now populated previous User (or null if first time through)
            return prev;
        }
    }
}