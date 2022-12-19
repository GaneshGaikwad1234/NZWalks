﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        //public int MyProperty { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        
        public Guid RegionId { get; set; }
        
        
        public Guid WalkDifficultyId { get; set; }
        
        //Navigation Properties
        
        public Region Region { get; set; }
       
        public WalkDifficulty WalkDifficulty { get; set; }

    }
}
