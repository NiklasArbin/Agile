using System;
using System.Collections.Generic;

namespace StyrBoard.Domain.Model
{
    public class Feature: IAggregateRoot, IDescribed, IHaveState
    {
        public Feature()
        {
            UserStories = new List<UserStory>();
        }

        public Guid Id { get; set; }
        public int DisplayId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<UserStory> UserStories { get; set; }
        public State State { get; set; }
    }
}
